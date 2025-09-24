using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_AktivitiOrganisasiRujPasukanPerunding")] // This is the key
    public class PDOAktivitiOrganisasiRujPasukanPerunding 
    {

        public string? KodRujPasukanPerunding { get; set; }
        public int? IdAktivitiOrganisasi { get; set; }

    }
}
