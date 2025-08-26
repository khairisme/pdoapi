using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.DTOs.PDO
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
    public class UnitOrganisasiAutocompleteDto
    {
        public string Kod { get; set; } = string.Empty;
        public string Nama { get; set; } = string.Empty;
    }
    // Code by Amar 17/05/25
    public class UnitOrganisasiPenjenamaanSemulaRequestDto
    {
        public int IdUnitOrganisasi { get; set; }
        public string NamaUnitOrganisasiBaharu { get; set; }

    }
    //End

    // Code by Amar 22/05/25
    public class UnitOrganisasiCarianKetuaPerkhidmatanResponseDto
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public string? KodCartaOrganisasi { get; set; }


    }
    //End
}
