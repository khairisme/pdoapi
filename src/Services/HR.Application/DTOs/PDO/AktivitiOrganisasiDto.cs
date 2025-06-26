using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs.PDO
{
    public class AktivitiOrganisasiDto
    {
        public int Id { get; set; }
        public int? IdIndukAktivitiOrganisasi { get; set; }
        public string KodProgram { get; set; }
        public string Nama { get; set; }
        public int? Tahap { get; set; }
    }
}
