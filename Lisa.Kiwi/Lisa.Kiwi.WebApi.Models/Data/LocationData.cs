using System.ComponentModel.DataAnnotations.Schema;

namespace Lisa.Kiwi.WebApi
{
    [Table("Locations")]
    public class LocationData
    {
        public int Id { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Building { get; set; }
        public string Description { get; set; }
    }
}