using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lisa.Kiwi.Web
{
    public class FightViewModel
    {
        [DisplayName("Hoeveel personen zijn er aan het vechten?*")]
        public int FighterCount { get; set; }

        [Required]
        [DisplayName("Zijn er wapens bij betrokken?")]
        public bool IsWeaponPresent { get; set; }

        [DisplayName("Wilt u nog iets toevoegen?")]
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
                    },
                };
            }
        }
    }
}