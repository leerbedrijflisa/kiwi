using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web.Reporting.Models
{
    public class Remark
    {
        public Remark()
        {
            Created = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public OriginalReport Report { get; set; }
    }
}