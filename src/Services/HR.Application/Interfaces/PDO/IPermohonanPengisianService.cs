using HR.Application.DTOs.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces.PDO
{
    public interface IPermohonanPengisianService
    {
        Task<List<PermohonanPOAFilterResponseDto>> GetPermohonanListPOAAsync(PermohonanPengisianPOAFilterDto search);
        Task<string> CreateAsync(SavePermohonanPengisianPOARequestDto request);
        Task<PermohonanPengisianHeaderResponseDto> GetPermohonanPengisianByAgensiAndId(PermohonanPengisianfilterdto filter);
        Task<bool> CheckDuplicateKodNamaAsync(SavePermohonanPengisianPOARequestDto dto);

         Task<List<BilanganPermohonanPengisianResponseDto>> GetBilanganPermohonanPengisianId(int idPermohonanPengisian);
        Task<bool> UpdateAsync(SavePermohonanPengisianPOARequestDto request);

        Task<List<PermohonanPOAIFilterResponseDto>> GetPermohonanListPOAIAsync(PermohonanPengisianPOAIFilterDto search);
        Task<List<PermohonanPengisianJawatanResponseDto>> GetFilteredPermohonanJawatanAsync(PermohonanPengisianJawatanFilterDto filter);
        Task<SkimNameWithJawatanDto?> GetJawatanBySkimAndAgensiAsync(PenolongPegawaiTeknologiMaklumatFilterDto filterDto);
        Task<List<AgensiWithJawatanDto>> GetGroupedJawatanByAgensiAsync(PenolongPegawaiTeknologiMaklumatFilterDto filter);
        Task<List<SimulasiKewanganByPermohonanDto>> GetSimulasiByPermohonanIdAsync(int idPermohonanPengisian);
        //Task<List<SimulasiKewanganResponseDto>> GetSimulasiByAgensiAsync(int agensiId);
    }
}
