using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities
{
    /// <summary>
    /// Gambar(Images) entity representing a user in the organization
    /// </summary>
    [Table("PNS_Gambar")] // This is the key
    public class PNSGambar : PNSBaseEntity
    {
        public string Lokasi { get; set; }
    }
}
