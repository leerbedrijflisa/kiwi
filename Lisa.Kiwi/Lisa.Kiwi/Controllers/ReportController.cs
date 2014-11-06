using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using Lisa.Kiwi.Data;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace Lisa.Kiwi.WebApi.Controllers
{
	public class ReportController : ODataController
	{
		private readonly CloudQueue _queue = new QueueConfig().BuildQueue();
		private readonly KiwiContext db = new KiwiContext();

		// GET odata/Report
		[EnableQuery]
		public IQueryable<Report> GetReport()
		{
			var result =
				from r in db.Reports
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
					Status = (from s in db.Statuses
						where s.Report == r
						orderby s.Created descending
						select s).FirstOrDefault().Name,
					Contacts = r.Contacts
				};

			return result;
		}

		// GET odata/Report(5)
		[EnableQuery]
		public SingleResult<Report> GetReport([FromODataUri] int key)
		{
			var result =
				from r in db.Reports
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
					Status = (from s in db.Statuses
						where s.Report == r
						orderby s.Created descending
						select s).FirstOrDefault().Name,
					Contacts = r.Contacts
				};

			return new SingleResult<Report>(result);
			;
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
					Type = report.Type
				};


				var dataStatus = new Data.Status
				{
					Created = report.Created,
					Name = report.Status,
					Report = dataReport
				};

				db.Statuses.Add(dataStatus);

				db.Reports.Add(dataReport);
				await db.SaveChangesAsync();

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
				Type = report.Type
			};

			var dataStatus = new Data.Status
			{
				Created = report.Created,
				Name = report.Status,
				Report = dataReport
			};

			db.Statuses.Add(dataStatus);

			db.Reports.Add(dataReport);

			await db.SaveChangesAsync();

			report.Id = dataReport.Id;

			return Created(report);
		}

		// PATCH odata/Report(5)
		[AcceptVerbs("PATCH", "MERGE")]
		public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Data.Report> patch)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Data.Report report = await db.Reports.FindAsync(key);
			if (report == null)
			{
				return NotFound();
			}

			patch.Patch(report);

			try
			{
				await _queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(report)));
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

		// DELETE odata/Report(5)
		public async Task<IHttpActionResult> Delete([FromODataUri] int key)
		{
			Data.Report report = await db.Reports.FindAsync(key);
			if (report == null)
			{
				return NotFound();
			}

			db.Reports.Remove(report);
			await db.SaveChangesAsync();

			return StatusCode(HttpStatusCode.NoContent);
		}

		// GET odata/Report(5)/Contact
		[EnableQuery]
		public IQueryable<Contact> GetContact([FromODataUri] int key)
		{
			return db.Reports.Where(m => m.Id == key).SelectMany(m => m.Contacts);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool ReportExists(int key)
		{
			return db.Reports.Count(e => e.Id == key) > 0;
		}
	}
}