using System.Collections.Generic;
using System.Threading.Tasks;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IFungsiUmum
    {
        public Task<JanaNomborRujukanDto> JanaNomborRujukan(int IdUnitOrganisasi);
    }
}
