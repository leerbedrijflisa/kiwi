using System.ComponentModel.DataAnnotations;
using Resources;

namespace Lisa.Kiwi.Web
{
    public class ContactRequiredViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [Display(Name = "Name", ResourceType = typeof(DisplayNames))]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [Display(Name = "PhoneNumber", ResourceType = typeof(DisplayNames))]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [Display(Name = "EmailAddress", ResourceType = typeof(DisplayNames))]
        public string EmailAddress { get; set; }
    }
}