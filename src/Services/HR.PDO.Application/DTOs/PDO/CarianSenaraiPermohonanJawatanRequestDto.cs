using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO for sending a request to search for a list of job applications (Permohonan Jawatan) based on filters.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to filter job applications by organizational unit, reference number, status, and title.
    /// </remarks>
    public class CarianSenaraiPermohonanJawatanRequestDto
    {
        /// <summary>
        /// Required. ID of the organizational unit for which the applications are being searched.
        /// </summary>
        public int IdUnitOrganisasi { get; set; }

        /// <summary>
        /// Required. Reference number of the application.
        /// </summary>
        public string NomborRujukan { get; set; }

        /// <summary>
        /// Required. Status code of the job application.
        /// </summary>
        public string KodRujStatusPermohonanJawatan { get; set; }

        /// <summary>
        /// Required. Title of the application.
        /// </summary>
        public string TajukPermohonan { get; set; }
    }
}
