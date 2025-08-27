using System.Collections.Generic;
using System.Threading.Tasks;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IJadualGajiExt
    {
        public Task<List<JadualGajiExtDto>> SenaraiSemuaJadualGaji(JadualGajiExtDto request);
    }
}
