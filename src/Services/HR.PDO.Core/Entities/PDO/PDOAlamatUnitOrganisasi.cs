using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Core.Entities.PDO
{
    [Table("PDO_AlamatUnitOrganisasi")]
    public class PDOAlamatUnitOrganisasi : PDOBaseEntity
    {
        public int Id { get; set; }
        public int IdUnitOrganisasi { get; set; }
        public string? KodRujPoskod { get; set; }
        public string? Alamat1 { get; set; }
        public string? Alamat2 { get; set; }
        public string? Alamat3 { get; set; }
        public string? KodRujNegara { get; set; }
        public string? KodRujNegeri { get; set; }
        public string? KodRujBandar { get; set; }
        public string? NomborTelefonPejabat { get; set; }
        public string? NomborFaksPejabat { get; set; }
        public string? LamanWeb { get; set; }
        public int? KoordinatUnitOrganisasi { get; set; }
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
