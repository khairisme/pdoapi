using System;
namespace HR.PDO.Application.DTOs
{
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

}
