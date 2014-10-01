using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lisa.Kiwi.WebApi.Access;
using System.Threading.Tasks;

namespace Lisa.Kiwi.Web.Dashboard.Controllers
{
    public class DashboardController : AsyncController
    {
        
     
        // GET: Index
        [HttpGet]
        public async Task<ActionResult> Index()
        {


               var connector = new Connector();
               var reports = await connector.GetAllReports();
               
               return View(reports);          
           
        }
    }
}