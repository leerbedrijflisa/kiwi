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