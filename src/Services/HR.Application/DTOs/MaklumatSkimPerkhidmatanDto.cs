using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs
{
    public class MaklumatSkimPerkhidmatanSearchResponseDto
    {
        public int Bil { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string StatusSkimPerkhidmatan { get; set; }
        public string StatusPermohonan { get; set; }
        public DateTime TarikhKemaskini { get; set; }        
    }
    public class MaklumatSkimPerkhidmatanFilterDto
    {
        public string? Kod { get; set; }
        public string? Nama { get; set; }
        public int? MaklumatKlasifikasiPerkhidmatanId { get; set; }
        public int? MaklumatKumpulanPerkhidmatanId { get; set; } 
        public string? StatusPermohonan { get; set; } // e.g., "Draf", "Disahkan", etc.
    }
    public class MaklumatSkimPerkhidmatanCreateRequestDto
    {
        public int IdKlasifikasiPerkhidmatan { get; set; }
        public int IdKumpulanPerkhidmatan { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public bool IndikatorSkimKritikal { get; set; }
        public bool IndikatorKenaikanPGT { get; set; }
        public int IdGred { get; set; }
    }
    public class MaklumatSkimPerkhidmatanResponseDto
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string KodKlasifikasiPerkhidmatan { get; set; }
        public string KlasifikasiPerkhidmatan { get; set; }
        public string KodKumpulanPerkhidmatan { get; set; }
        public string KumpulanPerkhidmatan { get; set; }
    }
}
