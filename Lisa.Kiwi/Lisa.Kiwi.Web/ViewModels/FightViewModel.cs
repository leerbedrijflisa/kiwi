using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.Web
{
    public class FightViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Hoeveel zijn er aan het vechten?")]
        public int FighterCount { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Zijn er wapens bij betrokken?")]
        public bool IsWeaponPresent { get; set; }
    }
}