using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) untuk output implikasi permohonan jawatan.
    /// Mengandungi senarai butiran jawatan yang dimansuhkan dan jawatan baharu yang diwujudkan.
    /// </summary>
    /// <author>Khairi Abu Bakar</author>
    /// <date>2025-09-11</date>
    /// <purpose>
    /// Digunakan untuk memaparkan hasil analisis implikasi permohonan jawatan,
    /// termasuk kos dan bilangan jawatan yang dimansuhkan serta jawatan baharu yang dipohon.
    /// </purpose>
    public class MansuhWujudImplikasiPermohonanOutputDto
    {
        /// <summary>
        /// Senarai butiran jawatan yang dimansuhkan.
        /// </summary>
        public List<MansuhmplikasiPermohonanDto> ButiranMansuh { get; set; } = new List<MansuhmplikasiPermohonanDto>();

        /// <summary>
        /// Senarai butiran jawatan baharu yang diwujudkan.
        /// </summary>
        public List<WujudImplikasiPermohonanDto> ButiranWujudBaru { get; set; } = new List<WujudImplikasiPermohonanDto>();
    }

    /// <summary>
    /// DTO untuk menyimpan butiran jawatan yang dimansuhkan dalam permohonan.
    /// </summary>
    /// <author>Khairi Abu Bakar</author>
    /// <date>2025-09-11</date>
    /// <purpose>
    /// Menyediakan maklumat terperinci mengenai jawatan yang dimansuhkan,
    /// termasuk gred, kos terlibat, jumlah jawatan, dan jumlah kos tahunan.
    /// </purpose>
    public class MansuhmplikasiPermohonanDto
    {
        /// <summary>
        /// Nama atau tajuk jawatan yang dimansuhkan.
        /// </summary>
        public string? PerjawatanMansuh { get; set; }

        /// <summary>
        /// Gred jawatan yang dimansuhkan.
        /// </summary>
        public string? Gred { get; set; }

        /// <summary>
        /// Kos bulanan yang terlibat akibat pemansuhan jawatan.
        /// </summary>
        public decimal? KosTerlibat { get; set; }

        /// <summary>
        /// Jumlah bilangan jawatan yang dimansuhkan.
        /// </summary>
        public int? JumlahJawatanMansuh { get; set; }

        /// <summary>
        /// Jumlah keseluruhan kos tahunan yang terlibat akibat pemansuhan jawatan.
        /// </summary>
        public decimal? JumlahKeseluruhanSetahun { get; set; }
    }

    /// <summary>
    /// DTO untuk menyimpan butiran jawatan baharu yang dipohon dalam permohonan.
    /// </summary>
    /// <author>Khairi Abu Bakar</author>
    /// <date>2025-09-11</date>
    /// <purpose>
    /// Menyediakan maklumat terperinci mengenai jawatan baharu yang dipohon,
    /// termasuk gred, kos terlibat, jumlah jawatan, dan jumlah kos tahunan.
    /// </purpose>
    public class WujudImplikasiPermohonanDto
    {
        /// <summary>
        /// Nama atau tajuk jawatan yang dipohon.
        /// </summary>
        public string? PerjawatanDipohon { get; set; }

        /// <summary>
        /// Gred jawatan yang dipohon.
        /// </summary>
        public string? GredDipohon { get; set; }

        /// <summary>
        /// Kos bulanan yang terlibat akibat pewujudan jawatan baharu.
        /// </summary>
        public decimal? KosTerlibat { get; set; }

        /// <summary>
        /// Jumlah bilangan jawatan baharu yang dipohon.
        /// </summary>
        public int? JumlahJawatanDipohon { get; set; }

        /// <summary>
        /// Jumlah keseluruhan kos tahunan bagi jawatan baharu yang dipohon.
        /// </summary>
        public decimal? JumlahKeseluruhanSetahun { get; set; }
    }
}
