using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.Web
{
    public class OtherViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Wat is er aan de hand? *")]
        public string Description { get; set; }
    }
}