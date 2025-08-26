using HR.PDO.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
namespace HR.PDO.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string? UserId { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            UserId = user?.FindFirst("user-id")?.Value;
        }
    }
}
