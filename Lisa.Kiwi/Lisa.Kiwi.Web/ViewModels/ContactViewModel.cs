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

        [Display(Name = "EmailAddress", ResourceType = typeof(DisplayNames))]
        public string EmailAddress { get; set; }
    }
}