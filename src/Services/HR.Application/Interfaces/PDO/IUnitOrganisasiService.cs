using HR.Application.DTOs.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces.PDO
{
    public interface IUnitOrganisasiService
    {
        Task<IEnumerable<UnitOrganisasiDto>> GetAllAsync();
        Task<List<UnitOrganisasiKementerianDto>> GetUnitOrganisasiByKategoriAsync();

        Task<List<UnitOrganisasiAutocompleteDto>> SearchUnitOrganisasiAsync(string keyword);

        // Amar Code 17/07/25
        Task<string> GetNamaUnitOrganisasi(int IdUnitOrganisasi);
        Task<bool> SetPenjenamaanSemula(UnitOrganisasiPenjenamaanSemulaRequestDto penjenamaanSemulaRequestDto);
        //End
    }
}
