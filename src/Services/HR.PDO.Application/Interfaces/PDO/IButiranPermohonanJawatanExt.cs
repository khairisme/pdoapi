using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IButiranPermohonanJawatanExt
    {
        public Task<List<ButiranPermohonanJawatanDto>> SenaraiButiranPermohonanJawatan();
        public Task<ButiranPermohonanJawatanDto> BacaButiranPermohonanJawatan(AddButiranPermohonanJawatanRequestDto request);
        public Task HapusTerusPermohonanJawatan(AddButiranPermohonanJawatanRequestDto request);
        public Task<ButiranPermohonanJawatanDto> TambahButiranPermohonanJawatan(TambahButiranPermohonanJawatanDto request);
    }
}
