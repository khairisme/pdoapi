using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_KekosonganJawatan")]
    public class PDOKekosonganJawatan : PDOBaseEntity
    {
        public int IdJawatan { get; set; }
        public string KodRujStatusKekosonganJawatan { get; set; }
        public DateTime? TarikhStatusKekosongan { get; set; }
    }
}
