using System;
using System.ComponentModel;
namespace HR.PDO.Application.DTOs
{
    public class UnitOrganisasiWujudDto
    {
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid? UserId { get; set; }
        [DefaultValue(0)]
        public int Tahap { get; set; }
        [DefaultValue("")]
        public string Keterangan { get; set; }
        [DefaultValue("")]
        public string Kod { get; set; }
        [DefaultValue("")]
        public string KodRujJenisAgensi { get; set; }
        [DefaultValue("")]
        public string Nama { get; set; }
        [DefaultValue("")]
        public string KodRujKategoriUnitOrganisasi { get; set; }
    }
}
