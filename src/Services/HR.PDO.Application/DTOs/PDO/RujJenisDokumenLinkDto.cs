using System;
namespace HR.PDO.Application.DTOs
{
    public class RujJenisDokumenLinkDto
    {
        public int IdPermohonanJawatan { get; set; }
        public string JenisDokumen { get; set; }
        public string NamaDokumen { get; set; }
        public string? FormatDokumen { get; set; }
        public string? PautanDokumen { get; set; }
        public bool? StatusAktif { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public DateTime? TarikhHapus { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public Guid? IdCipta { get; set; }
        public Guid? IdHapus { get; set; }
        public Guid? IdPinda { get; set; }
        public string? Keterangan { get; set; }
        public string? Kod { get; set; }
        public string? Nama { get; set; }
    }
}
