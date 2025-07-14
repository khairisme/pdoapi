using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs.PDO
{
    public class KumpulanPerkhidmatanDto
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string? Keterangan { get; set; }
        public string? ButiranKemaskini { get; set; }
        public string? Ulasan { get; set; }
        public string? KodJana { get; set; }
        public bool StatusAktif { get; set; }=true;

        public bool? IndikatorSkim { get; set; }
        public bool? IndikatorTanpaSkim { get; set; }
    }

    public class CarlKumpulanPerkhidmatanDto
    {
        public int Bil { get; set; }
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string StatusKumpulanPerkhidmatan { get; set; }
        public string StatusPermohonan { get; set; }
        public DateTime TarikhKemaskini { get; set; }
    }
    public class KumpulanPerkhidmatanFilterDto
    {
        public string? Kod { get; set; }
        public string? Nama { get; set; }
        public int? StatusKumpulan { get; set; } // 0 = Tidak Aktif, 1 = Aktif
        public string? StatusPermohonan { get; set; } // e.g., "Draf", "Disahkan", etc.
    }
    public class KumpulanPerkhidmatanDetailDto
    {
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string StatusPermohonan { get; set; }
        public string? KodJana { get; set; }
        public string? Ulasan { get; set; }
        public DateTime TarikhKemaskini { get; set; }

        public bool? IndikatorSkim { get; set; }
        public bool? IndikatorTanpaSkim { get; set; }
    }

    public class CarlStatusKumpulanPerkhidmatanDto
    {
        public int Bil { get; set; }
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
    
        public string KodRujStatusPermohonan { get; set; }
        public string StatusPermohonan { get; set; }
        public DateTime TarikhKemaskini { get; set; }
        public string? KodJana { get; set; }
        public string? Ulasan { get; set; }
        public bool? IndikatorSkim { get; set; }
        public bool? IndikatorTanpaSkim { get; set; }
    }
    public class KumpulanPerkhidmatanButiranDto
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string? ButiranKemaskini { get; set; }
        public string KodRujStatusPermohonan { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string StatusPermohonan { get; set; }
        public DateTime? TarikhKemaskini { get; set; }
    }
    public class KumpulanPerkhidmatanStatusDto
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public bool StatusAktif { get; set; }
        public string KodRujStatusPermohonan { get; set; }
        public string StatusPermohonan { get; set; }
        public DateTime? TarikhKemaskini { get; set; }
    }
    public class KumpulanPerkhidmatanRefStatusDto : KumpulanPerkhidmatanDto
    {
       public string KodRujStatusPermohonan { get; set; }
    }

    public class HantarKumpulanPermohonanDto
    {
        public string Kod { get; set; } = string.Empty;
    }
    public class KumpulanPerkhidmatanSubListDto
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public bool StatusAktif { get; set; }
        public string KodRujStatusPermohonan { get; set; }
        public string StatusPermohonan { get; set; }
        public DateTime? TarikhKemaskini { get; set; }
    }

    //Amar
    public class KumpulanPerkhidmatanHantarDto
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string? Keterangan { get; set; }
        public KumpulanPerkhidmatanDto? ButiranKemaskini { get; set; }
        public string? Ulasan { get; set; }
        public string? KodJana { get; set; }
        public bool StatusAktif { get; set; } = true;

        public bool? IndikatorSkim { get; set; }
        public bool? IndikatorTanpaSkim { get; set; }
    }
}
