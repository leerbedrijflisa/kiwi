using System;
using Lisa.Kiwi.Data.Models;

namespace Lisa.Kiwi.WebApi.Models
{
    public class Report
    {
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public string Guid { get; set; }
        public string UserAgent { get; set; }
        public string Ip { get; set; }
        public StatusName Status { get; set; }
    }
}