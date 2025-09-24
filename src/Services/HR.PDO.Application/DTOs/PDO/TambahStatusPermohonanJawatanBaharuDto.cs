using System;
namespace HR.PDO.Application.DTOs
{
    public class TambahStatusPermohonanJawatanBaharuDto
    {
        public bool StatusAktif { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public DateTime? TarikhStatusPermohonan { get; set; }
        public Guid UserId { get; set; }
        public int IdPermohonanJawatan { get; set; }
        public string KodRujStatusPermohonanJawatan { get; set; }
        public string Ulasan { get; set; }
    }
}
