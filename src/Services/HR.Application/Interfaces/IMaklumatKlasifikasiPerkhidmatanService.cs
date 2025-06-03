using HR.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces
{
    public interface IMaklumatKlasifikasiPerkhidmatanService
    {
        Task<IEnumerable<MaklumatKlasifikasiPerkhidmatanSearchResponseDto>> GetMaklumatKlasifikasiPerkhidmatan(MaklumatKlasifikasiPerkhidmatanFilterDto filter);

        Task<bool> CreateAsync(MaklumatKlasifikasiPerkhidmatanCreateRequestDto CreateRequestDto);
    }
}
