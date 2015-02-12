using System;
using System.Linq;
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

        public IHttpActionResult Get(int id)
        {
            var vehicle = _db.Vehicles.Find(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(_modelFactory.Create(vehicle));
        }

        public IHttpActionResult Post([FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vehicleData = _dataFactory.Create(vehicle);
            var reportData = _db.Reports.Find(vehicle.Report);

            vehicleData.Report = reportData;

            _db.Vehicles.Add(vehicleData);
            _db.SaveChanges();

            vehicle = _modelFactory.Create(vehicleData);

            string url = String.Format("/vehicles/{0}", vehicleData.Id);
            return Created(url, vehicle);
        }

        private readonly KiwiContext _db = new KiwiContext();
        private readonly ModelFactory _modelFactory = new ModelFactory();
        private readonly DataFactory _dataFactory = new DataFactory();
    }

}