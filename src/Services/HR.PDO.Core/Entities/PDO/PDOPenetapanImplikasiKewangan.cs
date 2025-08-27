using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_PenetapanImplikasiKewangan")]
    public class PDOPenetapanImplikasiKewangan : PDOBaseEntity
    {
        public int Id { get; set; }
        public int IdGred { get; set; }
        public int IdSkimPerkhidmatan { get; set; }
        public bool? IndikatorTBK1_2 { get; set; }
        public bool? IndikatorTBK2 { get; set; }
        public decimal? GajiPertengahan { get; set; }
        public decimal? ElaunBSH { get; set; }
        public decimal? ImplikasiKosSebulan { get; set; }
        public decimal? ImplikasiKosSetahun { get; set; }
        public string? ButiranKemaskini { get; set; }
        public bool? StatusAktif { get; set; }
        public Guid? IdCipta { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public Guid? IdPinda { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public Guid? IdHapus { get; set; }
        public DateTime? TarikhHapus { get; set; }
    }
}
