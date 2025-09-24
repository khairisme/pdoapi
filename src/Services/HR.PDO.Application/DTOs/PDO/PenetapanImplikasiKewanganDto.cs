using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO representing the financial implications of positions within a service scheme.
    /// </summary>
    public class PenetapanImplikasiKewanganDto
    {
        /// <summary>
        /// Monthly financial implication. Nullable.
        /// </summary>
        public decimal? ImplikasiKewanganSebulan { get; set; }

        /// <summary>
        /// Annual financial implication. Nullable.
        /// </summary>
        public decimal? ImplikasiKewanganSetahun { get; set; }

        /// <summary>
        /// Total monthly financial amount. Nullable.
        /// </summary>
        public decimal? JumlahKewanganSebulan { get; set; }

        /// <summary>
        /// Number of positions counted.
        /// </summary>
        public int? BilanganJawatan { get; set; }

        /// <summary>
        /// Grade of the position. Nullable.
        /// </summary>
        public string? Gred { get; set; }

        /// <summary>
        /// Service scheme associated with the position. Nullable.
        /// </summary>
        public string? SkimPerkhidmatan { get; set; }
    }

    public class  ImplikasiKewanganDto
    {
        public decimal? TotalKosSebulan { get; set; }
        public decimal? TotalKosSetahun { get; set; }
    }
    public class GredDto
    {
        public int? Id { get; set; }
        public string? code { get; set; }
    }
}
