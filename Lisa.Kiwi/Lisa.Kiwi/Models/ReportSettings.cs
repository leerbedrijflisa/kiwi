using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.WebApi
{
    public class ReportSettings
    {
        public int Id { get; set; }
        public int Report { get; set; }
        public bool Visible { get; set; }
    }
}