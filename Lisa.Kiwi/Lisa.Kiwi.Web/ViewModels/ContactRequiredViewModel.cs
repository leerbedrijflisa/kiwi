using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web
{
    public class ContactRequiredViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Naam")]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Telefoonnummer")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Email-Adres")]
        public string EmailAddress { get; set; }
    }
}