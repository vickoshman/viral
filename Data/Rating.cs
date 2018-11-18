using System;

namespace Data
{
  public class Rating
  {
    public int Id { get; set; }
    public DateTime RatedTime { get; set; }
    public User User { get; set; }
    public Post Post { get; set; }
    public Comment Comment { get; set; }
    public RatingType Type { get; set; }
  }

  public enum RatingType
  {
    Liked = 1,
    Disliked = 2
  }
}