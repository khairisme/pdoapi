using System;
namespace HR.PDO.Application.DTOs
{
    public class UnitOrganisasiDto
    {
        public int? Id { get; set; }
        public string? UnitOrganisasiInduk { get; set; }
        public bool? IndikatorAgensi { get; set; }
        public bool? IndikatorAgensiRasmi { get; set; }
        public bool? IndikatorJabatanDiKerajaanNegeri { get; set; }
        public bool? IndikatorPemohonPerjawatan { get; set; }
        public bool? StatusAktif { get; set; }
        public Guid UserId { get; set; }
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

    public class CarianUnitOrganisasiDto
    {
        public int? Id { get; set; }
        public string? Kod { get; set; }
        public string? NamaAgensi { get; set; }
    }

    public class AlamatUnitOrganisasiDto
    {
        public Guid? UserId { get; set; }
        public int? Id { get; set; }
        public int IdUnitOrganisasi { get; set; }
        
         public string? KodRujPoskod { get; set; }
        
        public string? KodRujNegara { get; set; }
        public string? KodRujNegeri { get; set; }
        public string? KodRujBandar { get; set; }
        
        public string? NomborTelefonPejabat { get; set; }
        public string? NomborFaksPejabat { get; set; }
        public string? LamanWeb { get; set; }
        public int? KoordinatUnitOrganisasi { get; set; }
        
        public string? Alamat1 { get; set; }
        public string? Alamat2 { get; set; }
        public string? Alamat3 { get; set; }
        public string? Bandar { get; set; }
        public string? Poskod { get; set; }
        public string? Negeri { get; set; }
        public string? Negara { get; set; }
        public string? TelefonPejabat { get; set; }
        public string? FaksPejabat { get; set; }
        public string? EmelPejabat { get; set; }
        public bool? StatusAktif { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public DateTime? TarikhHapus { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public Guid? IdCipta { get; set; }
        public Guid? IdHapus { get; set; }
        public Guid? IdPinda { get; set; }
        public string? Keterangan { get; set; }
    }
    public class MuatAlamatUnitOrganisasiDto
    {
        public List<DropDownDto> NegaraList { get; set; }
        public List<DropDownDto> NegeriList { get; set; }
        public List<DropDownDto> BandarList { get; set; }
    }

    public class NegaraDto 
    {
        public string? Kod { get; set; }
        public string? Nama { get; set; }
        public string? Keterangan { get; set; }

    }
    public class NegeriDto
    {
        public string? Kod { get; set; }
        public string? Nama { get; set; }
        public string? Keterangan { get; set; }

    }
    public class BandarDto
    {
        public string? Kod { get; set; }
        public string? Nama { get; set; }
        public string? Keterangan { get; set; }

    }
}
