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
    public class ButiranPermohonanExtService : IButiranPermohonanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<ButiranPermohonanExtService> _logger;

        public ButiranPermohonanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<ButiranPermohonanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task TambahButiranPermohonan(Guid UserId, TambahButiranPermohonanDto request)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var entity = new PDOButiranPermohonan();
                entity.IdCipta = UserId;
                entity.IdPermohonanJawatan = request.IdPermohonanJawatan;
                entity.IdAktivitiOrganisasi = request.IdAktivitiOrganisasi;
                entity.KodRujStatusJawatan = request.KodRujStatusJawatan;
                entity.TarikhMula = request.TarikhMula;
                entity.TarikhTamat = request.TarikhTamat;
                entity.KodRujJenisJawatan = request.KodRujJenisJawatan;
                entity.NoButiran = request.NoButiran;
                entity.IndikatorTBK = request.IndikatorTBK;
                entity.IndikatorHBS = request.IndikatorHBS;
                await _context.PDOButiranPermohonan.AddAsync(entity); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonan");

                throw;
            }

        }



    }

}

