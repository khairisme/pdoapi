using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// Request DTO for creating a new AktivitiOrganisasi (Organizational Activity) record.
    /// Contains all properties needed for creation and metadata tracking.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to create or insert new AktivitiOrganisasi entries in the system,
    ///               including activity details and metadata for auditing (created, modified, deleted).
    /// </remarks>
    public class AktivitiOrganisasiCreateRequest
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
        /// Required. The GUID of the user who created this record.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Optional. Internal record ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Optional. The unique identifier for this AktivitiOrganisasi.
        /// </summary>
        public int IdAktivitiOrganisasi { get; set; }

        /// <summary>
        /// Optional. Original ID (used for reference or historical tracking).
        /// </summary>
        public int IdAsal { get; set; }

        /// <summary>
        /// Optional. ID of the parent activity in the tree structure.
        /// </summary>
        public int IdIndukAktivitiOrganisasi { get; set; }

        /// <summary>
        /// Optional. Level or depth of the activity in the hierarchy.
        /// </summary>
        public int Tahap { get; set; }

        /// <summary>
        /// Optional. Additional details about updates made to this record.
        /// </summary>
        public string? ButiranKemaskini { get; set; }

        /// <summary>
        /// Optional. Description of the activity.
        /// </summary>
        public string? Keterangan { get; set; }

        /// <summary>
        /// Optional. The code assigned to this activity.
        /// </summary>
        public string? Kod { get; set; }

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
