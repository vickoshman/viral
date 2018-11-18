using System.Linq;
using Data;

namespace WebApp.Models
{
  public class CommentModel : Comment
  {
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public int? ParentCommentId { get; set; }
    public string AuthorAvatarPath { get; set; }
    public string AuthorUsername { get; set; }

    public CommentModel(Comment comment)
    {
      Id = comment.Id;
      PostedTime = comment.PostedTime;
      Text = comment.Text;
      ContentPath = comment.ContentPath;
      Likes = comment.Ratings.Count(p => p.Type == RatingType.Liked);
      Dislikes = comment.Ratings.Count(p => p.Type == RatingType.Disliked);
      ParentCommentId = ForComment?.Id;
      AuthorAvatarPath = comment.Author.AvatarPath;
      AuthorUsername = comment.Author.Username;

      comment.Ratings = null;
      comment.ForPost = null;
      comment.ForComment = null;
      comment.Author = null;
    }
  }
}