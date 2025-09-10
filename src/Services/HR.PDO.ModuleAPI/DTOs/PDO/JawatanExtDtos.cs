using System;
using System.ComponentModel;
namespace HR.PDO.ModuleAPI.DTOs
{
    public class JawatanListDto
    {
        public List<int?> IdJawatanList {  get; set; }
    }
    public class ButiranJawatanRequestDto
    {
        public int? IdPermohonanJawatan { get; set; }
        public int? IdButiranPermohonan { get; set; }

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

        public string search {  get; set; }
    }
    public class JawatanLinkDto
    {
        public int IdJawatan { get; set; }
        public string? KodJawatan { get; set; }
        public string? NamaJawatan { get; set; }
        public string? UnitOrganisasi { get; set; }
        public string? NamaPenyandang { get; set; }
    }
}
