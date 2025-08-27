using System;
namespace HR.PDO.Application.DTOs
{
    public class TambahButiranPermohonanSkimGredDto
    {
        public DateTime? TarikhCipta { get; set; }
        public Guid IdCipta { get; set; }
        public int IdButiranPermohonan { get; set; }
        public int IdGred { get; set; }
        public int IdKetuaPerkhidmatan { get; set; }
        public int IdSkimPerkhidmatan { get; set; }
    }
}
