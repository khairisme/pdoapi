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

        public async Task<List<ButiranPermohonanSkimGredKUJDto>> SenaraiButiranPermohonanSkimGredKUJ()
        {
            try

            {

                var result = await (from pdobpsgk in _context.PDOButiranPermohonanSkimGredKUJ
                    select new ButiranPermohonanSkimGredKUJDto{
                         IdButiranPermohonan = pdobpsgk.IdButiranPermohonan,
                         IdCipta = pdobpsgk.IdCipta,
                         IdGred = pdobpsgk.IdGred,
                         IdHapus = pdobpsgk.IdHapus,
                         IdPinda = pdobpsgk.IdPinda,
                         IdSkim = pdobpsgk.IdSkim,
                         TarikhCipta = pdobpsgk.TarikhCipta,
                         TarikhHapus = pdobpsgk.TarikhHapus,
                         TarikhPinda = pdobpsgk.TarikhPinda

                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiButiranPermohonanSkimGredKUJ");

                throw;
            }

        }



        public async Task<ButiranPermohonanSkimGredKUJDto> BacaButiranPermohonanSkimGredKUJ(int Id)
        {
            try

            {

                var result = await (from pdobpsgk in _context.PDOButiranPermohonanSkimGredKUJ
                    where pdobpsgk.Id == Id
                    select new ButiranPermohonanSkimGredKUJDto{
                         IdButiranPermohonan = pdobpsgk.IdButiranPermohonan,
                         IdCipta = pdobpsgk.IdCipta,
                         IdGred = pdobpsgk.IdGred,
                         IdHapus = pdobpsgk.IdHapus,
                         IdPinda = pdobpsgk.IdPinda,
                         IdSkim = pdobpsgk.IdSkim,
                         TarikhCipta = pdobpsgk.TarikhCipta,
                         TarikhHapus = pdobpsgk.TarikhHapus,
                         TarikhPinda = pdobpsgk.TarikhPinda
                    }
                ).FirstOrDefaultAsync();
                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in BacaButiranPermohonanSkimGredKUJ");

                throw;
            }

        }



        public async Task HapusTerusButiranPermohonanSkimGredKUJ(Guid UserId, int Id)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await (from pdobpsgk in _context.PDOButiranPermohonanSkimGredKUJ
                             where pdobpsgk.Id == Id select pdobpsgk
                              ).FirstOrDefaultAsync();
                if (entity == null)
                {
                    throw new Exception("PDOButiranPermohonanSkimGredKUJ not found.");
                }
                _context.PDOButiranPermohonanSkimGredKUJ.Remove(entity);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in HapusTerusButiranPermohonanSkimGredKUJ");

                throw;
            }

        }



        public async Task KemaskiniButiranPermohonanSkimGredKUJ(Guid UserId, int Id, ButiranPermohonanSkimGredKUJDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var data = await (from pdobpsgk in _context.PDOButiranPermohonanSkimGredKUJ
                             where pdobpsgk.Id == Id
                             select pdobpsgk).FirstOrDefaultAsync();

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in KemaskiniButiranPermohonanSkimGredKUJ");

                throw;
            }

        }



        public async Task TambahButiranPermohonanSkimGredKUJ(Guid UserId, TambahButiranPermohonanSkimGredKUJDto request)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOButiranPermohonanSkimGredKUJ();
                entity.IdCipta = UserId;
                entity.IdButiranPermohonan = request.IdButiranPermohonan;
                entity.IdSkim = request.IdSkim;
                entity.IdGred = request.IdGred;
                entity.IdCipta = UserId;
                entity.TarikhCipta = DateTime.Now; ;
                await _context.PDOButiranPermohonanSkimGredKUJ.AddAsync(entity); 

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

