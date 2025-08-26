using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities
{
    [Table("PNS_PerkhidmatanLog")] // This is the key
    public class PerkhidmatanLog : PNSBaseEntity
    {
        public string IdPengguna { get; set; }
        public byte[] Gambar { get; set; }
        public string KataLaluan { get; set; }
        [NotMapped]
        public new bool StatusAktif { get; set; }
    }
}
