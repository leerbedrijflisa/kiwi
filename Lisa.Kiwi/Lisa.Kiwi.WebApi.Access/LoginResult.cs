namespace Lisa.Kiwi.WebApi.Access
{
    public enum LoginStatus
    {
        Success,
        UserPassMismatch,
        ConnectionError
    }

    public class LoginResult
    {
        public LoginStatus Status { get; set; }
        public string Token { get; set; }
        public string TokenType { get; set; }
        public int TokenExpiresIn { get; set; }
    }
}