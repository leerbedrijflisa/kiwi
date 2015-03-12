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

            // anonymous login
            var anonymousLoginProxy = new AuthenticationProxy("http://localhost:20151/", "/api/oauth", String.Empty);

            var loginResult = await anonymousLoginProxy.LoginAnonymous(report.AnonymousToken);

            // TODO: add error handling
            var authCookie = new HttpCookie("token", String.Format("{0} {1}", loginResult.TokenType, loginResult.Token))
            {
                Expires = DateTime.Now.AddMinutes(10)
            };
            var cookie = new HttpCookie("report", report.Id.ToString());
            Response.SetCookie(cookie);
            Response.SetCookie(authCookie);

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
            var report = await GetCurrentReport();
            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(report.Id, report);

            // TODO: add error handling

            return RedirectToAction("Done");
        }

        public ActionResult Done()
        {
            // TODO: show report

            return View();
        }

        private async Task<Report> GetCurrentReport()
        {
            var cookie = Request.Cookies["report"];
            int reportId = Int32.Parse(cookie.Value);
            return await _reportProxy.GetAsync(reportId);
        }

        // Fiddler version
        //private readonly Proxy<Report> _reportProxy = new Proxy<Report>("http://localhost.fiddler:20151/", "/reports/");

        // Normal version
        private readonly Proxy<Report> _reportProxy = new Proxy<Report>("http://localhost:20151/", "/reports/");

        private readonly ModelFactory _modelFactory = new ModelFactory();
	}
}