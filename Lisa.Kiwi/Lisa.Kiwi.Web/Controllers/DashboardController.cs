using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi;
using Lisa.Kiwi.WebApi.Access;
using Resources;

namespace Lisa.Kiwi.Web
{
    public class DashboardController : Controller
    {
        public async Task<ActionResult> Index()
        {
            if (!await CheckAuth())
            {
                return RedirectToAction("Login", "Account");
            }

            var reports = await _reportProxy.GetAsync();
            return View(reports);
        }

        public async Task<ActionResult> Details(int id = 0)
        {
            if (!await CheckAuth())
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == 0)
            {
                return HttpNotFound();
            }

            var report = await _reportProxy.GetAsync(id);

            if (report == null)
            {
                return HttpNotFound();
            }

            ViewBag.StatusNames = new []
            {
                new SelectListItem()
                {
                    Value = "Open",
                    Text = Statuses.StatusOpen
                },
                new SelectListItem
                {
                    Value = "Solved",
                    Text = Statuses.StatusSolved
                },
                new SelectListItem
                {
                    Value = "Transferred",
                    Text = Statuses.StatusTransferred
                }
            };

            return View(report);
        }

        [HttpPost]
        public async Task<ActionResult> Details(StatusChangeViewModel model)
        {
            if (!await CheckAuth())
            {
                return RedirectToAction("Login", "Account");
            }

            //await _reportProxy.PatchAsync(model.Id, statusChange);

            return RedirectToAction("Details", new {id = model.Id});
        }

        private async Task<bool> CheckAuth()
        {
            var tokenCookie = Request.Cookies["token"];
            if (tokenCookie != null)
            {
                string tokenType;
                string token;

                try
                {
                    tokenType = tokenCookie.Value.Split(' ')[0];
                    token = tokenCookie.Value.Split(' ')[1];
                }
                catch (IndexOutOfRangeException)
                {
                    return false;
                }

                var authProxy = new AuthenticationProxy("http://localhost.fiddler:20151", "");

                if (await authProxy.GetIsAnonymous(tokenType, token))
                {
                    return false;
                }

                _reportProxy = new Proxy<Report>("http://localhost.fiddler:20151/", "/reports", token, tokenType);

                return true;
            }

            return false;
        }

        private Proxy<Report> _reportProxy;
    }
}