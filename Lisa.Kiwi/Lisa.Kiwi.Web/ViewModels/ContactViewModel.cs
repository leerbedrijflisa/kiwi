using System.ComponentModel.DataAnnotations;
using Lisa.Kiwi.Web.Resources;

namespace Lisa.Kiwi.Web
{
    public class ContactViewModel
    {
        public string Name { get; set; }

        [MinLength(6, ErrorMessageResourceName = "PhoneNumber", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceName = "Email", ErrorMessageResourceType = typeof(ErrorMessages))]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
    }
}