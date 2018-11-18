using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Data;
using WebApp.Models;

namespace WebApp.Controllers
{
  public class AuthController : BaseController
  {
    [HttpPost]
    public ActionResult Login(AuthUserModel authUser, string returnUrl)
    {
      if (Authenticate(authUser, out var user))
      {
        FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);

        if (!string.IsNullOrEmpty(returnUrl))
          return Redirect(returnUrl);

        return RedirectToAction("Index", "Home", user);
      }

      return RedirectToAction("Index", "Home", user);
    }

    public ActionResult SignUp()
    {
      return View("PartialSignup");
    }

    [HttpPost]
    public ActionResult SignUp(AuthUserModel signupUser)
    {
      User user;
      using (var cx = new ViralContext())
      {
        if (cx.Users.Any(u => u.Username == signupUser.Username))
          return View("PartialSignup", signupUser);

        user = cx.Users.Add(new User
        {
          Username = signupUser.Username,
          Password = signupUser.Password,
          Email = signupUser.Email,
          AvatarPath = signupUser.AvatarPath,
          RegisteredTime = DateTime.UtcNow
        });

        cx.SaveChanges();
      }

      FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);
      return RedirectToAction("Index", "Home", user);
    }

    public ActionResult Logout()
    {
      Session.Clear();
      FormsAuthentication.SignOut();
      return Redirect("/Home/Index");
    }

    private bool Authenticate(AuthUserModel authUser, out User user)
    {
      using (var cx = new ViralContext())
      {
        user = cx.Users.SingleOrDefault(u => authUser.Username.Equals(u.Username, StringComparison.OrdinalIgnoreCase) && u.Password == authUser.Password);
        return user != null;
      }
    }
  }
}