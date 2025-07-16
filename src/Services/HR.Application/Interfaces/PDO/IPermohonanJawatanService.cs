using HR.Application.DTOs.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces.PDO
{
    public interface IPermohonanJawatanService
    {
        IQueryable<PermohonanJawatanSearchResponseDto> Search(PermohonanJawatanFilterDto filter);
        Task<List<PermohonanJawatanResponseDto>> GetSenaraiPermohonanJawatanAsync(PermohonanJawatanFilterDto2 filter);
        Task<List<PermohonanPindaanResponseDto>> GetSenaraiPermohonanPindaanAsync(PermohonanPindaanFilterDto filter);
        //Amar Code Start
        Task<List<SalinanAsaResponseDto>> GetSalinanAsa(SalinanAsaFilterDto filter);
        Task<List<SalinanBaharuResponseDto>> GetSalinanBaharu(int IdUnitOrganisasi);
        Task<bool> SetUlasanPasukanPerunding(UlasanPasukanPerundingRequestDto ulasanPasukanPerundingRequestDto);
        Task<List<SenaraiPermohonanPerjawatanResponseDto>> GetSenaraiPermohonanPerjawatan(SenaraiPermohonanPerjawatanFilterDto filter);
        //Amar Code End
    }
}
