using System;
namespace HR.PDO.Application.DTOs
{
    public class CarianSenaraiPermohonanJawatanRequestDto
    {
        public int IdUnitOrganisasi { get; set; }
        public string  NomborRujukan  { get; set; }
        public string KodRujStatusPermohonanJawatan { get; set; }
        public string TajukPermohonan { get; set; }
    }
}
