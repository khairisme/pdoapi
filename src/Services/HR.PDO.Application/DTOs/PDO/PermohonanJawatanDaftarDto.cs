using System;
namespace HR.PDO.Application.DTOs
{
    public class PermohonanJawatanDaftarDto
    {
        public bool StatusAktif { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public Guid IdCipta { get; set; }
        public int IdAgensi { get; set; }
        public int IdUnitOrganisasi { get; set; }
        public string? Keterangan { get; set; }
        public string? KodRujJenisPermohonan { get; set; }
        public string? NomborRujukan { get; set; }
        public string? Tajuk { get; set; }
    }
}
