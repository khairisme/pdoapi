namespace HR.PDO.API.Models
{
    public class KeycloakSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Realm { get; set; }
        public string BaseUrl { get; set; }
    }
}
