using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lisa.Kiwi.WebApi
{
    [Table("StatusChanges")]
    public class StatusChangeData
    {
        // Information about the change.
        public int Id { get; set; }
        public DateTimeOffset Created { get; set; }
        //public virtual IdentityUser User { get; set; }

        // Information about the status.
        public string Status { get; set; }
        public bool IsVisible { get; set; }
    }
}