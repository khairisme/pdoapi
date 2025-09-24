using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PPA_ProfilPemilikKompetensi")]
    public class ProfilPemilikKompetensi 
    {
        public Guid? IdPemilikKompetensi { get; set; }
        public string? KodRujJantina { get; set; }
        public string? NamaJantina { get; set; }
        public string? KodRujBangsa { get; set; }
        public string? NamaBangsa { get; set; }
        public string? KodRujAgama { get; set; }
        public string? NamaAgama { get; set; }
        public int? IdKetuaJabatan { get; set; }
        public string? NamaKetuaJabatan { get; set; }
        public string? EmelRasmi { get; set; }
        public string? EmelPeribadi { get; set; }
        public string? KodRujNegeriLahir { get; set; }
        public string? NamaNegeri { get; set; }
        public DateTime? TarikhLahir { get; set; }
        public int? IdJawatan { get; set; }
        public string? Jawatan { get; set; }
        public string? KodJawatan { get; set; }
        public int? IdUnitOrganisasi { get; set; }
        public string? NamaUnitOrganisasi { get; set; }
        public int? IdIndukUnitOrganisasi { get; set; }
        public int? TahapUnitOrganisasi { get; set; }
        public string? HierarkiUnitOrganisasi { get; set; }
        //public string? KodKategoriUnitOrganisasi { get; set; }
        public string? KodUnitOrganisasi { get; set; }
        //public string? KodCartaUnit { get; set; }
        public int? IdAgensi { get; set; }
        public string? NamaAgensi { get; set; }
        public string? KodCartaAgensi { get; set; }
        public string? KodKumpulanAgensi { get; set; }
        public int? IdAgensiRasmi { get; set; }
        public string? NamaAgensiRasmi { get; set; }
        public string? KodCartaAgensiRasmi { get; set; }
        public int? IndikatorAgensiUnitOrganisasi { get; set; }
        public int? IdAktivitiOrganisasi { get; set; }
        public string? KodAktivitiOrganisasi { get; set; }
        public string? KodCartaAktiviti { get; set; }
        public int? IdSandangan { get; set; }
        public int? IdKumpulanPerkhidmatan { get; set; }
        public string? KodKumpulanPerkhidmatan { get; set; }
        public string? NamaKumpulanPerkhidmatan { get; set; }
        public int? IdSkimPerkhidmatanPemilikKompetensi { get; set; }
        public string? KodSkimPerkhidmatan { get; set; }
        public string? KodKlasifikasiPerkhidmatan { get; set; }
        public string? NamaKlasifikasiPerkhidmatan { get; set; }
        public int? IdSkimPerkhidmatan { get; set; }
        public string? NamaSkimPerkhidmatan { get; set; }
        public int? IdStatusLantikan { get; set; }
        public string? KodStatusLantikan { get; set; }
        public string? NamaStatusLantikan { get; set; }
        public DateTime? TarikhMulaStatusLantikan { get; set; }
        public DateTime? TarikhMulaLantikan { get; set; }
        public DateTime? TarikhMulaLantikanSebenar { get; set; }
        public decimal? AmaunGaji { get; set; }
        public decimal? GajiPokok { get; set; }
        public int? IdStatusGaji { get; set; }
        public int? IdGredPemilikKompetensi { get; set; }
        public int? IdJadualGaji { get; set; }
        public int? IdJadualGajiPemilikKompetensi { get; set; }
        public string? KodRujJenisSaraan { get; set; }
        public string? KodRujGelaran { get; set; }
        public string? NamaGelaran { get; set; }
        public string? NamaPemilikKompetensi { get; set; }
        public string? NomborKadPengenalan { get; set; }
        public int? Umur { get; set; }
        public int? UmurBulan { get; set; }
        public string? KodRujStatusKahwin { get; set; }
        public string? NamaStatusKahwin { get; set; }
        public string? KodRujStatusSandangan { get; set; }
        public int? IdKlasifikasiPerkhidmatan { get; set; }
        public string? KodPihakBerkuasaMelantik { get; set; }
        public string? NamaPihakBerkuasaMelantik { get; set; }
        public int? IdKetuaPerkhidmatan { get; set; }
        public string? NamaKetuaPerkhidmatan { get; set; }
        public string? KodRujStatusPerkhidmatan { get; set; }
        public string? NamaRujStatusPerkhidmatan { get; set; }
        public string? KodRujBebanPerkhidmatan { get; set; }
        public string? NamaRujBebanPerkhidmatan { get; set; }
        public string? KodRujAsasWaktuBekerja { get; set; }
        public string? NamaRujAsasWaktuBekerja { get; set; }
        public string? KodRujStatusPengesahanDalamPerkhdidmatan { get; set; }
        public string? NamaStatusPengesahanDalamPerkhdidmatan { get; set; }
        public string? KodRujStatusBerpencen { get; set; }
        public string? NamaStatusBerpencen { get; set; }
        public string? KodRujPihakBerkuasaBerpencen { get; set; }
        public string? NamaPihakBerkuasaBerpencen { get; set; }
        public string? KodRujJenisGaji { get; set; }
        public string? NamaJenisGaji { get; set; }
        public int? StatusGaji { get; set; }
        public string? NamaJenisSaraan { get; set; }
        public string? GredJawatan { get; set; }
        public string? NomborGred { get; set; }
        public int? IdGred { get; set; }
        public string? KodGred { get; set; }
        public int? TurutanGred { get; set; }
        public string? KodJadualGaji { get; set; }
        public string? BulanPergerakanGaji { get; set; }
        public decimal? NilaiGandaan { get; set; }
        public string? BilanganFail { get; set; }
        public string? BilanganRekodPerkhidmatan { get; set; }
        public string? KodRujPangkat { get; set; }
        public string? NamaPangkat { get; set; }
        public int? KodUmurBersara { get; set; }
        public DateTime? TarikhBersara { get; set; }
        public int? IdJawatanSebenarPenyelia { get; set; }
        public string? KodRujStatusKewarganegaraan { get; set; }
        public bool? IndikatorTanggungKerja { get; set; }
        public bool? IndikatorLokasiUnitOrganisasi { get; set; }
        public string? NamaUnitOrganisasiTahap3 { get; set; }
        public bool? IndikatorPemohonJawatan { get; set; }
        public string? JawatanPinjaman { get; set; }
        public string? GredJawatanPinjaman { get; set; }
        public string? AgensiPeminjam { get; set; }
        public DateTime? TarikhPinda { get; set; }
    }
}
