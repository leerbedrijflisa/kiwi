using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Lisa.Kiwi.Web.App_GlobalResources.Resources;
using Lisa.Kiwi.WebApi;

namespace Lisa.Kiwi.Web
{
    [Authorize]
    public class DashboardController : Controller
    {
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

            var contacts = await _contactProxy.GetAsync(); // No00O!
            ViewBag.Contact = contacts.FirstOrDefault(s => s.Report == report.Id);
             
            ViewBag.StatusNames = new []
            {
                new SelectListItem()
                {
                    Value = "Open",
                    Text = DisplayNames.StatusOpen
                },
                new SelectListItem
                {
                    Value = "Solved",
                    Text = DisplayNames.StatusSolved
                },
                new SelectListItem
                {
                    Value = "Transferred",
                    Text = DisplayNames.StatusTransferred
                }
            };

            return View(report);
        }

        [HttpPost]
        public async Task<ActionResult> Details(StatusChangeViewModel model)
        {
            var statusChange = new Report
            {
                IsVisible = model.IsVisible,
                CurrentStatus = (WebApi.Status) Enum.Parse(typeof(WebApi.Status), model.Status)
            };

            await _reportProxy.PatchAsync(model.Id, statusChange);

            return RedirectToAction("Details", new {id = model.Id});
        }

        private readonly Proxy<Report> _reportProxy = new Proxy<Report>("http://localhost:20151/", "/reports");
        private readonly Proxy<Contact> _contactProxy = new Proxy<Contact>("http://localhost:20151/", "/contacts");
    }
}