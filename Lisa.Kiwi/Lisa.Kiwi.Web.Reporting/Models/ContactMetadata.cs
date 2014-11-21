using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lisa.Kiwi.Web.Reporting.Models
{
	public class ContactMetadata : TableEntity
	{
		public int Id { get; set; }

		[Display(Name = "Naam")]
		[StringLength(255, ErrorMessage = "De naam mag niet langer zijn dan 255 karakters.")]
		public string Name { get; set; }

		[Display(Name = "Telefoon")]
		[StringLength(10, ErrorMessage = "Het telefoonnummer klopt niet.")]
		public string PhoneNumber { get; set; }

		[EmailAddress(ErrorMessage = "Dit is geen geldig e-mailadres.")]
		public string Email { get; set; }

		[Display(Name = "Studentnummer")]
		public int? StudentNumber { get; set; }
        public int Report { get; set; }
	}
}