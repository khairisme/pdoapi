using HR.PDO.API.Models;
using HR.PDO.API.Services;
using HR.PDO.Application.Interfaces;
using HR.PDO.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HR.PDO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IKeyCloakService _keycloakService;
        private readonly KeycloakSettings _keycloakSettings;
        private readonly TokenGeneratorService _tokenGeneratorService = new TokenGeneratorService();

        public AuthController(IKeyCloakService keyCloakService, IOptions<KeycloakSettings> keycloakSettings)
        {
            _keycloakService = keyCloakService;
            _keycloakSettings = keycloakSettings.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TokenRequest request)
        {
            string keyCloakUrl = _keycloakSettings.BaseUrl + "realms/" + _keycloakSettings.Realm + "/protocol/openid-connect/token";
            var tokenResponse = await _keycloakService.GetTokenAsync(request.UserName, request.Password,
                keyCloakUrl, _keycloakSettings.ClientId, _keycloakSettings.ClientSecret);

            var extractedToken = _tokenGeneratorService.ExtractToken(tokenResponse);
            var token = _tokenGeneratorService.GenerateNewToken(extractedToken);

            return Ok(token);

        //    var blankTokenResponse = new HR.PDO.API.Models.KeycloakTokenResponse
        //    {
        //        AccessToken = "",
        //        RefreshToken = "",
        //        ExpiresIn = 0,
        //        TokenType = "",
        //        Scope = ""
        //    };
        //    return Ok(blankTokenResponse);
        }
    }
}
