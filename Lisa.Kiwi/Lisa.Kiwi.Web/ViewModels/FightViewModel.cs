using System.ComponentModel;

namespace Lisa.Kiwi.Web
{
    public class FightViewModel
    {
        [DisplayName("Hoeveel personen zijn er aan het vechten?")]
        public int FighterCount { get; set; }

        [DisplayName("Zijn er wapens bij betrokken?")]
        public bool IsWeaponPresent { get; set; }

        [DisplayName("Wilt u nog iets toevoegen?")]
        public string Description { get; set; }
    }
}