using Socha3.Common.Spaminator;
using  Socha3.MemeBox2000.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace  Socha3.MemeBox2000.Controllers
{
    public class ContactController : MemeBox2000BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitMessage(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Message sent!";

                var msg = $"New Contact request:\r\nEmail: {model.Email}\r\nName: {model.Name}\r\nSubject: {model.Subject}\r\nMessage: {model.Message}";
                EmailUtil.Gmail("mike@socha3.com", "mike@socha3.com", "", "MemeBox2000 Contact", msg);
            }

            return View("Index", model);
        }
    }
}
