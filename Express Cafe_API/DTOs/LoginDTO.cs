namespace Express_Cafe_API.DTOs
{
    public class LoginDTO
    {
        public string? UserName { get; set; }
        //public string RolesName { get; set; }
        public string? Password { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
