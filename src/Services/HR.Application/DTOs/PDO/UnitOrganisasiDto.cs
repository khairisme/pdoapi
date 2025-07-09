using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs.PDO
{
    public class UnitOrganisasiDto
    {
        public int Id { get; set; }
       
        public string Nama { get; set; } = null!;
        public string? KodCartaOrganisasi { get; set; }
        

    }
    public class UnitOrganisasiKementerianDto
    {
        public string Kod { get; set; }

        public string Nama { get; set; } = null!;
        


    }
}
