using HR.Application.DTOs.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces.PDO
{
    public interface IJawatanService
    {
        Task<List<JawatanWithAgensiDto>> GetJawatanWithAgensiAsync(string namaJwtn, string kodCartaOrganisasi);
        Task<List<CarianJawatanResponseDto>> GetCarianJawatanAsync(CarianJawatanFilterDto filter);
        Task<List<CarianJawatanSebenarResponseDto>> GetCarianJawatanSebenar(CarianJawatanSebenarFilterDto filter);
        Task<List<CarianJawatanSebenarRespDto>> SearchCarianawatanSebenarAsync(CarianJawatanSebenarReqDto request);
        // code added by amar 220725
        Task<List<CarianKetuaPerkhidmatanResponseDto>> GetNamaJawatan(string KodCarta);
        Task<List<SenaraiKetuaPerkhidmatanResponseDto>> GetSenaraiKetuaPerkhidmatan(string NamaJawatan,string KodCartaOrganisasi);
        //end
    }
}
