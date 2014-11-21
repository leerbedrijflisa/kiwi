using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using Lisa.Kiwi.Data;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Lisa.Kiwi.WebApi.Controllers
{
	[Authorize]
	public class ReportController : ODataController
	{
		private readonly CloudQueue _queue = new QueueConfig().BuildQueue();
		private readonly KiwiContext _db = new KiwiContext();

		// GET odata/Report
		[EnableQuery]
		public IQueryable<Report> GetReport()
		{
			var result =
				from r in _db.Reports
				select new Report
				{
					Id = r.Id,
					Created = r.Created,
					Description = r.Description,
					Guid = r.Guid,
					Ip = r.Ip,
					Location = r.Location,
					Time = r.Time,
					Type = r.Type,
					Hidden = r.Hidden,
					UserAgent = r.UserAgent,
					Status = (from s in _db.Statuses
						where s.Report == r
						orderby s.Created descending
						select s).FirstOrDefault().Name
					// Do not give back edit token, that's not visible after creation
				};

			return result;
		}

		// GET odata/Report(5)
		[EnableQuery]
		public SingleResult<Report> GetReport([FromODataUri] int key)
		{
			var result =
				from r in _db.Reports
				where r.Id == key
				select new Report
				{
					Id = r.Id,
					Created = r.Created,
					Description = r.Description,
					Guid = r.Guid,
					Ip = r.Ip,
					Location = r.Location,
					Time = r.Time,
					Type = r.Type,
					Hidden = r.Hidden,
					UserAgent = r.UserAgent,
					Status = (from s in _db.Statuses
						where s.Report == r
						orderby s.Created descending
						select s).FirstOrDefault().Name,
					// Do not give back edit token, that's not visible after creation
				};

			return new SingleResult<Report>(result);
		}

		// PUT odata/Report(5)
		public async Task<IHttpActionResult> Put([FromODataUri] int key, Report report)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (key != report.Id)
			{
				return BadRequest();
			}

			try
			{
				var dataReport = new Data.Report
				{
					Description = report.Description,
					Created = report.Created,
					Location = report.Location,
					Time = report.Time,
					Guid = report.Guid,
					UserAgent = report.UserAgent,
					Ip = report.Ip,
					Type = report.Type,
					EditToken = report.EditToken
				};


				var dataStatus = new Data.Status
				{
					Created = report.Created,
					Name = report.Status,
					Report = dataReport
				};

				_db.Statuses.Add(dataStatus);

				_db.Reports.Add(dataReport);
				await _db.SaveChangesAsync();

				report.Id = dataReport.Id;
			}
			catch (Exception)
			{
				// TODO: Figure out possible exceptions!!
				if (!ReportExists(key))
				{
					return NotFound();
				}
				throw;
			}

			return Updated(report);
		}

		// POST odata/Report
		[AllowAnonymous]
		public async Task<IHttpActionResult> Post(Report report)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var dataReport = new Data.Report
			{
				Description = report.Description,
				Created = report.Created,
				Location = report.Location,
				Time = report.Time,
				Guid = report.Guid,
				UserAgent = report.UserAgent,
				Ip = report.Ip,
				Type = report.Type,

				// Used for adding contacts later
				EditToken = Guid.NewGuid()
			};

			_db.Reports.Add(dataReport);

			await _db.SaveChangesAsync();

			report.Id = dataReport.Id;

			return Created(report);
		}

		// PATCH odata/Report(5)
		[AcceptVerbs("PATCH", "MERGE")]
		public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<WebApi.Report> patch)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Data.Report dataReport = await _db.Reports.FindAsync(key);
            if (dataReport == null)
			{
				return NotFound();
			}

		    var changes = patch.GetChangedPropertyNames();

		    foreach (var change in changes)
		    {
		        object value;
                patch.TryGetPropertyValue(change, out value);

		        switch (change)
		        {
		            case "Description" :
		                dataReport.Description = value.ToString();
                        break;

                    case "Created" :
		                dataReport.Created = (DateTimeOffset)value;
		                break;

                    case "Location" :
		                dataReport.Location = value.ToString();
                        break;
		                
                    case "Time" :
		                dataReport.Time = (DateTimeOffset)value;
                        break;

                    case "Guid" :
		                dataReport.Guid = value.ToString();
                        break;

                    case "UserAgent" :
		                dataReport.UserAgent = value.ToString();
                        break;

                    case "Ip" :
		                dataReport.Ip = value.ToString();
                        break;

                    case "Type" :
		                dataReport.Type = (ReportType)value;
                        break;

                    case "Hidden" :
                    case "Visibility" :
		                dataReport.Hidden = (bool)value;
                        break;

					case "EditToken":
				        dataReport.EditToken = new Guid(value.ToString());
				        break;
		        }
		    }

			try
			{
			    await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				// TODO: Figure out possible exceptions!!
				if (!ReportExists(key))
				{
					return NotFound();
				}
				throw;
			}

			return Updated(patch);
		}

		// DELETE odata/Report(5)
		public async Task<IHttpActionResult> Delete([FromODataUri] int key)
		{
			Data.Report report = await _db.Reports.FindAsync(key);
			if (report == null)
			{
				return NotFound();
			}

			_db.Reports.Remove(report);
			await _db.SaveChangesAsync();

			return StatusCode(HttpStatusCode.NoContent);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool ReportExists(int key)
		{
			return _db.Reports.Count(e => e.Id == key) > 0;
		}
	}
}