using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO for returning detailed information about job application grades and service schemes.
    /// Includes auditing fields and references to grades, schemes, and career progression codes.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to transfer job application grade and scheme details from the system to clients.
    /// </remarks>
    public class ButiranPermohonanSkimGredDto
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
        public int? IdGred { get; set; }

        /// <summary>
        /// Required. ID of the service head (Ketua Perkhidmatan) approving or associated with this record.
        /// </summary>
        public int IdKetuaPerkhidmatan { get; set; }

        /// <summary>
        /// Required. ID of the service scheme (Skim Perkhidmatan) associated with this record.
        /// </summary>
        public int IdSkimPerkhidmatan { get; set; }

        /// <summary>
        /// Optional. Specialization code (Kod Bidang Pengkhususan) related to the job application.
        /// </summary>
        public string? KodBidangPengkhususan { get; set; }

        /// <summary>
        /// Optional. Reference code for career progression path (Kod Ruj Laluan Kemajuan Kerjaya).
        /// </summary>
        public string? KodRujLaluanKemajuanKerjaya { get; set; }
    }
}
