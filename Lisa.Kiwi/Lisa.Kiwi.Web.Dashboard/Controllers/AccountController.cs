using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Lisa.Kiwi.WebApi;
using Lisa.Kiwi.WebApi.Access;

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
               return RedirectToAction("Index", "Report");
           }
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var masterpass = "master"; // beveiliger
            var userpass = "hello"; // beveiliger


            if ((username == "hoofd beveiliger" && masterpass == password) || (username == "beveiliger" && userpass == password))
            {
                Session["user"] = username;
            }
            else
            {
                ModelState.AddModelError("password", "Geen geldig wachtwoord en/of gebruikersnaam.");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            
            return RedirectToAction("Index", "Report");
        }

        public ActionResult Logout()
        {
            if (Session.Count > 0)
            {
                Session.RemoveAll();
            }
            
            return RedirectToAction("Login");
        }
    }
}