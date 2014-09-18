using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lisa.Kiwi.Data.Models
{
    class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public const string Status = "Open";

        public DateTime Created { get; set; }

        public string Location { get; set; }

        public DateTime Time { get; set; }

        public string Guid { get; set; }

        public string UserAgent { get; set; }

        [MaxLength(45)]
        public string Ip { get; set; }

        public enum Type
        {
            Drugs,
            Overlast,
            Voertuigen,
            Inbraak,
            Diefstal,
            Intimidatie,
            Pesten,
            Digitaal
        }

        public virtual ICollection<Contact> Contact { get; set; }

        public Report()
        {
            Contact = new List<Contact>();
        }
    }
}
