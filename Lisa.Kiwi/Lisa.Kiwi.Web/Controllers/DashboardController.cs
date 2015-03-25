using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Lisa.Kiwi.WebApi;
using Resources;
using System.Web.Configuration;

namespace Lisa.Kiwi.Web
{
    public class DashboardController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var tokenCookie = Request.Cookies["token"];
            if (tokenCookie!= null)
            {
                string tokenType;
                string token;

                try
                {
                    tokenType = tokenCookie.Value.Split(' ')[0];
                    token = tokenCookie.Value.Split(' ')[1];
                }
                catch (IndexOutOfRangeException e)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Account" }, { "action", "Login" } });
                    return;
                }

                _reportProxy = new Proxy<Report>(WebConfigurationManager.AppSettings["WebApiUrl"], "/reports", token, tokenType);
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Account" }, { "action", "Login" } });
                return;
            }

            
            base.OnActionExecuting(filterContext);
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
            
            return RedirectToAction("Details", new { Id = viewModel.Id });
        }

        private readonly ModelFactory _modelFactory = new ModelFactory();
        private Proxy<Report> _reportProxy = new Proxy<Report>(WebConfigurationManager.AppSettings["WebApiUrl"], "/reports/");
    }
}