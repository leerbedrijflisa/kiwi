using System;
using System.Collections.Generic;
using System.Text;

namespace Lisa.Kiwi.WebApi
{
    public class Location
    {
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public string Building { get; set; }
        public string Description { get; set; }
    }
}
