using HR.Application.DTOs.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces.PDO
{
    public interface IKumpulanPerkhidmatanService
    {
        /// <summary>
        /// Get all KumpulanPerkhidmatan
        /// </summary>
        Task<IEnumerable<KumpulanPerkhidmatanDto>> GetAllAsync();

        Task<IEnumerable<CarlKumpulanPerkhidmatanDto>> GetKumpulanPerkhidmatanAsync(KumpulanPerkhidmatanFilterDto filter);

        Task<bool> CreateAsync(KumpulanPerkhidmatanDto perkhidmatanDto);
        Task<KumpulanPerkhidmatanDetailDto?> GetKumpulanPerkhidmatanByIdAsync(int id);
        Task<bool> CheckDuplicateKodNamaAsync(KumpulanPerkhidmatanDto dto);

        Task<bool> UpdateAsync(KumpulanPerkhidmatanDto perkhidmatanDto);

        Task<IEnumerable<CarlStatusKumpulanPerkhidmatanDto>> GetStatusKumpulanPerkhidmatan(KumpulanPerkhidmatanFilterDto filter);

        Task<KumpulanPerkhidmatanStatusDto?> GetMaklumatSediaAda(int id);
        Task<KumpulanPerkhidmatanButiranDto> GetMaklumatBaharuAsync(int id);

        Task<bool> KemaskiniStatusAsync(KumpulanPerkhidmatanRefStatusDto perkhidmatanDto);

        Task<bool> DaftarHantarKumpulanPermohonanAsync(KumpulanPerkhidmatanDto dto);

        Task<bool> UpdateHantarKumpulanPermohonanAsync(KumpulanPerkhidmatanDto perkhidmatanDto);
    }
}
