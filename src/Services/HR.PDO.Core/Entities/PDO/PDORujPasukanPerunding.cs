using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_RujPasukanPerunding")]
    public class PDORujPasukanPerunding : PDOBaseEntity
    {
        [NotMapped]
        public new int Id { get; set; }

        public string Kod { get; set; }
        public string Nama { get; set; }
        public string? Keterangan { get; set; }
    }
}
