using HR.Application.DTOs.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces.PDO
{
    public interface IAktivitiOrganisasiService
    {
        Task<List<AktivitiOrganisasiDto>> GetAktivitiOrganisasiAsync();
        //Amar Code Start
        Task<List<StrukturAktivitiOrganisasiResponseDto>> GetTreeStrukturAktivitiOrganisasi(int IdAktivitiOrganisasi);
         Task<string> GetNamaAktivitiOrganisasi(int IdIndukAktivitiOrganisasi);
        Task<bool> SetPenjenamaanSemula(PenjenamaanSemulaRequestDto penjenamaanSemulaRequestDto);
        Task<List<StrukturAktivitiOrganisasiResponseDto>> GetPindahAktivitiOrganisasi(int IdAktivitiOrganisasi);
        Task<object> GetNamaKodAktivitiOrganisasi(int IdIndukAktivitiOrganisasi);

        Task<bool> SetAktivitiOrganisasi(AktivitiOrganisasiRequestDto aktivitiOrganisasiRequestDto);
        //Amar Code End
    }
}
