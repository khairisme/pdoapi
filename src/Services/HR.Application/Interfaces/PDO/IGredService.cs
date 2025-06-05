using HR.Application.DTOs;
using HR.Application.DTOs.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces.PDO
{
    public interface IGredService
    {
        Task<List<PDOGredDto>> GetGredListAsync(GredFilterDto filter);
        Task<List<GredResultDto>> GetFilteredGredList(GredFilterDto filter);
    }
}
