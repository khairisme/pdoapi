using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_CadanganJawatan")]
    public class PDOCadanganJawatan : PDOBaseEntity
    {
        public int Id { get; set; }
        public int IdButiranPermohonan { get; set; }
        public string? SkimPerkhidmatan { get; set; }
        public string? Gred { get; set; }
        public string? KodRujJenisJawatan { get; set; }
        public string? KodRujStatusBekalan { get; set; }
        public string? KodRujStatusJawatan { get; set; }
        public int? IdAktivitiOrganisasi { get; set; }
        public int IdUnitOrganisasi { get; set; }
        public string? GelaranJawatan { get; set; }
        public string? Pangkat { get; set; }
        public string? Penyandang { get; set; }
        public bool? StatusAktif { get; set; }
        public Guid? IdCipta { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public Guid? IdPinda { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public Guid? IdHapus { get; set; }
        public DateTime? TarikhHapus { get; set; }
    }
}
