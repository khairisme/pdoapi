using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.Services.PDO;

namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IMaklumatKlasifikasiPerkhidmatanService
    {
        Task<IEnumerable<MaklumatKlasifikasiPerkhidmatanSearchResponseDto>> GetSearchMaklumatKlasifikasiPerkhidmatan(PenapisMaklumatKlasifikasiPerkhidmatanDto filter);

        Task<bool> NewAsync(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto CreateRequestDto);

        Task<bool> CheckDuplicateKodNamaAsync(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto dto);

        Task<MaklumatKlasifikasiPerkhidmatanResponseDto> GetMaklumatKlasifikasiPerkhidmatan(int id);
        Task<ButiranKemaskiniKlasifikasiPerkhidmatanResponseDto> GetMaklumatKlasifikasiPerkhidmatanView(int id);

        Task<bool> SetAsync(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto updateRequestDto);

        Task<IEnumerable<PengesahanPerkhidmatanKlasifikasiResponseDto>> GetSenaraiPengesahanPerkhidmatanKlasifikasi(PenapisPerkhidmatanKlasifikasiDto filter);

        

        Task<IEnumerable<MaklumatKlasifikasiPerkhidmatanDto>> GetAllAsync();


        Task<bool> DaftarHanatarMaklumatKlasifikasiPerkhidmatanAsync(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto dto);

        Task<bool> SetHanatarMaklumatKlasifikasiPerkhidmatanAsync(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto updateRequestDto);

        Task<bool> KemaskiniStatusAsync(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto dto);
        Task<bool> DeleteOrUpdateKlasifikasiPerkhidmatanAsync(int id);

        Task<MaklumatKlasifikasiPerkhidmatanResponseDto> GetMaklumatKlasifikasiPerkhidmatanOld(int id);

    }
}
