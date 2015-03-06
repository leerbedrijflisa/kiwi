using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisa.Kiwi.WebApi
{
    [Table("Perpetrators")]
    public class PerpetratorData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public char? Gender { get; set; }
        public string SkinColor { get; set; }
        public string Clothing { get; set; }
        public int Age { get; set; }
        public string UniqueProperties { get; set; }
    }
}
