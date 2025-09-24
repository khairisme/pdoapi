using Azure.Core;
using System;
using System.ComponentModel;
namespace HR.PDO.Application.DTOs
{
    public class UnitOrganisasiCarianRequestDto
    {
        [DefaultValue("")]
        public string? Kod { get; set; }
        [DefaultValue("")]
        public string? NamaAgensi { get; set; }
        [DefaultValue(1)]
        public int Page { get; set; }
        [DefaultValue(50)]
        public int PageSize { get; set; }
        [DefaultValue("Nama")]
        public string? SortBy { get; set; }
        [DefaultValue(false)]
        public bool Desc { get; set; }
    }
    public class UnitOrganisasiCarianDto
    {
        public bool Desc { get; set; }
        public int IdIndukAktivitiOrganisasi { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Keterangan { get; set; }
        public string? Kod { get; set; }
        public string? KodUnitOrganisasi { get; set; }
        public string? Nama { get; set; }
        public string? NamaUnitOrganisasi { get; set; }
        public string? SortBy { get; set; }
    }

    /// <summary>
    /// DTO for renaming a UnitOrganisasi entity.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-09-03
    /// Purpose     : Encapsulates data required to rename a UnitOrganisasi.
    /// </remarks>
    public class PenjenamaanUnitOrganisasiDto
    {
        /// <summary>
        /// The user performing the rename.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The identifier of the unit to rename.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The new name for the unit.
        /// </summary>
        public string Nama { get; set; } = string.Empty;
    }
    /// <summary>
    /// Request DTO for removing (mansuh) a Unit Organisasi.
    /// </summary>
    /// <remarks>
    /// This DTO is used when a client requests to delete or deactivate 
    /// a Unit Organisasi record in the system.
    /// </remarks>
    /// <author>Khairi bin Abu Bakar</author>
    /// <date>2025-09-04</date>
    public class MansuhUnitOrganisasiRequestDto
    {
        /// <summary>
        /// The unique identifier of the user performing the operation.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The unique identifier of the Unit Organisasi to be removed.
        /// </summary>
        public int Id { get; set; }
    }

    /// <summary>
    /// DTO for updating the details (butiran) of a record when it is removed (mansuh).
    /// </summary>
    /// <remarks>
    /// This DTO is typically used to mark a record as inactive, 
    /// track the deletion action, and record the timestamp of removal.
    /// </remarks>
    /// <author>Your Name</author>
    /// <date>2025-09-04</date>
    public class MansuhButiranKemaskiniDto
    {
        /// <summary>
        /// Indicates whether the record is still active.
        /// </summary>
        public bool StatusAktif { get; set; }

        /// <summary>
        /// The unique identifier of the user or process that performed the removal.
        /// </summary>
        public Guid? IdHapus { get; set; }

        /// <summary>
        /// The date and time when the record was removed.
        /// </summary>
        public DateTime TarikhHapus { get; set; }

        /// <summary>
        /// The Status
        /// </summary>
        public string? StatusTindakan{ get; set; }
    }
}
