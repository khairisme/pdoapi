using HR.PDO.Core.Entities.PDO;
using System;
namespace HR.PDO.Application.DTOs
{
    public class StrukturUnitOrganisasiDto
    {
        public int? Id { get; set; }
        public int? IdIndukUnitOrganisasi { get; set; }
        public int? Tahap { get; set; }
        public List<StrukturUnitOrganisasiDto> Children { get; set; }
        public bool HasChildren { get; set; }
        public string? KategoriUnitOrganisasi { get; set; }
        public string? Kod { get; set; }
        public string? UnitOrganisasi { get; set; }

        public List<PDOButiranPermohonan> ButiranPermohonan { get; set; }
    }
}
