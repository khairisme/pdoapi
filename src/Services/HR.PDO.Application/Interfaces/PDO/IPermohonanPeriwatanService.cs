using HR.PDO.Application.DTOs.PDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.Interfaces.PDO
{
    public interface IPermohonanPeriwatanService
    {
        Task<bool> CreateAsync(PermohonanPeriwatanCreateRequestDto dto);
        Task<bool> CreateAktivitiOrganisasiAsync(AktivitiOrganisasiCreateRequestDto dto);

        Task<bool> SimpanStatusPermohonanAsync(SimpanStatusPermohonanDto dto);
        Task<string?> GetUlasanStatusAsync(int idPermohonanJawatan);
    }
}
