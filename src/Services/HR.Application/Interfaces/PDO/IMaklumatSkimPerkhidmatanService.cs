using HR.Application.DTOs.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces.PDO
{
    public interface IMaklumatSkimPerkhidmatanService
    {
        Task<IEnumerable<MaklumatSkimPerkhidmatanSearchResponseDto>> GetSenaraiSkimPerkhidmatan(MaklumatSkimPerkhidmatanFilterDto filter);
        Task<bool> CreateAsync(MaklumatSkimPerkhidmatanCreateRequestDto maklumatSkimPerkhidmatanDto);
        Task<bool> CheckDuplicateKodNamaAsync(MaklumatSkimPerkhidmatanCreateRequestDto maklumatSkimPerkhidmatanDto);
        Task<MaklumatSkimPerkhidmatanResponseDto?> GetSenaraiSkimPerkhidmatanByIdAsync(string kod);
        Task<bool> UpdateAsync(MaklumatSkimPerkhidmatanCreateRequestDto dto);
        Task<IEnumerable<SkimPerkhidmatanDto>> GetActiveSkimPerkhidmatan(SkimPerkhidmatanFilterDto filter);
        Task<bool> DaftarHantarSkimPerkhidmatanAsync(MaklumatSkimPerkhidmatanCreateRequestDto dto);

        Task<bool> UpdateHantarSkimPerkhidmatanAsync(MaklumatSkimPerkhidmatanCreateRequestDto dto);

    }
}
