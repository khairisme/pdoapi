using HR.PDO.Application.DTOs.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IRujJenisSaraanService
    {
        /// <summary>
        /// Get all RujStatusPermohonan
        /// </summary>
        public Task<IEnumerable<RujJenisSaraanDto>> GetAllAsync();
    }
}
