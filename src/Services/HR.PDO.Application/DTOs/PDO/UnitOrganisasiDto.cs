using System;
namespace HR.PDO.Application.DTOs
{
    public class UnitOrganisasiDto
    {
        public string? UnitOrganisasiInduk { get; set; }
        public bool? IndikatorAgensi { get; set; }
        public bool? IndikatorAgensiRasmi { get; set; }
        public bool? IndikatorJabatanDiKerajaanNegeri { get; set; }
        public bool? IndikatorPemohonPerjawatan { get; set; }
        public bool? StatusAktif { get; set; }
        public DateTime? TarikhAkhirPengukuhan { get; set; }
        public DateTime? TarikhAkhirPenyusunan { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public DateTime? TarikhHapus { get; set; }
        public DateTime? TarikhPenubuhan { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public Guid? IdCipta { get; set; }
        public Guid? IdHapus { get; set; }
        public Guid? IdPinda { get; set; }
        public int Id { get; set; }
        public int IdAsal { get; set; }
        public int IdIndukUnitOrganisasi { get; set; }
        public int Tahap { get; set; }
        public string? ButiranKemaskini { get; set; }
        public string? Keterangan { get; set; }
        public string? Kod { get; set; }
        public string? KodCartaOrganisasi { get; set; }
        public string? KodJabatan { get; set; }
        public string? KodKementerian { get; set; }
        public string? KodRujJenisAgensi { get; set; }
        public string? KodRujKategoriUnitOrganisasi { get; set; }
        public string? KodRujKluster { get; set; }
        public string? Nama { get; set; }
        public string? SejarahPenubuhan { get; set; }
        public string? Singkatan { get; set; }
    }
}
