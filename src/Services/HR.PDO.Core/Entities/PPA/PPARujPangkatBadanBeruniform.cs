using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PPA_RujPangkatBadanBeruniform")]
    public class PPARujPangkatBadanBeruniform : PDOBaseEntity
    {
        public string? Kod { get; set; }
        public string? Nama { get; set; }
        public string? Keterangan { get; set; }
        public string? KodRujKategoriBadanBeruniform { get; set; }
        public bool? StatusAktif { get; set; }
        public Guid? IdCipta { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public Guid? IdPinda { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public Guid? IdHapus { get; set; }
        public DateTime? TarikhHapus { get; set; }
    }
}
