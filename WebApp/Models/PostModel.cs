using System.Linq;
using Data;

namespace WebApp.Models
{
  public class PostModel : Post
  {
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public int CommentCount { get; set; }
    public string AuthorAvatarPath { get; set; }
    public string AuthorUsername { get; set; }

    public PostModel(Post post)
    {
      Id = post.Id;
      PostedTime = post.PostedTime;
      ContentPath = post.ContentPath;
      Title = post.Title;
      Text = post.Text;
      Likes = post.Ratings.Count(p => p.Type == RatingType.Liked);
      Dislikes = post.Ratings.Count(p => p.Type == RatingType.Disliked);
      CommentCount = post.Comments.Count;
      AuthorAvatarPath = post.Author.AvatarPath;
      AuthorUsername = post.Author.Username;

      post.Ratings = null;
      post.Comments = null;
      post.Author = null;
    }
  }
}