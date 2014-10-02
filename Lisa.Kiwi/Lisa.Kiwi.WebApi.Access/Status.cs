using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisa.Kiwi.WebApi.Access
{
    public class Status
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public StatusName Name { get; set; }
    }

    public enum StatusName
    {
        Open,
        Solved,
        Doing,
        Transferred
    }
}
