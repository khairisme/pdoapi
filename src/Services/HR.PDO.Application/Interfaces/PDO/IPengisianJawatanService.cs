using HR.PDO.Application.DTOs.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IPengisianJawatanService
    {

        Task<List<PengisianJawatanSearchResponseDto>> GetPengisianJawatanAsync(int idSkimPerkhidmatan);
        Task<int> GetPengisianJawatanCountAsync(int idSkimPerkhidmatan);
        Task<bool> CreateAsync(PengisianJawatanDto dto);
        Task<bool> DeleteAsync(Guid Id);
        //Nitya Code Start
        Task<bool> CreateAsync(List<PengisianJawatanDto> dtos);
        Task<bool> DeleteAsync(int Id);

        Task<List<AgensiWithSkimDto>> GetAgensiWithSkimPengisianAsync();
        Task<PermohonanDetailDto> GetPermohonanDetailByIdAsync(int idPermohonan);
        Task<List<SenaraiJawatanPengisianDto>> GetSenaraiJawatanUntukPengisian(int idSkimPerkhidmatan);
        Task<List<UnitOrganisasiDataDto>> GetSenaraiJawatanSebenarAsync();

        //Nitya Code End
    }
}