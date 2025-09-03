using System;
namespace HR.PDO.Application.DTOs
{
    public class TambahButiranPermohonanDto
    {
        public int Id { get; set; }
        public bool? IndikatorHBS { get; set; }
        public bool? IndikatorTBK { get; set; }
        public DateTime? TarikhMula { get; set; }
        public DateTime? TarikhTamat { get; set; }
        public int IdAktivitiOrganisasi { get; set; }
        public int IdPermohonanJawatan { get; set; }
        public string KodRujJenisJawatan { get; set; }
        public string? KodRujStatusJawatan { get; set; }
        public string KodRujGelaranJawatan { get; set; }
        public string KodRujPangkatBadanBeruniform { get; set; }
        public int IdSkimPerkhidmatan { get; set;}
        public string NoButiran { get; set; }
        public string AnggaranTajukJawatan { get; set; }
        public decimal? JumlahKosSebulan { get; set; }
        public short?  TahunButiran { get; set; }
        public int BilanganJawatan { get; set; }
        public string NamaPemilikKompetensi { get; set; }
        public string NoKadPengenalanPemilikKompetensi { get; set; }
        public string IdSkimPerkhidmatanPemilikKompetensi { get; set; }
        public int IdGredPemilikKompetensi { get; set; }
        public bool IndikatorJawatanStrategik { get; set; }
        public bool IndikatorJawatanSensitif { get; set; }
        public bool IndikatorJawatanKritikal { get; set; }
        public string? ButirPerubahan { get; set; }
        public string? TagJawatan { get; set; }

    }

    public class TambahJawatanButiranPermohonanRequestDto
    {
        public int Id { get; set; }
        public int BilanganJawatan { get; set; }
        public int IdPermohonanJawatan { get; set; }

    }
    public class KemaskiniButiranPermohonanRequestDto
    { 
        public int IdButiranPermohonan { get; set; }
        public bool? IndikatorHBS { get; set; }
        public bool? IndikatorTBK { get; set; }
        public DateTime? TarikhMula { get; set; }
        public DateTime? TarikhTamat { get; set; }
        public int IdAktivitiOrganisasi { get; set; }
        public int? IdPermohonanJawatan { get; set; }
        public string KodRujJenisJawatan { get; set; }
        public string KodRujStatusJawatan { get; set; }
        public string KodRujGelaranJawatan { get; set; }
        public string KodRujPangkatBadanBeruniform { get; set; }
        public string NoButiran { get; set; }
        public string AnggaranTajukJawatan { get; set; }
        public decimal? JumlahKosSebulan { get; set; }
        public short? TahunButiran { get; set; }
        public string NamaPemilikKompetensi { get; set; }
        public string NoKadPengenalanPemilikKompetensi { get; set; }
        public int IdSkimPerkhidmatanPemilikKompetensi { get; set; }
        public bool IndikatorJawatanStrategik { get; set; }
        public bool IndikatorJawatanSensitif { get; set; }
        public bool IndikatorJawatanKritikal { get; set; }
        public string? ButirPerubahan { get; set; }
        public int BilanganJawatan { get; set; }


    }

    public class KiraImplikasiKewanganButiranPermohonanOuputDto
    {
        public int IdSkimPerkhidmatan { get; set; }
        public int? IdAktivitiOrganisasi { get; set; }
        public int? IdPermohonanJawatan { get; set; }
        public string NoButiran { get; set; }
        public int? BilanganJawatan { get; set; }
        public string AnggaranTajukJawatan { get; set; }
        public decimal? ImplikasiKewanganSebulan { get; set; }
        public decimal? JumlahImplikasiKewanganSebulan { get; set; }
        public decimal? ImplikasiKewanganSetahun { get; set; }
        public short? TahunButiran { get; set; }

    }

    public class KiraImplikasiKewanganRequestDto
    {
        public int IdSkimPerkhidmatan { get; set; }
        public int? IdAktivitiOrganisasi { get; set; }
        public int? IdPermohonanJawatan { get; set; }
        public int? IdButiranPermohonan { get; set; }
        public int? BilanganJawatan { get; set; }
        public string? AnggaranTajukJawatan { get; set; }

    }
}
