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
    public class StatusPermohonanJawatanExtService : IStatusPermohonanJawatanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<StatusPermohonanJawatanExtService> _logger;

        public StatusPermohonanJawatanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<StatusPermohonanJawatanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task TambahStatusPermohonanJawatan(Guid UserId, TambahStatusPermohonanJawatanDto request)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var entity = new PDOStatusPermohonanJawatan();
                entity.IdCipta = UserId;
                entity.IdPermohonanJawatan = request.IdPermohonanJawatan;
                entity.KodRujStatusPermohonanJawatan = request.KodRujStatusPermohonanJawatan;
                entity.TarikhStatusPermohonan = DateTime.Now;
                entity.Ulasan = request.Ulasan;
                entity.StatusAktif = request.StatusAktif;
                entity.IdCipta = request.IdCipta;
                entity.TarikhCipta = DateTime.Now;
                await _context.PDOStatusPermohonanJawatan.AddAsync(entity); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahStatusPermohonanJawatan");

                throw;
            }

        }



        public async Task TambahStatusPermohonanJawatanDraft(Guid UserId, TambahStatusPermohonanJawatanDraftDto request)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var entity = new PDOStatusPermohonanJawatan();
                entity.IdCipta = UserId;
                entity.IdPermohonanJawatan = request.IdPermohonanJawatan;
                entity.KodRujStatusPermohonanJawatan = "01";
                entity.TarikhStatusPermohonan = DateTime.Now;
                entity.Ulasan = request.Ulasan;
                entity.StatusAktif = request.StatusAktif;
                entity.IdCipta = request.IdCipta;
                entity.TarikhCipta = DateTime.Now;
                await _context.PDOStatusPermohonanJawatan.AddAsync(entity); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahStatusPermohonanJawatanDraft");

                throw;
            }

        }



        public async Task TambahStatusPermohonanJawatanBaharu(Guid UserId, TambahStatusPermohonanJawatanBaharuDto request)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var entity = new PDOStatusPermohonanJawatan();
                entity.IdCipta = UserId;
                entity.IdPermohonanJawatan = request.IdPermohonanJawatan;
                entity.KodRujStatusPermohonanJawatan = "02";
                entity.TarikhStatusPermohonan = DateTime.Now;
                entity.Ulasan = request.Ulasan;
                entity.StatusAktif = request.StatusAktif;
                entity.IdCipta = request.IdCipta;
                entity.TarikhCipta = DateTime.Now;
                await _context.PDOStatusPermohonanJawatan.AddAsync(entity); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahStatusPermohonanJawatanBaharu");

                throw;
            }

        }



    }

}

