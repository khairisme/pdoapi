using HR.Application.DTOs.PDO;
using HR.Application.DTOs.PDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces.PDP
{
    public interface IJadualGajiService
    {
        Task<IEnumerable<JadualGajiResponseDto>> GetAllAsync();
    }
}
