using System.Collections.Generic;
using System.Threading.Tasks;
using HR.PDO.Application.DTOs;
using HR.PDO.Application.Services.PDO;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IJawatanExt
    {
        public Task<List<JawatanLinkDto>> SenaraiJawatan(ButiranJawatanRequestDto request);

    }
}
