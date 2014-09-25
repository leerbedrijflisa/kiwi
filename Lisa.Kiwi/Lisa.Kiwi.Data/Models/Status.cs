using System;

namespace Lisa.Kiwi.Data.Models
{
    public class Status
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public StatusName Name { get; set; }
        public virtual Report Report { get; set; }
    }
}
