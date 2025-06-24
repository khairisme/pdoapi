using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities.PDO
{
    [Table("PDO_PermohonanJawatan")]
    public class PDOPermohonanJawatan : PDOBaseEntity
    {
    
        public int IdUnitOrganisasi { get; set; }
        public int IdAgensi { get; set; }
        public string KodRujJenisPermohonan { get; set; } = string.Empty;
        public string NomborRujukan { get; set; } = string.Empty;
        public string Tajuk { get; set; } = string.Empty;
        public string? Keterangan { get; set; }
        public string? KodRujPasukanPerunding { get; set; }
        public string? NoWaranPerjawatan { get; set; }
        public DateTime? TarikhPermohonan { get; set; }
        [NotMapped]
        public bool StatusAktif { get; set; }

    }
}
