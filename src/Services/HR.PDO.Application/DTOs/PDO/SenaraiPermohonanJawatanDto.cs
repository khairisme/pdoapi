using System;
namespace HR.PDO.Application.DTOs
{
    public class SenaraiPermohonanJawatanDto
    {
        public DateTime?  TarikhPermohonan { get; set; }
        public int Id { get; set; }
        public string  NomborRujukan { get; set; }
        public string  Status { get; set; }
        public string Agensi { get; set; }
        public string JenisPermohonan { get; set; }
        public string TajukPermohonan { get; set; }
    }
}
