using HR.Core.Entities.PDO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Entities.PDP
{
    [Table("PDP_JadualGaji")]
    public class PDPJadualGaji : PDPBaseEntity
    {
        public string? KodJenisSaraan { get; set; }
        public int? IdGred { get; set; }
        public int? NomborGred { get; set; }
        public decimal? GajiMinimum { get; set; }
        public decimal? GajiMaksimum { get; set; }
        public decimal? KadarKGT { get; set; }
        public decimal? KadarKGM { get; set; }
        public decimal PeratusKGT { get; set; }
        public string? KodMataGaji { get; set; }
        public int? PeringkatMataGaji { get; set; }
        public int? TingkatMataGaji { get; set; }
        public DateTime TarikhMulaJadualGaji { get; set; }
        public DateTime TarikhTamatJadualGaji { get; set; }
        public string? ButiranKemasKini { get; set; }
    }
}
