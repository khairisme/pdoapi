using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.DTOs
{
    public class ValidateNewPengguna
    {
        /// <summary>
        /// User ID
        /// </summary>
       public string IdPengguna { get; set; }
        /// <summary>
        /// temporary_password 
        /// </summary>
        public string KataLaluanSementara { get; set; }
    }
}
