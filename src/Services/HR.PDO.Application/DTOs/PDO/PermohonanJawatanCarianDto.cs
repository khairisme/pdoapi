using System;
using System.ComponentModel;
namespace HR.PDO.Application.DTOs
{
    public class PermohonanJawatanCarianDto
    {
        public int? IdUnitOrganisasi { get; set; }
        public int? AgensiId { get; set; }
        public string? KodRujJenisPermohonan { get; set; }
        public string? KodRujStatusPermohonanJawatan { get; set; }
        public string? NomborRujukan { get; set; }
        public string? TajukPermohonan { get; set; }
        public string? Keyword { get; set; }

        [DefaultValue(1)]
        public int Page { get; set; } = 1;

        [DefaultValue(50)]
        public int PageSize { get; set; } = 10;

        [DefaultValue("TarikhPermohonan")]
        public string? SortBy { get; set; } // "NomborRujukan","Tajuk","Status","TarikhPermohonan"

        [DefaultValue(false)]
        public bool Desc { get; set; }

        [DefaultValue(true)]
        public bool IncludeChildren { get; set; } = true;
    }
}
