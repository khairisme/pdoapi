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

namespace HR.Application.Services.PDO
{
    public class ButiranPermohonanJawatanExtService : IButiranPermohonanJawatanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<ButiranPermohonanJawatanExtService> _logger;

        public ButiranPermohonanJawatanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<ButiranPermohonanJawatanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task TambahButiranPermohonanJawatan(Guid UserId, TambahButiranPermohonanJawatanDto request)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var entity = new PDOButiranPermohonanJawatan();
                entity.IdCipta = UserId;
                entity.IdButiranPermohonan = request.IdButiranPermohonan;
                entity.IdJawatan = request.IdJawatan;
                entity.StatusAktif = request.StatusAktif;
                entity.IdCipta = request.IdCipta;
                entity.TarikhCipta = DateTime.Now;
                await _context.PDOButiranPermohonanJawatan.AddAsync(entity); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonanJawatan");

                throw;
            }

        }



    }

}

