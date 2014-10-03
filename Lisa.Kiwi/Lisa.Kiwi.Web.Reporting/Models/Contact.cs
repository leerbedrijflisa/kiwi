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
        
        [Required]
        [StringLength(50, ErrorMessage = "De naam mag niet langer zijn dan 50 karakters.")]
        public string Name { get; set; }
        
        [Display(Name = "Telefoon")]
        [StringLength(10, MinimumLength=0, ErrorMessage = "Dit is geen geldig telefoon nummer.")]
        public string PhoneNumber { get; set; }
        
        [EmailAddress(ErrorMessage = "Dit is geen geldig email adres.")]
        public string Email { get; set; }
        
        [Display(Name = "Studentennummer/OV")]
        [StringLength(10, MinimumLength=0, ErrorMessage="Dit is geen geldig studentennummer.")]
        public int StudentNumber { get; set; }
    }
}
