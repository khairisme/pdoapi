using HR.PDO.Core.Entities.PDO;
using System;
using System.Threading.Tasks;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO for reading organizational unit details (Unit Organisasi).
    /// Contains identifiers, classification codes, organizational hierarchy, 
    /// indicators, and historical information about the unit.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to transfer organizational unit data from the system to clients,
    ///               including reference codes, structural hierarchy, attributes, 
    ///               establishment details, and status indicators.
    /// </remarks>
    public class BacaUnitOrganisasiDto
    {
        /// <summary>
        /// Unique identifier for the organizational unit.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Reference code for the organizational unit category.
        /// </summary>
        public string KodRujKategoriUnitOrganisasi { get; set; }

        /// <summary>
        /// Reference code for the type of agency.
        /// </summary>
        public string KodRujJenisAgensi { get; set; }

        /// <summary>
        /// Reference code for the organizational cluster.
        /// </summary>
        public string KodRujKluster { get; set; }

        /// <summary>
        /// Indicator whether the unit has a parent organizational unit.
        /// </summary>
        public bool IdIndukUnitOrganisasi { get; set; }

        /// <summary>
        /// Reference code for the ministry associated with the unit.
        /// </summary>
        public string KodKementerian { get; set; }

        /// <summary>
        /// Reference code for the department associated with the unit.
        /// </summary>
        public string KodJabatan { get; set; }

        /// <summary>
        /// Organizational unit code.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Full name of the organizational unit.
        /// </summary>
        public string Nama { get; set; }

        /// <summary>
        /// Abbreviation or short form of the organizational unit name.
        /// </summary>
        public string Singkatan { get; set; }

        /// <summary>
        /// Description of the organizational unit.
        /// </summary>
        public string Keterangan { get; set; }

        /// <summary>
        /// Hierarchical level of the organizational unit.
        /// </summary>
        public int Tahap { get; set; }

        /// <summary>
        /// Reference code for the organizational chart.
        /// </summary>
        public string KodCartaOrganisasi { get; set; }

        /// <summary>
        /// Indicator whether the unit is recognized as an agency.
        /// </summary>
        public bool IndikatorAgensi { get; set; }

        /// <summary>
        /// Indicator whether the unit is an officially recognized agency.
        /// </summary>
        public bool IndikatorAgensiRasmi { get; set; }

        /// <summary>
        /// Indicator whether the unit can act as a job application applicant.
        /// </summary>
        public bool IndikatorPemohonPerjawatan { get; set; }

        /// <summary>
        /// Indicator whether the unit is a state government department.
        /// </summary>
        public bool IndikatorJabatanDiKerajaanNegeri { get; set; }

        /// <summary>
        /// Date the organizational unit was established.
        /// </summary>
        public DateTime TarikhPenubuhan { get; set; }

        /// <summary>
        /// Indicator whether the establishment history is recorded.
        /// </summary>
        public bool SejarahPenubuhan { get; set; }

        /// <summary>
        /// Date when the last structural reorganization was made.
        /// </summary>
        public DateTime TarikhAkhirPenyusunan { get; set; }

        /// <summary>
        /// Date when the last structural strengthening was made.
        /// </summary>
        public DateTime TarikhAkhirPengukuhan { get; set; }

        /// <summary>
        /// Indicator whether update details are recorded for this unit.
        /// </summary>
        public bool ButiranKemaskini { get; set; }

        /// <summary>
        /// Indicator whether the unit is currently active.
        /// </summary>
        public bool StatusAktif { get; set; }

        /// <summary>
        /// Identifier of the creator (user) associated with the unit.
        /// </summary>
        public Guid IdCipta { get; set; }
    }
}
