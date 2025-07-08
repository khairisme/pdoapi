using HR.Application.DTOs.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces.PDO
{
    public interface IPengisianJawatanService
    {
       // Task<IEnumerable<PengisianJawatanSearchResponseDto>> GetPengisianJawatanBySkimPerkhidmatan(PengisianJawatanFilterDto filter);
        Task<bool> DeleteAsync(Guid Id);
    }
}