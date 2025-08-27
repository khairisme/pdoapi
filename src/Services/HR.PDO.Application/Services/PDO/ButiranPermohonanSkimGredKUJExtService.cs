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
    public class ButiranPermohonanSkimGredKUJExtService : IButiranPermohonanSkimGredKUJExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<ButiranPermohonanSkimGredKUJExtService> _logger;

        public ButiranPermohonanSkimGredKUJExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<ButiranPermohonanSkimGredKUJExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task TambahButiranPermohonanSkimGredKUJ(Guid UserId, TambahButiranPermohonanSkimGredKUJDto request)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var entity = new PDOButiranPermohonanSkimGredKUJ();
                entity.IdCipta = UserId;
                entity.IdButiranPermohonan = request.IdButiranPermohonan;
                entity.IdSkim = request.IdSkim;
                entity.IdCipta = request.IdCipta;
                entity.TarikhCipta = request.TarikhCipta;
                entity.IdGred = request.IdGred;
                await _context.PDOButiranPermohonanSkimGredKUJ.AddAsync(entity); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonanSkimGredKUJ");

                throw;
            }

        }



    }

}

