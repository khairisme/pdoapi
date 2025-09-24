using HR.PDO.Application.DTOs;
using HR.PDO.Core.Entities.PDO;
using Shared.Contracts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IButiranPermohonanSkimGredTBKExt
    {

        public Task<List<ButiranPermohonanSkimGredTBKDto>> SenaraiButiranPermohonanSkimGredTBK();
        public Task<ButiranPermohonanSkimGredTBKDto> BacaButiranPermohonanSkimGredTBK(int? Id);
        public Task HapusTerusButiranPermohonanSkimGredTBK(int Id);
        public Task KemaskiniButiranPermohonanSkimGredTBK(ButiranPermohonanSkimGredTBKDto request);
        public Task<List<PDOButiranPermohonanSkimGredTBK>> TambahButiranPermohonanSkimGredTBK(TambahButiranPermohonanSkimGredTBKDto request);
    }
}
