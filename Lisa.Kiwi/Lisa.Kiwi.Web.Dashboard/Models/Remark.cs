using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web.Dashboard.Models
{
    public class Remark
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}