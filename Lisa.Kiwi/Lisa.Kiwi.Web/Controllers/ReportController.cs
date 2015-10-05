using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi;
using Lisa.Kiwi.WebApi.Access;
using System.Web.Configuration;
using Lisa.Common.Access;
using SendGrid;
using System.Net;
using System.Net.Mail;

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

        //Sends a message to the email address
        [HttpPost]
        public async Task<ActionResult> BugReport(BugReportViewModel viewModel)
        {
            try
            {
                //SendGrid username and password from azure.
                var username = "azure_90c1637306a250426ada1015ca65e6e0@azure.com";
                var pswrd = "27UICbH178dUI0m";
                //Create a new SendGrid message
                var mailMessage = new SendGridMessage();
                //Where the mail has to go to
                mailMessage.AddTo("beveiliging@davinci.nl");
                //Sender from the mail
                mailMessage.From = new MailAddress(viewModel.EmailAddress);
                //Subject from the mail
                mailMessage.Subject = viewModel.Subject;
                //Body of the mail
                mailMessage.Html = viewModel.Message;
                mailMessage.Html = mailMessage.Html.Replace(Environment.NewLine, "<br />");
                var credentials = new NetworkCredential(username, pswrd);
                //Send the mail using SendGrid
                var transportWeb = new SendGrid.Web(credentials);
                await transportWeb.DeliverAsync(mailMessage);
                return View();
            }
            catch (Exception ex)
            {
                Response.Write("Could not send the e-mail - error: " + ex.Message);
                return View("~/Views/Shared/Error.cshtml");
            }
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

            var reportId = GetCurrentReportId();
            var report = new Report();

            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(reportId, report);

            return RedirectToAction("AdditionalLocation");
        }

        public async Task<ActionResult> AdditionalLocation()
        {
            var report = await GetCurrentReport();
            var building = report.Location.Building;
            ViewBag.Building = Resources.Buildings.ResourceManager.GetString(building);
            ViewBag.Preposition = Resources.Buildings.ResourceManager.GetString(building + "_Preposition");

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
            report = await _reportProxy.PatchAsync(GetCurrentReportId(), report);

            return RedirectToAction("Details");
        }

        public async Task<ActionResult> Details()
        {
            var report = await GetCurrentReport();

            return View(report);
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

            var report = new Report();
            _modelFactory.Modify(report, viewModel);

            await _reportProxy.PatchAsync(GetCurrentReportId(), report);

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

            var report = new Report();

            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(GetCurrentReportId(), report);

            return RedirectToAction("ContactRequired");
        }

        public ActionResult Drugs()
        {
            return View(new DrugsViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Drugs(DrugsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var report = new Report();

            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(GetCurrentReportId(), report);

            return RedirectToAction("Contact");
        }

        public ActionResult Fight()
        {
            return View(new FightViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Fight(FightViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var report = new Report();

            _modelFactory.Modify(report, viewModel);
            report = await _reportProxy.PatchAsync(GetCurrentReportId(), report);

            // TODO: add error handling

            if (report.IsWeaponPresent ?? false)
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

            var report = new Report();

            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(GetCurrentReportId(), report);

            return RedirectToAction("Contact");
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

            var report = new Report();

            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(GetCurrentReportId(), report);

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

            var report = new Report();
            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(GetCurrentReportId(), report);
            
            return RedirectToAction("Contact");
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

            var report = new Report();
            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(GetCurrentReportId(), report);

            return RedirectToAction("ContactRequired");
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
                var report = new Report();
                _modelFactory.Modify(report, viewModel);
                await _reportProxy.PatchAsync(GetCurrentReportId(), report);
            }

            return RedirectToAction("Continue");
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

            var report = new Report();
            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(GetCurrentReportId(), report);

            return RedirectToAction("Continue");
        }

        public ActionResult Continue()
        {
            return View();
        }

        public async Task<ActionResult> Done()
        {
            var report = await GetCurrentReport();
            return View(new DoneViewModel(report));
        }

        [HttpPost]
        public async Task<ActionResult> Done(DoneViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(new DoneViewModel(await GetCurrentReport()));
            }
            
            var report = new Report();

            _modelFactory.Modify(report, viewModel);
            await _reportProxy.PatchAsync(GetCurrentReportId(), report);

            return RedirectToAction("Done");
        }

        [HttpPost]
        public async Task<JsonResult> UploadFiles()
        {
            try
            {
                var files = Request.Files;

                foreach (string file in files)
                {
                    if (!FileHelpers.IsMimes(files[file], new string[] { "image" }))
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(Resources.ErrorMessages.FileExtension);
                    }
                    if (!FileHelpers.IsSize(files[file], 10485760))
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(Resources.ErrorMessages.FileSize);
                    }
                }

                var report = await GetCurrentReport();
                await _modelFactory.Modify(report, files);
                await _reportProxy.PatchAsync(report.Id, report);
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(Resources.ErrorMessages.FileUnexpected);
            }

            return Json(Resources.PageItems.FileSuccess);
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
                Expires = DateTime.Now.AddMinutes(token.ExpiresIn)
            };
            var cookie = new HttpCookie("report", report.Id.ToString());

            Response.Cookies.Add(cookie);
            Response.Cookies.Add(authCookie);
        }

        private async Task<Report> GetCurrentReport()
        {
            return await _reportProxy.GetAsync(GetCurrentReportId());
        }

        private int GetCurrentReportId()
        {
            var cookie = Request.Cookies["report"];
            if (cookie == null)
            {
                throw new Exception("Report not found");
            }
            return int.Parse(cookie.Value);
        }

        private void CreateReportProxy()
        {
            _reportProxy = new Proxy<Report>(MvcApplication.GetApiUrl() + "reports");
        }

        private Proxy<Report> _reportProxy;

        private readonly ModelFactory _modelFactory = new ModelFactory();
    }
}