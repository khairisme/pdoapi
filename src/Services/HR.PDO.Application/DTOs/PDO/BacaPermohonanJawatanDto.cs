using HR.PDO.Core.Entities.PDO;
using System;
using System.Threading.Tasks;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO for reading job application details (Permohonan Jawatan).
    /// Contains organizational, agency, and application reference information.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to transfer job application data from the system to clients,
    ///               including metadata such as references, titles, descriptions,
    ///               dates, and related organizational information.
    /// </remarks>
    public class BacaPermohonanJawatanDto
    {
        public int Id { get; set; }
        public int IdUnitOrganisasi { get; set; }
        public int IdAgensi { get; set; }
        public string KodRujJenisPermohonan { get; set; }
        public string KodRujJenisPermohonanJPA { get; set; }
        public string NomborRujukan { get; set; }
        public string Tajuk { get; set; }
        public string Keterangan { get; set; }
        public string KodRujPasukanPerunding { get; set; }
        public string NoWaranPerjawatan { get; set; }
        public DateTime? TarikhPermohonan { get; set; }
        public DateTime? TarikhCadanganWaran { get; set; }
        public DateTime? TarikhWaranDiluluskan { get; set; }
        public Guid? IdCipta { get; set; }
        public DateTime? TarikhCipta { get; set; }

        public PDOStatusPermohonanJawatan StatusPermohonanJawatan { get; set;}
    }
}
