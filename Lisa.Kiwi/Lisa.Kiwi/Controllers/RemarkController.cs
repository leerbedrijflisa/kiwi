using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using Lisa.Kiwi.Data.Models;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace Lisa.Kiwi.WebApi.Controllers
{
    public class RemarkController : ODataController
    {
        private KiwiContext db = new KiwiContext();
        private readonly CloudQueue _queue = new QueueConfig().BuildQueue();

        // GET odata/Remark
        [EnableQuery]
        public IQueryable<Remark> GetRemark()
        {
            return db.Remarks;
        }

        // GET odata/Remark(5)
        [EnableQuery]
        public SingleResult<Remark> GetRemark([FromODataUri] int key)
        {
            return SingleResult.Create(db.Remarks.Where(remark => remark.Id == key));
        }

        // PUT odata/Remark(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Remark remark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != remark.Id)
            {
                return BadRequest();
            }

            try
            {
                await _queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(remark)));
            }
            catch (Exception)
            {
                if (!RemarkExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(remark);
        }

        // POST odata/Remark
        public async Task<IHttpActionResult> Post(Remark remark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(remark)));

            return Created(remark);
        }

        // PATCH odata/Remark(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Remark> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Remark remark = await db.Remarks.FindAsync(key);
            if (remark == null)
            {
                return NotFound();
            }

            patch.Patch(remark);

            try
            {
                await _queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(remark)));
            }
            catch (Exception)
            {
                if (!RemarkExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(remark);
        }

        // DELETE odata/Remark(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Remark remark = await db.Remarks.FindAsync(key);
            if (remark == null)
            {
                return NotFound();
            }

            db.Remarks.Remove(remark);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Remark(5)/Report
        [EnableQuery]
        public SingleResult<Report> GetReport([FromODataUri] int key)
        {
            return SingleResult.Create(db.Remarks.Where(m => m.Id == key).Select(m => m.Report));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RemarkExists(int key)
        {
            return db.Remarks.Count(e => e.Id == key) > 0;
        }
    }
}
