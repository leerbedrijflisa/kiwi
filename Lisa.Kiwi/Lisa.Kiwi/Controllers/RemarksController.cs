using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace Lisa.Kiwi.WebApi.Controllers
{
    public class RemarksController : ApiController
    {
        public IQueryable<Remark> Get()
        {
            var remarks = _db.Remarks
                .ToList()
                .Select(remark => _modelFactory.Create(remark))
                .AsQueryable();

            return remarks;
        }

        public IHttpActionResult Get(int id)
        {
            var remark = _db.Remarks.Find(id);

            if (remark == null)
            {
                return NotFound();
            }

            return Ok(_modelFactory.Create(remark));
        }

        public IHttpActionResult Post([FromBody] Remark remark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var remarkData = _dataFactory.Create(remark);
            var report = _db.Reports.Find(remark.Report);

            remarkData.Report = report;

            _db.Remarks.Add(remarkData);
            _db.SaveChanges();

            remark = _modelFactory.Create(remarkData);

            string url = String.Format("/remarks/{0}", remarkData.Id);
            return Created(url, remark);

        }

        public IHttpActionResult Patch(int id, [FromBody] JToken json)
        {
            var remarkData = _db.Remarks.Find(id);
            if (remarkData == null)
            {
                return NotFound();
            }

            if (json["description"] != null)
            {
                _dataFactory.Modify(remarkData, json);
                _db.SaveChanges();

                var remark = _modelFactory.Create(remarkData);
                return Ok(remark);
            }

            return StatusCode(HttpStatusCode.Forbidden);
        }

        private readonly KiwiContext _db = new KiwiContext();
        private readonly ModelFactory _modelFactory = new ModelFactory();
        private readonly DataFactory _dataFactory = new DataFactory();
    }
}

//using System;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.OData;
//using Lisa.Kiwi.Data;

//namespace Lisa.Kiwi.WebApi.Controllers
//{
//    [Authorize]
//    public class RemarkController : ODataController
//    {
//        private readonly KiwiContext db = new KiwiContext();

//        // GET odata/Remark
//        [EnableQuery]
//        public IQueryable<Remark> GetRemark()
//        {
//            var remarks = from r in db.Remarks
//                select new Remark
//                {
//                    Id = r.Id,
//                    Description = r.Description,
//                    User = r.User.UserName,
//                    Report = r.Report.Id,
//                    Created = r.Created
//                };
//            return remarks;
//        }

//        // GET odata/Remark(5)
//        [EnableQuery]
//        public SingleResult<Data.Remark> GetRemark([FromODataUri] int key)
//        {
//            return SingleResult.Create(db.Remarks.Where(remark => remark.Id == key));
//        }

//        // PUT odata/Remark(5)
//        public async Task<IHttpActionResult> Put([FromODataUri] int key, Remark remark)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (key != remark.Id)
//            {
//                return BadRequest();
//            }

//            db.Entry(remark).State = EntityState.Modified;

//            try
//            {
//                var user = (ClaimsIdentity) User.Identity;
//                var userId = user.Claims.First(c => c.Type == "id").Value;

//                db.Remarks.Add(new Data.Remark
//                {
//                    Id = remark.Id,
//                    Created = remark.Created,
//                    Description = remark.Description,
//                    Report = db.Reports.Find(remark.Report),
//                    User = db.Users.Where(u => u.Id == userId).FirstOrDefault(),
//                    //Claims.Any(c => c.Type == "is_admin" && bool.Parse(c.Value))
//                });

//                await db.SaveChangesAsync();
//            }
//            catch (Exception)
//            {
//                if (!RemarkExists(key))
//                {
//                    return NotFound();
//                }
//                throw;
//            }

//            return Updated(remark);
//        }

//        // POST odata/Remark
//        public async Task<IHttpActionResult> Post(Remark remark)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var user = (ClaimsIdentity)User.Identity;
//            var userId = user.Claims.First(c => c.Type == "id").Value;

//            var dataRemark = new Data.Remark
//            {
//                Id = remark.Id,
//                Created = remark.Created,
//                Description = remark.Description,
//                Report = db.Reports.Find(remark.Report)
//            };
//            dataRemark.User = db.Users.Where(u => u.Id == userId).FirstOrDefault();

//            db.Remarks.Add(dataRemark);

//            await db.SaveChangesAsync();

//            remark.Id = dataRemark.Id;

//            return Created(remark);
//        }

//        // PATCH odata/Remark(5)
//        [AcceptVerbs("PATCH", "MERGE")]
//        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Remark> patch)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            Data.Remark dataRemark = await db.Remarks.FindAsync(key);

//            if (dataRemark == null)
//            {
//                return NotFound();
//            }

//            var remark = new Remark
//            {
//                Id = dataRemark.Id,
//                Created = dataRemark.Created,
//                Description = dataRemark.Description,
//                User = dataRemark.User.Id,
//                Report = dataRemark.Report.Id
//            };

//            patch.Patch(remark);

//            var user = (ClaimsIdentity)User.Identity;
//            var userId = user.Claims.First(c => c.Type == "id").Value;

//            dataRemark.Id = remark.Id;
//            dataRemark.Created = remark.Created;
//            dataRemark.Description = remark.Description;
//            dataRemark.User = db.Users.Where(u => u.Id == userId).FirstOrDefault();
//            dataRemark.Report = db.Reports.Find(remark.Report);

//            try
//            {
//                await db.SaveChangesAsync();
//            }
//            catch (Exception)
//            {
//                if (!RemarkExists(key))
//                {
//                    return NotFound();
//                }
//                throw;
//            }

//            return Updated(remark);
//        }

//        // DELETE odata/Remark(5)
//        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
//        {
//            Data.Remark remark = await db.Remarks.FindAsync(key);
//            if (remark == null)
//            {
//                return NotFound();
//            }

//            db.Remarks.Remove(remark);
//            await db.SaveChangesAsync();

//            return StatusCode(HttpStatusCode.NoContent);
//        }

//        // GET odata/Remark(5)/Report
//        [EnableQuery]
//        public SingleResult<Data.Report> GetReport([FromODataUri] int key)
//        {
//            return SingleResult.Create(db.Remarks.Where(m => m.Id == key).Select(m => m.Report));
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private bool RemarkExists(int key)
//        {
//            return db.Remarks.Count(e => e.Id == key) > 0;
//        }
//    }
//}