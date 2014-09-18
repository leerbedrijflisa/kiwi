using System;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Kiwi.Data.Models
{
    class Status
    {
        [Key]
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public enum Name
        {
            Open,
            Afgehandeld,
            Bezig,
            Doorgeschakelt
        }

        public virtual Report Report { get; set; }
    }
}
