using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs.PDO
{
    public class PermohonanPengisianPOAFilterDto
    {
        public string? NoRujukan { get; set; }
        public string? Tajuk { get; set; }
        public string? StatusPermohonan { get; set; }
    }
    public class PermohonanPOAFilterResponseDto
    {
        public int Bil { get; set; }
        public int Id { get; set; }
        public string NomborRujukan { get; set; }
        public string Tajuk { get; set; }
        public DateTime TarikhPermohonan { get; set; }
        public string KodRujStatusPermohonan { get; set; }
        public string Status { get; set; }
    }

    public class SavePermohonanPengisianPOARequestDto
    {
        public int Id { get; set; } = 0;

        public int IdUnitOrganisasi { get; set; }
        public string NomborRujukan { get; set; }
        public string Tajuk { get; set; }
        public string? Keterangan { get; set; }
        public DateTime? TarikhPermohonan { get; set; }
        public List<SavePermohonanPengisianSkimDto> savePermohonanPengisianSkimDtos { get; set; } = new List<SavePermohonanPengisianSkimDto>();
    }
    public class SavePermohonanPengisianSkimDto
    {
        public int IdPermohonanPengisian { get; set; }
        public int IdSkimPerkhidmatan { get; set; }
        public int BilanganPengisian { get; set; }
    }
    public class PermohonanPengisianHeaderResponseDto
    {
        public string Agensi { get; set; }
        public string NomborRujukan { get; set; }
        public string Tajuk { get; set; }
        public string Keterangan { get; set; }
    }
    public class PermohonanPengisianfilterdto
    {
        public int IdPermohonanPengisian { get; set; }
        public int AgensiId { get; set; }
    }

    public class BilanganPermohonanPengisianResponseDto
    {
        public string KodSkim { get; set; }
        public string NamaSkimPerkhidmatan { get; set; }
        public int Bilangan { get; set; }
    }
    public class PermohonanPengisianPOAIFilterDto
    {
        public int? AgensiId { get; set; }
        public string? StatusPermohonan { get; set; }
    }
    public class PermohonanPOAIFilterResponseDto
    {

        public int Id { get; set; }
        public string NomborRujukan { get; set; }
        public string Agensi { get; set; }
        public string Tajuk { get; set; }
        public DateTime TarikhPermohonan { get; set; }
        public string KodRujStatusPermohonan { get; set; }
        public string Status { get; set; }
    }
    public class PermohonanPengisianJawatanFilterDto
    {
        public int? Kementerian { get; set; }
        public string StatusPermohonan { get; set; }
    }
    public class PermohonanPengisianJawatanResponseDto
    {
        public int Bil { get; set; }
        public int Id { get; set; }
        public string Kementerian { get; set; }
        public int BilanganPengisian { get; set; }
        public DateTime TarikhPermohonan { get; set; }
        public string Status { get; set; }
    }
    public class PenolongPegawaiTeknologiMaklumatResponseDto
    {

        public string KodJawatan { get; set; }
        public string NamaJawatan { get; set; }
        public string Gred { get; set; }

    }
    public class SkimNameWithJawatanDto
    {
        public int IdSkimPerkhidmatan { get; set; }
        public string Nama { get; set; }
        public List<PenolongPegawaiTeknologiMaklumatResponseDto> Data { get; set; }
    }
    public class PenolongPegawaiTeknologiMaklumatFilterDto
    {
        public int IdPermohonanPengisianSkim { get; set; }
        public int IdPermohonanPengisian { get; set; }
        public int AgensiId { get; set; }
    }

    public class PermohonanPengisianJawatanWithAgensiResponseDto
    {
        public int Bil { get; set; }
        public int Id { get; set; }
        public string KodJawatan { get; set; }
        public string NamaJawatan { get; set; }
        public string Gred { get; set; }
        public string Agensi { get; set; }
    }
    public class AgensiWithJawatanDto
    {
        public int AgensiId { get; set; }
        public string Kod { get; set; }
        public string NamaAgensi { get; set; }
        public List<PermohonanPengisianJawatanWithAgensiResponseDto> Data { get; set; }
    }

    public class SimulasiKewanganResponseDto
    {
        public string KodJawatan { get; set; }
        public string NamaJawatan { get; set; }
        public string Gred { get; set; }
        public decimal JumlahImplikasiKewanganSebulan { get; set; }
        public decimal JumlahImplikasiKewanganSetahun { get; set; }
    }
    public class SimulasiKewanganByPermohonanDto
    {
        public string KodJawatan { get; set; }
        public string NamaJawatan { get; set; }
        public string Gred { get; set; }
        public decimal JumlahImplikasiKewanganSebulan { get; set; }
        public decimal JumlahImplikasiKewanganSetahun { get; set; }
    }
    //Amar
    public class SenaraiJawatanSebenarFilterDto
    {
        public int AgensiId { get; set; }
        public int IdSkimPerkhidmatan { get; set; }
        public string KodJawatan { get; set; }
        public string KodStatusJawatan { get; set; }
    }
    //Amar
    public class SenaraiJawatanSebenarResponseDto
    {
        public int Bil { get; set; }
        public string KodJawatan { get; set; }
        public string NamaJawatan { get; set; }
        public string Gred { get; set; }
        public decimal JumlajImplikasiKewanganSebulan { get; set; }
        public decimal JumlajImplikasiKewanganSetahun { get; set; }
    }
    //Amar
    public class ImplikasiKewanganFilterDto
    {
        public int AgensiId { get; set; }
        public int IdSkimPerkhidmatan { get; set; }
        public string KodJawatan { get; set; }
        public string KodStatusJawatan { get; set; }
    }
    //Amar
    public class ImplikasiKewanganResponseDto
    {
        public int Bil { get; set; }
        public string KodJawatan { get; set; }
        public string NamaJawatan { get; set; }
        public string Gred { get; set; }
        public decimal JumlajImplikasiKewanganSebulan { get; set; }
        public decimal JumlajImplikasiKewanganSetahun { get; set; }
    }
    //Amar
    public class SenaraiPermohonanPengisianJawatanResponseDto
    {
        public int Bil { get; set; }
        public string Kementerian { get; set; }
        public int BilanganPengisian { get; set; }
        public DateTime? TarikhPermohonan { get; set; }
        public string Status { get; set; }
        
    }
    //Amar
    public class SenaraiPermohonanPengisianJawatanFilterDto
    {
        public int? Kementerian { get; set; }
        public string? StatusPermohonan { get; set; } = String.Empty;
    }

    //Amar
    public class BilanganPermohonanPengisianFilterDto
    {
        public int? AgensiId { get; set; }
        public string? NoRujukan { get; set; } = String.Empty;
        public string? TajukPermohonan { get; set; } = String.Empty;
        public DateTime? TarikhPermohonan { get; set; }
        public string? Keterangan { get; set; } = String.Empty;
        public int? HadSilingDitetapkan { get; set; }
        public string? StatusPermohonan { get; set; } = String.Empty;
    }
    //Amar
    public class BilanganPermohonanPengisianMaklumatPermohonanResponseDto
    {
        public int Id { get; set; }
        public string KodSkim { get; set; } = String.Empty;
        public string NamaSkimPerkhidmatan { get; set; } = String.Empty;
        public int BilanganPermohonanPengisian { get; set; }
        public int? HadSilingDitetapkan { get; set; }
        public string? Ulasan { get; set; } = String.Empty;
    }
    //Amar
    public class HantarBilanganPermohonanPengisianRowDto
    {
        public int Id { get; set; }
        public int BilanganHadSIling { get; set; }
        public string Ulasan { get; set; } = String.Empty;
    }
    //Amar
    public class HantarBilanganPermohonanPengisianRequestDto
    {
        public List<HantarBilanganPermohonanPengisianRowDto> Items { get; set; } = new List<HantarBilanganPermohonanPengisianRowDto>();
    }
    //Amar
    public class SenaraiJawatanSebenarGroupedAgencyResponseDto
    {
        public int IdUnitOrganisasi { get; set; }
        public string Kod { get; set; } = String.Empty;
        public string Agensi { get; set; } = String.Empty;
        public List<SenaraiJawatanSebenarAgencyDetailDto> SenaraiJawatan { get; set; } = new List<SenaraiJawatanSebenarAgencyDetailDto>();
    }

    //Amar
    public class SenaraiJawatanSebenarAgencyDetailDto
    {
        public int Bil { get; set; }
        public string KodJawatan { get; set; } = String.Empty;
        public string NamaJawatan { get; set; } = String.Empty;
        public string Gred { get; set; } = String.Empty;
    }

    //Amar
    public class ImplikasiKewanganJanaSimulasiKewanganFilterDto
    {
        public int AgensiId { get; set; }
        public int IdSkimPerkhidmatan { get; set; }
        public string KodJawatan { get; set; }
        public string KodStatusJawatan { get; set; }
    }
    //Amar
    public class ImplikasiKewanganJanaSimulasiKewanganResponseDto
    {
        public int Bil { get; set; }
        public string KodJawatan { get; set; }
        public string NamaJawatan { get; set; }
        public string Gred { get; set; }
        public decimal JumlajImplikasiKewanganSebulan { get; set; }
        public decimal JumlajImplikasiKewanganSetahun { get; set; }
    }

    //Nitya Code Start
    public class PermohonanPengisianDto
    {
        public string Kementerian { get; set; }
        public string NomborRujukan { get; set; }
        public string TajukPermhonan { get; set; }
        public string Keterangan { get; set; }
        public DateTime? TarikhPermohonan { get; set; }
        public int BilanganPermohonan { get; set; }
        public string Ulasan { get; set; }
    }
    public class SkimPerkhidmatanAgensiDto
    {
        public string KodSkim { get; set; }
        public string NamaSkimPerkhidmatan { get; set; }
        public int BilanganPengisian { get; set; }
    }
    public class PermohonanSkimDetailDto
    {
        public int RecordId { get; set; }
        public string KodSkim { get; set; }
        public string NamaSkimPerkhidmatan { get; set; }
        public int BilanganPengisian { get; set; }
        public int? BilanganHadSiling { get; set; }
    }
    public class MaklumatPermohonanDataDto
    {
        public string Agensi { get; set; }
        public string NoRujukan { get; set; }
        public string TajukPermohonan { get; set; }
        public DateTime? TarikhPermohonan { get; set; }
        public string Keterangan { get; set; }
        public int HadSilingDitetapkan { get; set; }
    }
    public class BilanganPermohonanPengisianDto
    {
        public string KodSkim { get; set; }
        public string NamaSkimPerkhidmatan { get; set; }
        public int BilanganPermohonanPengisian { get; set; }
        public int? HadSilingDitetapkan { get; set; }
        public string Ulasan { get; set; }
    }
    public class PermohonanUpdateDto
    {
        public int IdPermohonanPengisian { get; set; }
        public int AgensiId { get; set; }
        public string Tajuk { get; set; }
        public string Keterangan { get; set; }
        public List<SkimUpdateDto> GridItems { get; set; }
    }

    public class SkimUpdateDto
    {
        public int Id { get; set; }  // ppps.Id
        public int BilanganHadSiling { get; set; }
        public string Ulasan { get; set; }
    }
    public class JawatanKekosonganFilterDto
    {
        public int AgensiId { get; set; }
        public int IdSkimPerkhidmatan { get; set; }
        public string KodJawatan { get; set; }
        public string KodStatusJawatan { get; set; }
    }

    public class JawatanKekosonganDto
    {
        public string KodJawatan { get; set; }
        public string NamaJawatan { get; set; }
        public string UnitOrganisasi { get; set; }
        public string StatusPengisianJawatan { get; set; }
        public DateTime? TarikhKekosongan { get; set; }
    }
    public class MaklumatPermohonanFilterDto
    {
        public int? AgensiId { get; set; }
        public string? NoRujukan { get; set; }
    }
    public class MaklumatPermohonanDto
    {
        public string KodSkim { get; set; }
        public string NamaSkimPerkhidmatan { get; set; }
        public int? BilanganPermohonanPengisian { get; set; }
        public int? HadSilingDitetapkan { get; set; }
        public string? Ulasan { get; set; }
    }
    //Nita Code End
}
