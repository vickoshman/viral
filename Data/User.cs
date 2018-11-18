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
    public bool IsDeleted { get; set; }

    public List<Post> PostedPosts { get; set; }
    public List<Comment> PostedComments { get; set; }
    public List<Rating> Ratings { get; set; }
  }
}