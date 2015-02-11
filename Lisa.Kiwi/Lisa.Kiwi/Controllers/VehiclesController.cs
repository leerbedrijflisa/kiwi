//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Http.ModelBinding;
//using System.Web.Http.OData;
//using System.Web.Http.OData.Routing;
//using Lisa.Kiwi.Data;

//namespace Lisa.Kiwi.WebApi.Controllers
//{
//    public class VehiclesController : ODataController
//    {
//        private KiwiContext db = new KiwiContext();

//        // GET: odata/Vehicles
//        [EnableQuery]
//        public IQueryable<Vehicle> GetVehicles()
//        {
//            var result = from v in db.Vehicles
//                         select new Vehicle
//                         {
//                             Id = v.Id,
//                             Brand = v.Brand,
//                             Color = v.Color,
//                             LicensePlate = v.LicensePlate,
//                             Model = v.Model,
//                             Report = v.Report.Id
//                         };
//            return result;
//        }

//        // GET: odata/Vehicles(5)
//        [EnableQuery]
//        public SingleResult<Data.Vehicle> GetVehicle([FromODataUri] int key)
//        {
//            return SingleResult.Create(db.Vehicles.Where(vehicle => vehicle.Id == key));
//        }

//        // PUT: odata/Vehicles(5)
//        public async Task<IHttpActionResult> Put([FromODataUri] int key, Vehicle patch)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (key != patch.Id)
//            {
//                return BadRequest();
//            }

//            db.Entry(patch).State = EntityState.Modified;

//            try
//            {
//                db.Vehicles.Add(new Data.Vehicle
//                {
//                    Id = patch.Id,
//                    Brand = patch.Brand,
//                    Color = patch.Color,
//                    LicensePlate = patch.LicensePlate,
//                    Model = patch.Model,
//                    Report = db.Reports.Find(patch.Report)
//                });

//                await db.SaveChangesAsync();
//            }
//            catch (Exception)
//            {
//                if (!VehicleExists(key))
//                {
//                    return NotFound();
//                }
//                throw;
//            }

//            return Updated(patch);
//        }

//        // POST: odata/Vehicles
//        public async Task<IHttpActionResult> Post(Vehicle vehicle)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            db.Vehicles.Add(new Data.Vehicle
//            {
//                Id = vehicle.Id,
//                Brand = vehicle.Brand,
//                Color = vehicle.Color,
//                LicensePlate = vehicle.LicensePlate,
//                Model = vehicle.Model,
//                Report = db.Reports.Find(vehicle.Report)
//            });
//            await db.SaveChangesAsync();

//            return Created(vehicle);
//        }

//        // PATCH: odata/Vehicles(5)
//        [AcceptVerbs("PATCH", "MERGE")]
//        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Vehicle> patch)
//        {
//            Validate(patch.GetEntity());

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            Data.Vehicle dataVehicle = await db.Vehicles.FindAsync(key);
//            if (dataVehicle == null)
//            {
//                return NotFound();
//            }

//            var vehicle = new Vehicle
//            {
//                Id = dataVehicle.Id,
//                Brand = dataVehicle.Brand,
//                Color = dataVehicle.Color,
//                LicensePlate = dataVehicle.LicensePlate,
//                Model = dataVehicle.Model,
//                Report = dataVehicle.Report.Id
//            };

//            patch.Patch(vehicle);

//            try
//            {
//                await db.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!VehicleExists(key))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return Updated(vehicle);
//        }

//        // DELETE: odata/Vehicles(5)
//        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
//        {
//            Data.Vehicle vehicle = await db.Vehicles.FindAsync(key);
//            if (vehicle == null)
//            {
//                return NotFound();
//            }

//            db.Vehicles.Remove(vehicle);
//            await db.SaveChangesAsync();

//            return StatusCode(HttpStatusCode.NoContent);
//        }

//        // GET: odata/Vehicles(5)/Report
//        [EnableQuery]
//        public SingleResult<Data.Report> GetReport([FromODataUri] int key)
//        {
//            return SingleResult.Create(db.Vehicles.Where(m => m.Id == key).Select(m => m.Report));
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private bool VehicleExists(int key)
//        {
//            return db.Vehicles.Count(e => e.Id == key) > 0;
//        }
//    }
//}
