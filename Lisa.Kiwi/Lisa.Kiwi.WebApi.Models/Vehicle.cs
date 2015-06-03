namespace Lisa.Kiwi.WebApi
{
    public class Vehicle
    {
        public string Brand { get; set; }
        public string NumberPlate { get; set; }
        public string Color { get; set; }
        public string AdditionalFeatures { get; set; }
        public VehicleTypeEnum? VehicleType { get; set; }
    }

    public enum VehicleTypeEnum
    {
        Car,
        Bicycle,
        Moped,
        Motorcycle,
        Other
    }
}