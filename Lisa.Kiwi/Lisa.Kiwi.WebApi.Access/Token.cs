namespace Lisa.Kiwi.WebApi.Access
{
    public class Token
    {
        public string Value { get; set; }
        public string Type { get; set; }
        public int ExpiresIn { get; set; }
    }
}