using System;
namespace HR.PDO.Application.DTOs
{
    public class PermohonanJawatanDaftarDto
    {
        public DateTime? TarikhCipta { get; set; }
        public Guid UserId { get; set; }
        public int IdAgensi { get; set; }
        public string? Keterangan { get; set; }
        public string? KodRujJenisPermohonan { get; set; }
        public string? NomborRujukan { get; set; }
        public string? Tajuk { get; set; }
    }
    public class UlasanRequestDto
    {
        public int IdPermohonanJawatan {  get; set; }
        public Guid? UserId { get; set; }
        public string? KodRujStatusPermohonanJawatan { get; set; }
        public string? Ulasan { get; set; }
    }

}
