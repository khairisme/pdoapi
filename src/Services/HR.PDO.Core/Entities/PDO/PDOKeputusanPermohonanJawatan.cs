using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_KeputusanPermohonanJawatan")] // This is the key
    public class PDOKeputusanPermohonanJawatan : PDOBaseEntity
    {
       
        public int? Id { get; set; }
        public int? IdPermohonanJawatan { get; set; }
        public string? KodRujKeputusanPermohonan { get; set; }
        public DateTime? TarikhKeputusanPermohonan { get; set; }
        public string? Ulasan { get; set; }
        public bool? StatusAktif { get; set; }
        public Guid? IdCipta { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public DateTime? IdPinda { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public int? dHapus { get; set; }
        public DateTime? TarikhHapus { get; set; }
    }
}
