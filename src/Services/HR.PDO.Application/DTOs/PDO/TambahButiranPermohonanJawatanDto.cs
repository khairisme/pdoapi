using System;
namespace HR.PDO.Application.DTOs
{
    public class TambahButiranPermohonanJawatanDto
    {
        public bool StatusAktif { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public Guid IdCipta { get; set; }
        public int IdButiranPermohonan { get; set; }
        public int IdJawatan { get; set; }
    }
}
