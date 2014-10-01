using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web.Dashboard.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Guid { get; set; }
        public string UserAgent { get; set; }
        public string Ip { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Time { get; set; }
        public ReportPriority Priority { get; set; }

    }
}