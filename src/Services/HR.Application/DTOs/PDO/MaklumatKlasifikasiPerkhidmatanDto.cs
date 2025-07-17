using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs.PDO
{
    public class MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string FungsiUtama { get; set; }
        public string FungsiUmum { get; set; }
        public bool StatusAktif {  get; set; } = true;
        public bool? IndikatorSkim { get; set; }
        //public bool? IndSkimPerkhidmatan { get; set; }
    }

    public class MaklumatKlasifikasiPerkhidmatanSearchResponseDto
    {
        public int Bil { get; set; }
        public int Id  { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string FungsiUmum { get; set; }
        public string FungsiUtama { get; set; }
        public string Keterangan { get; set; }
        public string StatusKumpulanPerkhidmatan { get; set; }
        public string StatusPermohonan { get; set; }

    }

    public class MaklumatKlasifikasiPerkhidmatanResponseDto
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string FungsiUtama { get; set; }
        public string FungsiUmum { get; set; }
        public string Status { get; set; }
        public DateTime? TarikhKemaskini { get; set; }
        public string StatusKlasifikasiPerkhidmatan { get; set; }

        public bool? IndikatorSkim { get; set; }
        public bool? IndSkimPerkhidmatan { get; set; }
        public bool StatusAktif { get; set; }
    }

    public class PenapisMaklumatKlasifikasiPerkhidmatanDto
    {
        public string? Kod { get; set; }
        public string? Nama { get; set; }
        public int? StatusKumpulan { get; set; } // 0 = Tidak Aktif, 1 = Aktif
        public string? StatusPermohonan { get; set; } // e.g., "Draf", "Disahkan", etc.
    }
    public class MaklumatKlasifikasiPerkhidmatanDto
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string? FungsiUtama { get; set; }
        public string? FungsiUmum { get; set; }
        public string? ButiranKemaskini { get; set; }
        public bool? IndikatorSkim { get; set; }
        public bool? IndSkimPerkhidmatan { get; set; }
    }


    public class MaklumatKlasifikasiPerkhidmatanUpdateRequestDto
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string FungsiUtama { get; set; }
        public string FungsiUmum { get; set; }
    }

    public class PengesahanPerkhidmatanKlasifikasiResponseDto
    {
        public int Bil { get; set; }
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string KodRujStatusPermohonan { get; set; }
        public string StatusPermohonan { get; set; }
        public bool? IndikatorSkim { get; set; }
        public bool? IndSkimPerkhidmatan { get; set; }
    }

    public class PenapisPerkhidmatanKlasifikasiDto
    {
        public string? Kod { get; set; }
        public string? Nama { get; set; }
        public string? StatusPermohonan { get; set; }

    }
}
