using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using Lisa.Kiwi.Data;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Lisa.Kiwi.WebApi.Controllers
{
	public class StatusController : ODataController
	{
		private readonly CloudQueue _queue = new QueueConfig().BuildQueue();
		private readonly KiwiContext db = new KiwiContext();

		// GET odata/Status
		[EnableQuery]
		public IQueryable<Status> GetStatus()
		{
			var result = from s in db.Statuses
				select new Status
				{
					Id = s.Id,
					Name = s.Name,
					Created = s.Created,
					Report = s.Report.Id
				};
			return result;
		}

		// GET odata/Status(5)
		[EnableQuery]
		public SingleResult<Data.Status> GetStatus([FromODataUri] int key)
		{
			return SingleResult.Create(db.Statuses.Where(status => status.Id == key));
		}

		// PUT odata/Status(5)
		public async Task<IHttpActionResult> Put([FromODataUri] int key, Status status)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (key != status.Id)
			{
				return BadRequest();
			}

			db.Entry(status).State = EntityState.Modified;

			try
			{
				db.Statuses.Add(new Data.Status
				{
					Id = status.Id,
					Name = status.Name,
					Created = status.Created,
					Report = db.Reports.Find(status.Report)
				});

				await db.SaveChangesAsync();
			}
			catch (Exception)
			{
				if (!StatusExists(key))
				{
					return NotFound();
				}
				throw;
			}

			return Updated(status);
		}

		// POST odata/Status
		public async Task<IHttpActionResult> Post(Status status)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Statuses.Add(new Data.Status
			{
				Id = status.Id,
				Name = status.Name,
				Created = status.Created,
				Report = db.Reports.Find(status.Report)
			});

			await db.SaveChangesAsync();

			return Created(status);
		}

		// PATCH odata/Status(5)
		[AcceptVerbs("PATCH", "MERGE")]
		public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Status> patch)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			//map Data.Status to WebApi.Status
			Data.Status dataStatus = await db.Statuses.FindAsync(key);

			if (dataStatus == null)
			{
				return NotFound();
			}

			var status = new Status
			{
				Id = dataStatus.Id,
				Created = dataStatus.Created,
				Name = dataStatus.Name,
				Report = dataStatus.Report.Id
			};

			patch.Patch(status);

			dataStatus.Id = status.Id;
			dataStatus.Created = status.Created;
			dataStatus.Name = status.Name;
			dataStatus.Report = db.Reports.Find(status.Report);

			try
			{
				await db.SaveChangesAsync();
			}
			catch (Exception)
			{
				if (!StatusExists(key))
				{
					return NotFound();
				}
				throw;
			}

			return Updated(status);
		}

		// DELETE odata/Status(5)
		public async Task<IHttpActionResult> Delete([FromODataUri] int key)
		{
			Data.Status status = await db.Statuses.FindAsync(key);
			if (status == null)
			{
				return NotFound();
			}

			db.Statuses.Remove(status);
			await db.SaveChangesAsync();

			return StatusCode(HttpStatusCode.NoContent);
		}

		// GET odata/Status(5)/Report
		[EnableQuery]
		public SingleResult<Data.Report> GetReport([FromODataUri] int key)
		{
			return SingleResult.Create(db.Statuses.Where(m => m.Id == key).Select(m => m.Report));
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool StatusExists(int key)
		{
			return db.Statuses.Count(e => e.Id == key) > 0;
		}
	}
}