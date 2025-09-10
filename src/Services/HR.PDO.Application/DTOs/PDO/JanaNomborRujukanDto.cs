using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO for generating reference numbers for organizational units.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to generate and return reference numbers along with related organizational codes.
    /// </remarks>
    public class JanaNomborRujukanDto
    {
        /// <summary>
        /// Required. Identifier of the organizational unit.
        /// </summary>
        public int? IdUnitOrganisasi { get; set; }

        /// <summary>
        /// Required. Generated reference number.
        /// </summary>
        public int NomborRujukan { get; set; }

        /// <summary>
        /// Required. Code of the department (Jabatan).
        /// </summary>
        public string KodJabatan { get; set; }

        /// <summary>
        /// Required. Code of the ministry (Kementerian).
        /// </summary>
        public string KodKementerian { get; set; }

        /// <summary>
        /// Required. Code representing the type of agency.
        /// </summary>
        public string KodRujJenisAgensi { get; set; }
    }
}
