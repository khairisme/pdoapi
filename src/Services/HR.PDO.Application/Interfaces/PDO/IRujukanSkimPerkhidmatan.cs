using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IRujukanSkimPerkhidmatan
    {
        public Task<List<DropDownDto>> RujukanSkimPerkhidmatan();
        public Task<List<DropDownDto>> RujukanSkimPerkhidmatanIkutKumpulan(int IdKumpulanPerkhidmatan);
        public Task<List<DropDownDto>> RujukanSkimPerkhidmatanIkutKlasifikasi(int IdKlasifikasiPerkhidmatan);
        public Task<List<DropDownDto>> RujukanSkimPerkhidmatanIkutKlasifikasiDanKumpulan(int? IdKlasifikasiPerkhidmatan, int? IdKumpulanPerkhidmatan);
    }
}
