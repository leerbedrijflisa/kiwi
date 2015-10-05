using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Lisa.Kiwi.Web.Resources;

namespace Lisa.Kiwi.Web
{
    public class FightViewModel
    {
        public int FighterCount { get; set; }
        
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorMessages))]
        public bool IsWeaponPresent { get; set; }

        public string Description { get; set; }

        public IEnumerable<SelectListItem> WeaponPresent
        {
            get
            {
                return new[]
                {
                    new SelectListItem
                    {
                        Text = "Ja",
                        Value = "true"
                    },
                    new SelectListItem
                    {
                        Text = "Nee",
                        Value = "false"
                    }
                };
            }
        }
    }
}