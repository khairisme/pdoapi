using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities.PDO
{
    [Table("PDO_SkimKetuaPerkhidmatan")]
    public class PDOSkimKetuaPerkhidmatan : PDOBaseEntity
    {
        public int IdSkimPerkhidmatan { get; set; }
        public int IdJawatan { get; set; }
        [NotMapped]
        public int Id { get; set; }
    }
}
