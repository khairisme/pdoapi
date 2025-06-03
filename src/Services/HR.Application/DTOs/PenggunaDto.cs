using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs
{

    /// <summary>
    /// Data Transfer Object for PenggunaDto(UserDto) entity
    /// </summary>
    public class PenggunaDto
    {
        public int Id { get; set; }
        public int? IdPemilikKompetensi { get; set; }
        public string KodRujModDaftar { get; set; }
        public string KodRujJenisPengguna { get; set; }
        public DateTime? TarikhDaftar { get; set; }
        public string Emel { get; set; }
        public string IdPengguna { get; set; }
        public string KataLaluanHash { get; set; }
        public string KataLaluanSalt { get; set; }
        public DateTime? LogMasukTerakhir { get; set; }
        public DateTime? TukarKataLaluanTerakhir { get; set; }
        public int? BilCubaanLogMasuk { get; set; }
        public bool? AkaunDikunci { get; set; }
        public DateTime? SahDari { get; set; }
        public DateTime? SahHingga { get; set; }
        public bool? IndikatorSahEmel { get; set; }
        public string PautanPengesahanEmel { get; set; }
        public string KataLaluanSementara { get; set; }
        public bool? StatusAktif { get; set; }
        public Guid IdCipta { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public Guid IdPinda { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public Guid IdHapus { get; set; }
        public DateTime? TarikhHapus { get; set; }
    }
    public class TetapkanKataLaluanDto
    {
        public string IdPengguna { get; set; }
        public string KataLaluanHash { get; set; }

    }
    public class SoalanKeselamatanDto
    {
        public string IdPengguna { get; set; }
        public string KodRujSoalanKeselamatan { get; set; }
        public string JawapanSoalan { get; set; }
    }
    public class PerkhidmatanLogDto
    {
        public string IdPengguna { get; set; }
        public string Gambar { get; set; }
        public string KataLaluan { get; set; }
    }
}
