using HR.Application.DTOs;
using HR.Application.Interfaces;
using HR.Core.Entities;
using HR.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Services
{
    public class RujStatusPermohonanService : IRujStatusPermohonanService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly ILogger<RujStatusPermohonanService> _logger;

        public RujStatusPermohonanService(IPDOUnitOfWork unitOfWork, ILogger<RujStatusPermohonanService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<RujStatusPermohonanDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all RujStatusPermohonan using Entity Framework");
            var result = await _unitOfWork.Repository<PDORujStatusPermohonan>().GetAllAsync();
            result = result.ToList().Where(e => e.StatusAktif);
            return result.Select(MapToDto);
        }
        private RujStatusPermohonanDto MapToDto(PDORujStatusPermohonan pDOKumpulan)
        {
            return new RujStatusPermohonanDto
            {

                Nama = pDOKumpulan.Nama,
                Kod= pDOKumpulan.Kod,
                Keterangan=pDOKumpulan.Keterangan,
            };
        }
    }
}
