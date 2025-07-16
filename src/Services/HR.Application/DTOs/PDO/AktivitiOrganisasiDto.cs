using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs.PDO
{
    public class AktivitiOrganisasiDto
    {
        public int Id { get; set; }
        public int? IdIndukAktivitiOrganisasi { get; set; }
        public string KodProgram { get; set; }
        public string Nama { get; set; }
        public int? Tahap { get; set; }
    }


    public class AktivitiOrganisasiResponseDto
    {
      
        public string AktivitOrganisasiInduk { get; set; }
        public string KodAktivitiOrganisasi { get; set; }
        public string KategoriProgramAktiviti { get; set; }
       
    }

    public class AktivitiOrganisasiCreateRequest
    {
        public int IdAktivitiOrganisasi { get; set; }
        public string KodRujKategoriAktivitiOrganisasi { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string KodCartaAktiviti { get; set; }
    }


    //Amar Code Start
    public class StrukturAktivitiOrganisasiResponseDto
    {
        public int Id { get; set; }
        public int IdIndukAktivitiOrganisasi { get; set; }
        public string Nama { get; set; }
        public string FullPath { get; set; }
        public int Level { get; set; }
    }
    public class StrukturAktivitiOrganisasiTempDto
    {
        public int Id { get; set; }
        public int IdIndukAktivitiOrganisasi { get; set; }
        public string Nama { get; set; } = String.Empty;
        public int Level { get; set; }
        public string FullPath { get; set; } = String.Empty;
    }
    public class PenjenamaanSemulaRequestDto
    {
        public int IdAktivitiOrganisasi { get; set; }
        public string NamaAktivitiOrganisasiBaharu { get; set; }
       
    }
    public class AktivitiOrganisasiRequestDto
    {
        public int NewParentId { get; set; }
        public int OldParentId { get; set; }

    }
    

    //Amar Code End

}
