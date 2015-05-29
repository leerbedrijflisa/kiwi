using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi;
using Lisa.Kiwi.WebApi.Access;
using System.Web.Configuration;
using Lisa.Common.Access;

namespace Lisa.Kiwi.Web
{
    public class ReportController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.Result = filterContext.Exception.GetType() == typeof(UnauthorizedAccessException) ? View("SessionExpired") : View("Error", filterContext.Exception);

            filterContext.ExceptionHandled = true;

            base.OnException(filterContext);
        }

        public ActionResult Index()
        {
            return View(new CategoryViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Index(CategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // TODO: replace AnonymousToken in Report model by a custom HTTP header
            var report = _modelFactory.Create(viewModel);
            report = await _reportProxy.PostAsync(report);

            await EnsureReportAccess(report);

            return RedirectToAction("Location");
        }

        public ActionResult Location()
        {
            return View(new LocationViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Location(LocationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var report = await GetCurrentReport();
            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(report.Id, report);

            return RedirectToAction("AdditionalLocation");
        }

        public async Task<ActionResult> AdditionalLocation()
        {
            var report = await GetCurrentReport();
            ViewBag.Building = Resources.Buildings.ResourceManager.GetString(report.Location.Building);
            ViewBag.Preposition = Resources.Buildings.ResourceManager.GetString(report.Location.Building + "_Preposition");

            return View(new AdditionalLocationViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> AdditionalLocation(AdditionalLocationViewModel viewModel)
        {
            var report = await GetCurrentReport();
            if (!ModelState.IsValid)
            {
                ViewBag.Building = Resources.Buildings.ResourceManager.GetString(report.Location.Building);
                ViewBag.Preposition = Resources.Buildings.ResourceManager.GetString(report.Location.Building + "_Preposition");
                return View(viewModel);
            }

            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(report.Id, report);

            return RedirectToAction(report.Category);
        }

        public ActionResult FirstAid()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> FirstAid(FirstAidViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var report = await GetCurrentReport();

            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(report.Id, report);

            // TODO: add error handling

            return RedirectToAction("Contact");
        }


        public ActionResult Theft()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Theft(TheftViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var report = await GetCurrentReport();

            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(report.Id, report);

            return RedirectToAction("Vehicle");
        }

        public ActionResult Drugs()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Drugs(DrugsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var report = await GetCurrentReport();

            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(report.Id, report);

            return RedirectToAction("Perpetrator");
        }

        public ActionResult Fight()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Fight(FightViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var report = await GetCurrentReport();

            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(report.Id, report);

            // TODO: add error handling

            if (report.IsWeaponPresent == true)
            {
                return RedirectToAction("Weapons");
            }

            return RedirectToAction("Contact");
        }

        public ActionResult Weapons()
        {
            return View(new WeaponViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Weapons(WeaponViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var report = await GetCurrentReport();

            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(report.Id, report);

            if (report.Category == "Fight")
            {
                return RedirectToAction("Contact");
            }

            return RedirectToAction("Perpetrator");
        }

        public ActionResult Nuisance()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Nuisance(NuisanceViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var report = await GetCurrentReport();

            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(report.Id, report);

            return RedirectToAction("Vehicle");
        }

        public ActionResult Bullying()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Bullying(BullyingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var report = await GetCurrentReport();
            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(report.Id, report);

            if (viewModel.HasPerpetrator && !viewModel.HasVictim)
            {
                return RedirectToAction("Perpetrator");
            }
            else if (!viewModel.HasPerpetrator && viewModel.HasVictim)
            {
                return RedirectToAction("Victim");
            }
            else if (viewModel.HasPerpetrator && viewModel.HasVictim)
            {
                return RedirectToAction("Perpetrator", routeValues: new { hasVictim = viewModel.HasVictim });
            }

            return RedirectToAction("Vehicle");
        }

        public ActionResult Other()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Other(OtherViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var report = await GetCurrentReport();
            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(report.Id, report);

            return RedirectToAction("Vehicle");
        }

        public ActionResult Perpetrator()
        {
            return View(new PerpetratorViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Perpetrator(PerpetratorViewModel viewModel, bool? hasVictim)
        {
            var victim = hasVictim.HasValue && hasVictim.Value;

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var report = await GetCurrentReport();
            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(report.Id, report);

            return RedirectToAction(victim ? "Victim" : "Vehicle");
        }

        public ActionResult Victim()
        {
            return View(new VictimViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Victim(VictimViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var report = await GetCurrentReport();
            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(report.Id, report);

            // TODO: add error handling
            return RedirectToAction("Vehicle");
        }

        public ActionResult Vehicle()
        {
            return View(new VehicleViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Vehicle(VehicleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var report = await GetCurrentReport();

            if (viewModel.HasVehicle)
            {
                _modelFactory.Modify(report, viewModel);
                await _reportProxy.PatchAsync(report.Id, report);
            }

            switch (report.Category)
            {
                case "Theft":
                case "Nuisance":
                case "Other":
                    return RedirectToAction("ContactRequired");

                default:
                    return RedirectToAction("Contact");
            }
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Contact(ContactViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (viewModel.EmailAddress != null || viewModel.Name != null || viewModel.PhoneNumber != null)
            {
                var report = await GetCurrentReport();
                _modelFactory.Modify(report, viewModel);
                await _reportProxy.PatchAsync(report.Id, report);
            }

            // TODO: add error handling

            return RedirectToAction("Done");
        }

        public ActionResult ContactRequired()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ContactRequired(ContactRequiredViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var report = await GetCurrentReport();
            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(report.Id, report);

            // TODO: add error handling

            return RedirectToAction("Done");
        }

        public async Task<ActionResult> Done()
        {
            var report = await GetCurrentReport();
            return View(report);
        }

        [HttpPost]
        public async Task<ActionResult> Done(string category)
        {
            var report = await GetCurrentReport();
            switch (category)
            {
                case "Theft":
                case "Bullying":
                    return View("Help", report);

                default:
                    return View("End", report);
            }
        }

        public async Task<ActionResult> EditDone(EditDoneViewModel viewModel)
        {
            var report = await GetCurrentReport();
            _modelFactory.Create(report, viewModel);
            return View(viewModel);
        }

        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            CreateReportProxy();

            var tokenCookie = Request.Cookies["token"];
            if (tokenCookie != null)
            {
                _reportProxy.Token = new Common.Access.Token
                {
                    Value = tokenCookie.Value
                };
            }

            base.OnActionExecuting(context);
        }

        private async Task EnsureReportAccess(Report report)
        {
            var loginProxy = new AuthenticationProxy(WebConfigurationManager.AppSettings["WebApiUrl"], "/api/oauth");
            var token = await loginProxy.LoginAnonymous(report.AnonymousToken);

            // TODO: add error handling
            var authCookie = new HttpCookie("token", token.Value)
            {
                // TODO: let the web api determine the expiration time of the token. Right now, this doesn't
                // work because both types of token (for the reporter and the dashboard) have the same expiration
                // value, which is set in Startup.cs.
                Expires = DateTime.Now.AddMinutes(10)
            };
            var cookie = new HttpCookie("report", report.Id.ToString());

            Response.Cookies.Add(cookie);
            Response.Cookies.Add(authCookie);
        }

        private async Task<Report> GetCurrentReport()
        {
            var cookie = Request.Cookies["report"];
            if (cookie == null)
            {
                return null;
            }
            var reportId = Int32.Parse(cookie.Value);
            return await _reportProxy.GetAsync(reportId);
        }

        private void CreateReportProxy()
        {
            _reportProxy = new Proxy<Report>(MvcApplication.GetApiUrl() + "reports");
        }

        private Proxy<Report> _reportProxy;

        private readonly ModelFactory _modelFactory = new ModelFactory();
    }
}