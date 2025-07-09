using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs.PDO
{
    public class PegawaiTeknologiMaklumatResponseDto
    {
        public int Bil { get; set; }
        public int Id { get; set; }
        public string KodJawatan { get; set; }
        public string NamaJawatan { get; set; }
        public string UnitOrganisasi { get; set; }
        public string StatusPengisianJawatan { get; set; }
        public DateTime? TarikhKekosonganJawatan { get; set; }

    }
    public class BilanganPengisianHadSilingResponseDto
    {
        public int JumlahBilanganPengisian { get; set; }

        public int HadSilingDitetapkan { get; set; }
    }
}
