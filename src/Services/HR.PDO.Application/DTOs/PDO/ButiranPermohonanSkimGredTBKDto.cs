using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO for returning detailed information about job application grades in TBK service schemes.
    /// Includes auditing fields and references to grades and service schemes.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to transfer job application grade and TBK scheme details from the system to clients.
    /// </remarks>
    public class ButiranPermohonanSkimGredTBKDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }

        /// <summary>
        /// Required. Internal ID of the detail record for this job application.
        /// </summary>
        public int IdButiranPermohonan { get; set; }

        /// <summary>
        /// Required. ID of the grade associated with this job application.
        /// </summary>
        public int IdGred { get; set; }

        /// <summary>
        /// Required. ID of the TBK service scheme associated with this record.
        /// </summary>
        public int IdSkimPerkhidmatan { get; set; }
    }
}
