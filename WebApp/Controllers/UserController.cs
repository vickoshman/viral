using System.Web.Mvc;
using Data;

namespace WebApp.Controllers
{
  public class UserController : BaseController
  {
    [Authorize]
    public ActionResult Index()
    {
      return View(CurrentUser);
    }

    public ActionResult UserProfile(int id)
    {
      User model;
      using (var cx = new ViralContext())
      {
        model = cx.Users.Find(id);
      }

      return View("Index", model);
    }
  }
}