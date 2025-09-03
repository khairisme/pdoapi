using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO representing a node in the tree structure of AktivitiOrganisasi (Organizational Activities).
    /// Includes hierarchy, codes, names, and child nodes for building a tree.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to transfer hierarchical AktivitiOrganisasi data with children for tree structures.
    /// </remarks>
    public class StrukturAktivitiOrganisasiDto
    {
        /// <summary>
        /// Optional. Unique ID of the activity.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Optional. ID of the parent activity in the hierarchy.
        /// </summary>
        public int? IdIndukAktivitiOrganisasi { get; set; }

        /// <summary>
        /// Optional. Level or depth of the activity in the hierarchy.
        /// </summary>
        public int? Tahap { get; set; }

        /// <summary>
        /// Optional. Name of the activity.
        /// </summary>
        public string? AktivitiOrganisasi { get; set; }

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
        /// List of child nodes in the activity hierarchy.
        /// </summary>
        public List<StrukturAktivitiOrganisasiDto> Children { get; set; } = new List<StrukturAktivitiOrganisasiDto>();

        /// <summary>
        /// Indicates if the activity has child nodes.
        /// </summary>
        public bool HasChildren { get; set; }
    }

    /// <summary>
    /// DTO for requesting a tree structure of AktivitiOrganisasi (Organizational Activities).
    /// Includes filtering, sorting, and pagination options.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to request a hierarchical view of AktivitiOrganisasi with optional filters, pagination, and sorting.
    /// </remarks>
    public class StrukturAktivitiOrganisasiRequestDto
    {
        /// <summary>
        /// Optional. Code of the organizational chart to filter activities.
        /// </summary>
        public string? KodCartaAktiviti { get; set; }

        /// <summary>
        /// Optional. Parent activity ID to start building the tree. Default is 0 (root).
        /// </summary>
        public int parentId { get; set; } = 0;

        /// <summary>
        /// Optional. Page number for pagination. Default is 1.
        /// </summary>
        public int page { get; set; } = 1;

        /// <summary>
        /// Optional. Number of items per page for pagination. Default is 50.
        /// </summary>
        public int pageSize { get; set; } = 50;

        /// <summary>
        /// Optional. Keyword for filtering activities by name or code. Default is null (no filtering).
        /// </summary>
        public string? keyword { get; set; } = null;

        /// <summary>
        /// Optional. Field to sort the results by. Default is "UnitOrganisasi".
        /// </summary>
        public string? sortBy { get; set; } = "UnitOrganisasi";

        /// <summary>
        /// Optional. Indicates if the sorting should be descending. Default is false (ascending).
        /// </summary>
        public bool desc { get; set; } = false;
    }
    /// <summary>
    /// DTO for creating a new AktivitiOrganisasi (Organizational Activity).
    /// Contains required details for creating the activity in the system.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to send data from client to server when creating a new AktivitiOrganisasi.
    /// </remarks>
    public class WujudAktivitiOrganisasiRequestDto
    {
        /// <summary>
        /// Required. ID of the user creating the activity.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Required. ID of the parent activity in the hierarchy.
        /// </summary>
        public int IdIndukAktivitiOrganisasi { get; set; }

        /// <summary>
        /// Optional. Program code associated with this activity.
        /// </summary>
        public string? KodProgram { get; set; }

        /// <summary>
        /// Optional. Code assigned to the activity.
        /// </summary>
        public string? Kod { get; set; }

        /// <summary>
        /// Required. Name of the activity.
        /// </summary>
        public string? Nama { get; set; }

        /// <summary>
        /// Optional. Level or depth of the activity in the hierarchy.
        /// </summary>
        public int Tahap { get; set; }

        /// <summary>
        /// Optional. Reference code for the activity category.
        /// </summary>
        public string? KodRujKategoriAktivitiOrganisasi { get; set; }

        /// <summary>
        /// Optional. Description or additional notes about the activity.
        /// </summary>
        public string? Keterangan { get; set; }
    }
}
