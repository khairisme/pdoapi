using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Application.DTOs;
using HR.PDO.Application.DTOs.PDO;

namespace HR.Application.Services.PDO
{
    public class SkimKetuaPerkhidmatanExtService : ISkimKetuaPerkhidmatanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<SkimKetuaPerkhidmatanExtService> _logger;

        public SkimKetuaPerkhidmatanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<SkimKetuaPerkhidmatanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanKetuaPerkhidmatan(int IdSkimPerkhidmatan)
        {
            try

            {

                var result = await (from pdoj in _context.PDOJawatan
                    join pdoskp in _context.PDOSkimKetuaPerkhidmatan on pdoj.Id equals pdoskp.IdKetuaPerkhidmatan
                    select new DropDownDto{
                         Kod = pdoj.Kod,
                         Nama = pdoj.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanKetuaPerkhidmatan");

                throw;
            }

        }

        public async Task TambahKetuaPerkhidmatan(Guid UserId, SkimKetuaPerkhidmatanRequestDto request)
        {
           try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOSkimKetuaPerkhidmatan();
                entity.IdSkimPerkhidmatan = request.IdSkimPerkhidmatan;
                entity.IdKetuaPerkhidmatan = request.IdKetuaPerkhidmatan;
                entity.IdCipta = UserId;
                entity.TarikhCipta = DateTime.Now;
                entity.StatusAktif = true;
                await _context.PDOSkimKetuaPerkhidmatan.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahKetuaPerkhidmatan");

                throw;
            }
        }


    }

}

