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
    //Nitya Code Start
    public class CombinedUpdateRequestDto
    {
        public int IdPermohonanPengisianSkim { get; set; }
        public int IdPermohonanPengisian { get; set; }
        public string Ulasan { get; set; }
        public Guid? UserId { get; set; }

        public List<HadSilingUpdateRequestDto> GridItems { get; set; }
    }

    public class HadSilingUpdateRequestDto
    {
        public int RecordId { get; set; }
        public int BilanganHadSiling { get; set; }
    }
    //Nitya Code End

}
