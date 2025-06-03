using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs
{
    public class MaklumatKlasifikasiPerkhidmatanDto
    {
        public int Bil { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string StatusKumpulanPerkhidmatan { get; set; }
        public string StatusPermohonan { get; set; }

    }

    public class MaklumatKlasifikasiPerkhidmatanFilterDto
    {
        public string? Kod { get; set; }
        public string? Nama { get; set; }
        public int? StatusKumpulan { get; set; } // 0 = Tidak Aktif, 1 = Aktif
        public string? StatusPermohonan { get; set; } // e.g., "Draf", "Disahkan", etc.
    }

}
