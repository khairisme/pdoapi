using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IDokumenPermohonanExt
    {
        public Task<List<RujJenisDokumenLinkDto>> SenaraiDokumenPermohonan(int IdPermohonanJawatan);
        public Task WujudDokumenPermohonanBaru(Guid UserId, int IdPermohonanJawatan, string? KodRujJenisDokumen, string? NamaDokumen, string? PautanDokumen, string? FormatDokumen, int Saiz);
        public Task HapusTerusDokumenPermohonan(Guid UserId, int Id);
    }
}
