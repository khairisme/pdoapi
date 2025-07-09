using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities.PDO
{
    [Table("PDO_SkimPerkhidmatan")] // This is the key
    public class PDOSkimPerkhidmatan : PDOBaseEntity
    {
        public int IdKlasifikasiPerkhidmatan { get; set; }
        public int IdKumpulanPerkhidmatan { get; set; }
        public int? IdKetuaPerkhidmatan { get; set; } 
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string? Keterangan { get; set; }
        public bool IndikatorSkimKritikal { get; set; }
        public bool IndikatorKenaikanPGT { get; set; }
        public string? ButiranKemaskini { get; set; }
        public string? KodRujStatusSkim { get; set; }
        [NotMapped]
        public bool StatusAktif { get; set; }
        public bool? IndikatorSkim { get; set; }

        public string? KodRujMatawang { get; set; }

        public decimal? Jumlah { get; set; }
    }
}
