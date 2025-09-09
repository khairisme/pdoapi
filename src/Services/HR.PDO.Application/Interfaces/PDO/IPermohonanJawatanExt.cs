using System.Collections.Generic;
using System.Threading.Tasks;
using HR.PDO.Application.DTOs;
using HR.PDO.Application.Services.PDO;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IPermohonanJawatanExt
    {
        public Task TambahPermohonanJawatanBaru(Guid UserId, int IdAgensi, string? NomborRujukan, string? Tajuk, string? Keterangan, string? KodRujJenisPermohonan);
        public Task<BacaPermohonanJawatanDto> BacaPermohonanJawatan(int Id);
        public Task<BacaPermohonanJawatanDto> MuatPermohonanJawatan(int IdUnitOrganisasi);
        public Task HapusTerusPermohonanJawatan(Guid UserId, int Id);
        public Task KemaskiniPermohonanJawatan(Guid UserId, int Id, PermohonanJawatanDaftarDto request);
        public Task<List<PermohonanJawatanCarianDto>> CarianSenaraiPermohonanJawatan(CarianSenaraiPermohonanJawatanRequestDto filter);
        public Task<PagedResult<PermohonanJawatanLinkDto>> CarianPermohonanJawatan(PermohonanJawatanCarianDto request);
        public Task<List<PermohonanJawatanLinkDto>> SenaraiPermohonanJawatan(PermohonanJawatanCarianDto request);
        public Task DaftarPermohonanJawatan(PermohonanJawatanDaftarDto request);
        public Task KemaskiniUlasanPermohonanJawatan(UlasanRequestDto request);
        
    }
}
