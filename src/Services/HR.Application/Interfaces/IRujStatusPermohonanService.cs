using HR.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces
{
    public interface IRujStatusPermohonanService
    {
        /// <summary>
        /// Get all RujStatusPermohonan
        /// </summary>
        Task<IEnumerable<RujStatusPermohonanDto>> GetAllAsync();

    }
}
