using System;
namespace HR.PDO.Application.DTOs
{
    public class TambahButiranPermohonanSkimGredDto
    {
        public Guid UserId { get; set; }
        public int IdButiranPermohonan { get; set; }
        public int IdKumpulanPerkhidmatan { get; set; }
        public int BilanganJawatan { get; set; }
        public List<ButiranPermohonanSkimGredList> ButiranPermohonanSkimGredList { get; set; }
    }
    public class ImplikasiKewanganRequestDto
    {
        public int IdKumpulanPerkhidmatan { get; set; }
        public int BilanganJawatan { get; set; }
        public List<ButiranPermohonanSkimGredList> ButiranPermohonanSkimGredList { get; set; }
    }
    public class SenaraiImplikasiKewanganRequestDto
    {
        public int IdKumpulanPerkhidmatan { get; set; }
        public int BilanganJawatan { get; set; }
        public string? AnggaranBerkenaanTajukJawatan { get; set; }
        public List<ButiranPermohonanSkimGredList> ButiranPermohonanSkimGredList { get; set; }
    }
    public class SenaraiImplikasiKewanganOutputDto
    {
        public string? SkimPerkhidmatan { get; set; }
        public string? Gred { get; set; }

        public int? BilanganAJawatan { get; set; }

        public decimal? ImplikasiKewanganSebulan { get; set; }
        public decimal? JumlahKewanganSebulan{ get; set; }
        public decimal? ImplikasiKewanganSetahun { get; set; }

    }

    public class  ButiranPermohonanSkimGredList
    {
        public int IdKlasifikasiPerkhidmatan { get; set; }
        public int IdSkimPerkhidmatan { get; set; }
        public string? IdGredList { get; set; }
        public int IdKetuaPerkhidmatan { get; set; }
        public string? KodBidangPengkhususan { get; set; }
        public string? KodRujLaluanKemajuanKerjaya { get; set; }
    }
}
