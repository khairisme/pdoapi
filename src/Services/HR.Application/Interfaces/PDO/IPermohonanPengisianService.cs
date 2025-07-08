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
    }
}
