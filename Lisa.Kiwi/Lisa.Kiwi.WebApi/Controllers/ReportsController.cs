using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using System.Web.Security;
using Newtonsoft.Json.Linq;

namespace Lisa.Kiwi.WebApi.Controllers
{
    [Authorize]
    public class ReportsController : ApiController
    {
        [EnableQuery]
        public IHttpActionResult Get()
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;

            if (claimsIdentity.HasClaim(ClaimTypes.Role, "anonymous"))
            {
                return Unauthorized();
            }

            return Ok(GetCompleteReports());
        }

        public async Task<IHttpActionResult> Get(int? id)
        {
            var reportData = await GetCompleteReportData()
                .SingleOrDefaultAsync(r => id == r.Id);

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
            var claimsIdentity = (ClaimsIdentity) User.Identity;

            if (claimsIdentity.HasClaim(ClaimTypes.Role, "anonymous"))
            {
                var reportIdFromClaim = Int32.Parse(claimsIdentity.Claims.First(c => c.ValueType == "reportId").Value);

                if (id != reportIdFromClaim)
                {
                    return NotFound();
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