using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.WebApi
{
    public class Contact
    {
        [Required(ErrorMessage = "Foutieve naam")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Foutief telefoonnummer")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
    }
}