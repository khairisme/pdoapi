using HR.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces
{
    public interface IKumpulanPerkhidmatanService
    {
        /// <summary>
        /// Get all KumpulanPerkhidmatan
        /// </summary>
        Task<IEnumerable<KumpulanPerkhidmatanDto>> GetAllAsync();

        Task<IEnumerable<CarlKumpulanPerkhidmatanDto>> GetKumpulanPerkhidmatanAsync(KumpulanPerkhidmatanFilterDto filter);

    }
}
