using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{

    [Table("PDO_Jawatan")]
    public class PDOJawatan : PDOBaseEntity
    {
        public int IdUnitOrganisasi { get; set; }
        public int IdAktivitiOrganisasi { get; set; }
        public string KodRujStatusBekalan { get; set; }
        public string KodRujJenisJawatan { get; set; }
        public string KodRujStatusJawatan { get; set; }
        public string? KodRujKategoriJawatan { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string? Keterangan { get; set; }
        public string? GelaranJawatan { get; set; }
        public int? IdAgensiPembekal { get; set; }

        public int? IdJawatanPenyelia { get; set; }
        public bool IndikatorKetuaPerkhidmatan { get; set; }
        public bool IndikatorKetuaJabatan { get; set; }
    }
}
