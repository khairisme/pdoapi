using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IAktivitiOrganisasiExt
    {
        public Task<PagedResult<StrukturAktivitiOrganisasiDto>> StrukturAktivitiOrganisasi(string? KodCartaOrganisasi, int parentId = 0, int page = 1, int pageSize = 50, string? keyword = null, string? sortBy = "UnitOrganisasi", bool desc = false, CancellationToken ct = default);
        public Task WujudAktivitiOrganisasiBaru(Guid UserId, int IdIndukAktivitiOrganisasi, string? KodProgram, string? Kod, string? Nama, int Tahap, string? KodRujKategoriAktivitiOrganisasi, string? Keterangan);
        public Task PenjenamaanAktivitiOrganisasi(Guid UserId, int Id, string? Nama);
        public Task<AktivitiOrganisasiDto> BacaAktivitiOrganisasi(int Id);
        public Task<List<DropDownDto>> RujukanAktivitiOrganisasi();
        public Task DaftarAktivitiOrganisasi(Guid UserId, AktivitiOrganisasiDaftarDto request);
        public Task HapusTerusAktivitiOrganisasi(Guid UserId, int Id);
    }
}
