using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using Lisa.Kiwi.Data.Models;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace Lisa.Kiwi.WebApi.Controllers
{
    public class StatusController : ODataController
    {
        private KiwiContext db = new KiwiContext();
        private readonly CloudQueue _queue = new QueueConfig().BuildQueue();

        // GET odata/Status
        [EnableQuery]
        public IQueryable<Status> GetStatus()
        {
            return db.Statuses;
        }

        // GET odata/Status(5)
        [EnableQuery]
        public SingleResult<Status> GetStatus([FromODataUri] int key)
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
                await _queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(status)));
            }
            catch (Exception)
            {
                if (!StatusExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
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

            await _queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(status)));

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

            Status status = await db.Statuses.FindAsync(key);
            if (status == null)
            {
                return NotFound();
            }

            patch.Patch(status);

            try
            {
                await _queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(status)));
            }
            catch (Exception)
            {
                if (!StatusExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(status);
        }

        // DELETE odata/Status(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Status status = await db.Statuses.FindAsync(key);
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
        public SingleResult<Report> GetReport([FromODataUri] int key)
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
