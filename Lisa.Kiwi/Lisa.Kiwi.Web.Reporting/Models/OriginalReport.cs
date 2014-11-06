using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Lisa.Kiwi.Data;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lisa.Kiwi.Web.Reporting.Models
{
	public class OriginalReport : TableEntity
	{
		public OriginalReport()
		{
			Created = DateTime.UtcNow;
			UserAgent = HttpContext.Current.Request.UserAgent;
			Priority = ReportPriority.Normaal;
		}

		public int Id { get; set; }

		[Required(ErrorMessage = "U vergeet de Locatie")]
		[Display(Name = "Locatie")]
		[StringLength(60, ErrorMessage = "De locatie mag niet langer zijn dan 60 karakters.")]
		public string Location { get; set; }

		[StringLength(36)]
		public string Guid { get; set; }

		public string UserAgent { get; set; }
		public string Ip { get; set; }

		[Display(Name = "Beschrijving")]
		[StringLength(1000, ErrorMessage = "De beschrijving mag niet langer zijn dan 1000 karakters.")]
		public string Description { get; set; }

		public DateTime Created { get; set; }

		[Required]
		public DateTime Time { get; set; }

		[Display(Name = "Type melding")]
		public ReportType Type { get; set; }

		public ReportPriority Priority { get; set; }
	}

	public enum ReportPriority
	{
		// TODO: translate
		Laag,
		Normaal,
		Hoog
	}
}