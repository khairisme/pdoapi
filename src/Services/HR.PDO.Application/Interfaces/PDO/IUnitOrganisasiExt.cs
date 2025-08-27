using System.Collections.Generic;
using System.Threading.Tasks;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IUnitOrganisasiExt
    {
        public Task<List<StrukturUnitOrganisasiDto>> StrukturUnitOrganisasi(string? KodCartaOrganisasi);
        public Task<List<UnitOrganisasiLinkDto>> CarianUnitOrganisasi(UnitOrganisasiCarianDto request);
        public Task<List<DropDownDto>> RujukanUnitOrganisasi();
        public Task<List<UnitOrganisasiLinkDto>> SenaraiUnitOrganisasi(UnitOrganisasiCarianDto request);
        public Task KemaskiniUnitOrganisasi(Guid UserId, int Id, UnitOrganisasiDaftarDto request);
        public Task PenjenamaanSemulaUnitOrganisasi(Guid UserId, int Id, string? Nama);
        public Task HapusTerusUnitOrganisasi(Guid UserId, int Id);
    }
}
