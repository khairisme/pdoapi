using System;
namespace HR.PDO.Application.DTOs
{
    public class StrukturAktivitiOrganisasiResponseDto
    {
        public bool? StatusAktif { get; set; }
        public Guid UserId { get; set; }
        public int Id { get; set; }
        public int IdAsal { get; set; }
        public int IdIndukAktivitiOrganisasi { get; set; }
        public int Tahap { get; set; }
        public string? ButiranKemaskini { get; set; }
        public string? FullPath { get; set; }
        public string? Keterangan { get; set; }
        public string? Kod { get; set; }
        public string? KodCartaAktiviti { get; set; }
        public string? KodProgram { get; set; }
        public string? KodRujKategoriAktivitiOrganisasi { get; set; }
        public string? Nama { get; set; }
    }
}
