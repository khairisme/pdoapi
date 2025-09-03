using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
using HR.PDO.Application.DTOs.PDO;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface ISkimKetuaPerkhidmatanExt
    {
        public Task<List<DropDownDto>> RujukanKetuaPerkhidmatan(int IdSkimPerkhidmatan);
        public Task TambahKetuaPerkhidmatan(Guid UserId, SkimKetuaPerkhidmatanRequestDto request);
    }
}
