using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.DTOs.PDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.Interfaces.PDP
{
    public interface IJadualGajiService
    {
        Task<IEnumerable<JadualGajiResponseDto>> GetAllAsync();
    }
}
