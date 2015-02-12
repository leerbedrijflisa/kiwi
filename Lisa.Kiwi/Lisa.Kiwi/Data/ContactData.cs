using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lisa.Kiwi.WebApi
{
    [Table("Contacts")]
    public class ContactData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public virtual ReportData Report { get; set; }
        public Guid EditToken { get; set; }
    }
}