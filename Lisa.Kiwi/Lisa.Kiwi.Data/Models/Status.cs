using System;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.Data.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public enum StatusName
        {
            Open,
            Afgehandeld,
            Bezig,
            Doorgeschakelt
        }

        public StatusName Name { get; set; }

        public virtual Report Report { get; set; }
    }
}
