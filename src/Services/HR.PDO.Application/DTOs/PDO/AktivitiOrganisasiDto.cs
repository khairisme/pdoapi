using HR.PDO.Core.Entities.PDO;
using System;
namespace HR.PDO.Application.DTOs
{

    public class ButiranKemaskiniDto
    {
        public int? Id { get; set; }
        public int? IdAktivitiOrganisasi { get; set; }
        public int? IdOldAktivitiOrganisasi { get; set; }
        public int? IdPermohonanJawatan { get; set; }
        public int? IdButiranPermohonan { get; set; }
        public int? IdIndukAktivitiOrganisasi { get; set; }

        public int? NewParentId { get; set; }

        public int? OldParentId { get; set; }
        public string? Nama { get; set; }
        public Guid IdPinda { get; set; }
        public DateTime TarikhPinda { get; set; }
        public Guid IdHapus { get; set; }
        public DateTime TarikhHapus { get; set; }
        public bool? StatusAktif { get; set; }
        public string? StatusTindakan { get; set; }
    }
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
        public Guid UserId { get; set; }

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

        public string? KategoriAktivitiOrgansisasi { get; set; }
    }

    public class AktivitiOrganisasiAlamatIndukDto
    {
        public int? Id { get; set; }
        public int? IdUnitOrganisasi { get; set; }
        public string? KodRujPoskod { get; set; }
        public string? Alamat1 { get; set; }
        public string? Alamat2 { get; set; }
        public string? Alamat3 { get; set; }
        public string? KodRujNegara { get; set; }
        public string? KodRujNegeri { get; set; }
        public string? KodRujBandar { get; set; }
        public string? NomborTelefonPejabat { get; set; }
        public string? NomborFaksPejabat { get; set; }

        
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

    public class PindahAktivitiOrganisasiDto
    {
        public int IdIndukAktivitiOrganisasi { get; set; }
        public bool  StatusAktif { get; set; }
        
        public Guid IdPinda { get; set; }
        public DateTime TarikhPinda { get; set; }

        public int NewParentId { get; set; }

        public int OldParentId { get; set; }

    }

}
