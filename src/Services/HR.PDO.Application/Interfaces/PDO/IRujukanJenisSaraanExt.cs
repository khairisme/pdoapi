using System.Collections.Generic;
using System.Threading.Tasks;
using HR.PDO.Application.DTOs.PDO;
using Shared.Contracts.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IRujukanJenisSaraanExt
    {
        Task<IEnumerable<RujJenisSaraanDto>> SenaraiJenisSaraan();
    }
}
