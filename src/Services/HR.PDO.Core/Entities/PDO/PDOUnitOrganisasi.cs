using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_UnitOrganisasi")]
    public class PDOUnitOrganisasi : PDOBaseEntity
    {

        public string KodRujKategoriUnitOrganisasi { get; set; }
        public string KodRujJenisAgensi { get; set; }

       
        public string? KodRujKluster { get; set; }

       
        public int IdIndukUnitOrganisasi { get; set; }

       
        public string? KodKementerian { get; set; }

      
        public string? KodJabatan { get; set; }

        
        public string Kod { get; set; }

       
        public string Nama { get; set; }

       
        public string? Singkatan { get; set; }

        
        public string? Keterangan { get; set; }

        
        public int Tahap { get; set; }

        
        public string? KodCartaOrganisasi { get; set; }

        public bool? IndikatorAgensi { get; set; }
        public bool? IndikatorAgensiRasmi { get; set; }
        public bool? IndikatorPemohonPerjawatan { get; set; }
        public bool? IndikatorJabatanDiKerajaanNegeri { get; set; }

        
        public string? ObjektifAgensi { get; set; }

        
        public string? MisiAgensi { get; set; }

        
        public string? VisiAgensi { get; set; }

        
        public string? PiagamPelanggan { get; set; }

       
        public string? ButiranKemaskini { get; set; }


    }
}
