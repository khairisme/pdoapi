using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO for representing an AktivitiOrganisasi (Organizational Activity) record.
    /// Includes details, hierarchy information, and auditing metadata.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to transfer AktivitiOrganisasi data between the system and clients,
    ///               including status, hierarchy, codes, and metadata for auditing.
    /// </remarks>
    public class AktivitiOrganisasiDto
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
        /// Optional. The GUID of the user who created this record.
        /// </summary>
        public Guid? IdCipta { get; set; }

        /// <summary>
        /// Optional. The GUID of the user who deleted this record.
        /// </summary>
        public Guid? IdHapus { get; set; }

        /// <summary>
        /// Optional. The GUID of the user who last modified this record.
        /// </summary>
        public Guid? IdPinda { get; set; }

        /// <summary>
        /// Required. Internal record ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Optional. ID of the parent activity in the hierarchy.
        /// </summary>
        public int? IdIndukAktivitiOrganisasi { get; set; }

        /// <summary>
        /// Optional. Level or depth of the activity in the hierarchy.
        /// </summary>
        public int? Tahap { get; set; }

        /// <summary>
        /// Optional. Name of this activity.
        /// </summary>
        public string? AktiviOrganisasi { get; set; }

        /// <summary>
        /// Optional. Name of the parent activity in the hierarchy.
        /// </summary>
        public string? AktivitiOrganisasiInduk { get; set; }

        /// <summary>
        /// Optional. Additional details about updates made to this record.
        /// </summary>
        public string? ButiranKemaskini { get; set; }

        /// <summary>
        /// Optional. Description of the activity.
        /// </summary>
        public string? Keterangan { get; set; }

        /// <summary>
        /// Optional. Code assigned to this activity.
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
    }


    /// <summary> 
    /// DTO for renaming an existing AktivitiOrganisasi (Organizational Activity) record.
    /// Encapsulates the required details to update the name and audit the user performing the change.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar  
    /// Created On  : 2025-09-03  
    /// Purpose     : Used as the request payload when performing a renaming (penjenamaan semula) 
    ///               operation on an AktivitiOrganisasi entity.  
    /// </remarks>
    public class PenjenamaanAktivitiOrganisasiRequestDto
    {
        /// <summary>
        /// Required. The GUID of the user performing the renaming operation.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Required. The internal record ID of the AktivitiOrganisasi to be renamed.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Optional. The new name to assign to the AktivitiOrganisasi.
        /// </summary>
        public string? Nama { get; set; }
    }

    /// <summary>
    /// Request DTO for moving an Aktiviti Organisasi to a different parent.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar  
    /// Created On  : 2025-09-03  
    /// Purpose     : Used as the request payload when performing a move (pindah) 
    ///               operation on an AktivitiOrganisasi entity to change its parent node.  
    /// </remarks>
    public class PindahAktivitiOrganisasiRequestDto
    {
        /// <summary>
        /// The user performing the move.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The identifier of the activity being moved.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The identifier of the new parent node.
        /// </summary>
        public int NewParentId { get; set; }

        /// <summary>
        /// The identifier of the old parent node.
        /// </summary>
        public int OldParentId { get; set; }
    }

}
