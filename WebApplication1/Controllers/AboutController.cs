using  Socha3.MemeBox2000.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace  Socha3.MemeBox2000.Controllers
{
    public class AboutController : MemeBox2000BaseController
    {
        //
        // GET: /About/

        public ActionResult Index()
        {
            return View();
        }

    }
}
