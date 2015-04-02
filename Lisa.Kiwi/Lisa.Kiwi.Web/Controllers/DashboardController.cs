using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi;
using Lisa.Kiwi.WebApi.Access;
using Resources;
using System.Web.Configuration;

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
                    Text = Statuses.Open
                },
                new SelectListItem
                {
                    Value = "Solved",
                    Text = Statuses.Solved
                },
                new SelectListItem
                {
                    Value = "Transferred",
                    Text = Statuses.Transferred
                }
            };

            return View(report);
        }

        [HttpPost]
        public async Task<ActionResult> Details(StatusChangeViewModel viewModel)
        {
            if (!await CheckAuth())
            {
                return RedirectToAction("Login", "Account");
            }

            var report = new Report();
            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(viewModel.Id, report);

            return RedirectToAction("Details", new { viewModel.Id });
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

                var authProxy = new AuthenticationProxy(WebConfigurationManager.AppSettings["WebApiUrl"], "");

                if (await authProxy.GetIsAnonymous(tokenType, token))
                {
                    return false;
                }

                _reportProxy = new Proxy<Report>(WebConfigurationManager.AppSettings["WebApiUrl"], "/reports", token, tokenType);

                return true;
            }

            return false;
        }


        private readonly ModelFactory _modelFactory = new ModelFactory();
        private Proxy<Report> _reportProxy = new Proxy<Report>(WebConfigurationManager.AppSettings["WebApiUrl"], "/reports/");
    }
}