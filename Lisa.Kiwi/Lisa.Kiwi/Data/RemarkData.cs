using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lisa.Kiwi.WebApi
{
    [Table("Remarks")]
    public class RemarkData
    {
        public int Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public string Description { get; set; }
        //public virtual IdentityUser User { get; set; }
        public virtual ReportData Report { get; set; }
    }
}