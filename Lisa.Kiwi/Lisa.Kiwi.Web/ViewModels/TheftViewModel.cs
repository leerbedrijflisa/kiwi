using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web
{
    public class TheftViewModel
    {
        [Required]
        public string StolenObject { get; set; }

        [Required]
        public DateTime? DateOfTheft { get; set; }
    }
}