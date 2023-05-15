namespace MediMove.Server
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwtExpireHours { get; set; }
        public string JwtIssuer { get; set; }
    }
}
