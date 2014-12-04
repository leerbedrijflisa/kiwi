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
		[RegularExpression(@"[0-9()+\- ]+", ErrorMessage = "Het telefoonnummer mag alleen bestaan uit nummers, +, -, (, ) en spaties.")]
		public string PhoneNumber { get; set; }

		[EmailAddress(ErrorMessage = "Dit is geen geldig e-mailadres.")]
		public string Email { get; set; }

		[Display(Name = "Studentnummer")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Een studentnummer bestaat enkel uit cijfers.")]
        public int? StudentNumber { get; set; }
        public int Report { get; set; }
	}
}