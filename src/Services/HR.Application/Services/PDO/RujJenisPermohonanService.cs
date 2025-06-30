using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Core.Entities.PDO;
using HR.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Services.PDO
{
    public class RujJenisPermohonanService : IRujJenisPermohonanService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly ILogger<RujJenisPermohonanService> _logger;

        public RujJenisPermohonanService(IPDOUnitOfWork unitOfWork, ILogger<RujJenisPermohonanService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<RujJenisPermohonanDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all RujJenisPermohonan using Entity Framework");
            var result = await _unitOfWork.Repository<PDORujJenisPermohonan>().GetAllAsync();
            result = result.ToList().Where(e => e.StatusAktif);
            return result.Select(MapToDto);
        }
        private RujJenisPermohonanDto MapToDto(PDORujJenisPermohonan pdo)
        {
            return new RujJenisPermohonanDto
            {

                Nama = pdo.Nama,
                Kod = pdo.Kod
            };
        }
    }
}
