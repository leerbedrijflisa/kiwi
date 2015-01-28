using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisa.Kiwi.Data
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public virtual Report Report { get; set; }
    }
}
