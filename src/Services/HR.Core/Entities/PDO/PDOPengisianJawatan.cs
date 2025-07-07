using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities.PDO
{
    [Table("PDO_PengisianJawatan")]
    public class PDOPengisianJawatan : PDOBaseEntity
    {
        public int IdPermohonanPengisian { get; set; }
        public int IdJawatanSebenar { get; set; }
        public Guid? IdPemilikKompetensi { get; set; }
    }
}
