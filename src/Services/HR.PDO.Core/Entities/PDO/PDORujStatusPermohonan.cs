using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_RujStatusPermohonan")] // This is the key
    public class PDORujStatusPermohonan : PDOBaseEntity
    {
       
  
        public string Kod { get; set; } = null!;

        public string Nama { get; set; }
        public string Keterangan { get; set; }
        
    }
}
