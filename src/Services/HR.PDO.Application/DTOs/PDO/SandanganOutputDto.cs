using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO for returning a simple dropdown item with an ID, code, and name.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to populate dropdown lists in UI or API responses.
    /// </remarks>
    public class SandanganOutputDto
    {
        public int? Id { get; set; }
        public int? IdJawatan { get; set; }
        public Guid UserId { get; set; }
        public DateTime? TarikhTamatSandangan { get; set; }
        public Guid? IdPemilikKompetensi { get; set; }
        public DateTime? TarikhMulaSandangan { get; set; }
    }
}
