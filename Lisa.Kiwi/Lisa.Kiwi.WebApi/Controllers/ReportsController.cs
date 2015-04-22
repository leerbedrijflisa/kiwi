using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;

namespace Lisa.Kiwi.WebApi
{
    [Authorize]
    public class ReportsController : ApiController
    {
        [Authorize(Roles = "dashboardUser")]
        public IHttpActionResult Get()
        {
            return Ok(GetCompleteReports());
        }

        public IHttpActionResult Get(int? id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            
            if (claimsIdentity.HasClaim(ClaimTypes.Role, "anonymous"))
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
        public async Task<IHttpActionResult> Post([FromBody] JToken json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var reportData = _dataFactory.Create(json);

            _db.Reports.Add(reportData);
            await _db.SaveChangesAsync();

            var reportJson = _modelFactory.Create(reportData);

            reportJson.AnonymousToken = CreateAnonymousToken(reportJson.Id);

            var url = Url.Route("DefaultApi", new { controller = "reports", id = reportData.Id });
            return Created(url, reportJson);
        }

       
        public async Task<IHttpActionResult> Patch(int? id, [FromBody] JToken json)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;

            if (claimsIdentity.HasClaim(ClaimTypes.Role, "anonymous"))
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

            _dataFactory.Modify(reportData, json);

            await _db.SaveChangesAsync();

            var report = _modelFactory.Create(reportData);
            return Ok(report);
        }

        private IQueryable<ReportData> GetCompleteReportData()
        {
            return _db.Reports
                .Include("Location")
                .Include("Perpetrator")
                .Include("Contact")
                .Include("Vehicle");
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

        private readonly KiwiContext _db = new KiwiContext();
        private readonly ModelFactory _modelFactory = new ModelFactory();
        private readonly DataFactory _dataFactory = new DataFactory();
    }
}