using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.WebApi.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public int Report { get; set; }
    }
}