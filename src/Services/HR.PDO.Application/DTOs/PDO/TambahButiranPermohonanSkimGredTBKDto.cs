using System;
namespace HR.PDO.Application.DTOs
{
    public class TambahButiranPermohonanSkimGredTBKDto
    {
        public Guid UserId { get; set; }
        public int IdButiranPermohonan { get; set; }
        public int IdKumpulanPerkhidmatan { get; set; }
        public int BilanganJawatan { get; set; }
        public List<ButiranPermohonanSkimGredTBKList> ButiranPermohonanSkimGredTBKList { get; set; }
    }
    public class ButiranPermohonanSkimGredTBKList
    {
        public int IdKlasifikasiPerkhidmatan { get; set; }
        public int IdSkimPerkhidmatan { get; set; }
        public string? IdGredList { get; set; }
        public string? IdGredKUJList { get; set; }
        public int IdKetuaPerkhidmatan { get; set; }
        public string? KodBidangPengkhususan { get; set; }
        public string? KodRujLaluanKemajuanKerjaya { get; set; }
    }
}
