using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lisa.Kiwi.WebApi
{
    public class ReportData
    {
        public ReportData()
        {
            StatusChanges = new List<StatusChangeData>();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public DateTimeOffset Created { get; set; }
        public string Location { get; set; }
        public DateTimeOffset Time { get; set; }          // should we remove this?
        public string Guid { get; set; }            // what does this do?
        public string UserAgent { get; set; }       // should we remove this?

        public string Ip { get; set; }              // should we remove this?

        public string Type { get; set; }            // rename to Category

        public Guid EditToken { get; set; }         // has to do with security

        public virtual ICollection<StatusChangeData> StatusChanges { get; set; }
    }
}