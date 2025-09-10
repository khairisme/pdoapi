using Microsoft.Data.SqlClient.DataClassification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.DTOs.PDO
{
    public class TambahPermohonanJawatanBaruDto
    {
        public Guid UserId { get; set; }
        public int IdUnitOrganisasi { get; set; }
        public string NomborRujukan { get; set; }
        public string Keterangan { get; set; }
        public string Tajuk { get; set; }
        public string KodRujJenisPermohonan { get; set; }
        
        public DateTime TarikhPermohonan { get; set; }
    }
}
