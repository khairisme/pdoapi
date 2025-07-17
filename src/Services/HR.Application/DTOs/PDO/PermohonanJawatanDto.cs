using Microsoft.Data.SqlClient.DataClassification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs.PDO
{
    public class PermohonanJawatanFilterDto
    {
        public string? NomborRujukan { get; set; }
        public string? Tajuk { get; set; }
        public string? KodRujStatusPermohonan { get; set; }
    }
    public class PermohonanJawatanSearchResponseDto
    {
        public int Id { get; set; }
        public string NomborRujukan { get; set; } = string.Empty;
        public string Tajuk { get; set; } = string.Empty;
        public DateTime? TarikhPermohonan { get; set; }
        public string KodRujStatusPermohonan { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
    public class PermohonanJawatanResponseDto
    {
        public int RecordId { get; set; }
        public string NomborRujukan { get; set; }
        public string TajukPermohonan { get; set; }
        public DateTime? TarikhPermohonan { get; set; }
        public string Status { get; set; }
    }

    public class PermohonanJawatanFilterDto2
    {
        public string? NomborRujukan { get; set; }
        public string? TajukPermohonan { get; set; }
        public string? KodStatusPermohonan { get; set; }
    }
    public class PermohonanPindaanFilterDto
    {
        public string? NomborRujukan { get; set; }
        public string? TajukPermohonan { get; set; }
        public string? KodStatusPermohonan { get; set; }
    }
    public class PermohonanPindaanResponseDto
    {
        public int RecordId { get; set; }
        public string NomborRujukan { get; set; }
        public string TajukPermohonan { get; set; }
        public DateTime? TarikhPermohonan { get; set; }
        public string Status { get; set; }
    }

    public class PermohonanJawatanDto
    {
        public string Agensi { get; set; }
        public string NomborRujukan { get; set; }
        public string Tajuk { get; set; }
        public DateTime TarikhPermohonan { get; set; }
        public string Status { get; set; }
    }
    public class PermohonanJawatanRequestDto
    {
        public int AgensiId { get; set; }
        public string? NoRujukan { get; set; }
        public string? Tajuk { get; set; }
        public string? StatusKod { get; set; }
    }

    public class PermohonanJawatanListDto
    {
        public int IdPermohonanJawatan { get; set; }
        public int IdUnitOrganisasi { get; set; }
        public int IdAgensi { get; set; }
        public string Agensi { get; set; }
        public string NomborRujukan { get; set; }
        public string JenisPermohonan { get; set; }
        public string TajukPermohonan { get; set; }
        public DateTime TarikhPermohonan { get; set; }
        public string Status { get; set; }
    }

    //Amar Code Start
    public class SalinanAsaFilterDto
    {
        public int AgensiId { get; set; }
        public string NoRujukan { get; set; }
        public string TajukPermohonan { get; set; }
        public string StatusPermohonan { get; set; }
    }
    public class SalinanAsaResponseDto
    {
        public int Bil { get; set; }
        public string Agensi { get; set; }
        public string NomborRujukan { get; set; }
        public string Tajuk { get; set; }
        public DateTime? TarikhPermohonan { get; set; }
        public string Status { get; set; }
        
    }

    public class SalinanBaharuResponseDto
    {
        public int Bil { get; set; }
        public string Agensi { get; set; }

        public string NomborRujukan { get; set; }
        public string Tajuk { get; set; }
        public DateTime? TarikhPermohonan { get; set; }
        public string Status { get; set; }
       

    }

    public class UlasanPasukanPerundingRequestDto
    {
      
        public string KodRujJenisPermohonan { get; set; }
        public string? KodRujJenisPermohonanPP { get; set; }
        public int? AgensiId { get; set; }
        public int IdPermohonanJawatan { get; set; }
        public int  IdPermohonanJawatanBaharu { get; set; }
        public string Ulasan { get; set; }



    }
    public class SenaraiPermohonanPerjawatanFilterDto
    {

        public string NomborRujukan { get; set; }
        public string TajukPermohonan { get; set; }
        public string StatusPermohonan { get; set; }
    }
    public class SenaraiPermohonanPerjawatanResponseDto
    {
        public int Bil { get; set; }
        public int Id { get; set; }
        public string NomborRujukan { get; set; }
        public string Tajuk { get; set; }
        public DateTime? TarikhPermohonan { get; set; }
        public string KodRujStatusPermohonan { get; set; }
        public string Status { get; set; }

       
    }
    //Amar Code End


}
