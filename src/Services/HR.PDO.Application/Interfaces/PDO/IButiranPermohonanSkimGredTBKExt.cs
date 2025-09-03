using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IButiranPermohonanSkimGredTBKExt
    {
        public Task<List<ButiranPermohonanSkimGredTBKDto>> SenaraiButiranPermohonanSkimGredTBK();
        public Task<ButiranPermohonanSkimGredTBKDto> BacaButiranPermohonanSkimGredTBK(int Id);
        public Task HapusTerusButiranPermohonanSkimGredTBK(Guid UserId, int Id);
        public Task KemaskiniButiranPermohonanSkimGredTBK(Guid UserId, int Id, ButiranPermohonanSkimGredTBKDto request);
        public Task TambahButiranPermohonanSkimGredTBK(Guid UserId, TambahButiranPermohonanSkimGredTBKDto request);
    }
}
