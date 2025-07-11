using HR.Application.DTOs.PDO;
using HR.Core.Entities.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces.PDO
{
    public interface IPermohonanPengisianSkimService
    {
        
        Task<List<PegawaiTeknologiMaklumatResponseDto>> GetPegawaiTeknologiMaklumat(int IdSkimPerkhidmatan, int IdPermohonanPengisian);
        Task<BilanganPengisianHadSilingResponseDto> GetBilanganPengisianHadSiling(int IdPermohonanPengisian, int IdPermohonanPengisianSkim);
        //Nitya Code Start
        Task<int> GetBilanganPengisianByIdAsync(int idPermohonanPengisian);
        Task<bool> UpdateUlasanAndHadSilingAsync(CombinedUpdateRequestDto request);

        //Nitya Code End

        Task<PaparPermohonanDanSilingResponseDto?> GetJumlahDanSilingAsync(PaparPermohonanDanSilingRequestDto request);

    }
}
