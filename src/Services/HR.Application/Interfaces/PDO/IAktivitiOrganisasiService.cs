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


        Task<List<AktivitiOrganisasiResponseDto>> GetAktivitiOrganisasibyIdAsync(int Id);

        Task<int> SimpanAktivitiAsync(AktivitiOrganisasiCreateRequest request);

        //Amar Code Start
        Task<List<StrukturAktivitiOrganisasiResponseDto>> GetTreeStrukturAktivitiOrganisasi(int IdAktivitiOrganisasi);
         Task<string> GetNamaAktivitiOrganisasi(int IdIndukAktivitiOrganisasi);
        Task<bool> SetPenjenamaanSemula(PenjenamaanSemulaRequestDto penjenamaanSemulaRequestDto);
        Task<List<StrukturAktivitiOrganisasiResponseDto>> GetPindahAktivitiOrganisasi(int IdAktivitiOrganisasi);
        Task<object> GetNamaKodAktivitiOrganisasi(int IdIndukAktivitiOrganisasi);

        Task<bool> SetAktivitiOrganisasi(AktivitiOrganisasiRequestDto aktivitiOrganisasiRequestDto);
        //Amar Code End

        //Amar Code Start17/07/25
      
        Task<List<StrukturAktivitiOrganisasiResponseDto>> GetMansuhAktivitiOrganisasi(int IdAktivitiOrganisasi);
        Task<bool> SetMansuhAktivitiOrganisasi(MansuhAktivitiOrganisasiRequestDto mansuhAktivitiOrganisasiRequestDto);
        Task<List<AktivitiOrganisasiButiranJawatanResponseDto>> GeTreeButiranJawatan(string KodChartaOrganisasi);
       
        //Amar Code End

    }
}
