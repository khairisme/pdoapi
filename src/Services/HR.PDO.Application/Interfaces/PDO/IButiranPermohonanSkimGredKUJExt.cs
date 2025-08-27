using System.Collections.Generic;
using System.Threading.Tasks;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IButiranPermohonanSkimGredKUJExt
    {
        public Task TambahButiranPermohonanSkimGredKUJ(Guid UserId, TambahButiranPermohonanSkimGredKUJDto request);
    }
}
