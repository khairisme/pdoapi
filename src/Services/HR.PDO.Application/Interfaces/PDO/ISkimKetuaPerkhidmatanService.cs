using HR.PDO.Application.DTOs.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.Interfaces.PDO
{
    public interface ISkimKetuaPerkhidmatanService
    {
        Task<bool> CreateAsync(List<SkimKetuaPerkhidmatanRequestDto> dto);

        Task<bool> SoftDeleteSkimKetuaPerkhidmatanAsync(int IdSkim,int IdJawatan);
    }
}
