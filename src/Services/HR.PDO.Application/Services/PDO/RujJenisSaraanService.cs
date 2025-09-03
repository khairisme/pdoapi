using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.Services.PDO
{
    public class RujJenisSaraanService : IRujJenisSaraanService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly ILogger<RujJenisSaraanService> _logger;

        public RujJenisSaraanService(IPDOUnitOfWork unitOfWork, ILogger<RujJenisSaraanService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<RujJenisSaraanDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all RujJenisSaraan using Entity Framework");
            var result = await _unitOfWork.Repository<PDORujJenisSaraan>().GetAllAsync();
            result = result.ToList().Where(e => e.StatusAktif == true);
            return result.Select(MapToDto);
        }
        private RujJenisSaraanDto MapToDto(PDORujJenisSaraan pdoRujJenisSaraan)
        {
            return new RujJenisSaraanDto
            {

                Nama = pdoRujJenisSaraan.Nama,
                Kod = pdoRujJenisSaraan.Kod,
                Keterangan = pdoRujJenisSaraan.Keterangan,
            };
        }
    }

}
