using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities.PDO
{
    [Table("PDO_KetuaPerkhidmatan")]
    public class PDOKetuaPerkhidmatan : PDOBaseEntity
    {
        public int IdJawatan { get; set; }
        public string NamaJawatanKetuaPerkhidmatan { get; set; }
        public DateTime TarikhMula { get; set; }
        public DateTime? TarikhLuput { get; set; }
        [NotMapped]
        public bool StatusAktif { get; set; }

    }
}
