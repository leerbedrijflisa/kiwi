using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web.ViewModels
{
    public class FightViewModel
    {
        [Required]
        public int? FighterCount { get; set; }

        [Required]
        public bool? IsWeaponPresent { get; set; }
    }
}