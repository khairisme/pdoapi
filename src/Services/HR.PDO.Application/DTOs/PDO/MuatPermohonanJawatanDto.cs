using Microsoft.Data.SqlClient.DataClassification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.DTOs.PDO
{
    public class MuatPermohonanJawatanOutputDto
    {
        public int? IdUnitOrganisasi { get; set; }
        public string UnitOrganisasi { get; set; }
        public string? Agensi { get; set; }
        public string? NomborRujukan { get; set; }
    }

}
