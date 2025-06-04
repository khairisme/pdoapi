using HR.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces
{
    public interface IMaklumatSkimPerkhidmatanService
    {
        Task<IEnumerable<MaklumatSkimPerkhidmatanSearchResponseDto>> GetSenaraiSkimPerkhidmatan(MaklumatSkimPerkhidmatanFilterDto filter);
        Task<bool> CreateAsync(MaklumatSkimPerkhidmatanCreateRequestDto maklumatSkimPerkhidmatanDto);
        Task<bool> CheckDuplicateKodNamaAsync(MaklumatSkimPerkhidmatanCreateRequestDto maklumatSkimPerkhidmatanDto);
        Task<MaklumatSkimPerkhidmatanResponseDto?> GetSenaraiSkimPerkhidmatanByIdAsync(string kod);
        Task<bool> UpdateAsync(MaklumatSkimPerkhidmatanCreateRequestDto dto);
    }
}
