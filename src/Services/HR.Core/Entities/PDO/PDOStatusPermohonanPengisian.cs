using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities.PDO
{
    [Table("PDO_StatusPermohonanPengisian")]
    public class PDOStatusPermohonanPengisian : PDOBaseEntity
    {
        public int IdPermohonanPengisian { get; set; }
        public string KodRujStatusPermohonan { get; set; }
        public DateTime TarikhStatusPermohonan { get; set; }
    }
}
