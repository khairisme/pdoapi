using HR.PDO.Core.Entities.PDO;
using System;
using System.ComponentModel;
namespace HR.PDO.Application.DTOs
{
    public class KemaskiniSemakUnitOrganisasiRequestDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string? NamaUnitOrganisasi { get; set; }
        public string? KodRujKategoriUnitOrganisasi { get; set; }
        public string? Keterangan { get; set; }
    }

    public class UnitOrganisasiDaftarDto
    {
        public Guid UserId { get; set; }
        public int Id { get; set; }
        public bool? IndikatorAgensi { get; set; }
        public bool? IndikatorAgensiRasmi { get; set; }
        public bool? IndikatorJabatanDiKerajaanNegeri { get; set; }
        public bool? IndikatorPemohonPerjawatan { get; set; }
        public bool? StatusAktif { get; set; }
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
        public DateTime? TarikhPenubuhan { get; set; }
    }
    public class StrukturUnitOrganisasiRequestDto
    {
        [DefaultValue(1)]
        public int IdPermohonanJawatan{ get; set; }

        [DefaultValue(1)]
        public int IdAktivitiOrganisasi { get; set; } 

        [DefaultValue("7215")]
        public string? KodCartaOrganisasi { get; set; }
        public int ParentId { get; set; } = 0;
    
        [DefaultValue(1)]
        public int Page { get; set; } = 1;
        [DefaultValue(50)]
        public int PageSize { get; set; } = 50;
        public string? Keyword { get; set; }
        [DefaultValue("UnitOrganisasi")]
        public string? SortBy { get; set; } = "UnitOrganisasi";
        [DefaultValue(false)]
        public bool? ResultChild { get; set; }
        public bool Desc { get; set; } = false;
    }
    public class HapusTerusUnitOrganisasiRequestDto
    {
        public Guid UserId { get; set; }
        public int Id { get; set; }
    }

}
