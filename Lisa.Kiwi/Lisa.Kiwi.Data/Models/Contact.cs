using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.Data.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int? StudentNo { get; set; }
    }
}
