using System.Collections.Generic;
using System.Threading.Tasks;
using HR.PDO.Application.DTOs;
namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IRujukanExt
    {
        public Task<List<DropDownDto>> RujukanAgensi();
        public Task<List<DropDownDto>> RujukanStatusPermohonanJawatan(string KodRujPeranan);
        public Task<List<DropDownDto>> RujukanJenisPermohonan();
        public Task<List<DropDownDto>> RujukanStatusKelulusanJawatan();
        public Task<List<DropDownDto>> RujukanStatusSemakanPOAPWN();
        public Task<List<DropDownDto>> RujukanStatusPersetujuan();
        public Task<List<DropDownDto>> RujukanStatusPindaanWPSKP();
        public Task<List<DropDownDto>> RujukanStatusLengkap();
        public Task<List<DropDownDto>> RujukanStatusPindaan();
        public Task<List<DropDownDto>> RujukanStatusSokongan();
        public Task<List<DropDownDto>> RujukanStatusSemakan();
        public Task<List<DropDownDto>> RujukanStatusKajianSemula();
        public Task<List<DropDownDto>> RujukanStatusPerakuan();
        public Task<List<DropDownDto>> RujukanJenisAgensi();
        public Task<List<DropDownDto>> RujukanJenisDokumen();
        public Task<List<DropDownDto>> RujukanPasukanPerunding();
        public Task<List<DropDownDto>> RujukanKlasifikasiPerKhidmatan();
        public Task<List<DropDownDto>> RujukanPangkat();
        public Task<List<DropDownDto>> RujukanGelaranJawatan();
        public Task<List<DropDownDto>> RujukanUrusanPerkhidmatan();
        public Task<List<DropDownDto>> RujukanJenisMesyuarat();
        public Task<List<DropDownDto>> RujukanKategoriUnitOrganisasi();
        public Task<List<DropDownDto>> RujukanJenisJawatan();
        public Task<List<DropDownDto>> RujukanKetuaPerkhidmatan(int IdSkimPerkhidmatan);
        public Task<List<DropDownDto>> RujukanKumpulanPerkhidmatan();
    }
}
