using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IButiranPermohonanSkimGredExt
    {
        public Task<List<ButiranPermohonanSkimGredDto>> SenaraiButiranPermohonanSkimGred();
        public Task<ButiranPermohonanSkimGredDto> BacaButiranPermohonanSkimGred(int Id);
        public Task HapusTerusButiranPermohonanSkimGred(Guid UserId, int Id);
        public Task KemaskiniButiranPermohonanSkimGred(Guid UserId, int Id, ButiranPermohonanSkimGredDto request);
        public Task TambahButiranPermohonanSkimGred(Guid UserId, TambahButiranPermohonanSkimGredDto request);
    }
}
