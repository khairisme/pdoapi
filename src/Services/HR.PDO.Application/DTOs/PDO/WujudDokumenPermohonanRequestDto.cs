using System;
namespace HR.PDO.Application.DTOs
{
    public class HapusTerusDokumenPermohonanRequest
    {
        public int? IdPermohonanJawatan { get; set; }
        public int? IdDokumenPermhonan { get; set; }
    }
    /// <summary>
    /// DTO for returning detailed information about a job application (Permohonan Jawatan).
    /// Includes auditing fields and reference IDs for the job and detail records.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to transfer detailed job application information from the system to clients.
    /// </remarks>
    public class WujudDokumenPermohonanRequestDto
    {
        /// <summary>
        /// Identifier of the user who uploaded or created the document.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Identifier of the related job application (Permohonan Jawatan).
        /// </summary>
        public int IdPermohonanJawatan { get; set; }

        /// <summary>
        /// Reference code of the document type.
        /// </summary>
        public string? KodRujJenisDokumen { get; set; }

        /// <summary>
        /// Name of the document.
        /// </summary>
        public string? NamaDokumen { get; set; }

        /// <summary>
        /// Link or path to the document storage location.
        /// </summary>
        public string? PautanDokumen { get; set; }

        /// <summary>
        /// File format of the document (e.g., PDF, DOCX).
        /// </summary>
        public string? FormatDokumen { get; set; }

        /// <summary>
        /// File size of the document in bytes.
        /// </summary>
        public int Saiz { get; set; }
    }
}
