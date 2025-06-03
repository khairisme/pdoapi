using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs
{
    public class KumpulanPerkhidmatanDto
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string ButiranKemaskini { get; set; }
    }

    public class CarlKumpulanPerkhidmatanDto
    {
        public int Bil { get; set; }  
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string StatusKumpulanPerkhidmatan { get; set; }
        public string StatusPermohonan { get; set; }
        public DateTime TarikhKemaskini { get; set; }
    }
    public class KumpulanPerkhidmatanFilterDto
    {
        public string? Kod { get; set; }
        public string? Nama { get; set; }
        public int? StatusKumpulan { get; set; } // 0 = Tidak Aktif, 1 = Aktif
        public string? StatusPermohonan { get; set; } // e.g., "Draf", "Disahkan", etc.
    }
}
