using System;
namespace HR.PDO.Application.DTOs
{
    public class TambahButiranPermohonanDto
    {
        public bool? IndikatorHBS { get; set; }
        public bool? IndikatorTBK { get; set; }
        public DateTime? TarikhMula { get; set; }
        public DateTime? TarikhTamat { get; set; }
        public int IdAktivitiOrganisasi { get; set; }
        public int IdPermohonanJawatan { get; set; }
        public string KodRujJenisJawatan { get; set; }
        public string KodRujStatusJawatan { get; set; }
        public string NoButiran { get; set; }
    }
}
