using HR.PDO.Core.Entities.PDO;
using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) untuk menyimpan butiran permohonan
    /// berdasarkan kategori dan aktiviti organisasi.
    /// </summary>
    /// <author>Khairi Abu Bakar</author>
    /// <date>2025-09-11</date>
    /// <purpose>
    /// Digunakan untuk memaparkan dan mengurus maklumat permohonan jawatan
    /// mengikut aktiviti organisasi serta senarai butiran permohonan yang berkaitan.
    /// </purpose>
    public class ButiranPermohonanIkutAktivitiOrganisasiDto
    {
        /// <summary>
        /// Nama kategori dan aktiviti organisasi yang terlibat.
        /// </summary>
        public string? NamaKategoriDanAktivitiOrganisasi { get; set; }

        /// <summary>
        /// Senarai butiran permohonan yang berkaitan dengan kategori atau aktiviti organisasi.
        /// </summary>
        public List<ButiranPermohonanSemakDrafWPSKP> ButiranPermohonan { get; set; }
    }

    /// <summary>
    /// DTO untuk menyimpan maklumat butiran permohonan
    /// yang digunakan semasa semakan draf WPSKP.
    /// </summary>
    /// <author>Khairi Abu Bakar</author>
    /// <date>2025-09-11</date>
    /// <purpose>
    /// Menyimpan maklumat terperinci mengenai setiap butiran permohonan,
    /// termasuk nombor, tajuk, kod skim/gaji, bilangan jawatan dan butiran perubahan.
    /// </purpose>
    public class ButiranPermohonanSemakDrafWPSKP
    {
        /// <summary>
        /// Nombor butiran permohonan.
        /// </summary>
        public string? NoButiran { get; set; }

        /// <summary>
        /// Tajuk atau nama ringkas bagi butiran permohonan.
        /// </summary>
        public string? TajukButiran { get; set; }

        /// <summary>
        /// Kod gaji yang berkaitan dengan butiran permohonan.
        /// </summary>
        public string? KodGaji { get; set; }

        /// <summary>
        /// Kod skim jawatan yang berkaitan dengan butiran permohonan.
        /// </summary>
        public string? KodSkim { get; set; }

        /// <summary>
        /// Bilangan jawatan yang terlibat dalam permohonan.
        /// </summary>
        public int? BilanganJawatan { get; set; }

        /// <summary>
        /// Penerangan mengenai perubahan yang dicadangkan atau disemak.
        /// </summary>
        public string? ButirPerubahan { get; set; }
    }
}
