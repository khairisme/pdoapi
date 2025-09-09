using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("ONB_Sandangan")]
    public class ONBSandangan : PDOBaseEntity
    {
        public int Id { get; set; }
        public Guid? IdPemilikKompetensi { get; set; }
        public int? IdJawatan { get; set; }
        public DateTime? TarikhMulaSandangan { get; set; }
        public DateTime? TarikhTamatSandangan { get; set; }
        public Guid? IdCipta { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public Guid? IdPinda { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public Guid? IdHapus { get; set; }
        public DateTime? TarikhHapus { get; set; }
    }
}
