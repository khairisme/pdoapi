using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IImplikasiPermohonanJawatanExt
    {
        //public Task TambahImplikasiPermohonanJawatan(Guid UserId, TambahButiranPermohonanDto request);
        public Task TambahButiranPermohonan(Guid UserId, TambahButiranPermohonanDto request);
    }
}
