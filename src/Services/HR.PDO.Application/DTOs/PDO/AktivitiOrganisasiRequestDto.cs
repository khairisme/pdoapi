using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// Request DTO for creating, updating, or moving an AktivitiOrganisasi (Organizational Activity) record.
    /// Contains all necessary activity details, hierarchy information, and auditing metadata.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to send AktivitiOrganisasi data from the client to the server for create, update, 
    ///               or hierarchy change operations, including parent-child relationships and audit info.
    /// </remarks>
    public class AktivitiOrganisasiRequestDto
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
        public Guid UserId { get; set; }

        /// <summary>
        /// Required. Internal record ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Required. Original ID of the activity (used for reference or historical tracking).
        /// </summary>
        public int IdAsal { get; set; }

        /// <summary>
        /// Required. ID of the parent activity in the hierarchy.
        /// </summary>
        public int IdIndukAktivitiOrganisasi { get; set; }

        /// <summary>
        /// Optional. The new parent ID when moving the activity in the hierarchy.
        /// </summary>
        public int NewParentId { get; set; }

        /// <summary>
        /// Optional. The old parent ID before moving the activity.
        /// </summary>
        public int OldParentId { get; set; }

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
        /// Optional. Code assigned to the activity.
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
