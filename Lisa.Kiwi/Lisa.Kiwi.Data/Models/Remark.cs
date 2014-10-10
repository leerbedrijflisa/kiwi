using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lisa.Kiwi.Data
{
    public class Remark
    {
        public int Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public string Description { get; set; }

        public virtual Report Report { get; set; }
    }
}
