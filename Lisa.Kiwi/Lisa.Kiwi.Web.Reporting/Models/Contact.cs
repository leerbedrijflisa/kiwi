using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.Web.Reporting.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Display(Name = "Naam")]
        [StringLength(255, ErrorMessage = "De naam mag niet langer zijn dan 255 karakters")]
        public string Name { get; set; }
        
        [Display(Name = "Telefoon")]
        [StringLength(10, ErrorMessage = "Het telefoonnummer klopt niet")]
        public string PhoneNumber { get; set; }
        
        [EmailAddress(ErrorMessage = "Dit is geen geldig email adres.")]
        public string Email { get; set; }
        
        [Display(Name = "Studentennummer/OV")]
        public int StudentNumber { get; set; }
    }
}
