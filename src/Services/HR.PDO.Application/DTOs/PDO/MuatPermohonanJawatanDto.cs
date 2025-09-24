using Microsoft.Data.SqlClient.DataClassification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.DTOs.PDO
{
    /// <summary>
    /// Data Transfer Object (DTO) untuk memuatkan maklumat ringkas permohonan jawatan.
    /// </summary>
    /// <author>Khairi Abu Bakar</author>
    /// <date>2025-09-11</date>
    /// <purpose>
    /// Digunakan sebagai struktur output untuk memaparkan maklumat asas permohonan jawatan,
    /// termasuk unit organisasi, agensi, dan nombor rujukan.
    /// </purpose>
    public class MuatPermohonanJawatanOutputDto
    {
        /// <summary>
        /// ID Unit Organisasi yang berkaitan dengan permohonan jawatan.
        /// </summary>
        public int? IdUnitOrganisasi { get; set; }

        /// <summary>
        /// Nama Unit Organisasi yang terlibat dalam permohonan.
        /// </summary>
        public string UnitOrganisasi { get; set; }

        /// <summary>
        /// Nama agensi yang membuat permohonan (jika ada).
        /// </summary>
        public string? Agensi { get; set; }

        /// <summary>
        /// Nombor rujukan rasmi permohonan jawatan.
        /// </summary>
        public string? NomborRujukan { get; set; }
    }

}
