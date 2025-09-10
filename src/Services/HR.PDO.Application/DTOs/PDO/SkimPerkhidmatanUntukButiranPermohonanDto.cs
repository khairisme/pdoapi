using System;
namespace HR.PDO.Application.DTOs
{
    public class SkimPerkhidmatanUntukButiranPermohonanDto
    {
        public decimal? ImplikasiKosSebulan { get; set; }
        public decimal? ImplikasiKosSetahun { get; set; }
        public int? BilanganJawatan { get; set; }
        public string Gred { get; set; }
        public string SkimPerkhidmatan { get; set; }
    }
}
