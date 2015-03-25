using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.Web
{
    public class ContactRequiredViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Naam")]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("Telefoonnummer")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DisplayName("E-mail Address")]
        public string EmailAddress { get; set; }
    }
}