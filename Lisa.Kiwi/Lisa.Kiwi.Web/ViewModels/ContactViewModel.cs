using System.ComponentModel;

namespace Lisa.Kiwi.Web
{
    public class ContactViewModel
    {
        [DisplayName("Naam")]
        public string Name { get; set; }

        [DisplayName("Telefoonnummer")]
        public string PhoneNumber { get; set; }

        [DisplayName("E-mail Address")]
        public string EmailAddress { get; set; }
    }
}