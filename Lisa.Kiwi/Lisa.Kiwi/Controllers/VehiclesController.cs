using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Lisa.Kiwi.WebApi.Controllers
{
    public class VehiclesController : ApiController
    {
        public IQueryable<Vehicle> Get()
        {
            var vehicles = _db.Vehicles
                .ToList()
                .Select(vehicle => _modelFactory.Create(vehicle))
                .AsQueryable();

            return vehicles;
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var vehicle = await _db.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(_modelFactory.Create(vehicle));
        }

        //public async Task<IHttpActionResult> Post([FromBody] Vehicle vehicle)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    var vehicleData = _dataFactory.Create(vehicle);
        //    var reportData = await _db.Reports.FindAsync(vehicle.Report);

        //    vehicleData.Report = reportData;

        //    _db.Vehicles.Add(vehicleData);
        //    await _db.SaveChangesAsync();

        //    vehicle = _modelFactory.Create(vehicleData);

        //    var url = Url.Route("DefaultApi", new { controller = "vehicles", id = vehicleData.Id });
        //    return Created(url, vehicle);
        //}

        private readonly KiwiContext _db = new KiwiContext();
        private readonly ModelFactory _modelFactory = new ModelFactory();
        private readonly DataFactory _dataFactory = new DataFactory();
    }

}