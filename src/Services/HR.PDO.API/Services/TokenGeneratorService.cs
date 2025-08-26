using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;
using Azure.Core;
using HR.PDO.API.Models;

namespace HR.PDO.API.Services
{
    public class TokenGeneratorService
    {
        public JwtSecurityToken ExtractToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            return jwtToken;
        }

        public KeycloakTokenResponse GenerateNewToken(JwtSecurityToken originalToken)
        {
            var key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a-string-secret-at-least-256-bits-long"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();
            var expires = DateTime.UtcNow.AddMinutes(30); // Access token expiry  

            var claims = originalToken.Claims.ToList();

            // Optionally modify or add new claims  
            claims.Add(new Claim("custom-claim", "custom-value"));

            claims.Add(new Claim("user-id", "12345678-90ab-cdef-1234-567890abcdef"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = credentials
            };

            var newToken = new JwtSecurityToken(
                issuer: "your-microservice",
                audience: "your-microservice-client",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            // Fix for CS1061: Use a proper method to calculate Unix time seconds  
            var expiresIn = (int)(expires.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

            return new KeycloakTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = expiresIn,
                TokenType = "Bearer",
                Scope = "OpenID"
            };
        }
    }
}
