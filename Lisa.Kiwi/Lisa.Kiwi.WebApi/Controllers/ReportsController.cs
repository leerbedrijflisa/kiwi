using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Security;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;

namespace Lisa.Kiwi.WebApi
{
    [System.Web.Http.Authorize]
    public class ReportsController : ApiController
    {
        [EnableQuery]
        [System.Web.Http.Authorize(Roles = "DashboardUser, Administrator")]
        public IQueryable<Report> Get()
        {
            var reports = GetCompleteReports();
            return User.IsInRole("Administrator") ? reports : reports.Where(r => r.IsVisible && r.Status == 0);
        }

        public IHttpActionResult Get(int? id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            
            if (claimsIdentity.HasClaim(ClaimTypes.Role, "Anonymous"))
            {
                if(!claimsIdentity.HasClaim("reportId", id.ToString()))
                {
                    return Unauthorized();
                }
            }

            var reportData = GetCompleteReportData()
                .SingleOrDefault(r => id == r.Id);

            if (reportData == null)
            {
                return NotFound();
            }

            var report = _modelFactory.Create(reportData);
            return Ok(report);
        }

        [AllowAnonymous]
        public IHttpActionResult Post([FromBody] JToken json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var reportData = _dataFactory.Create(json);

            _db.Reports.Add(reportData);
            _db.SaveChanges();

            var reportJson = _modelFactory.Create(reportData);

            reportJson.AnonymousToken = CreateAnonymousToken(reportJson.Id);

            var url = Url.Route("DefaultApi", new { controller = "reports", id = reportData.Id });
            return Created(url, reportJson);
        }

       
        public async Task<IHttpActionResult> Patch(int? id, [FromBody] JToken json)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;

            if (claimsIdentity.HasClaim(ClaimTypes.Role, "Anonymous"))
            {
                if (!claimsIdentity.HasClaim("reportId", id.ToString()))
                {
                    return Unauthorized();
                }
            }
        
            var reportData = await _db.Reports.FindAsync(id);
            if (reportData == null)
            {
                return NotFound();
            }

            if (!claimsIdentity.HasClaim("is_admin", "True") && json.Value<bool>("isVisible") != reportData.IsVisible)
            {
                return Unauthorized();
            }

            _dataFactory.Modify(reportData, json);

            reportData.Modified = DateTimeOffset.Now;

            _db.SaveChanges();
            TriggerReportDataChange();
                
            var report = _modelFactory.Create(reportData);
            return Ok(report);
        }

        [System.Web.Http.Authorize(Roles = "Administrator")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var report = await _db.Reports.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            report.IsDeleted = true;

            await _db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        private IQueryable<ReportData> GetCompleteReportData()
        {
            return _db.Reports
                .Where(s => !s.IsDeleted)
                .Include("Location")
                .Include("Perpetrators")
                .Include("Contact")
                .Include("Vehicles");
        }

        private IQueryable<Report> GetCompleteReports()
        {
            return GetCompleteReportData()
                .ToList()
                .Select(reportData => _modelFactory.Create(reportData))
                .AsQueryable();
        }
        private string CreateAnonymousToken(int reportId)
        {
            var anonymousToken = String.Format("{0}‼{1}", reportId, DateTime.Now.AddMinutes(1));
            var value = MachineKey.Protect(Encoding.UTF8.GetBytes(anonymousToken));

            return HttpServerUtility.UrlTokenEncode(value);
        }

        private void TriggerReportDataChange()
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<ReportsHub>();
            hub.Clients.All.ReportDataChange();
        }

        private readonly KiwiContext _db = new KiwiContext();
        private readonly ModelFactory _modelFactory = new ModelFactory();
        private readonly DataFactory _dataFactory = new DataFactory();
    }
}