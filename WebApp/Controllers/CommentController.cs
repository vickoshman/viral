using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Data;
using WebApp.Models;

namespace WebApp.Controllers
{
  public class CommentController : BaseController
  {
    public ActionResult GetByPost(int postId)
    {
      var posts = new List<PostModel>();
      using (var cx = new ViralContext())
      {
        foreach (var post in cx.Posts
          .Where(p => !p.IsDeleted)
          .Include(p => p.Ratings)
          .Include(p => p.Comments)
          .Include(p => p.Author))
        {
          posts.Add(new PostModel(post));
        }
      }

      return Json(posts.ToJson());
    }

    [HttpPost]
    [Authorize]
    public ActionResult Create(Comment comment)
    {
      var currentUser = CurrentUser;
      using (var cx = new ViralContext())
      {
        comment.PostedTime = DateTime.UtcNow;

        if (currentUser.PostedComments == null)
          currentUser.PostedComments = new List<Comment>();
        currentUser.PostedComments.Add(comment);

        cx.Entry(currentUser).State = EntityState.Modified;
        cx.Entry(comment).State = EntityState.Added;

        cx.SaveChanges();
      }

      return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [Authorize]
    public ActionResult Edit(Comment comment)
    {
      var currentUser = CurrentUser;
      using (var cx = new ViralContext())
      {
        var savedComment = cx.Comments.SingleOrDefault(c => c.Id == comment.Id && c.Author.Id == currentUser.Id);
        if (savedComment == null)
          return RedirectToAction("Index", "Home");

        comment.PostedTime = savedComment.PostedTime;
        cx.Comments.AddOrUpdate(comment);
        cx.SaveChanges();

        return RedirectToAction("Index", "Home");
      }
    }

    [Authorize]
    public ActionResult Delete(int? id)
    {
      var currentUser = CurrentUser;
      using (var cx = new ViralContext())
      {
        var savedComment = cx.Comments.SingleOrDefault(c => c.Id == id && c.Author.Id == currentUser.Id);
        if (savedComment == null)
          return RedirectToAction("Index", "Home");

        savedComment.IsDeleted = true;
        cx.Entry(savedComment).State = EntityState.Modified;
        cx.SaveChanges();

        return RedirectToAction("Index", "Home");
      }
    }

    [Authorize]
    public ActionResult Like(int? id)
    {
      return Rate(id, RatingType.Liked);
    }

    [Authorize]
    public ActionResult Dislike(int? id)
    {
      return Rate(id, RatingType.Disliked);
    }

    private ActionResult Rate(int? id, RatingType type)
    {
      using (var cx = new ViralContext())
      {
        var existingRating = cx.Ratings.SingleOrDefault(r => r.User.Id == CurrentUserId && r.Comment.Id == id);
        if (existingRating != null)
        {
          existingRating.RatedTime = DateTime.UtcNow;
          existingRating.Type = type;

          cx.Entry(existingRating).State = EntityState.Modified;
        }
        else
        {
          var comment = cx.Comments
            .Where(p => p.Id == id)
            .Include(p => p.Ratings)
            .SingleOrDefault();

          if (comment == null)
            return RedirectToAction("Index", "Home");

          var user = cx.Users
            .Where(u => u.Id == CurrentUserId)
            .Include(u => u.Ratings)
            .Single();

          var rating = new Rating
          {
            RatedTime = DateTime.UtcNow,
            Type = type
          };

          if (user.Ratings == null)
            user.Ratings = new List<Rating>();
          user.Ratings.Add(rating);

          if (comment.Ratings == null)
            comment.Ratings = new List<Rating>();
          comment.Ratings.Add(rating);

          cx.Entry(user).State = EntityState.Modified;
          cx.Entry(comment).State = EntityState.Modified;
        }

        cx.SaveChanges();

        return RedirectToAction("Index", "Home");
      }
    }
  }
}