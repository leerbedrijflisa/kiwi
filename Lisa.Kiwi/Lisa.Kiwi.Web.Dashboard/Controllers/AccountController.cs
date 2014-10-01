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
           var sessionTimeOut = Session.Timeout = 60;

           if (Session["user"] == null || sessionTimeOut == 0)
           {
               return View();
           }
           else
           {
               return Redirect("Dashboard");
           }
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var masterpass = "hello";

            if (masterpass == password)
            {
                Session["user"] = username;

                return Redirect("Dashboard");
            }
            else
            {
                return View();
            }
        }
    }
}