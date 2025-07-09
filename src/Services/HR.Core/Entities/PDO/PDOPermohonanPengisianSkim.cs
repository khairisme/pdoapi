using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities.PDO
{
    [Table("PDO_PermohonanPengisianSkim")]
    public class PDOPermohonanPengisianSkim : PDOBaseEntity
    {
        [NotMapped]
        public new bool StatusAktif { get; set; }

        public int IdPermohonanPengisian { get; set; }
        public int IdSkimPerkhidmatan { get; set; }
        public int BilanganPengisian { get; set; }
        public int BilanganHadSIling { get; set; }
       public string Ulasan { get; set; }
    }
}
