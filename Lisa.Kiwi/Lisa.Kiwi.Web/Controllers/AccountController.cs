using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi.Access;
using System.Web.Configuration;
using Lisa.Common.Access;
using Lisa.Kiwi.WebApi;
using Lisa.Kiwi.Web.Resources;

namespace Lisa.Kiwi.Web
{
    public class AccountController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception.GetType() == typeof(UnauthorizedAccessException))
            {
                filterContext.Result = RedirectToAction("Login", "Account");
            }
            else
            {
                filterContext.Result = View("Error", filterContext.Exception);
            }

            filterContext.ExceptionHandled = true;

            base.OnException(filterContext);
        }
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

            var authProxy = new AuthenticationProxy(MvcApplication.GetApiUrl(), "/api/oauth");

            var token = await authProxy.Login(model.UserName, model.Password);
            if (token == null)
            {
                ModelState.AddModelError("password", ErrorMessages.IncorrectLogin);
                return View();
            }

            var tokenCookie = new HttpCookie("token", token.Value)
            {
                Expires = DateTime.Now.AddSeconds(token.ExpiresIn),
                HttpOnly = false
            };

            var roleCookie = new HttpCookie("role", token.Role)
            {
                Expires = DateTime.Now.AddSeconds(token.ExpiresIn),
                HttpOnly = true
            };

            Response.Cookies.Add(tokenCookie);
            Response.Cookies.Add(roleCookie);
            
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult EditPassword(string id, string userName)
        {
            return View(new PasswordViewModel{Username = userName, Id = id});
        }

        [HttpPost]
        public async Task<ActionResult> EditPassword(PasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var authProxy = new AuthenticationProxy(WebConfigurationManager.AppSettings["WebApiUrl"], "user");

            await authProxy.UpdatePassword(viewModel.Id, viewModel.NewPassword, "bearer", Request.Cookies["token"].Value);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Index()
        {
            var userProxy = new Proxy<User>(WebConfigurationManager.AppSettings["WebApiUrl"] + "user");
            EnsureAccess(userProxy);

            var users = await userProxy.GetAsync();

            return View(users);
        }

        public ActionResult Logout()
        {
            // TODO: the web API should invalidate token when loggong out
            Response.Cookies.Add(new HttpCookie("token")
            {
                Expires = DateTime.Now.AddDays(-1)
            });

            Response.Cookies.Add(new HttpCookie("role")
            {
                Expires = DateTime.Now.AddDays(-1)
            });

            return RedirectToAction("Login");
        }

        private void EnsureAccess(Proxy<User> proxy)
        {
            var tokenCookie = Request.Cookies["token"];
            if (tokenCookie != null)
            {
                proxy.Token = new Common.Access.Token
                {
                    Value = tokenCookie.Value
                };
            }
        }
    }
}