using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_ButiranPermohonan")]
    public class PDOButiranPermohonan : PDOBaseEntity
    {
        public int? IdPermohonanJawatan { get; set; }
        public int? IdAktivitiOrganisasi { get; set; }
        public string? KodRujStatusJawatan { get; set; }
        public string? KodRujStatusBekalan { get; set; }
        public DateTime? TarikhMula { get; set; }
        public DateTime? TarikhTamat { get; set; }
        public string? KodRujJenisJawatan { get; set; }
        public string? KodRujGelaranJawatan { get; set; }
        public string? KodRujPangkatBadanBeruniform { get; set; }
        public string? TagJawatan { get; set; }
        public bool? IndikatorJawatanStrategik { get; set; }
        public bool? IndikatorJawatanSensitif { get; set; }
        public bool? IndikatorJawatanKritikal { get; set; }
        public string? NoButiran { get; set; }
        public string? AnggaranTajukJawatan { get; set; }
        public string? ButirPerubahan { get; set; }
        public int? BilanganJawatan { get; set; }
        public short? TahunButiran { get; set; }
        public bool? IndikatorTBK { get; set; }
        public bool? IndikatorHBS { get; set; }
        //public bool? IndikatorPermohonan { get; set; }
        public decimal? JumlahKosSebulan { get; set; }
        public decimal? JumlahKosSetahun { get; set; }
        public int? IndikatorPemohon { get; set; }
        public string? KodRujUrusanPerkhidmatan { get; set; }
        //public string? KodBidangPengkhususan { get; set; }
        //public string? KodRujLaluanKemajuanKerjaya { get; set; }
        
        public int? IdButiranPermohonanLama { get; set; }
        
        public Guid? IdPemilikKompetensi { get; set; }
        public string? NamaPemilikKompetensi { get; set; }
        public string? NoKadPengenalanPemilikKompetensi { get; set; }
        public int? IdSkimPerkhidmatanPemilikKompetensi { get; set; }
        public int? IdGredPemilikKompetensi { get; set; }
        public string? KodRujTujuanTambahSentara { get; set; }
        public int? IndikatorRekod { get; set; }
        public string? ButiranKemaskini { get; set; }
    }
}
