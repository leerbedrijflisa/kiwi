using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web
{
    public class WeaponViewModel
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Location { get; set; }
    }
}