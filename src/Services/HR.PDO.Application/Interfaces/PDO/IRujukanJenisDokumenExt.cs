using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IRujukanJenisDokumenExt
    {
        public Task<List<DropDownDto>> RujukanJenisDokumen();
        public Task DaftarRujJenisDokumen(RujJenisDokumenDaftarDto request);
        public Task KemaskiniRujJenisDokumen(Guid UserId, RujJenisDokumenDaftarDto request);
    }
}
