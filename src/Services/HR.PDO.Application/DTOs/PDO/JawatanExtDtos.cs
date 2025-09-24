using System;
using System.ComponentModel;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO untuk menyimpan senarai ID jawatan.
    /// </summary>
    /// <author>Khairi Abu Bakar</author>
    /// <date>2025-09-11</date>
    /// <purpose>
    /// Digunakan untuk menghantar senarai ID jawatan
    /// sebagai parameter dalam operasi berkaitan jawatan.
    /// </purpose>
    public class JawatanListDto
    {
        /// <summary>
        /// Senarai ID jawatan yang terlibat.
        /// </summary>
        public List<int?> IdJawatanList { get; set; }
    }

    /// <summary>
    /// DTO untuk permintaan butiran jawatan.
    /// Mengandungi maklumat penapisan, carian, dan tetapan paparan.
    /// </summary>
    /// <author>Khairi Abu Bakar</author>
    /// <date>2025-09-11</date>
    /// <purpose>
    /// Menyediakan parameter yang diperlukan untuk mendapatkan
    /// senarai butiran jawatan dengan sokongan carian, penapisan,
    /// pagination, dan susunan.
    /// </purpose>
    public class ButiranJawatanRequestDto
    {
        /// <summary>
        /// ID permohonan jawatan (jika berkaitan).
        /// </summary>
        public int? IdPermohonanJawatan { get; set; }

        /// <summary>
        /// ID butiran permohonan (jika berkaitan).
        /// </summary>
        public int? IdButiranPermohonan { get; set; }

        /// <summary>
        /// Nombor halaman untuk pagination.
        /// </summary>
        [DefaultValue(1)]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Bilangan rekod setiap halaman.
        /// </summary>
        [DefaultValue(50)]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Medan yang digunakan untuk tujuan penyusunan data.
        /// Contoh: "NomborRujukan", "Tajuk", "Status", "TarikhPermohonan".
        /// </summary>
        [DefaultValue("TarikhPermohonan")]
        public string? SortBy { get; set; }

        /// <summary>
        /// Tentukan sama ada susunan adalah menurun (descending).
        /// </summary>
        [DefaultValue(false)]
        public bool Desc { get; set; }

        /// <summary>
        /// Tentukan sama ada data anak (children) perlu disertakan.
        /// </summary>
        [DefaultValue(true)]
        public bool IncludeChildren { get; set; } = true;

        /// <summary>
        /// Kata kunci untuk fungsi carian umum.
        /// </summary>
        public string search { get; set; }
    }

    /// <summary>
    /// DTO untuk menyimpan maklumat pautan jawatan.
    /// </summary>
    /// <author>Khairi Abu Bakar</author>
    /// <date>2025-09-11</date>
    /// <purpose>
    /// Digunakan untuk mewakili jawatan yang dipautkan,
    /// termasuk maklumat asas jawatan, unit organisasi, dan penyandang.
    /// </purpose>
    public class JawatanLinkDto
    {
        /// <summary>
        /// ID jawatan.
        /// </summary>
        public int? IdJawatan { get; set; }

        /// <summary>
        /// Kod jawatan rasmi.
        /// </summary>
        public string? KodJawatan { get; set; }

        /// <summary>
        /// Nama jawatan.
        /// </summary>
        public string? NamaJawatan { get; set; }

        /// <summary>
        /// Nama unit organisasi bagi jawatan.
        /// </summary>
        public string? UnitOrganisasi { get; set; }

        /// <summary>
        /// Nama penyandang semasa jawatan.
        /// </summary>
        public string? NamaPenyandang { get; set; }
    }
}
