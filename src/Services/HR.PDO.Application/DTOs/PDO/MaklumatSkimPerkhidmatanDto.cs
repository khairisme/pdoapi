using HR.PDO.Core.Entities.PDO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.DTOs.PDO
{
    /// <summary>
    /// DTO for returning search results of service schemes with detailed information.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to display service scheme information, including classifications, grades, approvals, and related metadata.
    /// </remarks>
    public class MaklumatSkimPerkhidmatanSearchResponseDto
    {
        /// <summary>
        /// Required. Sequence number in the result list.
        /// </summary>
        public int Bil { get; set; }

        /// <summary>
        /// Required. Unique identifier of the service scheme.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Required. Code of the service scheme.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Required. Name of the service scheme.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Required. Description or remarks of the service scheme.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Required. Current status of the service scheme.
        /// </summary>
        public string StatusSkimPerkhidmatan { get; set; }

        /// <summary>
        /// Required. Status of the application for this scheme.
        /// </summary>
        public string StatusPermohonan { get; set; }

        /// <summary>
        /// Optional. Date of the last update.
        /// </summary>
        public DateTime? TarikhKemaskini { get; set; }

        /// <summary>
        /// Optional. Indicator whether the scheme is part of a service.
        /// </summary>
        public bool? IndikatorSkim { get; set; }

        /// <summary>
        /// Optional. Currency code associated with the scheme.
        /// </summary>
        public string? KodRujMatawang { get; set; }

        /// <summary>
        /// Optional. Total amount associated with the scheme.
        /// </summary>
        public decimal? Jumlah { get; set; }

        /// <summary>
        /// Required. Identifier for the classification of the service.
        /// </summary>
        public int IdKlasifikasiPerkhidmatan { get; set; }

        /// <summary>
        /// Required. Identifier for the service group.
        /// </summary>
        public int IdKumpulanPerkhidmatan { get; set; }

        /// <summary>
        /// Required. Indicates if the service scheme is active.
        /// </summary>
        public bool StatusAktif { get; set; } = true;

        /// <summary>
        /// List of grades associated with the scheme.
        /// </summary>
        public List<GredResponseDTO> gredResponseDTOs { get; set; }

        /// <summary>
        /// List of heads of service associated with the scheme.
        /// </summary>
        public List<SkimKetuaPerkhidmatanResponseDTO> skimKetuaPerkhidmatanResponseDTOs { get; set; }

        /// <summary>
        /// Optional. Comma-separated string of grade IDs.
        /// </summary>
        public string idGred { get; set; }

        /// <summary>
        /// Optional. Comma-separated string of position IDs.
        /// </summary>
        public string idJawatan { get; set; }

        /// <summary>
        /// Required. Identifier for the scheme search.
        /// </summary>
        public int carianSkimId { get; set; }

        /// <summary>
        /// Required. Indicates if the scheme is critical.
        /// </summary>
        public bool indikatorSkimKritikal { get; set; }

        /// <summary>
        /// Required. Indicates if there is an increment in PGT.
        /// </summary>
        public bool indikatorKenaikanPGT { get; set; }

        /// <summary>
        /// Optional. Reference code for the scheme status.
        /// </summary>
        public string? KodRujStatusSkim { get; set; }

        /// <summary>
        /// Optional. Reference code for the type of remuneration.
        /// </summary>
        public string? KodRujJenisSaraan { get; set; }

        /// <summary>
        /// Optional. Name of the type of remuneration.
        /// </summary>
        public string? KodRujJenisSaraanNama { get; set; }

        /// <summary>
        /// Optional. Approval remarks from the verifier.
        /// </summary>
        public string? UlasanPengesah { get; set; }
    }
    /// <summary>
    /// DTO for filtering service scheme records based on multiple criteria.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to filter service schemes in search queries or API endpoints.
    /// </remarks>
    public class MaklumatSkimPerkhidmatanFilterDto
    {
        /// <summary>
        /// Optional. Code of the service scheme to filter by.
        /// </summary>
        public string? Kod { get; set; }

        /// <summary>
        /// Optional. Name of the service scheme to filter by.
        /// </summary>
        public string? Nama { get; set; }

        /// <summary>
        /// Optional. Identifier of the related classification for filtering.
        /// </summary>
        public int? MaklumatKlasifikasiPerkhidmatanId { get; set; }

        /// <summary>
        /// Optional. Identifier of the related service group for filtering.
        /// </summary>
        public int? MaklumatKumpulanPerkhidmatanId { get; set; }

        /// <summary>
        /// Optional. Application status to filter by (e.g., "Draf", "Disahkan").
        /// </summary>
        public string? StatusPermohonan { get; set; }

        /// <summary>
        /// Optional. Reference code for the type of remuneration to filter by.
        /// </summary>
        public string? KodRujJenisSaraan { get; set; }
    }
    /// <summary>
    /// DTO for creating a new service scheme record with all relevant details.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to submit service scheme creation requests via API or service layer.
    /// </remarks>
    public class MaklumatSkimPerkhidmatanCreateRequestDto
    {
        /// <summary>
        /// Required. Identifier of the related service classification.
        /// </summary>
        public int IdKlasifikasiPerkhidmatan { get; set; }

        /// <summary>
        /// Required. Identifier of the related service group.
        /// </summary>
        public int IdKumpulanPerkhidmatan { get; set; }

        /// <summary>
        /// Optional. Code of the service scheme.
        /// </summary>
        public string? Kod { get; set; }

        /// <summary>
        /// Required. Name of the service scheme.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Required. Description of the service scheme.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Required. Indicates if this scheme is critical.
        /// </summary>
        public bool IndikatorSkimKritikal { get; set; }

        /// <summary>
        /// Required. Indicates if the scheme is eligible for PGT increase.
        /// </summary>
        public bool IndikatorKenaikanPGT { get; set; }

        /// <summary>
        /// Required. Comma-separated list of grade IDs associated with the scheme.
        /// </summary>
        public string IdGred { get; set; }

        /// <summary>
        /// Required. Comma-separated list of job IDs associated with the scheme.
        /// </summary>
        public string IdJawatan { get; set; }

        /// <summary>
        /// Identifier of this record (default is 0 for new entries).
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// Required. Code of the related classification.
        /// </summary>
        public string KlasifikasiKod { get; set; }

        /// <summary>
        /// Required. Code of the related service group.
        /// </summary>
        public string KumpulanKod { get; set; }

        /// <summary>
        /// Required. Code of the service type.
        /// </summary>
        public string JenisKod { get; set; }

        /// <summary>
        /// Required. Indicates if this is a service scheme.
        /// </summary>
        public bool IndikatorSkim { get; set; }

        /// <summary>
        /// Optional. Currency code for the scheme's monetary value.
        /// </summary>
        public string? KodRujMatawang { get; set; }

        /// <summary>
        /// Optional. Monetary amount associated with the scheme.
        /// </summary>
        public decimal? Jumlah { get; set; }

        /// <summary>
        /// Optional. Identifier used for scheme search reference.
        /// </summary>
        public int CarianSkimId { get; set; } = 0;

        /// <summary>
        /// Optional. Notes or details of updates to the scheme.
        /// </summary>
        public string? ButiranKemaskini { get; set; }

        /// <summary>
        /// Optional. Reference code for the scheme's status.
        /// </summary>
        public string? KodRujStatusSkim { get; set; }

        /// <summary>
        /// Optional. Reference code for the application status.
        /// </summary>
        public string? KodRujStatusPermohonan { get; set; }

        /// <summary>
        /// Required. List of grades associated with the scheme.
        /// </summary>
        public List<GredResponseDTO> gredResponseDTOs { get; set; }

        /// <summary>
        /// Required. List of service head responses associated with the scheme.
        /// </summary>
        public List<SkimKetuaPerkhidmatanResponseDTO> skimKetuaPerkhidmatanResponseDTOs { get; set; }

        /// <summary>
        /// Optional. Reference code for the type of remuneration.
        /// </summary>
        public string? KodRujJenisSaraan { get; set; }
    }
    /// <summary>
    /// DTO for returning detailed information about a service scheme.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to provide service scheme details in API responses or service layer.
    /// </remarks>
    public class MaklumatSkimPerkhidmatanResponseDto
    {
        /// <summary>
        /// Required. Identifier of the service scheme.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Required. Code of the service scheme.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Required. Name of the service scheme.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Required. Description of the service scheme.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Required. Code of the related service classification.
        /// </summary>
        public string KodKlasifikasiPerkhidmatan { get; set; }

        /// <summary>
        /// Required. Name of the related service classification.
        /// </summary>
        public string KlasifikasiPerkhidmatan { get; set; }

        /// <summary>
        /// Required. Code of the related service group.
        /// </summary>
        public string KodKumpulanPerkhidmatan { get; set; }

        /// <summary>
        /// Required. Name of the related service group.
        /// </summary>
        public string KumpulanPerkhidmatan { get; set; }

        /// <summary>
        /// Optional. Indicates whether this is a service scheme.
        /// </summary>
        public bool? IndikatorSkim { get; set; }

        /// <summary>
        /// Required. Currency code for monetary value associated with the scheme.
        /// </summary>
        public string KodRujMatawang { get; set; }

        /// <summary>
        /// Optional. Monetary amount associated with the scheme.
        /// </summary>
        public decimal? Jumlah { get; set; }

        /// <summary>
        /// Required. Reference code for the status of the scheme.
        /// </summary>
        public string KodRujStatusSkim { get; set; }
    }
    /// <summary>
    /// DTO for filtering service schemes based on code, name, or status.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to pass filtering criteria for service schemes in API requests.
    /// </remarks>
    public class SkimPerkhidmatanFilterDto
    {
        /// <summary>
        /// Optional. Code of the service scheme to filter by.
        /// </summary>
        public string? Kod { get; set; }

        /// <summary>
        /// Optional. Name of the service scheme to filter by.
        /// </summary>
        public string? Nama { get; set; }

        /// <summary>
        /// Optional. Reference code for the status of the scheme to filter by.
        /// </summary>
        public string? KodRujStatusPermohonan { get; set; }
    }
    /// <summary>
    /// DTO for representing service scheme information with status, indicators, and financial details.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return service scheme data in API responses or UI lists.
    /// </remarks>
    public class SkimPerkhidmatanDto
    {
        /// <summary>
        /// Serial number or row number in a list.
        /// </summary>
        public int Bil { get; set; }

        /// <summary>
        /// Unique identifier for the service scheme.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Code of the service scheme.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Name of the service scheme.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Description or remarks about the service scheme.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Status of the service scheme (e.g., Active, Inactive).
        /// </summary>
        public string StatusSkimPerkhidmatan { get; set; }

        /// <summary>
        /// Status of the application or approval for the scheme.
        /// </summary>
        public string StatusPermohonan { get; set; }

        /// <summary>
        /// Last updated date for the scheme details.
        /// </summary>
        public DateTime? TarikhKemaskini { get; set; }

        /// <summary>
        /// Indicator if this is a critical scheme.
        /// </summary>
        public bool? IndikatorSkim { get; set; }

        /// <summary>
        /// Optional. Currency reference code.
        /// </summary>
        public string? KodRujMatawang { get; set; }

        /// <summary>
        /// Optional. Amount associated with the scheme.
        /// </summary>
        public decimal? Jumlah { get; set; }
    }
    /// <summary>
    /// DTO for returning a service scheme along with associated position (Jawatan) details.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to display or return service scheme information linked to a specific position.
    /// </remarks>
    public class SkimWithJawatanDto
    {
        /// <summary>
        /// Unique identifier for the service scheme.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Code of the service scheme.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Name of the service scheme.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Optional. Code of the associated position (Jawatan).
        /// </summary>
        public string? KodJawatan { get; set; }

        /// <summary>
        /// Optional. Name of the associated position (Jawatan).
        /// </summary>
        public string? NamaJawatan { get; set; }
    }
    /// <summary>
    /// DTO for filtering service scheme (Skim Perkhidmatan) search results.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to provide filter criteria when searching for service schemes.
    /// </remarks>
    public class CarianSkimPerkhidmatanFilterDto
    {
        /// <summary>
        /// Optional. Code of the service scheme to filter.
        /// </summary>
        public string? Kod { get; set; }

        /// <summary>
        /// Optional. Name of the service scheme to filter.
        /// </summary>
        public string? Nama { get; set; }

        /// <summary>
        /// Optional. Identifier of the service classification to filter.
        /// </summary>
        public int? KlasifikasiPerkhidmatanId { get; set; }

        /// <summary>
        /// Optional. Identifier of the service group to filter.
        /// </summary>
        public int? KumpulanPerkhidmatanId { get; set; }
    }
    /// <summary>
    /// DTO for returning search results of service schemes (Skim Perkhidmatan).
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to display search results for service schemes in UI or API responses.
    /// </remarks>
    public class CarianSkimPerkhidmatanResponseDto
    {
        /// <summary>
        /// Sequential number in the search result list.
        /// </summary>
        public int Bil { get; set; }

        /// <summary>
        /// Unique identifier of the service scheme.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Code of the service scheme.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Name of the service scheme.
        /// </summary>
        public string Nama { get; set; }
    }
    /// <summary>
    /// DTO for returning basic information of a service scheme (Skim Perkhidmatan).
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return the ID, code, and name of a service scheme in API responses.
    /// </remarks>
    public class SkimPerkhidmatanResponseDto
    {
        /// <summary>
        /// Unique identifier of the service scheme.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Code of the service scheme.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Name of the service scheme.
        /// </summary>
        public string Nama { get; set; }
    }
    /// <summary>
    /// DTO for returning detailed information of a service scheme (Skim Perkhidmatan).
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return detailed information including code, status, and update details of a service scheme.
    /// </remarks>
    public class SkimPerkhidmatanButiranDto
    {
        /// <summary>
        /// Unique identifier of the service scheme.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Code of the service scheme.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Optional. Details about recent updates or changes.
        /// </summary>
        public string? ButiranKemaskini { get; set; }

        /// <summary>
        /// Code referencing the status of the application.
        /// </summary>
        public string KodRujStatusPermohonan { get; set; }

        /// <summary>
        /// Name of the service scheme.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Description of the service scheme.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Status of the application.
        /// </summary>
        public string StatusPermohonan { get; set; }

        /// <summary>
        /// Indicates whether the service scheme is active.
        /// </summary>
        public bool StatusAktif { get; set; }

        /// <summary>
        /// Optional. Date when the record was last updated.
        /// </summary>
        public DateTime? TarikhKemaskini { get; set; }
    }
    /// <summary>
    /// DTO for referencing the status of a service scheme (Skim Perkhidmatan) including additional approval remarks.
    /// Inherits from <see cref="MaklumatSkimPerkhidmatanCreateRequestDto"/>.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return or submit service scheme data along with its current status and optional approver comments.
    /// </remarks>
    public class SkimPerkhidmatanRefStatusDto : MaklumatSkimPerkhidmatanCreateRequestDto
    {
        /// <summary>
        /// Code referencing the status of the service scheme.
        /// </summary>
        public string KodRujStatusSkim { get; set; }

        /// <summary>
        /// Optional. Comments or notes from the approver.
        /// </summary>
        public string? UlasanPengesah { get; set; }
    }
    /// <summary>
    /// DTO for updating a service scheme (Skim Perkhidmatan) in the PDO system.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to submit updated service scheme details including critical indicators, approval status, and related Gred or Jawatan data.
    /// </remarks>
    public class PDOUpdateSkimPerkhidmatan
    {
        /// <summary>
        /// Required. Unique identifier for the service scheme record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Required. Identifier for the classification of the service.
        /// </summary>
        public int IdKlasifikasiPerkhidmatan { get; set; }

        /// <summary>
        /// Required. Identifier for the service group.
        /// </summary>
        public int IdKumpulanPerkhidmatan { get; set; }

        /// <summary>
        /// Optional. Identifier for the head of the service.
        /// </summary>
        public int? IdKetuaPerkhidmatan { get; set; }

        /// <summary>
        /// Required. Service scheme code.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Required. Service scheme name.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Optional. Description or notes about the service scheme.
        /// </summary>
        public string? Keterangan { get; set; }

        /// <summary>
        /// Indicates whether the service scheme is critical.
        /// </summary>
        public bool IndikatorSkimKritikal { get; set; }

        /// <summary>
        /// Indicates whether the service scheme is eligible for PGT increase.
        /// </summary>
        public bool IndikatorKenaikanPGT { get; set; }

        /// <summary>
        /// Optional. Details of updates or changes.
        /// </summary>
        public string? ButiranKemaskini { get; set; }

        /// <summary>
        /// Optional. Reference code for the status of the service scheme.
        /// </summary>
        public string? KodRujStatusSkim { get; set; }

        /// <summary>
        /// Optional. Indicates if the scheme is active.
        /// </summary>
        public bool? IndikatorSkim { get; set; }

        /// <summary>
        /// Optional. Currency code associated with the scheme.
        /// </summary>
        public string? KodRujMatawang { get; set; }

        /// <summary>
        /// Optional. Related Gred identifier(s).
        /// </summary>
        public string? IdGred { get; set; }

        /// <summary>
        /// Optional. Related Jawatan identifier(s).
        /// </summary>
        public string? IdJawatan { get; set; }

        /// <summary>
        /// Optional. Amount associated with the scheme.
        /// </summary>
        public decimal? Jumlah { get; set; }

        /// <summary>
        /// Optional. List of Gred responses associated with this scheme.
        /// </summary>
        public List<GredResponseDTO>? gredResponseDTOs { get; set; }

        /// <summary>
        /// Optional. List of Ketua Perkhidmatan responses associated with this scheme.
        /// </summary>
        public List<SkimKetuaPerkhidmatanResponseDTO>? skimKetuaPerkhidmatanResponseDTOs { get; set; }
    }
    /// <summary>
    /// DTO representing a response for a Gred (grade) entry in the system.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-09-02
    /// Purpose     : Used to return Gred information including identifiers, code, name, and optional description.
    /// </remarks>
    public class GredResponseDTO
    {
        /// <summary>
        /// Serial number for display purposes.
        /// </summary>
        public int Bil { get; set; }

        /// <summary>
        /// Unique identifier for the Gred.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Optional code representing the Gred.
        /// </summary>
        public string? Kod { get; set; }

        /// <summary>
        /// Optional name of the Gred.
        /// </summary>
        public string? Nama { get; set; }

        /// <summary>
        /// Optional description or notes about the Gred.
        /// </summary>
        public string? Keterangan { get; set; }
    }
    /// <summary>
    /// DTO representing detailed information of a Skim Perkhidmatan entry.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-09-02
    /// Purpose     : Used to return detailed information about a Skim Perkhidmatan, including associated jawatan and organizational unit.
    /// </remarks>
    public class SkimPerkhidmatanDetailsDTO
    {
        /// <summary>
        /// Unique identifier for the Skim Perkhidmatan record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique identifier for the associated Jawatan.
        /// </summary>
        public int IdJawatan { get; set; }

        /// <summary>
        /// Optional code for the Skim Perkhidmatan.
        /// </summary>
        public string? Kod { get; set; }

        /// <summary>
        /// Optional name for the Skim Perkhidmatan.
        /// </summary>
        public string? Nama { get; set; }

        /// <summary>
        /// Optional code for the Jawatan.
        /// </summary>
        public string? KodJawatan { get; set; }

        /// <summary>
        /// Optional name of the Jawatan.
        /// </summary>
        public string? NamaJawatan { get; set; }

        /// <summary>
        /// Optional organizational unit associated with the Skim Perkhidmatan.
        /// </summary>
        public string? UnitOrganisasi { get; set; }
    }
    /// <summary>
    /// DTO representing the response details for a Skim Ketua Perkhidmatan entry.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-09-02
    /// Purpose     : Used to return detailed information about Ketua Perkhidmatan including associated jawatan and identifiers.
    /// </remarks>
    public class SkimKetuaPerkhidmatanResponseDTO
    {
        /// <summary>
        /// Sequential number for display or ordering purposes.
        /// </summary>
        public int Bil { get; set; }

        /// <summary>
        /// Unique identifier for the Skim Ketua Perkhidmatan record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique identifier for the associated Jawatan.
        /// </summary>
        public int IdJawatan { get; set; }

        /// <summary>
        /// Code for the Skim Ketua Perkhidmatan.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Name of the Skim Ketua Perkhidmatan.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Code of the associated Jawatan.
        /// </summary>
        public string KodJawatan { get; set; }

        /// <summary>
        /// Name of the associated Jawatan.
        /// </summary>
        public string NamaJawatan { get; set; }
    }

}
