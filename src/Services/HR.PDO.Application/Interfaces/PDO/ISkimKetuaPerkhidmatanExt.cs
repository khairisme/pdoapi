using HR.PDO.Application.DTOs;
using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Core.Entities.PDO;
using Shared.Contracts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface ISkimKetuaPerkhidmatanExt
    {
        public Task<List<DropDownDto>> RujukanKetuaPerkhidmatan(int IdSkimPerkhidmatan);
        public Task<PDOSkimKetuaPerkhidmatan> TambahKetuaPerkhidmatan(SkimKetuaPerkhidmatanRequestDto request);
    }
}
