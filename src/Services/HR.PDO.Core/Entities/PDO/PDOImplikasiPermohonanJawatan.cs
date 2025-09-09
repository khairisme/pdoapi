using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_ImplikasiPermohonanJawatan")]
    public class PDOImplikasiPermohonanJawatan : PDOBaseEntity
    {
        public DateTime TarikhCipta { get; set; }
        public DateTime TarikhHapus { get; set; }
        public DateTime TarikhPinda { get; set; }
        public int Id { get; set; }
        public int IdPermohonanJawatan { get; set; }
        public decimal ImplikasiKewangan { get; set; }
        public decimal JumlahKosMansuh { get; set; }
        public decimal JumlahKosWujud { get; set; }
        public decimal JumlahSumber { get; set; }
        public decimal SumberKewanganBersepadu { get; set; }
        public decimal SumberKewanganNegeri { get; set; }
        public Guid IdCipta { get; set; }
        public Guid IdHapus { get; set; }
        public Guid IdPinda { get; set; }
    }
}
