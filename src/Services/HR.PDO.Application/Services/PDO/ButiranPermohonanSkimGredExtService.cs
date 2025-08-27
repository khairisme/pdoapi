using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Application.DTOs;

namespace HR.Application.Services.PDO
{
    public class ButiranPermohonanSkimGredExtService : IButiranPermohonanSkimGredExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<ButiranPermohonanSkimGredExtService> _logger;

        public ButiranPermohonanSkimGredExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<ButiranPermohonanSkimGredExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task TambahButiranPermohonanSkimGred(Guid UserId, TambahButiranPermohonanSkimGredDto request)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var entity = new PDOButiranPermohonanSkimGred();
                entity.IdCipta = UserId;
                entity.IdButiranPermohonan = request.IdButiranPermohonan;
                entity.IdSkimPerkhidmatan = request.IdSkimPerkhidmatan;
                entity.IdGred = request.IdGred;
                entity.IdKetuaPerkhidmatan = request.IdKetuaPerkhidmatan;
                entity.IdCipta = request.IdCipta;
                entity.TarikhCipta = DateTime.Now;
                await _context.PDOButiranPermohonanSkimGred.AddAsync(entity); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonanSkimGred");

                throw;
            }

        }



    }

}

