using System;
namespace HR.PDO.Application.DTOs
{
    public class PermohonanJawatanLinkDto
    {
        public int Id { get; set; }
        public string? NomborRujukan { get; set; }
        public string? TajukPermohonan { get; set; }
        public DateTime? TarikhPermohonan { get; set; }
        public string? Status { get; set; }
        public int IdAgensi { get; set; }
        public int IdUnitOrganisasi { get; set; }
        public string? Agensi { get; set; }
        public string? JenisPermohonan { get; set; }
        public string? Keterangan { get; set; }
        public string? PasukanPerunding { get; set; }
    }
}
