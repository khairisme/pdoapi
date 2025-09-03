using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.DTOs.PDO
{
    /// <summary>
    /// DTO representing a job position along with its associated agency.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return job positions and their related agency information in API responses.
    /// </remarks>
    public class JawatanWithAgensiDto
    {
        /// <summary>
        /// Required. Unique identifier of the job position.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Required. Code of the job position.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Required. Name of the job position.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Required. Name of the agency associated with the job position.
        /// </summary>
        public string Agensi { get; set; }
    }
    /// <summary>
    /// DTO for filtering job positions during search operations.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to provide filtering criteria when searching for job positions.
    /// </remarks>
    public class CarianJawatanFilterDto
    {
        /// <summary>
        /// Optional. Identifier of the service scheme to filter by.
        /// </summary>
        public int? SkimPerkhidmatanId { get; set; }

        /// <summary>
        /// Optional. Name of the job position to filter by.
        /// </summary>
        public string? NamaJawatan { get; set; }

        /// <summary>
        /// Optional. Name of the organizational unit to filter by.
        /// </summary>
        public string? UnitOrganisasi { get; set; }
    }
    /// <summary>
    /// DTO for returning job position search results.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return detailed information of job positions for UI or API responses.
    /// </remarks>
    public class CarianJawatanResponseDto
    {
        /// <summary>
        /// Serial number or sequence number in the search result.
        /// </summary>
        public int Bil { get; set; }

        /// <summary>
        /// Unique identifier of the job position.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Code representing the job position.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Name of the job position.
        /// </summary>
        public string NamaJawatan { get; set; }

        /// <summary>
        /// Name of the organizational unit where the job position belongs.
        /// </summary>
        public string UnitOrganisasi { get; set; }

        /// <summary>
        /// Status of the job position filling (e.g., vacant, filled).
        /// </summary>
        public string StatusPengisian { get; set; }

        /// <summary>
        /// Date when the vacancy status was last updated.
        /// </summary>
        public DateTime? TarikhStatusKekosongan { get; set; }

        /// <summary>
        /// Indicates whether the job position is selected in the UI.
        /// </summary>
        public bool TickCheckBox { get; set; }
    }
    /// <summary>
    /// DTO for filtering real job positions (Jawatan Sebenar) in search queries.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to filter job positions based on various criteria for UI or API queries.
    /// </remarks>
    public class CarianJawatanSebenarFilterDto
    {
        /// <summary>
        /// Optional. ID of the service scheme to filter job positions.
        /// </summary>
        public int? SkimPerkhidmatanId { get; set; }

        /// <summary>
        /// Optional. Code of the actual job position.
        /// </summary>
        public string? KodJawatanSebenar { get; set; }

        /// <summary>
        /// Optional. Name of the actual job position.
        /// </summary>
        public string? NamaJawatanSebenar { get; set; }

        /// <summary>
        /// Optional. Status of the job vacancy (e.g., vacant, filled).
        /// </summary>
        public string? StatusKekosonganJawatan { get; set; }

        /// <summary>
        /// Optional. ID of the organizational unit where the job position belongs.
        /// </summary>
        public int? UnitOrganisasiId { get; set; }
    }
    /// <summary>
    /// DTO for returning real job positions (Jawatan Sebenar) in search results.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to present details of actual job positions for UI or API responses.
    /// </remarks>
    public class CarianJawatanSebenarResponseDto
    {
        /// <summary>
        /// Serial number or sequence in the result set.
        /// </summary>
        public int Bil { get; set; }

        /// <summary>
        /// Unique identifier of the job position.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Code of the job position.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Name of the job position.
        /// </summary>
        public string NamaJawatan { get; set; }

        /// <summary>
        /// Name of the organizational unit the job belongs to.
        /// </summary>
        public string UnitOrganisasi { get; set; }

        /// <summary>
        /// Status of the job position (e.g., filled, vacant).
        /// </summary>
        public string StatusPengisian { get; set; }

        /// <summary>
        /// Optional. Date when the status of the job vacancy was last updated.
        /// </summary>
        public DateTime? TarikhStatusKekosongan { get; set; }

        /// <summary>
        /// Indicates whether the job is selected in the UI (checkbox state).
        /// </summary>
        public bool TickCheckBox { get; set; }
    }

    /// <summary>
    /// DTO for requesting real job positions (Jawatan Sebenar) based on filters.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to filter and fetch actual job positions via API or service.
    /// </remarks>
    public class CarianJawatanSebenarReqDto
    {
        /// <summary>
        /// Identifier of the service scheme (Skim Perkhidmatan).
        /// </summary>
        public int IdSkimPerkhidmatan { get; set; }

        /// <summary>
        /// Code of the organizational chart (Kod Carta). Default is empty string.
        /// </summary>
        public string KodCarta { get; set; } = string.Empty;

        /// <summary>
        /// Optional. Code of the specific real job position (Jawatan Sebenar) to filter.
        /// </summary>
        public string? KodJawatanSebenar { get; set; }

        /// <summary>
        /// Optional. Status of the job vacancy to filter (e.g., filled, vacant).
        /// </summary>
        public string? StatusKekosonganJawatan { get; set; }

        /// <summary>
        /// Optional. Identifier of the organizational unit to filter.
        /// </summary>
        public int? UnitOrganisasi { get; set; }
    }
    /// <summary>
    /// DTO for returning details of real job positions (Jawatan Sebenar).
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return job position details from API or service responses.
    /// </remarks>
    public class CarianJawatanSebenarRespDto
    {
        /// <summary>
        /// Unique identifier of the job position record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Code of the job position.
        /// </summary>
        public string Kod { get; set; } = string.Empty;

        /// <summary>
        /// Name of the job position.
        /// </summary>
        public string NamaJawatan { get; set; } = string.Empty;

        /// <summary>
        /// Name of the organizational unit the job belongs to.
        /// </summary>
        public string UnitOrganisasi { get; set; } = string.Empty;

        /// <summary>
        /// Current status of the job position (e.g., filled, vacant).
        /// </summary>
        public string StatusPengisian { get; set; } = string.Empty;

        /// <summary>
        /// Optional. Date when the job position became vacant.
        /// </summary>
        public DateTime? TarikhKekosonganJawatan { get; set; }
    }

    // code added by amar 220725
    /// <summary>
    /// DTO for returning details of the head of service (Ketua Perkhidmatan).
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return Ketua Perkhidmatan details from API or service responses.
    /// </remarks>
    public class CarianKetuaPerkhidmatanResponseDto
    {
        /// <summary>
        /// Unique identifier of the head of service.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the head of service.
        /// </summary>
        public string Nama { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO for returning a list of heads of service (Ketua Perkhidmatan) with their positions and agencies.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to return a list of Ketua Perkhidmatan details from API or service responses.
    /// </remarks>
    public class SenaraiKetuaPerkhidmatanResponseDto
    {
        /// <summary>
        /// Unique identifier of the head of service.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Code associated with the head of service.
        /// </summary>
        public string Kod { get; set; } = string.Empty;

        /// <summary>
        /// Job title or position of the head of service.
        /// </summary>
        public string Jawatan { get; set; } = string.Empty;

        /// <summary>
        /// Agency the head of service belongs to.
        /// </summary>
        public string Agensi { get; set; } = string.Empty;
    }
    //end
}
