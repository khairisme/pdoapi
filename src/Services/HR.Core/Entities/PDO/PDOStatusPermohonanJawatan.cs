using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities.PDO
{
    [Table("PDO_StatusPermohonanJawatan")]
    public class PDOStatusPermohonanJawatan : PDOBaseEntity
    {
        public int IdPermohonanJawatan { get; set; }
        public string KodRujStatusPermohonan { get; set; } = null!;
        public DateTime TarikhStatusPermohonan { get; set; }
        public string KodRujStatusPermohonanJawatan { get; set; }
        public string UlasanStatusPermohonan { get; set; }
    }
}
