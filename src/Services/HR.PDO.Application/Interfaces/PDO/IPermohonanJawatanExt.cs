using System.Collections.Generic;
using System.Threading.Tasks;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IPermohonanJawatanExt
    {
        public Task TambahPermohonanJawatanBaru(Guid UserId, string? NomborRujukan, string? Tajuk, string? Keterangan, string? KodRujJenisPermohonan);
        public Task<BacaPermohonanJawatanDto> BacaPermohonanJawatan(int Id);
        public Task HapusTerusPermohonanJawatan(Guid UserId, int Id);
        public Task KemaskiniPermohonanJawatan(Guid UserId, int Id, PermohonanJawatanDaftarDto request);
        public Task<List<PermohonanJawatanCarianDto>> CarianSenaraiPermohonanJawatan(CarianSenaraiPermohonanJawatanRequestDto filter);
        public Task<List<SenaraiPermohonanJawatanDto>> SenaraiPermohonanJawatanCarianAgensiNoRujukanTajukStatus(string KodAgensi, string NoRujukan, string TajukPermohonan, string KodRujStatusPermohonanJawatan);
        public Task<List<SenaraiPermohonanJawatanDto>> SenaraiPermohonanJawatanCarianNoRujukanJenisTajukStatus(string NoRujukan, string KodRujJenisPermohonan, string TajukPermohonan, string KodRujStatusPermohonanJawatan);
        public Task<List<PermohonanJawatanLinkDto>> CarianPermohonanJawatan(int Id, PermohonanJawatanCarianDto request);
        public Task<List<PermohonanJawatanLinkDto>> SenaraiPermohonanJawatan(PermohonanJawatanCarianDto request);
        public Task DaftarPermohonanJawatan(Guid UserId, PermohonanJawatanDaftarDto request);
    }
}
