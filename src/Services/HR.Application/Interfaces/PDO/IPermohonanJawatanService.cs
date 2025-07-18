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


        Task<List<PermohonanJawatanDto>> GetSenaraiAsalAsync(int agensiId, string? noRujukan, string? tajuk, string? statusKod);
        Task<PermohonanJawatanDto?> GetSenaraiBaruByIdAsync(int idPermohonanJawatan);
        Task<List<PermohonanJawatanListDto>> GetPermohonanListAsync(int agensiId, string? noRujukan, string? tajuk, string? kodStatus);

        //Amar Code Start
        Task<List<SalinanAsaResponseDto>> GetSalinanAsa(SalinanAsaFilterDto filter);
        Task<List<SalinanBaharuResponseDto>> GetSalinanBaharu(int IdPermohonanJawatanSelected);
        Task<bool> SetUlasanPasukanPerunding(UlasanPasukanPerundingRequestDto ulasanPasukanPerundingRequestDto);
        Task<List<SenaraiPermohonanPerjawatanResponseDto>> GetSenaraiPermohonanPerjawatan(SenaraiPermohonanPerjawatanFilterDto filter);
        //Amar Code End

        #region Akhilesh Region
        Task<List<SenaraiPermohonanPerjawatanSearchResponseDto>> SenaraiPermohonanPerjawatanSearchData(SenaraiPermohonanPerjawatanSearchRequestDto filter);
        #endregion

    }
}
