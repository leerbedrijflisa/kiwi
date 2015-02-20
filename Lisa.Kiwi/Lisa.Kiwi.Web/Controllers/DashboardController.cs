using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi;

namespace Lisa.Kiwi.Web
{
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

            return View(report);
        }

        private readonly Proxy<Report> _reportProxy = new Proxy<Report>("http://localhost:20151/", "/reports");
        private readonly Proxy<Contact> _contactProxy = new Proxy<Contact>("http://localhost:20151/", "/contacts");
    }
}