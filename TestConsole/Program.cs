using System;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace TestConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      using (var cx = new ViralContext())
      {
        var user = new User
        {
          Email = "victor.a.koshman@gmail.com",
          Username = "victor",
          Password = "test",
          RegisteredTime = DateTime.UtcNow
        };

        var post = new Post
        {
          Title = "Funny title",
          Text = "Funny text",
          PostedTime = DateTime.UtcNow,
          ContentPath = "url"
        };

        cx.Users.Add(user);
        user.PostedPosts = new List<Post> { post };
        cx.Posts.Add(post);
        cx.SaveChanges();
        /*var user1 = cx.Users.Find(1);
        var user2 = cx.Users.Find(2);
        var posts = cx.Posts.Where(p => p.Author.Id == 2).ToList();*/
      }

      Console.WriteLine("Finished");
      Console.ReadKey();
    }
  }
}
