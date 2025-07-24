using HR.Application.DTOs;
using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Core.Entities;
using HR.Core.Entities.PDO;
using HR.Core.Interfaces;
using HR.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.Application.Services.PDO
{
    public class SkimKetuaPerkhidmatanService : ISkimKetuaPerkhidmatanService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<SkimKetuaPerkhidmatanService> _logger;
        public SkimKetuaPerkhidmatanService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<SkimKetuaPerkhidmatanService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(List<SkimKetuaPerkhidmatanRequestDto> dto)
        {
            _logger.LogInformation("Service: Creating new SkimKetuaPerkhidmatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {



                var entities = dto.Select(d => new PDOSkimKetuaPerkhidmatan
                {
                    IdSkimPerkhidmatan = d.IdSkimPerkhidmatan,
                    IdJawatan = d.IdKetuaPerkhidmatan
                }).ToList();

                await _unitOfWork.Repository<PDOSkimKetuaPerkhidmatan>().AddRangeAsync(entities);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service CreateAsync");
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> SoftDeleteSkimKetuaPerkhidmatanAsync(int IdSkim,int IdJawatan)
        {
            var entity = await _context.PDOSkimKetuaPerkhidmatan.FirstOrDefaultAsync(x => x.IdJawatan== IdJawatan
             && x.IdSkimPerkhidmatan==IdSkim
            );
            if (entity == null) return false;

            entity.StatusAktif = false;
            _context.PDOSkimKetuaPerkhidmatan.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
