using System.ComponentModel.DataAnnotations.Schema;

namespace Lisa.Kiwi.WebApi
{
    [Table("Locations")]
    public class LocationData
    {
        public int Id { get; set; }
        public string Building { get; set; }
        public string Description { get; set; }
    }
}