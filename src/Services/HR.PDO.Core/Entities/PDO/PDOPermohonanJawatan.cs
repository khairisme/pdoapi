using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_PermohonanJawatan")]
    public class PDOPermohonanJawatan : PDOBaseEntity
    {
        public int Id { get; set; }
        public int IdUnitOrganisasi { get; set; }
        public int IdAgensi { get; set; }
        public string? KodRujJenisPermohonan { get; set; }
        public string? KodRujJenisPermohonanJPA { get; set; }
        public string? NomborRujukan { get; set; }
        public string? Tajuk { get; set; }
        public string? Keterangan { get; set; }
        public string? KodRujPasukanPerunding { get; set; }
        public string? NoWaranPerjawatan { get; set; }
        public DateTime? TarikhPermohonan { get; set; }
        public DateTime? TarikhCadanganWaran { get; set; }
        public DateTime? TarikhWaranDiluluskan { get; set; }
        public Guid? IdCipta { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public Guid? IdPinda { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public Guid? IdHapus { get; set; }
        public DateTime? TarikhHapus { get; set; }
    }
}
