using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IButiranPermohonanSkimGredKUJExt
    {
        public Task<List<ButiranPermohonanSkimGredKUJDto>> SenaraiButiranPermohonanSkimGredKUJ();
        public Task<ButiranPermohonanSkimGredKUJDto> BacaButiranPermohonanSkimGredKUJ(int Id);
        public Task HapusTerusButiranPermohonanSkimGredKUJ(Guid UserId, int Id);
        public Task KemaskiniButiranPermohonanSkimGredKUJ(Guid UserId, int Id, ButiranPermohonanSkimGredKUJDto request);
        public Task TambahButiranPermohonanSkimGredKUJ(Guid UserId, TambahButiranPermohonanSkimGredKUJDto request);
    }
}
