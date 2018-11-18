using System;
using System.Collections.Generic;

namespace Data
{
  public class User
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime RegisteredTime { get; set; }
    public string AvatarPath { get; set; }

    public ICollection<Post> PostedPosts { get; set; }
    public ICollection<Comment> PostedComments { get; set; }
    public ICollection<Rating> Ratings { get; set; }
  }
}