using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities.PDO
{
    [Table("PDO_SkimKetuaPerkhidmatan")]
    public class PDOSkimKetuaPerkhidmatan
    {
        public int IdSkimPerkhidmatan { get; set; }
        public int IdKetuaPerkhidmatan { get; set; }
    }
}
