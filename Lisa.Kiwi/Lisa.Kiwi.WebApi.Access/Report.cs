using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisa.Kiwi.WebApi.Access
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
        public ReportType Type { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }

    public enum ReportPriority
    {
        Low,
        Normal,
        High
    }

    public enum ReportType
    {
        Drugs,
        Nuisance,
        Vehicles,
        Burglary,
        Theft,
        Intimidation,
        Bullying,
        Digital,
        Fire
    }
}
