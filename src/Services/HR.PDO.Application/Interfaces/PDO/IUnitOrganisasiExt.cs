using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IUnitOrganisasiExt
    {
        public Task<PagedResult<StrukturUnitOrganisasiDto>> StrukturUnitOrganisasi(StrukturUnitOrganisasiRequestDto request);
        public Task<List<UnitOrganisasiLinkDto>> CarianUnitOrganisasi(UnitOrganisasiCarianDto request);
        public Task<List<DropDownDto>> RujukanUnitOrganisasi();
        public Task<List<UnitOrganisasiLinkDto>> SenaraiUnitOrganisasi(UnitOrganisasiCarianDto request);
        public Task KemaskiniUnitOrganisasi(UnitOrganisasiDaftarDto request);
        public Task PenjenamaanSemulaUnitOrganisasi(PenjenamaanUnitOrganisasiDto request);
        public Task HapusTerusUnitOrganisasi(HapusTerusUnitOrganisasiRequestDto request);
        public Task<UnitOrganisasiFormDisplayDto> BacaUnitOrganisasi(int Id);
        public Task WujudUnitOrganisasiBaru(Guid UserId, UnitOrganisasiWujudDto request);
        public Task MansuhUnitOrganisasi(MansuhUnitOrganisasiRequestDto request);
    }
}
