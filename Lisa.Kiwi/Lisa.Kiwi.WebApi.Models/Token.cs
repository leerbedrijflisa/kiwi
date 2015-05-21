namespace Lisa.Kiwi.WebApi
{
    public class Token
    {
        public int ExpiresIn { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Role { get; set; }
    }
}