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
    public class ReportSettingsController : ODataController
    {

        private KiwiContext db = new KiwiContext();

        // GET: odata/ReportSettings
        [EnableQuery]
        public IQueryable<Data.ReportSettings> GetReportSettings()
        {
            return db.ReportSettings;
        }

        // GET: odata/ReportSettings(5)
        [EnableQuery]
        public SingleResult<Data.ReportSettings> GetReportSettings([FromODataUri] int key)
        {
            return SingleResult.Create(db.ReportSettings.Where(reportSettings => reportSettings.Id == key));
        }

        // PUT: odata/ReportSettings(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, ReportSettings reportSettings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != reportSettings.Id)
            {
                return BadRequest();
            }

            db.Entry(reportSettings).State = EntityState.Modified;

            try
            {
                db.ReportSettings.Add(new Data.ReportSettings
                {
                    Id = reportSettings.Id,
                    Visible = reportSettings.Visible,
                    Report = db.Reports.Find(reportSettings.Report)
                });

                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!ReportSettingsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(reportSettings);
        }

        // POST: odata/ReportSettings
        public async Task<IHttpActionResult> Post(ReportSettings reportSettings)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReportSettings.Add(new Data.ReportSettings
            {
                Id = reportSettings.Id,
                Visible = reportSettings.Visible,
                Report = db.Reports.Find(reportSettings.Report)
            });

            await db.SaveChangesAsync();

            return Created(reportSettings);
        }

        // PATCH: odata/ReportSettings(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<ReportSettings> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Data.ReportSettings dataSettings = await db.ReportSettings.FindAsync(key);

            if (dataSettings == null)
            {
                return NotFound();
            }

            var reportSettings = new ReportSettings
            {
                Id = dataSettings.Id,
                Visible = dataSettings.Visible,
                Report = dataSettings.Id
            };

            patch.Patch(reportSettings);

            dataSettings.Id = reportSettings.Id;
            dataSettings.Visible = reportSettings.Visible;
            dataSettings.Report = db.Reports.Find(reportSettings.Report);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!ReportSettingsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(reportSettings);
        }

        // DELETE: odata/ReportSettings(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Data.ReportSettings reportSettings = await db.ReportSettings.FindAsync(key);
            if (reportSettings == null)
            {
                return NotFound();
            }

            db.ReportSettings.Remove(reportSettings);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ReportSettings(5)/Report
        [EnableQuery]
        public SingleResult<Data.Report> GetReport([FromODataUri] int key)
        {
            return SingleResult.Create(db.ReportSettings.Where(m => m.Id == key).Select(m => m.Report));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReportSettingsExists(int key)
        {
            return db.ReportSettings.Count(e => e.Id == key) > 0;
        }
    }
}
