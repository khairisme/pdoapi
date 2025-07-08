using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities.PDO
{
    [Table("PDO_PermohonanPengisian")]
    public class PDOPermohonanPengisian : PDOBaseEntity
    {
       
        public int IdUnitOrganisasi { get; set; }
        public string NomborRujukan { get; set; }
        public string Tajuk { get; set; }
        public string? Keterangan { get; set; }
        public DateTime? TarikhPermohonan { get; set; }
        [NotMapped]
        public new bool StatusAktif { get; set; }
    }
}
