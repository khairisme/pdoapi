using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.DTOs;
using HR.PDO.Core.Entities.PDO;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IProfilPemilikKompetensiExt
    {
        public Task<List<ProfilPemilikKompetensiDisplayDto>> CarianProfilPemilikKompetensi(ProfilPemilikKompetensiCarianDto filter);
        public Task<List<ProfilPemilikKompetensi>> SenaraiProfilPemilikKompetensi(JawatanListDto request);
        public Task<List<ProfilPemilikKompetensi>> GetExternalSenaraiProfilPemilikKompetensiAsync(JawatanListDto request);
    }
}
