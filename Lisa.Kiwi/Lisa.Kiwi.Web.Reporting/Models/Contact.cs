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
        [StringLength(50, ErrorMessage = "De naam mag niet langer zijn dan 50 karakters."), Required]
        public string Name { get; set; }
        [Display(Name = "Telefoon"), Phone(ErrorMessage = "Dit is geen geldig telefoon nummer.")]
        public int Phone { get; set; }
        [EmailAddress(ErrorMessage = "Dit is geen geldig email adres.")]
        public string Email { get; set; }
        [Display(Name = "Studentennummer/OV")]
        public int StudentNo { get; set; }
    }
}
