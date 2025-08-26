using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HR.PDO.API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtMiddleware> _logger;
        private readonly JwtDto _settings;

        public JwtMiddleware(RequestDelegate next, ILogger<JwtMiddleware> logger, IOptions<JwtDto> settings)
        {
            _next = next;
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task Invoke(HttpContext context, JwtService jwtService)
        {
            var accessToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(accessToken))
            {
                try
                {
                    var principal = jwtService.ValidateAccessToken(accessToken);
                    if (principal != null)
                    {
                        //context.User = principal;
                        var userId = principal.Claims.FirstOrDefault(c => c.Type == "user-id")?.Value;

                        if (userId == _settings.UserID)
                        {
                            context.User = principal;
                        }
                        else
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            await context.Response.WriteAsync("Forbidden: Invalid user-id");
                            return;
                        }
                    }
                }
                catch (SecurityTokenExpiredException)
                {
                    //var refreshToken = context.Request.Cookies["refreshToken"];

                    //if (!string.IsNullOrEmpty(refreshToken))
                    //{
                    //    var newAccessToken = jwtService.TryRefreshAccessToken(refreshToken);
                    //    if (!string.IsNullOrEmpty(newAccessToken))
                    //    {
                    //        context.Response.Headers["X-New-Access-Token"] = newAccessToken;
                    //        // Optionally re-assign context.User
                    //    }
                    //}

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Access token expired");
                    return;
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Token validation failed");
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid access token");
                    return;
                }
            }

            await _next(context);
        }
    }

}
