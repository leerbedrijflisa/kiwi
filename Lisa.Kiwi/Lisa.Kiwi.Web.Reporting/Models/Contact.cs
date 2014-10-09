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
