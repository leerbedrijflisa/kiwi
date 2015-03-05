using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web
{
    public class FirstAidViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Is het slachtoffer bewusteloos?")]
        public bool IsUnconscious { get; set; }
    }
}