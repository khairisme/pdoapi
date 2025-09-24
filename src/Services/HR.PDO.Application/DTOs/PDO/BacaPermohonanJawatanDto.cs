using HR.PDO.Core.Entities.PDO;
using System;
using System.Threading.Tasks;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) untuk membaca maklumat permohonan jawatan.
    /// Menyimpan butiran asas berkaitan permohonan jawatan termasuk maklumat unit,
    /// agensi, rujukan, tarikh, dan status permohonan.
    /// </summary>
    /// <author>Khairi Abu Bakar</author>
    /// <date>2025-09-11</date>
    /// <purpose>
    /// Digunakan untuk memindahkan data permohonan jawatan daripada lapisan data
    /// ke lapisan aplikasi atau paparan tanpa mendedahkan entiti pangkalan data secara terus.
    /// </purpose>
    public class BacaPermohonanJawatanDto
    {
        /// <summary>
        /// ID unik bagi permohonan jawatan.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID unit organisasi yang terlibat dalam permohonan.
        /// </summary>
        public int IdUnitOrganisasi { get; set; }

        /// <summary>
        /// ID agensi yang membuat permohonan.
        /// </summary>
        public int IdAgensi { get; set; }
        public string UnitOrganisasi { get; set; }
        /// <summary>
        /// Kod rujukan jenis permohonan dalaman.
        /// </summary>
        public string KodRujJenisPermohonan { get; set; }

        /// <summary>
        /// Kod rujukan jenis permohonan berdasarkan klasifikasi JPA.
        /// </summary>
        public string KodRujJenisPermohonanJPA { get; set; }

        /// <summary>
        /// Nombor rujukan rasmi permohonan.
        /// </summary>
        public string NomborRujukan { get; set; }

        /// <summary>
        /// Tajuk ringkas bagi permohonan jawatan.
        /// </summary>
        public string Tajuk { get; set; }

        /// <summary>
        /// Keterangan lanjut mengenai permohonan jawatan.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Kod rujukan pasukan perunding berkaitan permohonan.
        /// </summary>
        public string KodRujPasukanPerunding { get; set; }

        /// <summary>
        /// Nombor waran perjawatan (jika berkaitan).
        /// </summary>
        public string NoWaranPerjawatan { get; set; }

        /// <summary>
        /// Tarikh permohonan dibuat.
        /// </summary>
        public DateTime? TarikhPermohonan { get; set; }

        /// <summary>
        /// Tarikh cadangan waran dikemukakan.
        /// </summary>
        public DateTime? TarikhCadanganWaran { get; set; }

        /// <summary>
        /// Tarikh waran perjawatan diluluskan.
        /// </summary>
        public DateTime? TarikhWaranDiluluskan { get; set; }

        /// <summary>
        /// ID pengguna yang mencipta rekod permohonan.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Tarikh rekod permohonan dicipta.
        /// </summary>
        public DateTime? TarikhCipta { get; set; }

        /// <summary>
        /// Status semasa permohonan jawatan.
        /// </summary>
        public PDOStatusPermohonanJawatan StatusPermohonanJawatan { get; set; }
    }

    public class SemakPermohonanJawatanDto
    {
        /// <summary>
        /// ID unik bagi permohonan jawatan.
        /// </summary>
        public int Id { get; set; }

        public string? Agensi { get; set; }

        /// <summary>
        /// ID unit organisasi yang terlibat dalam permohonan.
        /// </summary>
        public int IdUnitOrganisasi { get; set; }

        /// <summary>
        /// ID agensi yang membuat permohonan.
        /// </summary>
        public int IdAgensi { get; set; }

        /// <summary>
        /// Kod rujukan jenis permohonan dalaman.
        /// </summary>
        public string KodRujJenisPermohonan { get; set; }

        /// <summary>
        /// Kod rujukan jenis permohonan berdasarkan klasifikasi JPA.
        /// </summary>
        public string KodRujJenisPermohonanJPA { get; set; }

        /// <summary>
        /// Nombor rujukan rasmi permohonan.
        /// </summary>
        public string NomborRujukan { get; set; }

        /// <summary>
        /// Tajuk ringkas bagi permohonan jawatan.
        /// </summary>
        public string Tajuk { get; set; }

        /// <summary>
        /// Keterangan lanjut mengenai permohonan jawatan.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Kod rujukan pasukan perunding berkaitan permohonan.
        /// </summary>
        public string KodRujPasukanPerunding { get; set; }

        /// <summary>
        /// Nombor waran perjawatan (jika berkaitan).
        /// </summary>
        public string NoWaranPerjawatan { get; set; }

        /// <summary>
        /// Tarikh permohonan dibuat.
        /// </summary>
        public DateTime? TarikhPermohonan { get; set; }

        /// <summary>
        /// Tarikh cadangan waran dikemukakan.
        /// </summary>
        public DateTime? TarikhCadanganWaran { get; set; }

        /// <summary>
        /// Tarikh waran perjawatan diluluskan.
        /// </summary>
        public DateTime? TarikhWaranDiluluskan { get; set; }

        /// <summary>
        /// ID pengguna yang mencipta rekod permohonan.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Tarikh rekod permohonan dicipta.
        /// </summary>
        public DateTime? TarikhCipta { get; set; }

        /// <summary>
        /// Status semasa permohonan jawatan.
        /// </summary>
        public PDOStatusPermohonanJawatan StatusPermohonanJawatan { get; set; }
    }

}
