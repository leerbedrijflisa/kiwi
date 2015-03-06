using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web
{
    public class DrugsViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Word er gedeald of gebruikt?")]
        public string Action { get; set; }

        [DisplayName("Overige informatie")]
        public string Description { get; set; }
    }
}