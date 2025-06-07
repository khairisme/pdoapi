namespace HR.API.Services
{
    public class KeyCloakService
    {
        private readonly HttpClient _httpClient;

        public KeyCloakService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetTokenAsync(string username, string password,
            string keyCloakUrl, string clientId, string clientSecret)
        {
            var parameters = new Dictionary<string, string>
            {
                { "client_id", clientId },
                { "client_secret", clientSecret }, // if confidential client
                { "username", username },
                { "password", password },
                { "grant_type", "password" }
            };

            var content = new FormUrlEncodedContent(parameters);

            var response = await _httpClient.PostAsync(keyCloakUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result; // or parse JSON for access_token
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to get token: {response.StatusCode} - {error}");
            }
        }
    }
}
