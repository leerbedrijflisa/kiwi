using System;
using System.Collections.Generic;
using System.Text;

namespace Lisa.Kiwi.WebApi
{
    public class Perpetrator
    {
        public string Name { get; set; }
        public SexEnum? Sex { get; set; }
        public string SkinColor { get; set; }
        public string Clothing { get; set; }
        public int? MinimumAge { get; set; }
        public int? MaximumAge { get; set; }
        public string UniqueProperties { get; set; }
    }
}
