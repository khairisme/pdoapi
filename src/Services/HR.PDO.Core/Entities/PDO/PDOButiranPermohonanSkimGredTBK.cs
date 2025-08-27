using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_ButiranPermohonanSkimGredTBK")]
    public class PDOButiranPermohonanSkimGredTBK : PDOBaseEntity
    {
        public int IdButiranPermohonan { get; set; }
        public int IdSkimPerkhidmatan { get; set; }
        public int IdGred { get; set; }
        public Guid? IdCipta { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public Guid? IdPinda { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public Guid? IdHapus { get; set; }
        public DateTime? TarikhHapus { get; set; }
    }
}
