using System;
namespace HR.PDO.Application.DTOs
{
    public class PermohonanJawatanCarianDto
    {
        public int IdUnitOrganisasi { get; set; }
        public int? AgensiId { get; set; }
        public string? KodRujJenisPermohonan { get; set; }
        public string? KodRujStatusPermohonanJawatan { get; set; }
        public string? NomborRujukan { get; set; }
        public string? TajukPermohonan { get; set; }
    }
}
