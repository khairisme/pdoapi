using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities
{
    /// <summary>
    /// SoalanKeselamatan(User Sequrity question) entity representing a user in the organization
    /// </summary>
    [Table("PNS_SoalanKeselamatan")] // This is the key
    public class PNSSoalanKeselamatan:PNSBaseEntity
    {
        public string IdPengguna { get; set; }
        public string KodRujSoalanKeselamatan { get; set; }
        public string JawapanSoalan { get; set; }
       

        [NotMapped]
        public new bool StatusAktif { get; set; }


    }
}
