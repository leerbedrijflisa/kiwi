using System.Web.Mvc;


namespace Lisa.Kiwi.Web.Reporting.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.FietsventielDopjesDoosjesFabrieksDeurRuitjesMakelaarsBureautafel = "Location";
            return View();
        }

        [HttpPost]
        public ActionResult Location()
        {
            return View();
        }
    }
}