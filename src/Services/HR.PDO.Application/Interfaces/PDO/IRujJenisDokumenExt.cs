using System.Collections.Generic;
using System.Threading.Tasks;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IRujJenisDokumenExt
    {
        public Task<List<RujJenisDokumenLinkDto>> CarianRujJenisDokumen(RujJenisDokumenCarianDto request);
        public Task<List<RujJenisDokumenLinkDto>> SenaraiRujJenisDokumen(RujJenisDokumenCarianDto request);
        public Task<List<DropDownDto>> RujukanRujJenisDokumen(RujJenisDokumenDaftarDto request);
        public Task DaftarRujJenisDokumen(Guid UserId, RujJenisDokumenDaftarDto request);
        public Task KemaskiniRujJenisDokumen(Guid UserId, int Id, RujJenisDokumenDaftarDto request);
    }
}
