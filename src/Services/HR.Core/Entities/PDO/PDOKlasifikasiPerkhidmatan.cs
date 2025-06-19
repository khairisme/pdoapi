using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Core.Entities.PDO
{

    [Table("PDO_KlasifikasiPerkhidmatan")] // This is the key
    public class PDOKlasifikasiPerkhidmatan : PDOBaseEntity
    {
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string? Keterangan { get; set; }
        public string? FungsiUtama { get; set; }
        public string? FungsiUmum { get; set; }
        public string? ButiranKemaskini { get; set; }
        public bool? IndikatorSkim { get; set; }
        public bool? IndSkimPerkhidmatan { get; set; }
    }
}
