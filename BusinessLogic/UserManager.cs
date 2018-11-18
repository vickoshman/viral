using System.Data.Entity.Migrations;
using Data;

namespace BusinessLogic
{
  public class UserManager
  {
    public static void Add(User user)
    {
      using (var cx = new ViralContext())
      {
        cx.Users.Add(user);
        cx.SaveChanges();
      }
    }

    public static void Update(User user)
    {
      using (var cx = new ViralContext())
      {
        cx.Users.AddOrUpdate(user);
        cx.SaveChanges();
      }
    }

    public static void Get(int id)
    {
      using (var cx = new ViralContext())
      {
        cx.Users.Find(id);
      }
    }
  }
}