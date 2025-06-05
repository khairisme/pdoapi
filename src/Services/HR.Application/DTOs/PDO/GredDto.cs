using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs.PDO
{
    public class GredFilterDto
    {
        public int? IdKumpulanPerkhidmatan { get; set; }
        public int? IdKlasifikasiPerkhidmatan { get; set; }
        public string? KodRujStatusPermohonan { get; set; }
        public string? Nama { get; set; }
    }
    public class GredResultDto
    {
        public int Bil { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string StatusPermohonan { get; set; }
        public string StatusGred { get; set; }
    }
}
