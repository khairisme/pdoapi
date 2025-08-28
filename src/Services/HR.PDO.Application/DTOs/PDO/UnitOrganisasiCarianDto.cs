using System;
namespace HR.PDO.Application.DTOs
{
    public class UnitOrganisasiCarianDto
    {
        public bool Desc { get; set; }
        public int IdIndukAktivitiOrganisasi { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Keterangan { get; set; }
        public string? Kod { get; set; }
        public string? KodUnitOrganisasi { get; set; }
        public string? Nama { get; set; }
        public string? NamaUnitOrganisasi { get; set; }
        public string? SortBy { get; set; }
    }
}
