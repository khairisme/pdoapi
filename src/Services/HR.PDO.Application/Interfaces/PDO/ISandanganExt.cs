using HR.PDO.Application.DTOs;
using Shared.Contracts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface ISandanganExt
    {
        public Task<List<SandanganOutputDto>> SenaraiSandangan(JawatanListDto request);
        public Task<List<SandanganOutputDto>> GetSandanganAsync(JawatanListDto request);
    }
}
