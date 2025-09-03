using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO for reading job application details (Permohonan Jawatan).
    /// Contains organizational, agency, and application reference information.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to transfer job application data from the system to clients.
    /// </remarks>
    public class BacaPermohonanJawatanDto
    {
        /// <summary>
        /// Required. Internal record ID of the job application.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Optional. Name of the organizational unit related to the application.
        /// </summary>
        public string? UnitOrganisasi { get; set; }

        /// <summary>
        /// Optional. Name of the agency related to the application.
        /// </summary>
        public string? Agensi { get; set; }

        /// <summary>
        /// Optional. Title of the job application.
        /// </summary>
        public string? TajukPermohonan { get; set; }

        /// <summary>
        /// Optional. Description or additional notes related to the application.
        /// </summary>
        public string? Keterangan { get; set; }

        /// <summary>
        /// Optional. Reference number of the application.
        /// </summary>
        public string? NomborRujukan { get; set; }

        /// <summary>
        /// Optional. Ministry code associated with the application.
        /// </summary>
        public string? KodKementerian { get; set; }

        /// <summary>
        /// Optional. Department code associated with the application.
        /// </summary>
        public string? KodJabatan { get; set; }
    }
}
