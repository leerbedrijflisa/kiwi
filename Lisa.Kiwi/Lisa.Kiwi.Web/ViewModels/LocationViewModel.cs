using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lisa.Kiwi.Web
{
    public class LocationViewModel
    {
        // TODO: use resource files for error messages and display names

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Gebouw *")]
        public string Building { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Locatie *")]
        public string Location { get; set; }
    }
}