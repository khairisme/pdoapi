using System.Collections.Generic;
using System.Threading.Tasks;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IRujukanGred
    {
        public Task<List<DropDownDto>> RujukanGredIkutKlasifikasiDanKumpulan(int IdKlasifikasiPerkhidmatan, int IdKumpulanPerkhidmatan);
    }
}
