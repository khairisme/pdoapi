using HR.PDO.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.Extensions
{
    public static class StatusKumpulanPerkhidmatanEnumExtensions
    {
        public static string ToDisplayString(this StatusKumpulanPerkhidmatanEnum status)
        {
            return status switch
            {
                StatusKumpulanPerkhidmatanEnum.Aktif => "Aktif",
                StatusKumpulanPerkhidmatanEnum.TidakAktif => "Tidak Aktif",
                _ => "Unknown"
            };
        }
    }
}
