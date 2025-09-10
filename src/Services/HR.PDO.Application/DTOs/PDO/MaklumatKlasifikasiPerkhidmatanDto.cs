using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.DTOs.PDO
{
    /// <summary>
    /// DTO for creating or updating service classification information.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to send service classification details for creation or update operations.
    /// </remarks>
    public class MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto
    {
        /// <summary>
        /// Required. Unique identifier for the classification record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Required. Code representing the classification.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Required. Name of the classification.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Required. Detailed description or notes for the classification.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Required. Main function of the classification.
        /// </summary>
        public string FungsiUtama { get; set; }

        /// <summary>
        /// Required. General function of the classification.
        /// </summary>
        public string FungsiUmum { get; set; }

        /// <summary>
        /// Optional. Additional review or comments.
        /// </summary>
        public string? Ulasan { get; set; }

        /// <summary>
        /// Optional. Details of updates or modifications.
        /// </summary>
        public string? ButiranKemaskini { get; set; }

        /// <summary>
        /// Indicates whether the classification is active.
        /// </summary>
        public bool StatusAktif { get; set; } = true;

        /// <summary>
        /// Optional. Flag indicating whether the classification is part of a scheme.
        /// </summary>
        public bool? IndikatorSkim { get; set; }

        /// <summary>
        /// Optional. Approver's comments or review notes.
        /// </summary>
        public string? UlasanPengesah { get; set; }

        /// <summary>
        /// Optional. Reference code for the application status.
        /// </summary>
        public string? KodRujStatusPermohonan { get; set; }

        //public bool? IndSkimPerkhidmatan { get; set; }
    }

    /// <summary>
    /// DTO for returning service classification search results.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to display service classification search results in UI or API responses.
    /// </remarks>
    public class MaklumatKlasifikasiPerkhidmatanSearchResponseDto
    {
        /// <summary>
        /// Sequential number in the search result list.
        /// </summary>
        public int Bil { get; set; }

        /// <summary>
        /// Unique identifier of the classification record.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Code representing the classification.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Name of the classification.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// General function of the classification.
        /// </summary>
        public string FungsiUmum { get; set; }

        /// <summary>
        /// Main function of the classification.
        /// </summary>
        public string FungsiUtama { get; set; }

        /// <summary>
        /// Description or notes about the classification.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Status of the service group associated with this classification.
        /// </summary>
        public string StatusKumpulanPerkhidmatan { get; set; }

        /// <summary>
        /// Status of the application or approval related to this classification.
        /// </summary>
        public string StatusPermohonan { get; set; }
    }

    /// <summary>
    /// DTO for returning detailed information about a service classification.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to provide detailed classification information in UI or API responses.
    /// </remarks>
    public class MaklumatKlasifikasiPerkhidmatanResponseDto
    {
        /// <summary>
        /// Unique identifier of the classification record.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Code representing the classification.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Name of the classification.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Description or additional information about the classification.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Main function of the classification.
        /// </summary>
        public string FungsiUtama { get; set; }

        /// <summary>
        /// General function of the classification.
        /// </summary>
        public string FungsiUmum { get; set; }

        /// <summary>
        /// Current status of the classification.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Date and time when the classification was last updated.
        /// </summary>
        public DateTime? TarikhKemaskini { get; set; }

        /// <summary>
        /// Status of the classification within the service group.
        /// </summary>
        public string StatusKlasifikasiPerkhidmatan { get; set; }

        /// <summary>
        /// Optional reviewer comments.
        /// </summary>
        public string? Ulasan { get; set; }

        /// <summary>
        /// Optional approver comments.
        /// </summary>
        public string? UlasanPengesah { get; set; }

        /// <summary>
        /// Optional details of the last update.
        /// </summary>
        public string? ButiranKemaskini { get; set; }

        /// <summary>
        /// Indicates if this classification is part of a scheme.
        /// </summary>
        public bool? IndikatorSkim { get; set; }

        /// <summary>
        /// Indicates if this classification is associated with a service scheme.
        /// </summary>
        public bool? IndSkimPerkhidmatan { get; set; }

        /// <summary>
        /// Indicates whether this classification is active.
        /// </summary>
        public bool StatusAktif { get; set; }
    }

    /// <summary>
    /// DTO for filtering service classification records based on code, name, and status.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to pass filter criteria when searching or listing service classifications.
    /// </remarks>
    public class PenapisMaklumatKlasifikasiPerkhidmatanDto
    {
        /// <summary>
        /// Optional. Code of the classification to filter by.
        /// </summary>
        public string? Kod { get; set; }

        /// <summary>
        /// Optional. Name of the classification to filter by.
        /// </summary>
        public string? Nama { get; set; }

        /// <summary>
        /// Optional. Status of the classification group.
        /// 0 = Tidak Aktif (Inactive), 1 = Aktif (Active)
        /// </summary>
        public int? StatusKumpulan { get; set; }

        /// <summary>
        /// Optional. Status of the application, e.g., "Draf" (Draft), "Disahkan" (Approved), etc.
        /// </summary>
        public string? StatusPermohonan { get; set; }
    }
    /// <summary>
    /// DTO representing service classification information.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to transfer service classification data between layers or APIs.
    /// </remarks>
    public class MaklumatKlasifikasiPerkhidmatanDto
    {
        /// <summary>
        /// Unique identifier of the service classification.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Code of the service classification.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Name of the service classification.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Description or remarks about the service classification.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Optional. Main function of the classification.
        /// </summary>
        public string? FungsiUtama { get; set; }

        /// <summary>
        /// Optional. General function of the classification.
        /// </summary>
        public string? FungsiUmum { get; set; }

        /// <summary>
        /// Optional. Details about updates or modifications.
        /// </summary>
        public string? ButiranKemaskini { get; set; }

        /// <summary>
        /// Optional. Indicator if the classification is part of a scheme.
        /// </summary>
        public bool? IndikatorSkim { get; set; }

        /// <summary>
        /// Optional. Indicator if the classification is used in service schemes.
        /// </summary>
        public bool? IndSkimPerkhidmatan { get; set; }
    }


    /// <summary>
    /// DTO for updating a service classification record.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to send update information for a service classification.
    /// </remarks>
    public class MaklumatKlasifikasiPerkhidmatanUpdateRequestDto
    {
        /// <summary>
        /// Unique identifier of the service classification to update.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Updated code of the service classification.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Updated name of the service classification.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Updated description or remarks about the classification.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Updated main function of the classification.
        /// </summary>
        public string FungsiUtama { get; set; }

        /// <summary>
        /// Updated general function of the classification.
        /// </summary>
        public string FungsiUmum { get; set; }
    }

    /// <summary>
    /// DTO for returning a list of service classification confirmations.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to present service classification information along with status and indicators.
    /// </remarks>
    public class PengesahanPerkhidmatanKlasifikasiResponseDto
    {
        /// <summary>
        /// Sequential number in the list.
        /// </summary>
        public int Bil { get; set; }

        /// <summary>
        /// Unique identifier of the service classification record.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Code of the service classification.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Name of the service classification.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Description or remarks about the classification.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Reference code for the application status.
        /// </summary>
        public string KodRujStatusPermohonan { get; set; }

        /// <summary>
        /// Human-readable status of the application.
        /// </summary>
        public string StatusPermohonan { get; set; }

        /// <summary>
        /// Indicator whether this classification has an associated scheme.
        /// </summary>
        public bool? IndikatorSkim { get; set; }

        /// <summary>
        /// Indicator whether this classification is part of the service classification scheme.
        /// </summary>
        public bool? IndSkimPerkhidmatan { get; set; }
    }

    /// <summary>
    /// DTO for filtering service classifications.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to filter service classification records by code, name, or application status.
    /// </remarks>
    public class PenapisPerkhidmatanKlasifikasiDto
    {
        /// <summary>
        /// Optional. Code of the service classification.
        /// </summary>
        public string? Kod { get; set; }

        /// <summary>
        /// Optional. Name of the service classification.
        /// </summary>
        public string? Nama { get; set; }

        /// <summary>
        /// Optional. Status of the application.
        /// </summary>
        public string? StatusPermohonan { get; set; }
    }

    /// <summary>
    /// DTO for returning detailed update information of a service classification.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return detailed update information, including status, functions, and indicators.
    /// </remarks>
    public class ButiranKemaskiniKlasifikasiPerkhidmatanResponseDto
    {
        /// <summary>
        /// Required. Unique identifier of the classification record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Required. Code of the classification.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Required. Name of the classification.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Required. Description or remarks of the classification.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Required. Main function of the classification.
        /// </summary>
        public string FungsiUtama { get; set; }

        /// <summary>
        /// Required. General function of the classification.
        /// </summary>
        public string FungsiUmum { get; set; }

        /// <summary>
        /// Required. Current status of the classification.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Optional. Date of the last update.
        /// </summary>
        public DateTime? TarikhKemaskini { get; set; }

        /// <summary>
        /// Required. Status of the service classification.
        /// </summary>
        public string StatusKlasifikasiPerkhidmatan { get; set; }

        /// <summary>
        /// Optional. Comments or review notes.
        /// </summary>
        public string? Ulasan { get; set; }

        /// <summary>
        /// Optional. Indicates whether this classification is part of a service scheme.
        /// </summary>
        public bool? IndikatorSkim { get; set; }

        /// <summary>
        /// Optional. Indicates whether this classification belongs to a service scheme.
        /// </summary>
        public bool? IndSkimPerkhidmatan { get; set; }

        /// <summary>
        /// Required. Active status of the classification.
        /// </summary>
        public bool StatusAktif { get; set; }
    }
}
