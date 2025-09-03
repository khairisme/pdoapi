using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
using System.Net.Http;

namespace HR.PDO.Application.Interfaces.PDO

{
    public interface IRujPangkatBadanBeruniformExt
    {
        public Task<List<DropDownDto>> RujukanPangkat();
        public Task<List<DropDownDto>> GetPangkatAsync();
    }
}
