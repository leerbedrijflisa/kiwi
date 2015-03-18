using System.ComponentModel.DataAnnotations.Schema;

namespace Lisa.Kiwi.WebApi
{
    [Table("Perpetrators")]
    public class PerpetratorData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SexEnum? Sex { get; set; }
        public string SkinColor { get; set; }
        public string Clothing { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }
        public string UniqueProperties { get; set; }
    }
}