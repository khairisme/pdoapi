using System;
namespace HR.PDO.Application.DTOs
{
    public class TambahButiranPermohonanSkimGredDto
    {
        public int IdButiranPermohonan { get; set; }
        public int IdGred { get; set; }
        public int IdKetuaPerkhidmatan { get; set; }
        public int IdSkimPerkhidmatan { get; set; }
        public string? KodBidangPengkhususan { get; set; }
        public string? KodRujLaluanKemajuanKerjaya { get; set; }

    }
}
