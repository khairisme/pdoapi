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
    }
}
