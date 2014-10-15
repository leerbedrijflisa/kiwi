using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisa.Kiwi.Data
{
    public class ReportSettings
    {
        public int Id { get; set; }
        public virtual Report Report { get; set; }
        public bool Visible { get; set; }
    }
}
