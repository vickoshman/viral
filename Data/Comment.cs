using System;
using System.Collections.Generic;

namespace Data
{
  public class Comment
  {
    public int Id { get; set; }
    public User Author { get; set; }
    public DateTime PostedTime { get; set; }
    public DateTime LastEditTime { get; set; }
    public Post ForPost { get; set; }
    public Comment ForComment { get; set; }
    public string Text { get; set; }
    public string ContentPath { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Rating> Ratings { get; set; }
  }
}