namespace HR.PDO.API.Middleware
{
    public class JwtDto
    {
        public string SecretKey { get; set; } = string.Empty;
        public string UserID { get; set; } = string.Empty;
    }
}
