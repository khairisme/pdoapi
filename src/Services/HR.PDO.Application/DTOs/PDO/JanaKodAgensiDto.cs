using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO for representing an AktivitiOrganisasi (Organizational Activity) record.
    /// Includes details, hierarchy information, and auditing metadata.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to transfer AktivitiOrganisasi data between the system and clients,
    ///               including status, hierarchy, codes, and metadata for auditing.
    /// </remarks>
    public class JanaKodAgensiRequestDto
    {
        public int? IdUnitOrganisasi { get; set; }
    }



}
