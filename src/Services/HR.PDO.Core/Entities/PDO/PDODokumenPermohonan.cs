using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_DokumenPermohonan")]
    public class PDODokumenPermohonan : PDOBaseEntity
    {
        public long? Id { get; set; }
        public int IdPermohonanJawatan { get; set; }
        public string? KodRujJenisDokumen { get; set; }
        public string? NamaDokumen { get; set; }
        public string? PautanDokumen { get; set; }
        public string? FormatDokumen { get; set; }
        public int Saiz { get; set; }
        public Guid? IdCipta { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public Guid? IdPinda { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public Guid? IdHapus { get; set; }
        public DateTime? TarikhHapus { get; set; }
    }
}
