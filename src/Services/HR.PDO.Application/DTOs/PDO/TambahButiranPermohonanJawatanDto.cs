using System;
namespace HR.PDO.Application.DTOs
{
    public class TambahButiranPermohonanJawatanDto
    {
        public Guid UserId { get; set; }
        public int IdButiranPermohonan { get; set; }
        public int IdJawatan { get; set; }
    }
}
