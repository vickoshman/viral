﻿using System.Web.Mvc;

namespace WebApp.Controllers
{
  public class HomeController : BaseController
  {
    public ActionResult Index()
    {
      return View(CurrentUser);
    }
  }
}