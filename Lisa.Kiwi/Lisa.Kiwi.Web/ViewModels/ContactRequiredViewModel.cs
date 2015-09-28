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

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [Display(Name = "EmailAddress", ResourceType = typeof(DisplayNames))]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
    }
}