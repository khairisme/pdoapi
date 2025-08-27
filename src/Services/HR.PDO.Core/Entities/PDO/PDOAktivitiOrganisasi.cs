using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_AktivitiOrganisasi")]
    public class PDOAktivitiOrganisasi : PDOBaseEntity
    {
        public int Id { get; set; }
        public string? KodRujKategoriAktivitiOrganisasi { get; set; }
        public int IdIndukAktivitiOrganisasi { get; set; }
        public string? Kod { get; set; }
        public string? Nama { get; set; }
        public string? Keterangan { get; set; }
        public string? KodProgram { get; set; }
        public int Tahap { get; set; }
        public string? KodCartaAktiviti { get; set; }
        public string? ButiranKemaskini { get; set; }
        public bool? StatusAktif { get; set; }
        public Guid? IdCipta { get; set; }
        public DateTime? TarikhCipta { get; set; }
        public Guid? IdPinda { get; set; }
        public DateTime? TarikhPinda { get; set; }
        public Guid? IdHapus { get; set; }
        public DateTime? TarikhHapus { get; set; }
        public int IdAsal { get; set; }
    }
}
