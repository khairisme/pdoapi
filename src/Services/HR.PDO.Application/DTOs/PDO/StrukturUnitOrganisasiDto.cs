using System;
namespace HR.PDO.Application.DTOs
{
    public class StrukturUnitOrganisasiDto
    {
        public bool HasChildren { get; set; }
        public int Id { get; set; }
        public int IdIndukUnitOrganisasi { get; set; }
        public int Tahap { get; set; }
        public string? KategoriUnitOrganisasi { get; set; }
        public string? Kod { get; set; }
        public string? UnitOrganisasi { get; set; }
    }
}
