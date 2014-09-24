using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using Lisa.Kiwi.Data.Models;

namespace Lisa.Kiwi.WebApi.Controllers
{
    public class StatusController : ODataController
    {
        private KiwiContext db = new KiwiContext();

        // GET odata/Status
        [EnableQuery]
        public IQueryable<Status> GetStatus()
        {
            return db.Status;
        }

        // GET odata/Status(5)
        [EnableQuery]
        public SingleResult<Status> GetStatus([FromODataUri] int key)
        {
            return SingleResult.Create(db.Status.Where(status => status.Id == key));
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
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
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

            db.Status.Add(status);
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

            Status status = await db.Status.FindAsync(key);
            if (status == null)
            {
                return NotFound();
            }

            patch.Patch(status);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
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
            Status status = await db.Status.FindAsync(key);
            if (status == null)
            {
                return NotFound();
            }

            db.Status.Remove(status);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Status(5)/Report
        [EnableQuery]
        public SingleResult<Report> GetReport([FromODataUri] int key)
        {
            return SingleResult.Create(db.Status.Where(m => m.Id == key).Select(m => m.Report));
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
            return db.Status.Count(e => e.Id == key) > 0;
        }
    }
}
