using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs.PDO
{
    public class JawatanWithAgensiDto
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Nama { get; set; }
        public string Agensi { get; set; }
    }
    public class CarianJawatanFilterDto
    {
        public int? SkimPerkhidmatanId { get; set; }
        public string? NamaJawatan { get; set; }
        public int? UnitOrganisasi { get; set; }
    }
    public class CarianJawatanResponseDto
    {
        public int Bil { get; set; }
        public int Id { get; set; }
        public string Kod { get; set; }
        public string NamaJawatan { get; set; }
        public string UnitOrganisasi { get; set; }
        public string StatusPengisian { get; set; }
        public DateTime? TarikhStatusKekosongan { get; set; }
    }
    public class CarianJawatanSebenarFilterDto
    {
        public int? SkimPerkhidmatanId { get; set; }
        public string? KodJawatanSebenar { get; set; }
        public string? NamaJawatanSebenar { get; set; }
        public string? StatusKekosonganJawatan { get; set; }
        public int? UnitOrganisasi { get; set; }
        
    }
    public class CarianJawatanSebenarResponseDto
    {
        public int Bil { get; set; }
        public int Id { get; set; }
        public string Kod { get; set; }
        public string NamaJawatan { get; set; }
        public string UnitOrganisasi { get; set; }
        public string StatusPengisian { get; set; }
        public DateTime? TarikhStatusKekosongan { get; set; }
    }
}
