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
  public class PostController : BaseController
  {
    public ActionResult Index(int id)
    {
      Post post;
      using (var cx = new ViralContext())
      {
        post = cx.Posts
          .Where(p => p.Id == id)
          .Where(p => !p.IsDeleted)
          .Include(p => p.Ratings)
          .Include(p => p.Comments)
          .Include(p => p.Author)
          .SingleOrDefault();
      }

      return View(new PostModel(post));
    }

    public ActionResult GetTop(int pageIndex, int pageSize)
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

      return Json(posts, JsonRequestBehavior.AllowGet);
    }
    
    [Authorize]
    public ActionResult Create()
    {
      return View("Create");
    }

    [HttpPost]
    [Authorize]
    public ActionResult Create(Post post)
    {
      var currentUser = CurrentUser;
      using (var cx = new ViralContext())
      {
        post.PostedTime = DateTime.UtcNow;
        post.LastEditTime = post.PostedTime;

        if (currentUser.PostedPosts == null)
          currentUser.PostedPosts = new List<Post>();
        currentUser.PostedPosts.Add(post);

        cx.Entry(currentUser).State = EntityState.Modified;
        cx.Entry(post).State = EntityState.Added;

        cx.SaveChanges();
      }

      return View("Create");
    }
    
    [Authorize]
    public ActionResult Edit(int id)
    {
      Post post;
      using (var cx = new ViralContext())
      {
        post = cx.Posts.Find(id);
        cx.SaveChanges();
      }

      return View(post);
    }

    [HttpPost]
    [Authorize]
    public ActionResult Edit(Post post)
    {
      var currentUser = CurrentUser;
      using (var cx = new ViralContext())
      {
        var savedPost = cx.Posts.SingleOrDefault(p => p.Id == post.Id && p.Author.Id == currentUser.Id);
        if (savedPost == null)
          return RedirectToAction("Index", "Home");

        post.LastEditTime = savedPost.LastEditTime;
        cx.Posts.AddOrUpdate(post);
        cx.SaveChanges();

        return Index(post.Id);
      }
    }
    
    [Authorize]
    public ActionResult Delete(int? id)
    {
      var currentUser = CurrentUser;
      using (var cx = new ViralContext())
      {
        var savedPost = cx.Posts.SingleOrDefault(p => p.Id == id && p.Author.Id == currentUser.Id);
        if (savedPost == null)
          return RedirectToAction("Index", "Home");

        savedPost.IsDeleted = true;
        cx.Entry(savedPost).State = EntityState.Modified;
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
        var existingRating = cx.Ratings.SingleOrDefault(r => r.User.Id == CurrentUserId && r.Post.Id == id);
        if (existingRating != null)
        {
          existingRating.RatedTime = DateTime.UtcNow;
          existingRating.Type = type;

          cx.Entry(existingRating).State = EntityState.Modified;
        }
        else
        {
          var post = cx.Posts
            .Where(p => p.Id == id)
            .Include(p => p.Ratings)
            .SingleOrDefault();

          if (post == null)
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

          if (post.Ratings == null)
            post.Ratings = new List<Rating>();
          post.Ratings.Add(rating);

          cx.Entry(user).State = EntityState.Modified;
          cx.Entry(post).State = EntityState.Modified;
        }

        cx.SaveChanges();

        return GetTop(0, 0);
      }
    }
  }
}