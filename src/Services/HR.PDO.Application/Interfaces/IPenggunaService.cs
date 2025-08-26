using HR.PDO.Application.DTOs;
using HR.PDO.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.Interfaces
{
    public interface IPenggunaService
    {
        /// <summary>
        /// mengesahkan kelayakan pengguna (validate user credentials)
        /// </summary>
        Task<string>mengesahkankelayakanpengguna(string userId, string temporary_password);
        Task<string> tetapkankatalaluan(string userId, string password);

        Task<IEnumerable<SoalanKeselamatanDto>> CreateSoalanKeselamatanAsync(List<SoalanKeselamatanDto> soalanKeselamatanDto);
        Task<IEnumerable<GambarDto>> GetAllGambarAsync();
        Task<string> CreatePerkhidmatanAsync(PerkhidmatanLogDto perkhidmatanLog);
    }
}
