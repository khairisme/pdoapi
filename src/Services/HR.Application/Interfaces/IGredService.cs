using HR.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Interfaces
{
    public interface IGredService
    {
        Task<List<PDOGredDto>> GetGredListAsync(int idKlasifikasi, int idKumpulan);
        Task<List<GredSearchResultDTO>> SearchGredAsync(int? idKlasifikasi, int? idKumpulan);
    }
}
