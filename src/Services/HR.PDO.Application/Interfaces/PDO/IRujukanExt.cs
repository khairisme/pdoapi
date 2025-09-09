using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IRujukanExt
    {
        public Task<List<DropDownDto>> RujukanPangkat();
        public Task<List<DropDownDto>> RujukanGelaranJawatan();
        public Task<List<DropDownDto>> RujukanUrusanPerkhidmatan();
        public Task<List<DropDownDto>> RujukanJenisMesyuarat();
        public Task<List<DropDownDto>> RujukanKategoriUnitOrganisasi();
        public Task<List<DropDownDto>> RujukanJenisJawatan();
        public Task<List<DropDownDto>> RujukanKetuaPerkhidmatan(int IdSkimPerkhidmatan);
        public Task<List<DropDownDto>> RujukanKumpulanPerkhidmatan();
    }
}
