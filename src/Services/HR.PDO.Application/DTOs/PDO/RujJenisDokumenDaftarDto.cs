using System;
namespace HR.PDO.Application.DTOs
{
    public class RujJenisDokumenDaftarDto
    {
        public bool StatusAktif { get; set; }
        public DateTime? TarikhHapus { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public Guid? IdPinda { get; set; }
        public string? FormatDiterima { get; set; }
        public string? Keterangan { get; set; }
        public string? Kod { get; set; }
        public string? Nama { get; set; }
    }
}
