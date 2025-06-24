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
}
