namespace Lisa.Kiwi.WebApi.Access
{
    public class Token
    {
        public Token()
        {
            Type = "bearer";
        }

        public string Value { get; set; }
        public string Type { get; set; }
        public int ExpiresIn { get; set; }
    }
}