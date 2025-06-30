using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities.PDO
{
    [Table("PDO_RujJenisPermohonan")]
   public class PDORujJenisPermohonan: PDOBaseEntity
    {
        public string Kod { get; set; } // PK, char(2), not null
        public string Nama { get; set; } // varchar(30), not null
        public string? Keterangan { get; set; } // varchar(200), null
    }
}
