using System.ComponentModel.DataAnnotations;
using ExpressiveAnnotations.Attributes;
using Lisa.Kiwi.Web.Resources;

namespace Lisa.Kiwi.Web
{
    public class ContactViewModel
    {
        public string Name { get; set; }

        [RequiredIf("Enabled && IsNullOrWhiteSpace(EmailAddress)", ErrorMessageResourceName = "PhoneOrEmail", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(4, ErrorMessageResourceName = "PhoneNumber", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string PhoneNumber { get; set; }

        [RequiredIf("Enabled && IsNullOrWhiteSpace(PhoneNumber)", ErrorMessageResourceName = "PhoneOrEmail", ErrorMessageResourceType = typeof(ErrorMessages))]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceName = "Email", ErrorMessageResourceType = typeof(ErrorMessages))]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public bool Enabled { get; set; }
    }
}