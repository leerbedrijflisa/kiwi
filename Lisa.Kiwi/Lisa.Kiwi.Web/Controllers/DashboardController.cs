using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Lisa.Common.Access;
using Lisa.Kiwi.Web.Resources;
using Lisa.Kiwi.WebApi;
using Lisa.Kiwi.WebApi.Access;
using Token = Lisa.Common.Access.Token;

namespace Lisa.Kiwi.Web
{
    public class DashboardController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            _reportProxy = new Proxy<Report>(MvcApplication.GetApiUrl() + "reports");

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

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception.GetType() == typeof (UnauthorizedAccessException))
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

        public async Task<ActionResult> Index()
        {
            if (await _authenticationProxy.GetIsAnonymous("bearer", Request.Cookies["token"].Value))
            {
                throw new UnauthorizedAccessException();
            }

            return View();
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var report = await _reportProxy.GetAsync(id.Value);

            if (report == null)
            {
                return HttpNotFound();
            }

            ViewBag.Changed = TempData["Changed"] ?? false;

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

            TempData["Changed"] = true;

            return RedirectToAction("Details", new { viewModel.Id });
        }

        public async Task<ActionResult> Archive()
        {
            if (!await _authenticationProxy.GetIsAdmin("bearer", Request.Cookies["token"].Value))
            {
                throw new UnauthorizedAccessException();
            }

            return View();
        }

        public async Task<ActionResult> ToggleSolved(int id, StatusEnum currentSolved)
        {
            Report report;

            switch(currentSolved)
            {
                case StatusEnum.Open:
                case StatusEnum.Transferred:
                    report = new Report { Status = StatusEnum.Solved };
                    break;
                case StatusEnum.Solved:
                    report = new Report { Status = StatusEnum.Open };
                    break;
                default:
                    throw new ArgumentException();
            }

            await _reportProxy.PatchAsync(id, report);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> ToggleVisible(int id, bool isVisible)
        {
            var report = new Report { IsVisible = !isVisible };
            await _reportProxy.PatchAsync(id, report);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DeleteReport(int id)
        {
            await _reportProxy.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        private readonly ModelFactory _modelFactory = new ModelFactory();
        private Proxy<Report> _reportProxy = new Proxy<Report>(MvcApplication.GetApiUrl() + "reports");
        private readonly AuthenticationProxy _authenticationProxy = new AuthenticationProxy(MvcApplication.GetApiUrl(), "");
    }
}