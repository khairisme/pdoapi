using HR.Application.DTOs.PDO;
using HR.Application.Services.PDO;

namespace HR.Application.Interfaces.PDO
{
    public interface IMaklumatKlasifikasiPerkhidmatanService
    {
        Task<IEnumerable<MaklumatKlasifikasiPerkhidmatanSearchResponseDto>> GetSearchMaklumatKlasifikasiPerkhidmatan(PenapisMaklumatKlasifikasiPerkhidmatanDto filter);

        Task<bool> NewAsync(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto CreateRequestDto);

        Task<MaklumatKlasifikasiPerkhidmatanResponseDto?> GetMaklumatKlasifikasiPerkhidmatan(int id);

        Task<bool> SetAsync(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto updateRequestDto);

        Task<IEnumerable<PengesahanPerkhidmatanKlasifikasiResponseDto>> GetSenaraiPengesahanPerkhidmatanKlasifikasi(PenapisPerkhidmatanKlasifikasiDto filter);

        

        Task<IEnumerable<MaklumatKlasifikasiPerkhidmatanDto>> GetAllAsync();


        Task<bool> DaftarHanatarMaklumatKlasifikasiPerkhidmatanAsync(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto dto);

        Task<bool> SetHanatarMaklumatKlasifikasiPerkhidmatanAsync(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto updateRequestDto);

    }
}
