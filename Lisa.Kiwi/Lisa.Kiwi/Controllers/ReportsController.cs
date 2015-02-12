using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using Newtonsoft.Json.Linq;

namespace Lisa.Kiwi.WebApi.Controllers
{
    //[System.Web.Http.Authorize]
	public class ReportsController : ApiController
	{
		// GET Report
		[EnableQuery]
		public IQueryable<Report> Get()
		{
            var reports = _db.Reports.Include("StatusChanges")
                .ToList()
                .Select(reportData => _modelFactory.Create(reportData))
                .AsQueryable();

            return reports;
		}

        public IHttpActionResult Get(int? id)
        {
            var reportData = _db.Reports.Find(id);
            if (reportData == null)
            {
                return NotFound();
            }

            var report = _modelFactory.Create(reportData);
            return Ok(report);
        }

        public IHttpActionResult Post([FromBody] Report report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var reportData = _dataFactory.Create(report);
            var statusChangeData = new StatusChangeData
            {
                Created = DateTimeOffset.Now,
                IsVisible = report.IsVisible,
                Status = report.CurrentStatus.ToString()
            };
            reportData.StatusChanges.Add(statusChangeData);

            _db.Reports.Add(reportData);
            _db.SaveChanges();

            report = _modelFactory.Create(reportData);
            var url = Url.Route("DefaultApi", new { controller = "reports", id = reportData.Id });
            return Created(url, report);
        }

        public IHttpActionResult Patch(int? id, [FromBody] JToken json)
        {
            var reportData = _db.Reports.Find(id);
            if (reportData == null)
            {
                return NotFound();
            }

            // You are allowed to change status information only.
            if (json["isVisible"] != null || json["currentStatus"] != null)
            {
                _dataFactory.Modify(reportData, json);
                _db.SaveChanges();

                var report = _modelFactory.Create(reportData);
                return Ok(report);
            }

            return StatusCode(HttpStatusCode.Forbidden);
        }

        private readonly KiwiContext _db = new KiwiContext();
        private readonly ModelFactory _modelFactory = new ModelFactory();
        private readonly DataFactory _dataFactory = new DataFactory();

        
        //// PUT odata/Report(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] int key, Report report)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (key != report.Id)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        var dataReport = new Data.Report
        //        {
        //            Description = report.Description,
        //            Created = report.Created,
        //            Location = report.Location,
        //            Time = report.Time,
        //            Guid = report.Guid,
        //            UserAgent = report.UserAgent,
        //            Ip = report.Ip,
        //            Type = report.Type,
        //            EditToken = report.EditToken
        //        };


        //        var dataStatus = new Data.Status
        //        {
        //            Created = report.Created,
        //            Name = report.Status,
        //            Report = dataReport
        //        };

        //        _db.Statuses.Add(dataStatus);

        //        _db.Reports.Add(dataReport);
        //        await _db.SaveChangesAsync();

        //        report.Id = dataReport.Id;
        //    }
        //    catch (Exception)
        //    {
        //        // TODO: Figure out possible exceptions!!
        //        if (!ReportExists(key))
        //        {
        //            return NotFound();
        //        }
        //        throw;
        //    }

        //    return Updated(report);
        //}

        //// POST odata/Report
        //[AllowAnonymous]
        //public async Task<IHttpActionResult> Post(Report report)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var dataReport = new Data.Report
        //    {
        //        Description = report.Description,
        //        Created = report.Created,
        //        Location = report.Location,
        //        Time = report.Time,
        //        Guid = report.Guid,
        //        UserAgent = report.UserAgent,
        //        Ip = report.Ip,
        //        Type = report.Type,

        //        // Used for adding contacts later
        //        EditToken = Guid.NewGuid()
        //    };

        //    _db.Reports.Add(dataReport);
        //    await _db.SaveChangesAsync();

        //    report.Id = dataReport.Id;
        //    var status = new Data.Status
        //    {
        //        Name = StatusName.Open,
        //        Created = report.Created,
        //        Visible = true,
        //        Report = dataReport
        //    };

        //    _db.Statuses.Add(status);
        //    await _db.SaveChangesAsync();

        //    var context = GlobalHost.ConnectionManager.GetHubContext<ReportHub>();
        //    context.Clients.All.newReportItem(report.Id, report.Created.ToString("yyyy-MM-dd HH:mm:ss"), report.Time.ToString("yyyy-MM-dd HH:mm:ss"), report.Type, report.Location, report.Description, report.Status.ToString());

        //    return Created(report);
        //}

        //// PATCH odata/Report(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<WebApi.Report> patch)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Data.Report dataReport = await _db.Reports.FindAsync(key);
        //    if (dataReport == null)
        //    {
        //        return NotFound();
        //    }

        //    var changes = patch.GetChangedPropertyNames();

        //    foreach (var change in changes)
        //    {
        //        object value;
        //        patch.TryGetPropertyValue(change, out value);

        //        switch (change)
        //        {
        //            case "Description" :
        //                dataReport.Description = value.ToString();
        //                break;

        //            case "Created" :
        //                dataReport.Created = (DateTimeOffset)value;
        //                break;

        //            case "Location" :
        //                dataReport.Location = value.ToString();
        //                break;
		                
        //            case "Time" :
        //                dataReport.Time = (DateTimeOffset)value;
        //                break;

        //            case "Guid" :
        //                dataReport.Guid = value.ToString();
        //                break;

        //            case "UserAgent" :
        //                dataReport.UserAgent = value.ToString();
        //                break;

        //            case "Ip" :
        //                dataReport.Ip = value.ToString();
        //                break;

        //            case "Type" :
        //                dataReport.Type = value.ToString();
        //                break;

        //            case "Hidden" :
        //            case "Visibility" :
        //                dataReport.Hidden = (bool)value;
        //                break;

        //            case "EditToken":
        //                dataReport.EditToken = new Guid(value.ToString());
        //                break;
        //        }
        //    }

        //    try
        //    {
        //        await _db.SaveChangesAsync();
        //    }
        //    catch (Exception)
        //    {
        //        // TODO: Figure out possible exceptions!!
        //        if (!ReportExists(key))
        //        {
        //            return NotFound();
        //        }
        //        throw;
        //    }

        //    return Updated(patch);
        //}

        //// DELETE odata/Report(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        //{
        //    Data.Report report = await _db.Reports.FindAsync(key);
        //    if (report == null)
        //    {
        //        return NotFound();
        //    }

        //    _db.Reports.Remove(report);
        //    await _db.SaveChangesAsync();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool ReportExists(int key)
        //{
        //    return _db.Reports.Count(e => e.Id == key) > 0;
        //}
	}
}