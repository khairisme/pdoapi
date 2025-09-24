using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
using HR.PDO.Core.Entities.PDO;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface ICadanganJawatanExt
    {
        public Task<List<CadanganJawatanDto>> SenaraiCadanganJawatan(int IdPermohonanJawatan);
        public Task<List<ButiranCadanganJawatanDto>> SenaraiButiranCadanganJawatan(SenaraiCadanganJawatanRequestDto request);
        public Task<List<PDOCadanganJawatan>> TambahCadanganJawatan(SenaraiCadanganJawatanRequestDto request);
        public Task<PDOCadanganJawatan> KemaskiniUnitOrganisasiCadanganJawatan(KemaskiniUnitOrganisasiCadanganJawatanRequestDto request);
        public Task KemaskiniCadanganJawatan(KemaskiniCadanganJawatanRequestDto request);
    }
}
