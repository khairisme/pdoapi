using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_Gred")] // This is the key
    public class PDOGred : PDOBaseEntity
    {
       
        public string? KodRujJenisSaraan { get; set; }
        public int? IdKlasifikasiPerkhidmatan { get; set; }
        public int? IdKumpulanPerkhidmatan { get; set; }
        public string? Kod { get; set; }
        public string? Nama { get; set; }
        public int? TurutanGred { get; set; }
        public string? KodGred { get; set; }
        public int? NomborGred { get; set; }
        public string? Keterangan { get; set; }
        public bool? IndikatorGredLantikanTerus { get; set; }
        public bool? IndikatorGredLantikan { get; set; }
        public string? UlasanPengesah { get;set; }
        public string? ButiranKemaskini { get; set; }

    }
}
