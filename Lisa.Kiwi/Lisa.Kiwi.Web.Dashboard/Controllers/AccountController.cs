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
               return RedirectToAction("Index", "Dashboard");
           }
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var masterpass = "hello";

            if (masterpass != password)
            {
                ModelState.AddModelError("password", "Geen geldig wachtwoord en/of gebruikersnaam.");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            
            Session["user"] = username;

            return RedirectToAction("Index", "Dashboard");
        }
    }
}