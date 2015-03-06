using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lisa.Kiwi.WebApi
{
    [Table("Reports")]
    public class ReportData
    {
        public ReportData()
        {
            StatusChanges = new List<StatusChangeData>();
            Created = DateTimeOffset.Now;
            IsVisible = true;
        }

        public int Id { get; set; }

        public string Category { get; set; }
        public bool IsVisible { get; set; }

        public DateTimeOffset Created { get; set; }

        public int Status { get; set; }

        public string Guid { get; set; }

        public string Description { get; set; }
        
        public bool? IsUnconscious { get; set; }

        public Guid EditToken { get; set; }

        
        public virtual ICollection<StatusChangeData> StatusChanges { get; set; }
        public virtual LocationData Location { get; set; }
        public virtual PerpetratorData Perpetrator { get; set; }
        public virtual ContactData Contact { get; set; }
        public virtual VehicleData Vehicle { get; set; }

    }
}