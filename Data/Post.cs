using System;
using System.Collections.Generic;

namespace Data
{
  public class Post
  {
    public int Id { get; set; }
    public DateTime PostedTime { get; set; }
    public string ContentPath { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public bool IsDeleted { get; set; }

    public User Author { get; set; }
    public List<Comment> Comments { get; set; }
    public List<Rating> Ratings { get; set; }
  }
}