using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.Data.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int? StudentNumber { get; set; }
    }
}