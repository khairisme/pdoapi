using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.PDO.Application.Interfaces;
using Newtonsoft.Json;

namespace HR.PDO.Application.Services
{
    public class KeyCloakService : IKeyCloakService
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
                var tokenResponse = JsonConvert.DeserializeObject<KeycloakTokenResponse>(result); // Deserialize to appropriate DTO
                return tokenResponse.AccessToken; // Assuming AccessToken is a property in KeycloakTokenResponse
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to get token: {response.StatusCode} - {error}");
            }
        }
    }

    // Add the missing KeycloakTokenResponse class
    public class KeycloakTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
