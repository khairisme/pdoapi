using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_GredSkimJawatan")]
    public class PDOGredSkimJawatan : PDOBaseEntity
    {
        [NotMapped]
        public new int Id { get; set; }

        [NotMapped]
        public new bool StatusAktif { get; set; }

        [NotMapped]
        public new DateTime TarikhCipta { get; set; }

        [NotMapped]
        public new Guid IdCipta { get; set; }

        [NotMapped]
        public new DateTime? TarikhPinda { get; set; }

        [NotMapped]
        public new Guid? IdPinda { get; set; }

        [NotMapped]
        public new Guid? IdHapus { get; set; }

        [NotMapped]
        public new DateTime? TarikhHapus { get; set; }

        public int IdJawatan { get; set; }
        public int IdGred { get; set; }
        public int IdSkimPerkhidmatan { get; set; }
    }
}
