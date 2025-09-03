using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
using Azure.Core;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IButiranPermohonanExt
    {
        public Task TambahButiranPermohonan(Guid UserId, TambahButiranPermohonanDto request);
        public Task KemaskiniButiranPermohonan(Guid UserId, KemaskiniButiranPermohonanRequestDto request);
        public Task MansuhButiranPermohonanCadanganJawatan(Guid UserId, int IdButiranPermohonan,int IdCadanganJawatan);
        public Task KemaskiniButiranPerubahanButiranPermohonan(Guid UserId, KemaskiniButiranPermohonanRequestDto request);
        public Task KiraImplikasiKewanganButiranPermohonan(Guid UserId, KiraImplikasiKewanganRequestDto request);
        public Task<TambahButiranPermohonanDto> BacaButiranPermohonan(int IdPermohonanJawatan);
        public Task<List<TambahButiranPermohonanDto>> SenaraiButiranPermohonan();
    }
}
