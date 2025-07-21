using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs.PDO
{
    public class MaklumatSkimPerkhidmatanSearchResponseDto
    {
        public int Bil { get; set; }
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string StatusSkimPerkhidmatan { get; set; }
        public string StatusPermohonan { get; set; }
        public DateTime? TarikhKemaskini { get; set; }
        public bool? IndikatorSkim { get; set; }

        public string? KodRujMatawang { get; set; }

        public decimal? Jumlah { get; set; }
        public int IdKlasifikasiPerkhidmatan { get; set; }
        public int IdKumpulanPerkhidmatan { get; set; }
        public bool StatusAktif { get; set; } = true;
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
        public string? Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public bool IndikatorSkimKritikal { get; set; }
        public bool IndikatorKenaikanPGT { get; set; }
        public int IdGred { get; set; }
        public int Id { get; set; } = 0;
        public string KlasifikasiKod { get; set; }
        public string KumpulanKod { get; set; }
        public string JenisKod { get; set; }
        public bool IndikatorSkim { get; set; }

        public string? KodRujMatawang { get; set; }

        public decimal? Jumlah { get; set; }
        public int CarianSkimId { get; set; } = 0;
        public string? ButiranKemaskini { get; set; }
        public bool StatusAktif { get; set; } = true;
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
        public bool? IndikatorSkim { get; set; }

        public string KodRujMatawang { get; set; }

        public decimal? Jumlah { get; set; }
        public string KodRujStatusSkim { get; set; }
        
    }
    public class SkimPerkhidmatanFilterDto
    {
        public string? Kod { get; set; }
        public string? Nama { get; set; }
        public string? KodRujStatusPermohonan { get; set; }
    }
    public class SkimPerkhidmatanDto
    {
        public int Bil { get; set; }
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string StatusSkimPerkhidmatan { get; set; }
        public string StatusPermohonan { get; set; }
        public DateTime? TarikhKemaskini { get; set; }
        public bool? IndikatorSkim { get; set; }

        public string? KodRujMatawang { get; set; }

        public decimal? Jumlah { get; set; }
    }
    public class SkimWithJawatanDto
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string? KodJawatan { get; set; }
        public string? NamaJawatan { get; set; }
    }
    public class CarianSkimPerkhidmatanFilterDto
    {
        public string? Kod { get; set; }
        public string? Nama { get; set; }
        public int? KlasifikasiPerkhidmatanId { get; set; }
        public int? KumpulanPerkhidmatanId { get; set; }
        
    }
    public class CarianSkimPerkhidmatanResponseDto
    {
        public int Bil { get; set; }
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        
    }

    public class SkimPerkhidmatanResponseDto
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }

    }
    public class SkimPerkhidmatanButiranDto
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string? ButiranKemaskini { get; set; }
        public string KodRujStatusPermohonan { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string StatusPermohonan { get; set; }
        public bool StatusAktif { get; set; }
        public DateTime? TarikhKemaskini { get; set; }

    }
    public class SkimPerkhidmatanRefStatusDto : MaklumatSkimPerkhidmatanCreateRequestDto
    {
        public string KodRujStatusSkim { get; set; }
    }

}
