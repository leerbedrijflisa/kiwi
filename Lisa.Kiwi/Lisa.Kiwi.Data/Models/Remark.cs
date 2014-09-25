using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lisa.Kiwi.Data.Models
{
    public class Remark
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public virtual Report Report { get; set; }
    }
}
