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
}
