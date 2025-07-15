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
    }
}
