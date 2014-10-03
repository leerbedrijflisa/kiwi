using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lisa.Kiwi.Web.Dashboard.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
           if (Session["user"] == null)
           {
               return View();
           }
           else
           {
               return RedirectToAction("Index", "Dashboard");
           }
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var masterpass = "hello";

            if (masterpass == password)
            {
                Session["user"] = username;
                Session.Timeout = 20;

                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }
    }
}