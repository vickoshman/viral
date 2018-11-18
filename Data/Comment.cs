using System;
using System.Collections.Generic;

namespace Data
{
  public class Comment
  {
    public int Id { get; set; }
    public User Author { get; set; }
    public DateTime PostedTime { get; set; }
    public Post ForPost { get; set; }
    public Comment ForComment { get; set; }

    public ICollection<Rating> Ratings { get; set; }
  }
}