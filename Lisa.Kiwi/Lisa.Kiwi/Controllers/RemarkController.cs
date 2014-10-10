using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using Lisa.Kiwi.Data;

namespace Lisa.Kiwi.WebApi.Controllers
{
    public class RemarkController : ODataController
    {
        private KiwiContext db = new KiwiContext();

        // GET odata/Remark
        [EnableQuery]
        public IQueryable<Data.Remark> GetRemark()
        {
            return db.Remarks;
        }

        // GET odata/Remark(5)
        [EnableQuery]
        public SingleResult<Data.Remark> GetRemark([FromODataUri] int key)
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

            db.Entry(remark).State = EntityState.Modified;

            try
            {
                db.Remarks.Add(new Data.Remark
                {
                    Id = remark.Id,
                    Created = remark.Created,
                    Description = remark.Description,
                    Report = db.Reports.Find(remark.Report)
                });

                await db.SaveChangesAsync();
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

            db.Remarks.Add(new Data.Remark
            {
                Id = remark.Id,
                Created = remark.Created,
                Description = remark.Description,
                Report = db.Reports.Find(remark.Report)
            });

            await db.SaveChangesAsync();

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

            Data.Remark dataRemark = await db.Remarks.FindAsync(key);
            
            if (dataRemark == null)
            {
                return NotFound();
            }

            var remark = new WebApi.Remark
            {
                Id = dataRemark.Id,
                Created = dataRemark.Created,
                Description = dataRemark.Description,
                Report = dataRemark.Report.Id
            };

            patch.Patch(remark);

            dataRemark.Id = remark.Id;
            dataRemark.Created = remark.Created;
            dataRemark.Description = remark.Description;
            dataRemark.Report = db.Reports.Find(remark.Report);

            try
            {
                await db.SaveChangesAsync();
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
            Data.Remark remark = await db.Remarks.FindAsync(key);
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
        public SingleResult<Data.Report> GetReport([FromODataUri] int key)
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
