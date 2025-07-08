using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Core.Entities;
using HR.Core.Entities.PDO;
using HR.Core.Enums;
using HR.Core.Interfaces;
using HR.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Services.PDO
{
    public class PengisianJawatanService: IPengisianJawatanService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _dbContext;
        private readonly ILogger<PengisianJawatanService> _logger;

        public PengisianJawatanService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<PengisianJawatanService> logger)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting Pengisian Jawatan with ID {Id} using Entity Framework", id);
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var result = await _unitOfWork.Repository<PDOPengisianJawatan>().DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting employee with ID {Id} using Entity Framework", id);
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }


    }
}
