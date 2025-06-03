using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities
{
    [Table("PDO_KumpulanPerkhidmatan")] // This is the key
    public class PDOKumpulanPerkhidmatan : PDOBaseEntity
    {
        public string  Kod { get; set; }
        public string  Nama { get; set; }
        public string? Keterangan { get; set; }
        public string? ButiranKemaskini { get; set; }
    }
}
