using  Socha3.MemeBox2000.Controllers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace  Socha3.MemeBox2000.Controllers
{
    public class AccountController : MemeBox2000BaseController
    {
        public ActionResult Login(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                returnUrl = "/";
            }

            // you can use this for the 'authParams.state' parameter
            // in Lock, to provide a return URL after the authentication flow.
            ViewBag.State = $"ru={HttpUtility.UrlEncode(returnUrl)}";

            return View();
        }

        public ActionResult Logout()
        {
            FederatedAuthentication.SessionAuthenticationModule.SignOut();
            return RedirectToAction("Login");
        }
    }
}
