using HR.PDO.Core.Entities.PDO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_AktivitiOrganisasi")] // This is the key
    public class PDOAktivitiOrganisasi : PDOBaseEntity
    {
      

       
        public string KodRujKategoriAktivitiOrganisasi { get; set; }

        public int IdIndukAktivitiOrganisasi { get; set; }

       
        public string Kod { get; set; }

       
        public string Nama { get; set; }

       
        public string? Keterangan { get; set; }

       
        public string? KodProgram { get; set; }

      
        public int Tahap { get; set; }

       
        public string? KodCartaAktiviti { get; set; }

        
        public string? ButiranKemaskini { get; set; }

       


    }
}
