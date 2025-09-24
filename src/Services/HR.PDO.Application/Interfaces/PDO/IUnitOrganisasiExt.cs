using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IUnitOrganisasiExt
    {
        public Task<PagedResult<StrukturUnitOrganisasiDto>> StrukturUnitOrganisasi(StrukturUnitOrganisasiRequestDto request);
        public Task<List<CarianUnitOrganisasiDto>> CarianUnitOrganisasi(UnitOrganisasiCarianRequestDto request);
        public Task<List<DropDownDto>> RujukanUnitOrganisasi();
        public Task<string?> JanaKodAgensi(JanaKodAgensiRequestDto request);

        public Task<string?> BacaNamaUnitOrganisasi(int IdUnitOrganisasi);
        public Task<List<UnitOrganisasiLinkDto>> SenaraiUnitOrganisasi(UnitOrganisasiCarianDto request);
        public Task KemaskiniUnitOrganisasi(UnitOrganisasiDaftarDto request);
        public Task KemaskiniSemakUnitOrganisasi(KemaskiniSemakUnitOrganisasiRequestDto request);
        public Task PenjenamaanSemulaUnitOrganisasi(PenjenamaanUnitOrganisasiDto request);
        public Task HapusTerusUnitOrganisasi(HapusTerusUnitOrganisasiRequestDto request);
        public Task<UnitOrganisasiFormDisplayDto> BacaUnitOrganisasi(int? Id);
        public Task<MuatUnitOrganisasiDto> MuatUnitOrganisasi();

        public Task<UnitOrganisasiFormDisplayDto> WujudUnitOrganisasiBaru(UnitOrganisasiWujudDto request);
        public Task MansuhUnitOrganisasi(MansuhUnitOrganisasiRequestDto request);

        
    }
}
