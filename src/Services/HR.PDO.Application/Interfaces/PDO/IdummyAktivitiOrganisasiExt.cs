using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IdummyAktivitiOrganisasiExt
    {
        public Task<UnitOrganisasiFormDisplayDto> BacaUnitOrganisasi(int Id);
        public Task WujudUnitOrganisasiBaru(Guid UserId, UnitOrganisasiWujudDto  request);
        public Task<AktivitiOrganisasiDto> BacaAktivitiOrganisasi(int Id);
    }
}
