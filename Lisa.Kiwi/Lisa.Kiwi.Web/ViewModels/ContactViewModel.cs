using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web
{
    public class ContactViewModel
    {
        [DisplayName("Naam")]
        public string Name { get; set; }

        [DisplayName("Telefoonnummer")]
        public string PhoneNumber { get; set; }

        [DisplayName("Email-Adres")]
        public string EmailAddress { get; set; }
    }
}