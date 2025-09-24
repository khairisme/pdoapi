using System;
namespace HR.PDO.Application.DTOs
{
    /// <summary>
    /// DTO for returning a simple dropdown item with an ID, code, and name.
    /// </summary>
    /// <remarks>
    /// Author      : Khairi bin Abu Bakar
    /// Created On  : 2025-08-31
    /// Purpose     : Used to populate dropdown lists in UI or API responses.
    /// </remarks>
    public class DropDownDto
    {
        #region
        public int? IdKumpulanPerkhidmatan { get; set; }
        public int? IdKlasifikasiPerkhidmatan { get; set; }
        public int? IdSkimPerkhidmatan { get; set; }
        public string? KodRujJenisSaraan{ get; set; }
        #endregion
        /// <summary>
        /// Required. Unique identifier for the dropdown item.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Required. Code or shorthand for the item.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Required. Display name of the item.
        /// </summary>
        public string Nama { get; set; }
    }
    public class DropDownNegaraDto
    {
        public string? kod { get; set; }
        public string? nama { get; set; }
        public string? keterangan { get; set; }
        public bool? statusAktif { get; set; }
        public string? rawatan { get; set; }
    }
}
