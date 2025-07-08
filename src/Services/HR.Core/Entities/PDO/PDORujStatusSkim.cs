using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities.PDO
{
    [Table("PDO_RujStatusSkim")] // This is the key
    public class PDORujStatusSkim : PDOBaseEntity
    {
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
    }
}
