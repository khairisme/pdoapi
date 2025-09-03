using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// Response DTO for returning AktivitiOrganisasi (Organizational Activity) details.
    /// Includes activity information, hierarchy, codes, and auditing metadata.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return AktivitiOrganisasi data from the server to the client,
    ///               including parent activity info, activity codes, descriptions, and audit fields.
    /// </remarks>
    public class AktivitiOrganisasiResponseDto
    {
        /// <summary>
        /// Optional. Indicates whether the activity is active.
        /// </summary>
        public bool? StatusAktif { get; set; }

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
        /// Required. GUID of the user who created the record.
        /// </summary>
        public Guid IdCipta { get; set; }

        /// <summary>
        /// Optional. GUID of the user who deleted the record.
        /// </summary>
        public Guid IdHapus { get; set; }

        /// <summary>
        /// Optional. GUID of the user who last modified the record.
        /// </summary>
        public Guid IdPinda { get; set; }

        /// <summary>
        /// Required. Internal record ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Required. Original ID of the activity (for reference or historical tracking).
        /// </summary>
        public int IdAsal { get; set; }

        /// <summary>
        /// Required. ID of the parent activity in the hierarchy.
        /// </summary>
        public int IdIndukAktivitiOrganisasi { get; set; }

        /// <summary>
        /// Optional. Level or depth of the activity in the hierarchy.
        /// </summary>
        public int Tahap { get; set; }

        /// <summary>
        /// Optional. Name of the parent activity in the hierarchy.
        /// </summary>
        public string? AktivitOrganisasiInduk { get; set; }

        /// <summary>
        /// Optional. Additional details about updates made to this record.
        /// </summary>
        public string? ButiranKemaskini { get; set; }

        /// <summary>
        /// Optional. Category or program name of the activity.
        /// </summary>
        public string? KategoriProgramAktiviti { get; set; }

        /// <summary>
        /// Optional. Description of the activity.
        /// </summary>
        public string? Keterangan { get; set; }

        /// <summary>
        /// Optional. Code assigned to the activity.
        /// </summary>
        public string? Kod { get; set; }

        /// <summary>
        /// Optional. Code of the activity itself.
        /// </summary>
        public string? KodAktivitiOrganisasi { get; set; }

        /// <summary>
        /// Optional. Code of the organizational chart this activity belongs to.
        /// </summary>
        public string? KodCartaAktiviti { get; set; }

        /// <summary>
        /// Optional. Program code associated with this activity.
        /// </summary>
        public string? KodProgram { get; set; }

        /// <summary>
        /// Optional. Reference code for the activity category.
        /// </summary>
        public string? KodRujKategoriAktivitiOrganisasi { get; set; }

        /// <summary>
        /// Required. Name of the activity.
        /// </summary>
        public string? Nama { get; set; }
    }
}
