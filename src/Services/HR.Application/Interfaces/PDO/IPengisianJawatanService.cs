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

        Task<List<PengisianJawatanSearchResponseDto>> GetPengisianJawatanAsync(int idSkimPerkhidmatan);
        Task<int> GetPengisianJawatanCountAsync(int idSkimPerkhidmatan);
        Task<bool> CreateAsync(PengisianJawatanDto dto);
        Task<bool> DeleteAsync(Guid Id);
    }
}