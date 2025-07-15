using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities.PDO
{
    [Table("PDO_ButiranPermohonan")]
    public class PDOButiranPermohonan: BaseEntity
    {
        public int? IdPermohonanJawatan { get; set; }
        public int? IdAktivitiOrganisasi { get; set; }

        [Required]
        [StringLength(2)]
        public string KodRujStatusJawatan { get; set; }

        public DateTime? TarikhMula { get; set; }
        public DateTime? TarikhTamat { get; set; }

        [Required]
        [StringLength(2)]
        public string KodRujJenisJawatan { get; set; }

        [Required]
        [StringLength(5)]
        public string KodRujGelaranJawatan { get; set; }

        [StringLength(4)]
        public string? KodRujPangkatBadanBeruniform { get; set; }

        [StringLength(30)]
        public string? TajJawatan { get; set; }

        [Required]
        [StringLength(1)]
        public string KodRujKategoriJawatan { get; set; }

        [Required]
        [StringLength(20)]
        public string NoButiran { get; set; }

        [Required]
        [StringLength(85)]
        public string AnggaranTajukJawatan { get; set; }

        [StringLength(200)]
        public string? ButirPerubahan { get; set; }

        public int BilanganJawatan { get; set; }
        public short? TahunButiran { get; set; }

        public bool? IndikatorTBK { get; set; }
        public bool? IndikatorHBS { get; set; }

        [Column(TypeName = "numeric(7,2)")]
        public decimal? JumlahKosSebulan { get; set; }

        [Column(TypeName = "numeric(7,2)")]
        public decimal? JumlahKosSetahun { get; set; }
    }
}
