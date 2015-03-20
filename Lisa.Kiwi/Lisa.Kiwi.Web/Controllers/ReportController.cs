using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi;
using Lisa.Kiwi.WebApi.Access;

namespace Lisa.Kiwi.Web.Reporting.Controllers
{
    public class ReportController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            // the user can't be authorized on Index Action
            if (context.ActionDescriptor.ActionName.ToLower() == "index")
            {
                _reportProxy = new Proxy<Report>("http://localhost:20151/", "/reports/");
            }
            else
            {
                var tokenCookie = Request.Cookies["token"];
                if (tokenCookie != null)
                {
                    var tokenType = tokenCookie.Value.Split(' ')[0];
                    var token = tokenCookie.Value.Split(' ')[1];

                    _reportProxy = new Proxy<Report>("http://localhost:20151", "/reports/", token, tokenType);
                }
            }

            base.OnActionExecuting(context);
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

            var report = _modelFactory.Create(viewModel);
            report = await _reportProxy.PostAsync(report);

            var loginProxy = new AuthenticationProxy("http://localhost:20151/", "/api/oauth");

            var loginResult = await loginProxy.LoginAnonymous(report.AnonymousToken);

            // TODO: add error handling
            var authCookie = new HttpCookie("token", String.Join(" ", loginResult.TokenType, loginResult.Token))
            {
                Expires = DateTime.Now.AddMinutes(10)
            };
            var cookie = new HttpCookie("report", report.Id.ToString());

            Response.Cookies.Add(cookie);
            Response.Cookies.Add(authCookie);

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

            return RedirectToAction("ContactRequired");
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

            return RedirectToAction("ContactRequired");
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

            return RedirectToAction("Perpetrator");
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

            return RedirectToAction("ContactRequired");
        }

        public ActionResult Perpetrator()
        {
            return View(new PerpetratorViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Perpetrator(PerpetratorViewModel viewModel)
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

            var report = await GetCurrentReport();
            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(report.Id, report);

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

        public async Task<ActionResult> Redirect()
        {
            string link = null;
            var report = await GetCurrentReport();
            switch (report.Category){
                case "Theft":
                    link = "Police";
                    break;
                case "Bullying":
                    link = "Help";
                    break;
                default:
                    link = "End";
                    break;
            }
            return View(link);
        }

        private async Task<Report> GetCurrentReport()
        {
            var cookie = Request.Cookies["report"];
            if (cookie == null)
            {
                return null;
            }
            int reportId = Int32.Parse(cookie.Value);
            return await _reportProxy.GetAsync(reportId);
        }

        // Fiddler version
        //private  Proxy<Report> _reportProxy = new Proxy<Report>("http://localhost.fiddler:20151/", "/reports/");

        // Normal version
        private  Proxy<Report> _reportProxy;

        private readonly ModelFactory _modelFactory = new ModelFactory();
    }
}