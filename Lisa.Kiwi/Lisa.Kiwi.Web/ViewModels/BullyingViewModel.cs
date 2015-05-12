using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.Web
{
    public class BullyingViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Wat is er aan de hand? *")]
        public string Description { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Wie wordt er gepest? *")]
        public string VictimName { get; set; }

        public bool HasVictim { get; set; }
        public bool HasPerpetrator { get; set; }
    }
}