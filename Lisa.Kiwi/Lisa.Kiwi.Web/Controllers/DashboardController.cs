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
        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            _reportProxy = new Proxy<Report>(WebConfigurationManager.AppSettings["WebApiUrl"] + "reports");

            var tokenCookie = Request.Cookies["token"];
            if (tokenCookie != null)
            {
                _reportProxy.Token = new Token
                {
                    Value = tokenCookie.Value
                };
            }

            base.OnActionExecuting(context);
        }

        public async Task<ActionResult> Index()
        {
            var reports = await _reportProxy.GetAsync();
            return View(reports);
        }

        public async Task<ActionResult> Details(int id = 0)
        {
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
            var report = new Report();
            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(viewModel.Id, report);

            return RedirectToAction("Details", new { viewModel.Id });
        }

        private readonly ModelFactory _modelFactory = new ModelFactory();
        private Proxy<Report> _reportProxy = new Proxy<Report>(WebConfigurationManager.AppSettings["WebApiUrl"] + "reports");
    }
}