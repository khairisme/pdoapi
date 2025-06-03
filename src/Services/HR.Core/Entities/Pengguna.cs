using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities
{

    /// <summary>
    /// Pengguna(User) entity representing a user in the organization
    /// </summary>
     [Table("PNS_Pengguna")] // This is the key
    public class Pengguna : PNSBaseEntity
    {
        
      
        public int? IdPemilikKompetensi { get; set; }
        public string IdPengguna { get; set; }
        public string KodRujModDaftar { get; set; }
        public string KodRujJenisPengguna { get; set; }
        public DateTime? TarikhDaftar { get; set; }
        public string Emel { get; set; }
        
        public string KataLaluanHash { get; set; }
        public string KataLaluanSalt { get; set; }
        public DateTime? LogMasukTerakhir { get; set; }
        public DateTime? TukarKataLaluanTerakhir { get; set; }
        public int? BilCubaanLogMasuk { get; set; }
        public bool? AkaunDikunci { get; set; }
        public DateTime? SahDari { get; set; }
        public DateTime? SahHingga { get; set; }
        public bool? IndikatorSahEmel { get; set; }
        public string PautanPengesahanEmel { get; set; }
        public string KataLaluanSementara { get; set; }
       
       
    }
}
