using System;
namespace HR.PDO.Application.DTOs
{
    public class UnitOrganisasiPenjenamaanSemulaRequestDto
    {
        public int? Id { get; set; }
        public int? IdUnitOrganisasi { get; set; }
        public string? KodCartaAktiviti { get; set; }
        public string? NamaUnitOrganisasiBaharu { get; set; }
    }
}
