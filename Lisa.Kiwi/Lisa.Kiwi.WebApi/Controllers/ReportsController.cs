using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using Newtonsoft.Json.Linq;

namespace Lisa.Kiwi.WebApi.Controllers
{
    [Authorize]
	public class ReportsController : ApiController
	{
		[EnableQuery]
		public IQueryable<Report> Get()
		{
           return GetCompleteReports();
		}

        [AllowAnonymous]
        public async Task<IHttpActionResult> Get(int? id)
        {
            var reportData = await GetCompleteReportDatas()
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
            var url = Url.Route("DefaultApi", new { controller = "reports", id = reportData.Id });
            return Created(url, reportJson);
        }

        [AllowAnonymous]
        public async Task<IHttpActionResult> Patch(int? id, [FromBody] JToken json)
        {
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

        private IQueryable<ReportData> GetCompleteReportDatas()
        {
            return _db.Reports
                .Include("Location")
                .Include("Perpetrator")
                .Include("Contact")
                .Include("Vehicle");
        }

        private IQueryable<Report> GetCompleteReports()
        {
            return GetCompleteReportDatas()
                .ToList()
                .Select(reportData => _modelFactory.Create(reportData))
                .AsQueryable();
        } 

        private readonly KiwiContext _db = new KiwiContext();
        private readonly ModelFactory _modelFactory = new ModelFactory();
        private readonly DataFactory _dataFactory = new DataFactory();

	}
}