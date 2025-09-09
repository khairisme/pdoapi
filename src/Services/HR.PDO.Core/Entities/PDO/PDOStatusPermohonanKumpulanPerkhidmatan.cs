using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_StatusPermohonanKumpulanPerkhidmatan")] // This is the key
    public class PDOStatusPermohonanKumpulanPerkhidmatan :  PDOBaseEntity
    {
        public int? IdKumpulanPerkhidmatan { get; set; }
        public string KodRujStatusPermohonan { get; set; }
        public DateTime TarikhKemaskini { get; set; }
    }
}
