using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) untuk permintaan penjenamaan semula aktiviti organisasi.
    /// </summary>
    /// <author>Khairi Abu Bakar</author>
    /// <date>2025-09-11</date>
    /// <purpose>
    /// Digunakan untuk menghantar maklumat penjenamaan semula sesuatu aktiviti organisasi,
    /// termasuk status, tarikh tindakan, ID berkaitan, serta butiran kemaskini.
    /// </purpose>
    public class PenjenamaanSemulaRequestDto
    {
        /// <summary>
        /// Menunjukkan sama ada rekod masih aktif.
        /// </summary>
        public bool? StatusAktif { get; set; }

        /// <summary>
        /// ID pengguna yang mencipta rekod.
        /// </summary>
        public Guid UserId { get; set; }


        /// <summary>
        /// ID unik bagi rekod penjenamaan semula.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID aktiviti organisasi berkaitan.
        /// </summary>
        public int IdAktivitiOrganisasi { get; set; }

        /// <summary>
        /// ID asal bagi aktiviti organisasi sebelum penjenamaan semula.
        /// </summary>
        public int IdAsal { get; set; }

        /// <summary>
        /// ID induk aktiviti organisasi.
        /// </summary>
        public int IdIndukAktivitiOrganisasi { get; set; }

        /// <summary>
        /// Tahap aktiviti organisasi dalam hierarki.
        /// </summary>
        public int Tahap { get; set; }

        /// <summary>
        /// Butiran tambahan mengenai kemaskini yang dilakukan.
        /// </summary>
        public string? ButiranKemaskini { get; set; }

        /// <summary>
        /// Keterangan tambahan untuk rekod ini.
        /// </summary>
        public string? Keterangan { get; set; }

        /// <summary>
        /// Kod unik aktiviti organisasi.
        /// </summary>
        public string? Kod { get; set; }

        /// <summary>
        /// Kod carta aktiviti.
        /// </summary>
        public string? KodCartaAktiviti { get; set; }

        /// <summary>
        /// Kod program berkaitan.
        /// </summary>
        public string? KodProgram { get; set; }

        /// <summary>
        /// Kod kategori aktiviti organisasi yang dirujuk.
        /// </summary>
        public string? KodRujKategoriAktivitiOrganisasi { get; set; }

        /// <summary>
        /// Nama asal aktiviti organisasi.
        /// </summary>
        public string? Nama { get; set; }

        /// <summary>
        /// Nama baharu aktiviti organisasi selepas penjenamaan semula.
        /// </summary>
        public string? NamaAktivitiOrganisasiBaharu { get; set; }
    }
}
