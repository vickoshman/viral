using System.Web.Mvc;
using Data;

namespace WebApp.Controllers
{
  public abstract class BaseController : Controller
  {
    protected int CurrentUserId => int.Parse(User.Identity.Name);

    protected User CurrentUser
    {
      get
      {
        if (!User.Identity.IsAuthenticated)
          return null;

        using (var cx = new ViralContext())
          return cx.Users.Find(int.Parse(User.Identity.Name));
      }
    }
  }
}