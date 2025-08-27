using System.Collections.Generic;
using System.Threading.Tasks;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IStatusPermohonanJawatanExt
    {
        public Task TambahStatusPermohonanJawatan(Guid UserId, TambahStatusPermohonanJawatanDto request);
        public Task TambahStatusPermohonanJawatanDraft(Guid UserId, TambahStatusPermohonanJawatanDraftDto request);
        public Task TambahStatusPermohonanJawatanBaharu(Guid UserId, TambahStatusPermohonanJawatanBaharuDto request);
    }
}
