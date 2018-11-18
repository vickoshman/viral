using System.Data.Entity;

namespace Data
{
  public class ViralContext : DbContext
  {
    public ViralContext() : base("Server=localhost;Database=ViralDb;Trusted_Connection=True;")
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Rating> Ratings { get; set; }
  }
}