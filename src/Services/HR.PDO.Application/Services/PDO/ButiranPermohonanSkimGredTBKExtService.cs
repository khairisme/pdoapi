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
    public class ButiranPermohonanSkimGredTBKExtService : IButiranPermohonanSkimGredTBKExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<ButiranPermohonanSkimGredTBKExtService> _logger;

        public ButiranPermohonanSkimGredTBKExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<ButiranPermohonanSkimGredTBKExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task TambahButiranPermohonanSkimGredTBK(Guid UserId, TambahButiranPermohonanSkimGredTBKDto request)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var entity = new PDOButiranPermohonanSkimGredTBK();
                entity.IdCipta = UserId;
                entity.IdButiranPermohonan = request.IdButiranPermohonan;
                entity.IdSkimPerkhidmatan = request.IdSkimPerkhidmatan;
                entity.IdCipta = request.IdCipta;
                entity.TarikhCipta = DateTime.Now;
                entity.IdGred = request.IdGred;
                await _context.PDOButiranPermohonanSkimGredTBK.AddAsync(entity); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonanSkimGredTBK");

                throw;
            }

        }



    }

}

