using System;
namespace HR.PDO.Application.DTOs
{
    public class StrukturAktivitiOrganisasiTempDto
    {
        public int Id { get; set; }
        public int IdIndukAktivitiOrganisasi { get; set; }
        public int Tahap { get; set; }
        public string? FullPath { get; set; }
        public string? KodCartaAktiviti { get; set; }
        public string? KodProgram { get; set; }
        public string? Nama { get; set; }
        public string? NamaAgensi { get; set; }
    }
}
