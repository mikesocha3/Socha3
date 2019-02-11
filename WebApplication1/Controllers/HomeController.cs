using System;
using System.Web.Mvc;

namespace Socha3.MemeBox2000.Controllers
{
    public class HomeController : MemeBox2000BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}