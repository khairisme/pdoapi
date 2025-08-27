using System;
namespace HR.PDO.Application.DTOs
{
    public class PermohonanJawatanLinkDto
    {
        public DateTime? TarikhCadanganWaran { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public DateTime? TarikhHapus { get; set; }
        public DateTime? TarikhPermohonan { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public DateTime? TarikhWaranDiluluskan { get; set; }
        public Guid? IdCipta { get; set; }
        public Guid? IdHapus { get; set; }
        public Guid? IdPinda { get; set; }
        public int Id { get; set; }
        public int IdAgensi { get; set; }
        public int IdUnitOrganisasi { get; set; }
        public string? Agensi { get; set; }
        public string? JenisPermohonan { get; set; }
        public string? Keterangan { get; set; }
        public string? NomborRujukan { get; set; }
        public string? NoWaranPerjawatan { get; set; }
        public string? PasukanPerunding { get; set; }
        public string? Tajuk { get; set; }
    }
}
