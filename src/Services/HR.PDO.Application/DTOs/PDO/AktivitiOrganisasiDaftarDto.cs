using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO for listing or registering AktivitiOrganisasi entries.
    /// Contains minimal identifying and descriptive information about each activity.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return a list of AktivitiOrganisasi for registration or selection purposes.
    /// </remarks>
    public class AktivitiOrganisasiDaftarRequestDto
    {
        /// <summary>
        /// The user performing the operation (for auditing).
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Activity code for the new AktivitiOrganisasi.
        /// </summary>
        public string? KodAktiviti { get; set; }

        /// <summary>
        /// Activity name.
        /// </summary>
        public string? NamaAktiviti { get; set; }

        /// <summary>
        /// Reference category code.
        /// </summary>
        public string? KodRujKategoriAktivitiOrganisasi { get; set; }

        /// <summary>
        /// Original source ID (optional).
        /// </summary>
        public int? IdAsal { get; set; }
    }

    /// <summary>
    /// Request DTO for permanently deleting an AktivitiOrganisasi.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar  
    /// Created On  : 2025-09-03  
    /// Purpose     : Encapsulates both the user performing the operation and 
    ///               the identifier of the AktivitiOrganisasi being deleted.  
    /// </remarks>
    public class HapusTerusAktivitiOrganisasiRequestDto
    {
        /// <summary>
        /// The user performing the delete operation.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The identifier of the AktivitiOrganisasi to delete.
        /// </summary>
        public int Id { get; set; }
    }
    /// <summary>
    /// Request DTO for deactivating (mansuh) an Aktiviti Organisasi entity.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar  
    /// Created On  : 2025-09-03  
    /// Purpose     : Encapsulates the required data for deactivating an AktivitiOrganisasi entity 
    ///               without permanently deleting it.  
    /// </remarks>
    public class MansuhAktivitiOrganisasiRequestDto
    {
        /// <summary>
        /// The user performing the operation.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The identifier of the Aktiviti Organisasi to deactivate.
        /// </summary>
        public int IdAktivitiOrganisasi { get; set; }
    }

}