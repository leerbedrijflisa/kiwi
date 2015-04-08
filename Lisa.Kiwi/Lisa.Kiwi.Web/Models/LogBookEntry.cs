using System;

namespace Lisa.Kiwi.Web
{
    public class LogBookEntry
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}