using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Lisa.Kiwi.Data;

namespace Lisa.Kiwi.Web.Dashboard.Models
{
    public class ReportMetadata
    {
        [Display(Name = "ReportDescription", ResourceType = typeof(Resources.DisplayNames))]
        public string Description { get; set; }

        [Display(Name = "ReportCreated", ResourceType = typeof(Resources.DisplayNames))]
        public DateTimeOffset Created { get; set; }

        [Display(Name = "ReportLocation", ResourceType = typeof(Resources.DisplayNames))]
        public string Location { get; set; }

        [Display(Name = "ReportTime", ResourceType = typeof(Resources.DisplayNames))]
        public DateTimeOffset Time { get; set; }

        [Display(Name = "ReportUserAgent", ResourceType = typeof(Resources.DisplayNames))]
        public string UserAgent { get; set; }

        [MaxLength(45)]
        [Display(Name = "ReportIp", ResourceType = typeof(Resources.DisplayNames))]
        public string Ip { get; set; }

        [Display(Name = "ReportType", ResourceType = typeof(Resources.DisplayNames))]
        public ReportType Type { get; set; }

        [Display(Name = "ReportContacts", ResourceType = typeof(Resources.DisplayNames))]
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}