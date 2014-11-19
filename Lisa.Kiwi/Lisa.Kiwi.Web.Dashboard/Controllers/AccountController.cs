using System.Security.Authentication;
using System.Threading.Tasks;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi.Access;
using Newtonsoft.Json.Linq;
using Resources;

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
			return RedirectToAction("Index", "Report");
		}

		[HttpPost]
		public async Task<ActionResult> Login(string username, string password)
		{
			try
			{
			    var authenticationClient = new AuthenticationProxy(ConfigHelper.GetAuthUri());
			    var authenticationReponse = await authenticationClient.Login(username, password);
                var authenticationInformation = JObject.Parse(authenticationReponse);

                Session["token"] = authenticationInformation.SelectToken("access_token").ToString();
                Session["token_type"] = authenticationInformation.SelectToken("token_type").ToString();
                Session["token_aliveTime"] = authenticationInformation.SelectToken("expires_in").ToString();
				Session["user"] = username;
			}
			catch(AuthenticationException)
			{
				ModelState.AddModelError("password", DisplayNames.AccountLoginInvalid);
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