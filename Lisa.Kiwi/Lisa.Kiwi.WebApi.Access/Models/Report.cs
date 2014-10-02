using System;

namespace Lisa.Kiwi.WebApi.Access.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public string Guid { get; set; }
        public string UserAgent { get; set; }
        public string Ip { get; set; }
    }
}
