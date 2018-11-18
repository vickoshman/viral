using System.Web.Mvc;
using Data;

namespace WebApp.Controllers
{
  public abstract class BaseController : Controller
  {
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