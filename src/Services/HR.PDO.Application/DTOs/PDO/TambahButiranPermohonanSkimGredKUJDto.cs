using System;
namespace HR.PDO.Application.DTOs
{
    public class TambahButiranPermohonanSkimGredKUJDto
    {
        public DateTime? TarikhCipta { get; set; }
        public Guid IdCipta { get; set; }
        public int IdButiranPermohonan { get; set; }
        public int IdGred { get; set; }
        public int IdSkim { get; set; }
    }
}
