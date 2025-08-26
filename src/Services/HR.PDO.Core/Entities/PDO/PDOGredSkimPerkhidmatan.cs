using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_GredSkimPerkhidmatan")] // This is the key
    public class PDOGredSkimPerkhidmatan : PDOBaseEntity
    {
        public int IdGred { get; set; }
        public int IdSkimPerkhidmatan { get; set; }
        [NotMapped]
        public int Id { get; set; }
        [NotMapped]
        public bool StatusAktif { get; set; }
        [NotMapped]
        public DateTime TarikhCipta { get; set; }
        [NotMapped]
        public Guid IdCipta { get; set; }
        [NotMapped]
        public DateTime? TarikhPinda { get; set; }
        [NotMapped]
        public Guid? IdPinda { get; set; }
        [NotMapped]
        public Guid? IdHapus { get; set; }
        [NotMapped]
        public DateTime? TarikhHapus { get; set; }
    }
}
