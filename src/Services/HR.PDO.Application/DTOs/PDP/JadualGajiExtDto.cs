using System;
namespace HR.PDO.Application.DTOs
{
    public class JadualGajiExtDto
    {
        public DateTime? TarikhMulaJadualGaji { get; set; }
        public DateTime? TarikhTamatJadualGaji { get; set; }
        public decimal PeratusKGT { get; set; }
        public decimal? GajiMaksimum { get; set; }
        public decimal? GajiMinimum { get; set; }
        public decimal? KadarKGM { get; set; }
        public decimal? KadarKGT { get; set; }
        public int Id { get; set; }
        public int? IdGred { get; set; }
        public string? NomborGred { get; set; }
        public int? PeringkatMataGaji { get; set; }
        public int? TingkatMataGaji { get; set; }
        public string? ButiranKemasKini { get; set; }
        public string? KodJenisSaraan { get; set; }
        public string? KodMataGaji { get; set; }
    }
}
