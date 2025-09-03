using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO for returning detailed information about job application grades in KUJ service schemes.
    /// Includes auditing fields and references to grades and schemes.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to transfer job application grade and KUJ scheme details from the system to clients.
    /// </remarks>
    public class ButiranPermohonanSkimGredKUJDto
    {
        /// <summary>
        /// Optional. The date and time when the record was created.
        /// </summary>
        public DateTime? TarikhCipta { get; set; }

        /// <summary>
        /// Optional. The date and time when the record was deleted.
        /// </summary>
        public DateTime? TarikhHapus { get; set; }

        /// <summary>
        /// Optional. The date and time when the record was last modified.
        /// </summary>
        public DateTime? TarikhPinda { get; set; }

        /// <summary>
        /// Optional. GUID of the user who created the record.
        /// </summary>
        public Guid? IdCipta { get; set; }

        /// <summary>
        /// Optional. GUID of the user who deleted the record.
        /// </summary>
        public Guid? IdHapus { get; set; }

        /// <summary>
        /// Optional. GUID of the user who last modified the record.
        /// </summary>
        public Guid? IdPinda { get; set; }

        /// <summary>
        /// Required. Internal ID of the detail record for this job application.
        /// </summary>
        public int IdButiranPermohonan { get; set; }

        /// <summary>
        /// Required. ID of the grade associated with this job application.
        /// </summary>
        public int IdGred { get; set; }

        /// <summary>
        /// Required. ID of the KUJ service scheme associated with this record.
        /// </summary>
        public int IdSkim { get; set; }
    }
}
