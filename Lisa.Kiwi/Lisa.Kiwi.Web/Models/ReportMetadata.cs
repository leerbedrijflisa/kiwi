using System;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace Lisa.Kiwi.Web
{
    public class ReportMetadata
    {
        [Display(Name = "ReportDescription", ResourceType = typeof(DisplayNames))]
        public string Description { get; set; }

        [Display(Name = "ReportCreated", ResourceType = typeof(DisplayNames))]
        public DateTimeOffset Created { get; set; }

        [Display(Name = "ReportLocation", ResourceType = typeof(DisplayNames))]
        public string Location { get; set; }

        [Display(Name = "ReportTime", ResourceType = typeof(DisplayNames))]
        public DateTimeOffset Time { get; set; }

        [Display(Name = "ReportUserAgent", ResourceType = typeof(DisplayNames))]
        public string UserAgent { get; set; }

        [MaxLength(45)]
        [Display(Name = "ReportIp", ResourceType = typeof(DisplayNames))]
        public string Ip { get; set; }

        [Display(Name = "ReportType", ResourceType = typeof(DisplayNames))]
        public string Type { get; set; }
    }
}