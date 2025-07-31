using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs
{
    public class PDOGredDto
    {
        public int Bil { get; set; }
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string? Keterangan { get; set; }
        public string? KodRujJenisSaraan { get; set; }

    }
    public class GredSearchResultDTO
    {
        public int Bil { get; set; }
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string StatusGred { get; set; }
        public string StatusPermohonan { get; set; }
        public DateTime? TarikhKemaskini { get; set; }
    }
}
