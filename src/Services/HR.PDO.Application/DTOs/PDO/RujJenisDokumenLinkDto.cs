using System;
namespace HR.PDO.Application.DTOs
{
    public class RujJenisDokumenLinkDto
    {
        public long? Id { get; set; }
        public int IdPermohonanJawatan { get; set; }
        public string JenisDokumen { get; set; }
        public string NamaDokumen { get; set; }
        public string? FormatDokumen { get; set; }
        public string? PautanDokumen { get; set; }

        public int? Saiz { get; set; }
        public bool? StatusAktif { get; set; }
        public Guid UserId { get; set; }
        public string? Keterangan { get; set; }
        public string? Kod { get; set; }
        public string? Nama { get; set; }
    }
}
