using System;
namespace HR.PDO.Application.DTOs
{
    public class StrukturAktivitiOrganisasiDto
    {
        public bool HasChildren { get; set; }
        public int Id { get; set; }
        public int IdIndukAktivitiOrganisasi { get; set; }
        public int Tahap { get; set; }
        public string? AktivitiOrganisasi { get; set; }
        public string? Kod { get; set; }
        public string? KodCartaAktiviti { get; set; }
        public string? KodProgram { get; set; }
    }
}
