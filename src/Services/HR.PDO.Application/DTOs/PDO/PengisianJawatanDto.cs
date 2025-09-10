using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.DTOs.PDO
{
    /// <summary>
    /// DTO representing the filling of a position within a service application.
    /// </summary>
    public class PengisianJawatanDto
    {
        /// <summary>
        /// The ID of the position (Jawatan).
        /// </summary>
        public int IdJawatan { get; set; }

        /// <summary>
        /// The ID of the service application (Permohonan Pengisian Skim).
        /// </summary>
        public int IdPermohonanPengisianSkim { get; set; }

        // Uncomment if needed in future for user tracking
        // public int UserId { get; set; } 
    }
    /// <summary>
    /// DTO for filtering position filling (Pengisian Jawatan) requests.
    /// </summary>
    public class PengisianJawatanFilterDto
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Service Scheme ID (IdSkimPerkhidmatan parameter from SQL).
        /// </summary>
        public int IdSkimPerkhidmatan { get; set; }

        /// <summary>
        /// Gets or sets the Application ID.
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// Gets or sets the Company ID.
        /// </summary>
        public int CompId { get; set; }

        /// <summary>
        /// Gets or sets the User ID.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the User GUID.
        /// </summary>
        public string UserGUID { get; set; } = string.Empty;

        #endregion
    }
    public class PengisianJawatanSearchResponseDto
    {
        #region Properties

        /// <summary>
        /// Gets or sets the row number (for UI pagination).
        /// </summary>
        public int Bil { get; set; }

        /// <summary>
        /// Gets or sets the Id Pengisian Jawatan ID (a.Id from PDO_PengisianJawatan).
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the Pengisian Jawatan ID (a.Id from PDO_PengisianJawatan).
        /// </summary>
        public int IdPengisianJawatan { get; set; }

        /// <summary>
        /// Gets or sets the Job Position Code (b.Kod from PDO_Jawatan).
        /// </summary>
        public string KodJawatan { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Job Position Name (b.Nama from PDO_Jawatan).
        /// </summary>
        public string NamaJawatan { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Organizational Unit Name (c.Nama from PDO_UnitOrganisasi).
        /// </summary>
        public string UnitOrganisasi { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Job Position Filling Status (e.Nama from PDO_RujStatusKekosonganJawatan).
        /// </summary>
        public string StatusPengisianJawatan { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Job Vacancy Status Date (d.TarikhStatusKekosongan from PDO_KekosonganJawatan).
        /// </summary>
        public DateTime? TarikhKekosonganJawatan { get; set; }

        #endregion
    }

    //Nitya Code Start
    public class AgensiDto
    {
        public int AgensiId { get; set; }
        public string Kod { get; set; }
        public string Agensi { get; set; }
    }
    public class SkimPerkhidmatanDataDto
    {
        public string KodSkim { get; set; }
        public string NamaSkimPerkhidmatan { get; set; }
        public int BilanganPengisian { get; set; }
    }

    public class AgensiWithSkimDto
    {
        public int? AgensiId { get; set; }
        public string Kod { get; set; }
        public string Agensi { get; set; }
        public List<SkimPerkhidmatanDataDto> SkimList { get; set; } = new List<SkimPerkhidmatanDataDto>();
    }
    public class PermohonanDetailDto
    {
        public string Kementerian { get; set; }
        public string NomborRujukan { get; set; }
        public DateTime? TarikhPermohonan { get; set; }
        public string Keterangan { get; set; }
        public int BilanganPermohonan { get; set; }
        public string Ulasan { get; set; }
    }
    public class SenaraiJawatanPengisianDto
    {
        public int? Id { get; set; }
        public string KodJawatan { get; set; }
        public string NamaJawatan { get; set; }
        public string UnitOrganisasi { get; set; }
        public string StatusPengisianJawatan { get; set; }
        public DateTime? TarikhKekosonganJawatan { get; set; }
    }
    public class UnitOrganisasiDataDto
    {
        public int? IdUnitOrganisasi { get; set; }
        public string Kod { get; set; }
        public string Agensi { get; set; }
        public List<JawatanDto> JawatanList { get; set; }
    }

    public class JawatanDto
    {
        public string KodJawatan { get; set; }
        public string NamaJawatan { get; set; }
        public string Gred { get; set; }
    }

    //Nitya Code End
}
