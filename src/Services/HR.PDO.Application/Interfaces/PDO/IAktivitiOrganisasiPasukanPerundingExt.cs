using HR.PDO.Application.DTOs;
using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Core.Entities.PDO;
using Shared.Contracts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IAktivitiOrganisasiPasukanPerundingExt
    {
        public Task<List<AktivitiOrganisasiPasukanPerundingOutputDto>> SenaraiAktivitiOrganisasiPasukanPerunding();
        public Task<PDOAktivitiOrganisasiRujPasukanPerunding> TambahAktivitiOrganisasiPasukanPerunding(TambahAktivitiOrganisasiPasukanPerundingRequestDto request);
        public Task<bool> HapusTerusAktivitiOrganisasiPasukanPerunding(TambahAktivitiOrganisasiPasukanPerundingRequestDto request);


    }
}
