using HR.Application.DTOs.PDO;
using HR.Core.Entities.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces.PDO
{
    public interface IPermohonanPengisianSkimService
    {
        
        Task<List<PegawaiTeknologiMaklumatResponseDto>> GetPegawaiTeknologiMaklumat(int IdSkimPerkhidmatan, int IdPermohonanPengisian);

    }
}
