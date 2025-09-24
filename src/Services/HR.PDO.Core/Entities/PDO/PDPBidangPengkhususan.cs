using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDP_BidangPengkhususan")]
    public class PDPBidangPengkhususan : PDOBaseEntity
    {
        public string? Kod { get; set; }
        public string? Nama { get; set; }
        public string? Keterangan { get; set; }
        public string? KodInduk { get; set; }

        public string? KodRujKategoriBidangPengkhususan { get; set; }

        public int? IdKetuaPerkhidmatan { get; set; }
        public bool? StatusAktif { get; set; }
        public string? ButiranKemasKini { get; set; }
        public Guid? IdCipta { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public Guid? IdPinda { get; set; }
        public string? TarikhPinda { get; set; }
        public string? IdHapus { get; set; }
        public string? TarikhHapus { get; set; }
        
        
            
      }
}
