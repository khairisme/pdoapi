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


}
