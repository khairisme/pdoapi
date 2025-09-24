using HR.PDO.Application.DTOs;
using HR.PDO.Core.Entities.PDO;
using Shared.Contracts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IImplikasiPermohonanJawatanExt
    {
        public Task<PDOImplikasiPermohonanJawatan> TambahImplikasiPermohonanJawatan(TambahImplikadiPermohonanJawatanRequestDto request);
        public Task<TambahButiranPermohonanDto> TambahButiranPermohonan(TambahButiranPermohonanDto request);
        public Task<MansuhWujudImplikasiPermohonanOutputDto> SenaraiMansuhWujudImplikasiPermohonanJawatan(int IdPermohonanJawatan);
    }
}
