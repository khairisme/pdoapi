using System;
using System.ComponentModel;
namespace HR.PDO.Application.DTOs
{
    public class PermohonanJawatanCarianDto
    {
        [DefaultValue(0)]
        public int? IdUnitOrganisasi { get; set; }
        [DefaultValue(0)]
        public int? AgensiId { get; set; }
        [DefaultValue("")]
        public string? KodRujJenisPermohonan { get; set; }
        [DefaultValue("")]
        public string? KodRujStatusPermohonanJawatan { get; set; }
        [DefaultValue("")]
        public string? NomborRujukan { get; set; }
        [DefaultValue("")]
        public string? TajukPermohonan { get; set; }
        [DefaultValue("")]
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
