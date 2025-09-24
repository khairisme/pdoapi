using HR.PDO.Core.Entities.PDO;
using System;
namespace HR.PDO.Application.DTOs
{
    public class PermohonanJawatanLinkDto
    {
        public int Id { get; set; }
        public string? NomborRujukan { get; set; }
        public string? TajukPermohonan { get; set; }
        public string? TarikhPermohonan { get; set; }
        public string? Status { get; set; }
        public int IdAgensi { get; set; }
        public int IdUnitOrganisasi { get; set; }
        public string? Agensi { get; set; }
        public string? JenisPermohonan { get; set; }
        public string? Keterangan { get; set; }
        public string? PasukanPerunding { get; set; }
        public PDOStatusPermohonanJawatan StatusPermohonanJawatan { get; set; }
    }
}
