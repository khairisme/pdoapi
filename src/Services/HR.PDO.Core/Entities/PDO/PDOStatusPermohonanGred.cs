using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_StatusPermohonanGred")]
    public class PDOStatusPermohonanGred : PDOBaseEntity
    {
       
        public int? IdGred { get; set; }
        public string KodRujStatusPermohonan { get; set; }
        public DateTime TarikhKemasKini { get; set; }
    }
}
