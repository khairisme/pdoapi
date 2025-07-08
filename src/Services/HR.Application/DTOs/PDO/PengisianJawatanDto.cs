using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs.PDO
{
    internal class PengisianJawatanDto
    {

    }
    public class PengisianJawatanFilterDto
    {
        #region Properties
        /// <summary>
        /// Gets or sets the Service Scheme ID (IdSkimPerkhidmatan parameter from SQL)
        /// </summary>
        public int IdSkimPerkhidmatan { get; set; }

        /// <summary>
        /// Gets or sets the Application ID
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// Gets or sets the Company ID
        /// </summary>
        public int CompId { get; set; }

        /// <summary>
        /// Gets or sets the User ID
        /// </summary>
        public string UserId { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the User GUID
        /// </summary>
        public string UserGUID { get; set; } = String.Empty;
        #endregion
    }
    public class PengisianJawatanSearchResponseDto
    {
        #region Properties
        /// <summary>
        /// Gets or sets the Pengisian Jawatan ID (a.Id from PDO_PengisianJawatan)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Job Position Code (b.kod as KodJawatan from PDO_Jawatan)
        /// </summary>
        public string KodJawatan { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the Job Position Name (b.Nama as NamaJawatan from PDO_Jawatan)
        /// </summary>
        public string NamaJawatan { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the Organizational Unit Name (c.Nama as UnitOrganisasi from PDO_UnitOrganisasi)
        /// </summary>
        public string UnitOrganisasi { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the Job Position Filling Status (e.Nama as StatusPengisianJawatan from PDO_RujStatusKekosonganJawatan)
        /// </summary>
        public string StatusPengisianJawatan { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the Job Vacancy Status Date (d.TarikhStatusKekosongan as TarikhKekosonganJawatan from PDO_KekosonganJawatan)
        /// </summary>
        public DateTime? TarikhKekosonganJawatan { get; set; }
        #endregion
    }
}
