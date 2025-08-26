using HR.PDO.Application.DTOs;
using HR.PDO.Application.DTOs.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IGredService
    {
        Task<List<PDOGredDto>> GetGredListAsync(GredFilterDto filter);
        Task<List<GredResultDto>> GetFilteredGredList(GredFilterDto filter);
        Task<bool> CreateAsync(CreateGredDto dto);
        Task<bool> DaftarGredJawatanAsync(CreateGredDto dto);
        Task<bool> UpdateAsync(CreateGredDto dto);
        Task<bool> UpdateHantarGredJawatanAsync(CreateGredDto dto);
        Task<PaparMaklumatGredDto> GetMaklumatGred(int id);
        Task<bool> CheckDuplicateNamaAsync(CreateGredDto dto);
        Task<PaparMaklumatGredDto> GetMaklumatGredSediaAda(int id);

        Task<PaparMaklumatGredDto> GetMaklumatGredBaharuAsync(int id);

        Task<bool> KemaskiniStatusAsync(CreateGredDto dto);
        Task<bool> DeleteOrUpdateGredAsync(int id);
    }
}
