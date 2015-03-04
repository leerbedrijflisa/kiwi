using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace Lisa.Kiwi.WebApi.Controllers
{
    [Authorize]
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

        public async Task<IHttpActionResult> Get(int id)
        {
            var remark = await _db.Remarks.FindAsync(id);

            if (remark == null)
            {
                return NotFound();
            }

            return Ok(_modelFactory.Create(remark));
        }

        public async Task<IHttpActionResult> Post([FromBody] Remark remark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var remarkData = _dataFactory.Create(remark);
            var report = await _db.Reports.FindAsync(remark.Report);

            remarkData.Report = report;

            _db.Remarks.Add(remarkData);
            await _db.SaveChangesAsync();

            remark = _modelFactory.Create(remarkData);

            var url = Url.Route("DefaultApi", new { controller = "remarks", id = remarkData.Id });
            return Created(url, remark);

        }

        public async Task<IHttpActionResult> Patch(int id, [FromBody] JToken json)
        {
            var remarkData = await _db.Remarks.FindAsync(id);
            if (remarkData == null)
            {
                return NotFound();
            }

            if (json["description"] != null)
            {
                _dataFactory.Modify(remarkData, json);
                await _db.SaveChangesAsync();

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