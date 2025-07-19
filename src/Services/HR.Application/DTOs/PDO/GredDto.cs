using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs.PDO
{
    public class GredFilterDto
    {
        public int? IdKumpulanPerkhidmatan { get; set; }
        public int? IdKlasifikasiPerkhidmatan { get; set; }
        public string? KodRujStatusPermohonan { get; set; }
        public string? Nama { get; set; }
    }
    public class GredResultDto
    {
        public int Bil { get; set; }
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string StatusPermohonan { get; set; }
        public string StatusGred { get; set; }
    }
    public class CreateGredDto
    {
        public int Id { get; set; } = 0;
        public string KodRujJenisSaraan { get; set; }
        public int IdKlasifikasiPerkhidmatan { get; set; }
        public int IdKumpulanPerkhidmatan { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public int? TurutanGred { get; set; }
        public string KodGred { get; set; }
        public int NomborGred { get; set; }
        public string? Keterangan { get; set; }
        public bool? IndikatorGredLantikanTerus { get; set; }
        public bool? IndikatorGredKenaikan { get; set; }
        public bool StatusAktif { get; set; }

       public string? ButiranKemaskini { get; set; }

        public string? KodRujStatusPermohonan { get; set; }
    }
    public class PaparMaklumatGredButiranKemasDto
    {
        public string? ButiranKemaskini { get; set; }
    }
    public class PaparMaklumatGredDto
    {
        public int Id { get; set; }
        public string KodRujJenisSaraan { get; set; }
        public string Klasifikasi { get; set; }
        public int IdKlasifikasiPerkhidmatan { get; set; }
        public int IdKumpulanPerkhidmatan { get; set; }
        public string Kumpulan { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public int? TurutanGred { get; set; }
        public string KodGred { get; set; }
        public int NomborGred { get; set; }
        public string? Keterangan { get; set; }
        public bool? IndikatorGredLantikanTerus { get; set; }
        public bool? IndikatorGredKenaikan { get; set; }
        public bool StatusAktif { get; set; }
        public string KodRujStatusPermohonan { get; set; }
        public string StatusPermohonan { get; set; }
        public DateTime? TarikhKemaskini { get; set; }
    }


   
}
