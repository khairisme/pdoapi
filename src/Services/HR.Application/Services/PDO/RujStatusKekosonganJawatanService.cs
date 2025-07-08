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
    public class RujStatusKekosonganJawatanService : IRujStatusKekosonganJawatanService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly ILogger<RujStatusKekosonganJawatanService> _logger;

        public RujStatusKekosonganJawatanService(IPDOUnitOfWork unitOfWork, ILogger<RujStatusKekosonganJawatanService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IEnumerable<RujStatusKekosonganJawatanResponseDto>> GetStatusKekosonganJawatan()
        {
            _logger.LogInformation("GetStatusKekosonganJawatan: Getting all RujStatusKekosonganJawatan records");
            try
            {
                _logger.LogInformation("GetStatusKekosonganJawatan: Executing query to fetch all records from repository");
                var result = await _unitOfWork.Repository<PDORujStatusKekosonganJawatan>().GetAllAsync();

                _logger.LogInformation("GetStatusKekosonganJawatan: Retrieved {Count} records from database", result.Count());

                var mappedResult = result.Select(x => new RujStatusKekosonganJawatanResponseDto
                {
                    Kod = x.Kod ?? String.Empty,
                    Nama = x.Nama ?? String.Empty
                });

                _logger.LogInformation("GetStatusKekosonganJawatan: Successfully processed {Count} RujStatusKekosonganJawatan records", mappedResult.Count());
                return mappedResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetStatusKekosonganJawatan: Failed to retrieve RujStatusKekosonganJawatan data");
                throw;
            }
        }
    }
}
