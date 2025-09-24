using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_RujLaluanKemajuanKerjaya")]
    public class PDORujLaluanKemajuanKerjaya : PDOBaseEntity
    {
        public string? Nama { get; set; }
        public string? Kod { get; set; }
        public string? Keterangan { get; set; }
        public string? IdPinda { get; set; }
        public string? IdHapus { get; set; }
        public string? IdCipta { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public DateTime? TarikhHapus { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public bool? StatusAktif { get; set; }
     }
}
