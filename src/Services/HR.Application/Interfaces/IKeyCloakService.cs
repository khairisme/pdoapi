using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces
{
    public interface IKeyCloakService
    {
        Task<string> GetTokenAsync(string username, string password, 
            string keyCloakUrl, string clientId, string clientSecret);
    }
}
