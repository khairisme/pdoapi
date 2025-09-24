using System;
using System.ComponentModel;
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
    public class NegeriRequestDto
    {
        /// <summary>
        /// Required. Code or shorthand for the item.
        /// </summary>
        [DefaultValue("MYS")]
        public string kodRujNegara { get; set; }

        /// <summary>
        /// Required. Display name of the item.
        /// </summary>
        //public string kodBandar { get; set; }
    }
    public class BandarRequestDto
    {
        /// <summary>
        /// Required. Code or shorthand for the item.
        /// </summary>
        public string kodRujNegeri { get; set; }
        [DefaultValue("MYS")]
        public string kodRujNegara { get; set; }
    }
}
