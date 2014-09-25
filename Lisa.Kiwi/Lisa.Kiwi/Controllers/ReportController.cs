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
    public class ReportController : ODataController
    {
        private KiwiContext db = new KiwiContext();

        // GET odata/Report
        [Queryable]
        public IQueryable<Report> GetReport()
        {
            return db.Reports;
        }

        // GET odata/Report(5)
        [Queryable]
        public SingleResult<Report> GetReport([FromODataUri] int key)
        {
            return SingleResult.Create(db.Reports.Where(report => report.Id == key));
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

            db.Entry(report).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
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

            db.Reports.Add(report);
            await db.SaveChangesAsync();

            return Created(report);
        }

        // PATCH odata/Report(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Report> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Report report = await db.Reports.FindAsync(key);
            if (report == null)
            {
                return NotFound();
            }

            patch.Patch(report);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(report);
        }

        // DELETE odata/Report(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Report report = await db.Reports.FindAsync(key);
            if (report == null)
            {
                return NotFound();
            }

            db.Reports.Remove(report);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Report(5)/Contact
        [Queryable]
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
