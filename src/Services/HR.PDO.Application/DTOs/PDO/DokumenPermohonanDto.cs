using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO for transferring information about a job application document (Dokumen Permohonan).
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return details of documents associated with a job application.
    /// </remarks>
    public class DokumenPermohonanDto
    {
        /// <summary>
        /// Optional. Type of the document (e.g., CV, supporting letter, certificate).
        /// </summary>
        public string? JenisDokumen { get; set; }

        /// <summary>
        /// Optional. Name of the document file.
        /// </summary>
        public string? NamaDokumen { get; set; }
    }
}
