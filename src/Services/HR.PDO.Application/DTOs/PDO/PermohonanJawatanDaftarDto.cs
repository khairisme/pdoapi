using System;
namespace HR.PDO.Application.DTOs
{
    public class PermohonanJawatanDaftarDto
    {

        public int Id { get; set;  }
        public DateTime? TarikhCipta { get; set; }
        public Guid UserId { get; set; }
        public int IdAgensi { get; set; }
        public string? Keterangan { get; set; }
        public string? KodRujJenisPermohonan { get; set; }
        public string? NomborRujukan { get; set; }
        public string? Tajuk { get; set; }
    }
    public class SemakPermohonanJawatanRequestDto
    {

        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string? KodRujJenisPermohonan { get; set; }
        public string? KodRujPasukanPerunding { get; set; }
        public DateTime? TarikhCadanganWaran { get; set; }
    }
    public class UlasanRequestDto
    {
        public int IdPermohonanJawatan {  get; set; }
        public Guid? UserId { get; set; }
        public string? KodRujStatusPermohonanJawatan { get; set; }
        public string? Ulasan { get; set; }
    }
    public class HantarRequestDto
    {
        public Guid? UserId { get; set; }
        public int IdPermohonanJawatan { get; set; }
    }
    public class UlasanStatusJenisRequestDto
    {
        public int IdPermohonanJawatan { get; set; }
        public Guid? UserId { get; set; }
        public string? KodRujStatusPermohonanJawatan { get; set; }
        public string? KodRujJenisPermohonanAgensi { get; set; }
        public string? KodRujJenisPermohonanPasukanPerunding { get; set; }
        public string? Ulasan { get; set; }
    }
    public class UlasanStatusKeputusanRequestDto
    {
        public int IdPermohonanJawatan { get; set; }
        public Guid? UserId { get; set; }
        public string? KodRujStatusPermohonanJawatan { get; set; }
        public string? KodRujKeputusanPermohonan { get; set; }
        public string? KodRujJenisPermohonanAgensi { get; set; }
        
        public string? Ulasan { get; set; }
    }

}
