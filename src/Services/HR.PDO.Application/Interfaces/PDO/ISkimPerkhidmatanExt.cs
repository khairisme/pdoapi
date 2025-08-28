using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface ISkimPerkhidmatanExt
    {
        public Task<List<SkimPerkhidmatanUntukButiranPermohonanDto>> SenaraiSkimPerkhidmatanUntukButiranPermohonan(int IdPermohonanJawatan);
    }
}
