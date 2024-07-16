namespace Express_Cafe_API.Security
{
    public class AuthenticatedResponse
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? Role { get; set; }
    }
}
