using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lisa.Kiwi.Web.Models
{
	public class OriginalReport : TableEntity
	{
		public OriginalReport()
		{
			Created = DateTime.UtcNow;
			UserAgent = HttpContext.Current.Request.UserAgent;
		}

		public int Id { get; set; }

		[Required(ErrorMessage = "U vergeet de locatie")]
		[Display(Name = "Locatie")]
		[StringLength(60, ErrorMessage = "De locatie mag niet langer zijn dan 60 karakters.")]
		public string Location { get; set; }

        [Display(Name = "Gebouw")]
        public string Building { get; set; }

		[StringLength(36)]
		public string Guid { get; set; }

		public string UserAgent { get; set; }
		public string Ip { get; set; }

		[Display(Name = "Beschrijving")]
		[StringLength(1000, ErrorMessage = "De beschrijving mag niet langer zijn dan 1000 karakters.")]
		public string Description { get; set; }

		public DateTime Created { get; set; }

        [Required(ErrorMessage = "U vergeet de tijd")]
        [DataType(DataType.DateTime, ErrorMessage = "Dit is geen geldige tijd. DD-MM-YYYY HH:MM:SS")]
        [Display(Name = "Tijd van incident")]
		public DateTime Time { get; set; }
        public int Offset { get; set; }

		[Display(Name = "Type melding")]
		public string Type { get; set; }
	}
}