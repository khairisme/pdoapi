using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_StatusSkimPerkhidmatan")]
    public class PDOStatusSkimPerkhidmatan : PDOBaseEntity
    {
        public int? IdSkimPerkhidmatan { get; set; }
        public string KodRujStatusRekod { get; set; }
        public DateTime? TarikhKemaskini { get; set; }
    }
}
