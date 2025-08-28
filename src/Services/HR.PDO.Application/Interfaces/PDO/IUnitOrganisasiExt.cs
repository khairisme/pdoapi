using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IUnitOrganisasiExt
    {
        public Task<PagedResult<StrukturUnitOrganisasiDto>> StrukturUnitOrganisasi(string? KodCartaOrganisasi, int parentId = 0, int page = 1, int pageSize = 50, string? keyword = null, string? sortBy = "UnitOrganisasi", bool desc = false, CancellationToken ct = default);
        public Task<List<UnitOrganisasiLinkDto>> CarianUnitOrganisasi(UnitOrganisasiCarianDto request);
        public Task<List<DropDownDto>> RujukanUnitOrganisasi();
        public Task<List<UnitOrganisasiLinkDto>> SenaraiUnitOrganisasi(UnitOrganisasiCarianDto request);
        public Task KemaskiniUnitOrganisasi(Guid UserId, int Id, UnitOrganisasiDaftarDto request);
        public Task PenjenamaanSemulaUnitOrganisasi(Guid UserId, int Id, string? Nama);
        public Task HapusTerusUnitOrganisasi(Guid UserId, int Id);
    }
}
