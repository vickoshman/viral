namespace WebApp.Models
{
  public class AuthUserModel
  {
    public string Username { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    public string Email { get; set; }
    public string AvatarPath { get; set; }
  }
}