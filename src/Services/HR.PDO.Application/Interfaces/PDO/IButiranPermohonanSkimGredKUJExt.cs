using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IButiranPermohonanSkimGredKUJExt
    {
        public Task<List<ButiranPermohonanSkimGredKUJDto>> SenaraiButiranPermohonanSkimGredKUJ();
        public Task<ButiranPermohonanSkimGredKUJDto> BacaButiranPermohonanSkimGredKUJ(int? Id);
        public Task HapusTerusButiranPermohonanSkimGredKUJ(int Id);
        public Task KemaskiniButiranPermohonanSkimGredKUJ(ButiranPermohonanSkimGredKUJDto request);
        public Task<int?> TambahButiranPermohonanSkimGredKUJ(TambahButiranPermohonanSkimGredKUJDto request);
    }
}
