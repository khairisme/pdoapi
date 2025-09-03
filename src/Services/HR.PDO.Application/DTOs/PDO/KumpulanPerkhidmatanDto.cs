using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.DTOs.PDO
{
    /// <summary>
    /// DTO for representing a service group (Kumpulan Perkhidmatan) with related details.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return or transfer information about service groups in API or service responses.
    /// </remarks>
    public class KumpulanPerkhidmatanDto
    {
        /// <summary>
        /// Unique identifier of the service group.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Code associated with the service group.
        /// </summary>
        public string Kod { get; set; } = string.Empty;

        /// <summary>
        /// Name of the service group.
        /// </summary>
        public string Nama { get; set; } = string.Empty;

        /// <summary>
        /// Optional description of the service group.
        /// </summary>
        public string? Keterangan { get; set; }

        /// <summary>
        /// Optional details regarding updates or modifications.
        /// </summary>
        public string? ButiranKemaskini { get; set; }

        /// <summary>
        /// Optional reviewer comments or remarks.
        /// </summary>
        public string? Ulasan { get; set; }

        /// <summary>
        /// Optional code for generating the group.
        /// </summary>
        public string? KodJana { get; set; }

        /// <summary>
        /// Indicates if the service group is active.
        /// </summary>
        public bool StatusAktif { get; set; } = true;

        /// <summary>
        /// Optional indicator whether the group has a defined scheme.
        /// </summary>
        public bool? IndikatorSkim { get; set; }

        /// <summary>
        /// Optional indicator whether the group is without a scheme.
        /// </summary>
        public bool? IndikatorTanpaSkim { get; set; }

        /// <summary>
        /// Optional reviewer or approver comments.
        /// </summary>
        public string? UlasanPengesah { get; set; }
    }

    /// <summary>
    /// DTO for returning summarized information about service groups (Kumpulan Perkhidmatan) with status and update details.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to display service group lists along with status and last updated date in API or UI responses.
    /// </remarks>
    public class CarlKumpulanPerkhidmatanDto
    {
        /// <summary>
        /// Serial number or sequence in the list.
        /// </summary>
        public int Bil { get; set; }

        /// <summary>
        /// Unique identifier of the service group.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Code associated with the service group.
        /// </summary>
        public string Kod { get; set; } = string.Empty;

        /// <summary>
        /// Name of the service group.
        /// </summary>
        public string Nama { get; set; } = string.Empty;

        /// <summary>
        /// Description of the service group.
        /// </summary>
        public string Keterangan { get; set; } = string.Empty;

        /// <summary>
        /// Current status of the service group.
        /// </summary>
        public string StatusKumpulanPerkhidmatan { get; set; } = string.Empty;

        /// <summary>
        /// Status of the related application or request.
        /// </summary>
        public string StatusPermohonan { get; set; } = string.Empty;

        /// <summary>
        /// Date and time when the service group was last updated.
        /// </summary>
        public DateTime TarikhKemaskini { get; set; }
    }
    /// <summary>
    /// DTO for filtering service groups (Kumpulan Perkhidmatan) based on code, name, and status.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to filter service group records in API requests or UI searches.
    /// </remarks>
    public class KumpulanPerkhidmatanFilterDto
    {
        /// <summary>
        /// Optional. Code of the service group to filter by.
        /// </summary>
        public string? Kod { get; set; }

        /// <summary>
        /// Optional. Name of the service group to filter by.
        /// </summary>
        public string? Nama { get; set; }

        /// <summary>
        /// Optional. Status of the service group.
        /// 0 = Inactive (Tidak Aktif), 1 = Active (Aktif).
        /// </summary>
        public int? StatusKumpulan { get; set; }

        /// <summary>
        /// Optional. Status of the related application or request, e.g., "Draf", "Disahkan".
        /// </summary>
        public string? StatusPermohonan { get; set; }
    }
    /// <summary>
    /// DTO for returning detailed information about a service group (Kumpulan Perkhidmatan).
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to provide detailed service group data in API responses or UI displays.
    /// </remarks>
    public class KumpulanPerkhidmatanDetailDto
    {
        /// <summary>
        /// Required. Code of the service group.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Required. Name of the service group.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Required. Description or additional information of the service group.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Required. Status of the related application or request.
        /// </summary>
        public string StatusPermohonan { get; set; }

        /// <summary>
        /// Optional. Code for generated reference (Kod Jana).
        /// </summary>
        public string? KodJana { get; set; }

        /// <summary>
        /// Optional. Review or notes for the service group.
        /// </summary>
        public string? Ulasan { get; set; }

        /// <summary>
        /// Optional. Verifier’s review or approval notes.
        /// </summary>
        public string? UlasanPengesah { get; set; }

        /// <summary>
        /// Required. Date and time when the service group record was last updated.
        /// </summary>
        public DateTime TarikhKemaskini { get; set; }

        /// <summary>
        /// Optional. Indicator whether the service group has associated schemes (Skim).
        /// </summary>
        public bool? IndikatorSkim { get; set; }

        /// <summary>
        /// Optional. Indicator whether the service group has no schemes (Tanpa Skim).
        /// </summary>
        public bool? IndikatorTanpaSkim { get; set; }

        /// <summary>
        /// Required. Status of the service group (active or inactive).
        /// </summary>
        public bool StatusAktif { get; set; }
    }

    /// <summary>
    /// DTO for returning the status of a service group (Kumpulan Perkhidmatan) along with relevant metadata.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to display service group status, details, and review information in API responses or UI.
    /// </remarks>
    public class CarlStatusKumpulanPerkhidmatanDto
    {
        /// <summary>
        /// Required. Sequential number or count in the list.
        /// </summary>
        public int Bil { get; set; }

        /// <summary>
        /// Required. Unique identifier of the service group.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Required. Code of the service group.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Required. Name of the service group.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Required. Description or additional information about the service group.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Required. Reference code for the status of the application.
        /// </summary>
        public string KodRujStatusPermohonan { get; set; }

        /// <summary>
        /// Required. Status of the application or request.
        /// </summary>
        public string StatusPermohonan { get; set; }

        /// <summary>
        /// Required. Date and time when the service group record was last updated.
        /// </summary>
        public DateTime TarikhKemaskini { get; set; }

        /// <summary>
        /// Optional. Generated reference code (Kod Jana).
        /// </summary>
        public string? KodJana { get; set; }

        /// <summary>
        /// Optional. Review notes or comments for the service group.
        /// </summary>
        public string? Ulasan { get; set; }

        /// <summary>
        /// Optional. Indicator whether the service group has associated schemes (Skim).
        /// </summary>
        public bool? IndikatorSkim { get; set; }

        /// <summary>
        /// Optional. Indicator whether the service group has no schemes (Tanpa Skim).
        /// </summary>
        public bool? IndikatorTanpaSkim { get; set; }
    }
    /// <summary>
    /// DTO for returning detailed information about a service group (Kumpulan Perkhidmatan).
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to display detailed service group information in API responses or UI.
    /// </remarks>
    public class KumpulanPerkhidmatanButiranDto
    {
        /// <summary>
        /// Required. Unique identifier of the service group.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Required. Code of the service group.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Optional. Update details or notes.
        /// </summary>
        public string? ButiranKemaskini { get; set; }

        /// <summary>
        /// Required. Reference code for the status of the application.
        /// </summary>
        public string KodRujStatusPermohonan { get; set; }

        /// <summary>
        /// Required. Name of the service group.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Required. Description or additional information about the service group.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Required. Status of the application or request.
        /// </summary>
        public string StatusPermohonan { get; set; }

        /// <summary>
        /// Required. Indicates whether the service group is active.
        /// </summary>
        public bool StatusAktif { get; set; }

        /// <summary>
        /// Optional. Date and time when the service group record was last updated.
        /// </summary>
        public DateTime? TarikhKemaskini { get; set; }
    }
    /// <summary>
    /// DTO for returning the status information of a service group (Kumpulan Perkhidmatan).
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to display the current status and related details of a service group in API responses or UI.
    /// </remarks>
    public class KumpulanPerkhidmatanStatusDto
    {
        /// <summary>
        /// Required. Unique identifier of the service group.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Required. Code of the service group.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Required. Name of the service group.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Required. Description or additional information about the service group.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Required. Indicates whether the service group is active.
        /// </summary>
        public bool StatusAktif { get; set; }

        /// <summary>
        /// Required. Reference code for the status of the application.
        /// </summary>
        public string KodRujStatusPermohonan { get; set; }

        /// <summary>
        /// Required. Status of the application or request.
        /// </summary>
        public string StatusPermohonan { get; set; }

        /// <summary>
        /// Optional. Date and time when the service group record was last updated.
        /// </summary>
        public DateTime? TarikhKemaskini { get; set; }

        /// <summary>
        /// Optional. Generated code for internal reference.
        /// </summary>
        public string? KodJana { get; set; }

        /// <summary>
        /// Optional. Indicates if the service group has a scheme.
        /// </summary>
        public bool? IndikatorSkim { get; set; }

        /// <summary>
        /// Optional. Indicates if the service group is without a scheme.
        /// </summary>
        public bool? IndikatorTanpaSkim { get; set; }

        /// <summary>
        /// Optional. Notes or comments about the service group.
        /// </summary>
        public string? Ulasan { get; set; }

        /// <summary>
        /// Optional. Approval remarks by the approver.
        /// </summary>
        public string? UlasanPengesah { get; set; }
    }
    /// <summary>
    /// DTO for returning a service group along with its reference status.
    /// Inherits all properties from <see cref="KumpulanPerkhidmatanDto"/>.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to provide both service group details and its application status in API responses or UI.
    /// </remarks>
    public class KumpulanPerkhidmatanRefStatusDto : KumpulanPerkhidmatanDto
    {
        /// <summary>
        /// Required. Reference code indicating the status of the application for the service group.
        /// </summary>
        public string KodRujStatusPermohonan { get; set; }
    }

    /// <summary>
    /// DTO for submitting a service group application with its code.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used when sending a service group application code in API requests.
    /// </remarks>
    public class HantarKumpulanPermohonanDto
    {
        /// <summary>
        /// Required. The code of the service group to be submitted.
        /// </summary>
        public string Kod { get; set; } = string.Empty;
    }
    /// <summary>
    /// DTO representing a sub-list item of a service group, including status and metadata.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to display detailed sub-list information of service groups.
    /// </remarks>
    public class KumpulanPerkhidmatanSubListDto
    {
        /// <summary>
        /// Required. Unique identifier of the sub-list item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Required. Code of the service group sub-list item.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Required. Name of the service group sub-list item.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Optional. Description or remarks of the service group sub-list item.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Indicates whether the sub-list item is active.
        /// </summary>
        public bool StatusAktif { get; set; }

        /// <summary>
        /// Required. Reference code for the application status.
        /// </summary>
        public string KodRujStatusPermohonan { get; set; }

        /// <summary>
        /// Required. Human-readable status of the application.
        /// </summary>
        public string StatusPermohonan { get; set; }

        /// <summary>
        /// Optional. Date and time when the record was last updated.
        /// </summary>
        public DateTime? TarikhKemaskini { get; set; }
    }

    //Amar
    /// <summary>
    /// DTO used for sending service group data, including details, status, and metadata.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used when transmitting service group information in requests or API payloads.
    /// </remarks>
    public class KumpulanPerkhidmatanHantarDto
    {
        /// <summary>
        /// Required. Unique identifier of the service group.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Required. Code of the service group.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Required. Name of the service group.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Optional. Description or additional information of the service group.
        /// </summary>
        public string? Keterangan { get; set; }

        /// <summary>
        /// Optional. Details of the service group, including last updates.
        /// </summary>
        public KumpulanPerkhidmatanDto? ButiranKemaskini { get; set; }

        /// <summary>
        /// Optional. Review or comments regarding the service group.
        /// </summary>
        public string? Ulasan { get; set; }

        /// <summary>
        /// Optional. Auto-generated code or reference for the service group.
        /// </summary>
        public string? KodJana { get; set; }

        /// <summary>
        /// Indicates whether the service group is active.
        /// </summary>
        public bool StatusAktif { get; set; } = true;

        /// <summary>
        /// Optional. Flag indicating whether this service group is part of a scheme.
        /// </summary>
        public bool? IndikatorSkim { get; set; }

        /// <summary>
        /// Optional. Flag indicating whether this service group is not part of any scheme.
        /// </summary>
        public bool? IndikatorTanpaSkim { get; set; }
    }
}
