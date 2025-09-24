using HR.PDO.Application.DTOs;
using HR.PDO.Core.Entities.PDO;
using Shared.Contracts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IAlamatUnitOrganisasiExt
    {
        public Task<PDOAlamatUnitOrganisasi> TambahAlamatUnitOrganisasi(AlamatUnitOrganisasiDto request);
    }
}
