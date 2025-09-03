using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO for returning suggested positions (Cadangan Jawatan) for a job application or organizational activity.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to transfer suggested position information including organizational unit, grade, rank, and other references.
    /// </remarks>
    public class CadanganJawatanDto
    {
        /// <summary>
        /// Required. ID of the associated organizational activity.
        /// </summary>
        public int IdAktivitiOrganisasi { get; set; }

        /// <summary>
        /// Required. ID of the detailed job application record.
        /// </summary>
        public int IdButiranPermohonan { get; set; }

        /// <summary>
        /// Required. ID of the organizational unit where the position is suggested.
        /// </summary>
        public int IdUnitOrganisasi { get; set; }

        /// <summary>
        /// Optional. Job title (Gelaran Jawatan).
        /// </summary>
        public string? GelaranJawatan { get; set; }

        /// <summary>
        /// Optional. Job grade.
        /// </summary>
        public string? Gred { get; set; }

        /// <summary>
        /// Optional. Reference code for job type.
        /// </summary>
        public string? KodRujJenisJawatan { get; set; }

        /// <summary>
        /// Optional. Reference code for job title.
        /// </summary>
        public string? KodRujGelaranJawatan { get; set; }

        /// <summary>
        /// Optional. Reference code for supply status.
        /// </summary>
        public string? KodRujStatusBekalan { get; set; }

        /// <summary>
        /// Optional. Reference code for job status.
        /// </summary>
        public string? KodRujStatusJawatan { get; set; }

        /// <summary>
        /// Optional. Rank (Pangkat) associated with the position.
        /// </summary>
        public string? Pangkat { get; set; }

        /// <summary>
        /// Optional. Current occupant (Penyandang) of the position.
        /// </summary>
        public string? Penyandang { get; set; }

        /// <summary>
        /// Optional. Service scheme (Skim Perkhidmatan) for the position.
        /// </summary>
        public string? SkimPerkhidmatan { get; set; }

        /// <summary>
        /// Optional. Number of positions suggested.
        /// </summary>
        public int? BilanganJawatan { get; set; }
    }

    /// <summary>
    /// DTO for sending a request to create or update suggested positions (Cadangan Jawatan).
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to transfer suggested position request information including organizational unit, codes, rank, and quantity.
    /// </remarks>
    public class CadanganJawatanRequestDto
    {
        /// <summary>
        /// Required. ID of the associated organizational activity.
        /// </summary>
        public int IdAktivitiOrganisasi { get; set; }

        /// <summary>
        /// Required. ID of the detailed job application record.
        /// </summary>
        public int IdButiranPermohonan { get; set; }

        /// <summary>
        /// Required. ID of the organizational unit where the position is suggested.
        /// </summary>
        public int IdUnitOrganisasi { get; set; }

        /// <summary>
        /// Optional. Reference code for job title.
        /// </summary>
        public string? KodRujGelaranJawatan { get; set; }

        /// <summary>
        /// Optional. Reference code for grade.
        /// </summary>
        public string? KodGred { get; set; }

        /// <summary>
        /// Optional. Reference code for job type.
        /// </summary>
        public string? KodRujJenisJawatan { get; set; }

        /// <summary>
        /// Optional. Reference code for supply status.
        /// </summary>
        public string? KodRujStatusBekalan { get; set; }

        /// <summary>
        /// Optional. Reference code for job status.
        /// </summary>
        public string? KodRujStatusJawatan { get; set; }

        /// <summary>
        /// Optional. Reference code for rank in uniformed services.
        /// </summary>
        public string? KodRujPangkatBadanBeruniform { get; set; }

        /// <summary>
        /// Optional. Current occupant of the position.
        /// </summary>
        public string? Penyandang { get; set; }

        /// <summary>
        /// Optional. ID of the associated service scheme.
        /// </summary>
        public int? IdSkimPerkhidmatan { get; set; }

        /// <summary>
        /// Optional. Number of positions requested.
        /// </summary>
        public int? BilanganJawatan { get; set; }
    }
}
