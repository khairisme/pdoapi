using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs.PDO
{
    public class PermohonanPeriwatanDto
    {

    }
    public class PermohonanPeriwatanCreateRequestDto
    {
        public int AgensiId { get; set; }
        public string NoRujukan { get; set; }
        public string TajukPermohonan { get; set; }
        public string Keterangan { get; set; }
        public string JenisPermohonan { get; set; }
    }
    public class AktivitiOrganisasiCreateRequestDto
    {
        public int IdIndukAktivitiOrganisasi { get; set; }
        public string KodProgram { get; set; } = string.Empty;          // UI: No. Program/Aktiviti
        public string Kod { get; set; } = string.Empty;                 // auto-generated
        public string Nama { get; set; } = string.Empty;                // Nama Aktiviti Organisasi
        public int Tahap { get; set; }                                  // auto-generated
        public string? Keterangan { get; set; }                         // optional
    }

    public class SimpanStatusPermohonanDto
    {
        public int IdPermohonanJawatan { get; set; }
        public string Ulasan { get; set; } = string.Empty;

    }
}
