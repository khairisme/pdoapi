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
        Task<MaklumatSkimPerkhidmatanSearchResponseDto?> GetSenaraiSkimPerkhidmatanByIdAsync(int id);
        Task<bool> UpdateAsync(MaklumatSkimPerkhidmatanCreateRequestDto dto);
        Task<IEnumerable<SkimPerkhidmatanDto>> GetActiveSkimPerkhidmatan(SkimPerkhidmatanFilterDto filter);
        Task<bool> DaftarHantarSkimPerkhidmatanAsync(MaklumatSkimPerkhidmatanCreateRequestDto dto);

        Task<bool> UpdateHantarSkimPerkhidmatanAsync(MaklumatSkimPerkhidmatanCreateRequestDto dto);

        Task<List<SkimWithJawatanDto>> GetSkimWithJawatanAsync(int idSkim);
        Task<IEnumerable<CarianSkimPerkhidmatanResponseDto>> GetCarianSkimPerkhidmatan(CarianSkimPerkhidmatanFilterDto filter);
        Task<IEnumerable<SkimPerkhidmatanResponseDto>> GetAllAsync();
        Task<bool> KemaskiniStatusAsync(SkimPerkhidmatanRefStatusDto perkhidmatanDto);
        Task<SkimPerkhidmatanRefStatusDto> GetMaklumatBaharuAsync(int id);
        Task<bool> DeleteOrUpdateSkimPerkhidmatanAsync(int id);
        Task<MaklumatSkimPerkhidmatanSearchResponseDto?> GetSenaraiSkimPerkhidmatanByKodAsync(string kod);

        Task<List<SkimPerkhidmatanDetailsDTO>> GetSkimPerkhidmatanByIdAsync(int id);


    }
}
