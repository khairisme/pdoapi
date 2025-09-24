using HR.PDO.Application.DTOs;
using HR.PDO.Application.DTOs.PDO;
using Shared.Contracts.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IGredExt
    {
        public Task<List<DropDownDto>> RujukanGredIkutKlasifikasiDanKumpulan(int IdKlasifikasiPerkhidmatan, int IdKumpulanPerkhidmatan);
        public Task<List<DropDownDto>> RujukanGredIkutSkimPerkhidmatan(GredSkimRequestDto request);
        public Task<List<DropDownDto>> RujukanGred();
        public Task<List<DropDownDto>> RujukanGredKUP();
    }
}
