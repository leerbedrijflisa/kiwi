using System.ComponentModel.DataAnnotations;
using Resources;

namespace Lisa.Kiwi.Web
{
    public class ContactViewModel
    {
        [Display(Name = "Name", ResourceType = typeof(DisplayNames))]
        public string Name { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(DisplayNames))]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "EmailAddress", ResourceType = typeof(DisplayNames))]
        public string EmailAddress { get; set; }
    }
}