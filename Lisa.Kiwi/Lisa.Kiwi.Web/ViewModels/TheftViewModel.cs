using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.Web
{
    public class TheftViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Wat is er gestolen?")]
        public string StolenObject { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DataType(DataType.Date)]
        [DisplayName("Op welke datum is het gestolen?")]
        public DateTime DateOfTheft { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DataType(DataType.Time)]
        [DisplayName("Hoe laat is het gestolen?")]
        public DateTime TimeOfTheft { get; set; }

        [DisplayName("Wilt u nog iets toevoegen?")]
        public string Description { get; set; }
    }
}