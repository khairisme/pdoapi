using System.Collections.Generic;
using System.Threading.Tasks;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IAktivitiOrganisasiExt
    {
        public Task<List<StrukturAktivitiOrganisasiDto>> StrukturAktivitiOrganisasi(string? KodCarta);
        public Task WujudAktivitiOrganisasiBaru(Guid UserId, int IdIndukAktivitiOrganisasi, string? KodProgram, string? Kod, string? Nama, int Tahap, string? KodRujKategoriAktivitiOrganisasi, string? Keterangan);
        public Task PenjenamaanAktivitiOrganisasi(Guid UserId, int Id, string? Nama);
        public Task<AktivitiOrganisasiDto> BacaAktivitiOrganisasi(int Id);
        public Task<List<DropDownDto>> RujukanAktivitiOrganisasi();
        public Task DaftarAktivitiOrganisasi(Guid UserId, AktivitiOrganisasiDaftarDto request);
        public Task HapusTerusAktivitiOrganisasi(Guid UserId, int Id);
    }
}
