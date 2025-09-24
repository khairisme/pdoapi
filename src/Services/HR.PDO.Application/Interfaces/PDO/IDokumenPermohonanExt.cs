using HR.PDO.Application.DTOs;
using HR.PDO.Core.Entities.PDO;
using Shared.Contracts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IDokumenPermohonanExt
    {
        public Task<List<RujJenisDokumenLinkDto>> SenaraiDokumenPermohonan(int IdPermohonanJawatan);
        public Task<PDODokumenPermohonan> WujudDokumenPermohonanBaru(WujudDokumenPermohonanRequestDto request);
        public Task<string?> HapusTerusDokumenPermohonan(HapusTerusDokumenPermohonanRequest request);
    }
}
