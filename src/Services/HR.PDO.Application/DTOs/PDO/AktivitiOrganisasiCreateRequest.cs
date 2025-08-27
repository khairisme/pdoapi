using System;
namespace HR.PDO.Application.DTOs
{
    public class AktivitiOrganisasiCreateRequest
    {
        public bool? StatusAktif { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public DateTime? TarikhHapus { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public Guid IdCipta { get; set; }
        public Guid IdHapus { get; set; }
        public Guid IdPinda { get; set; }
        public int Id { get; set; }
        public int IdAktivitiOrganisasi { get; set; }
        public int IdAsal { get; set; }
        public int IdIndukAktivitiOrganisasi { get; set; }
        public int Tahap { get; set; }
        public string? ButiranKemaskini { get; set; }
        public string? Keterangan { get; set; }
        public string? Kod { get; set; }
        public string? KodCartaAktiviti { get; set; }
        public string? KodProgram { get; set; }
        public string? KodRujKategoriAktivitiOrganisasi { get; set; }
        public string? Nama { get; set; }
    }
}
