using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.DTOs.PDO
{
    public class SkimKetuaPerkhidmatanRequestDto
    {
        public Guid UserId { get; set; }
        public int IdSkimPerkhidmatan { get; set; }
        public int IdKetuaPerkhidmatan { get; set; }
    }
}
