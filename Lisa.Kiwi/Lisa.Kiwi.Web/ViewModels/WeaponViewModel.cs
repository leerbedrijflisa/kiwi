using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web
{
    public class WeaponViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Wat voor wapen is het?")]
        public string Type { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Waar is het wapen?")]
        public string Location { get; set; }
    }
}