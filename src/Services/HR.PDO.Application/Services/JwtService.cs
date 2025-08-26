using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR.PDO.API.Middleware
{
    public class JwtService
    {
       // private const string SecretKey = "a-string-secret-at-least-256-bits-long"; // Keep in config ideally
        private readonly JwtDto _settings;

        public JwtService(IOptions<JwtDto> settings)
        {
            _settings = settings.Value;
        }

        public ClaimsPrincipal ValidateAccessToken(string token)
        {
            return ValidateToken(token, _settings.SecretKey);
        }

       

        private ClaimsPrincipal ValidateToken(string token, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secret);
            var parameters = new TokenValidationParameters
            {
               
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };

            return tokenHandler.ValidateToken(token, parameters, out _);
        }

    }
}
