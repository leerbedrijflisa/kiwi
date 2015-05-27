using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi.Access;
using System.Web.Configuration;

namespace Lisa.Kiwi.Web
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var authProxy = new AuthenticationProxy(WebConfigurationManager.AppSettings["WebApiUrl"], "/api/oauth");

            var token = await authProxy.Login(model.UserName, model.Password);
            if (token == null)
            {
                ModelState.AddModelError("password", "Wachtwoord of gebruikersnaam is onjuist");
                return View();
            }

            var tokenCookie = new HttpCookie("token", token.Value)
            {
                Expires = DateTime.Now.AddMinutes(token.ExpiresIn),
                HttpOnly = false
            };
            Response.Cookies.Add(tokenCookie);
            
            return RedirectToAction("Index", "Dashboard");
        }
    }
}