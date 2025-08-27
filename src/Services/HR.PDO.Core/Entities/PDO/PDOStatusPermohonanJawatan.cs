using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_StatusPermohonanJawatan")]
    public class PDOStatusPermohonanJawatan : PDOBaseEntity
    {
        public int Id { get; set; }
        public int IdPermohonanJawatan { get; set; }
        public string? KodRujStatusPermohonanJawatan { get; set; }
        public DateTime? TarikhStatusPermohonan { get; set; }
        public string? Ulasan { get; set; }
        public bool? StatusAktif { get; set; }
        public Guid? IdCipta { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public Guid? IdPinda { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public Guid? IdHapus { get; set; }
        public DateTime? TarikhHapus { get; set; }
    }
}
