using System;

namespace Lisa.Kiwi.WebApi
{
    public class Remark
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public int Report { get; set; }
    }
}