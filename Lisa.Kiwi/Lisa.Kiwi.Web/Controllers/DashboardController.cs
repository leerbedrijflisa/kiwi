using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Lisa.Kiwi.WebApi;
using Resources;

namespace Lisa.Kiwi.Web
{
    public class DashboardController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var tokenCookie = Request.Cookies["token"];
            if (tokenCookie!= null)
            {
                var token = Request.Cookies["token"].Value;
                var tokenType = Request.Cookies["token_type"].Value;

                _reportProxy = new Proxy<Report>("http://localhost:20151/", "/reports", token, tokenType);
                _contactProxy = new Proxy<Contact>("http://localhost:20151/", "/contacts", token, tokenType);
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

            //var contacts = await _contactProxy.GetAsync(); // No00O!
            ViewBag.Contact = report.Contact; //contacts.FirstOrDefault(s => s.Report == report.Id);
             
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
            //await _reportProxy.PatchAsync(model.Id, statusChange);

            return RedirectToAction("Details", new {id = model.Id});
        }

        private Proxy<Report> _reportProxy;
        private Proxy<Contact> _contactProxy;
    }
}