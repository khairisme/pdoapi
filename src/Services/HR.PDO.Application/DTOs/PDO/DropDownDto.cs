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
        /// <summary>
        /// Required. Unique identifier for the dropdown item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Required. Code or shorthand for the item.
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Required. Display name of the item.
        /// </summary>
        public string Nama { get; set; }
    }
}
