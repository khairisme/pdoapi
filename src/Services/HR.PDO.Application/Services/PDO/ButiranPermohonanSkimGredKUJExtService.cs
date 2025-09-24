using Azure.Core;
using HR.PDO.Application.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                         IdGred = pdobpsgk.IdGred,
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



        public async Task<ButiranPermohonanSkimGredKUJDto> BacaButiranPermohonanSkimGredKUJ(int? Id)
        {
            try

            {

                var result = await (from pdobpsgk in _context.PDOButiranPermohonanSkimGredKUJ
                    where pdobpsgk.Id == Id
                    select new ButiranPermohonanSkimGredKUJDto{
                         IdButiranPermohonan = pdobpsgk.IdButiranPermohonan,
                         IdGred = pdobpsgk.IdGred,
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



        public async Task HapusTerusButiranPermohonanSkimGredKUJ(int Id)
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



        public async Task KemaskiniButiranPermohonanSkimGredKUJ(ButiranPermohonanSkimGredKUJDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var data = await (from pdobpsgk in _context.PDOButiranPermohonanSkimGredKUJ
                             where pdobpsgk.Id == request.Id
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



        public async Task<int?> TambahButiranPermohonanSkimGredKUJ(TambahButiranPermohonanSkimGredKUJDto request)
        {

            try
            {
                var insertedRecords = 0;
                foreach (var req in request.ButiranPermohonanSkimGredKUJList)
                {
                    var GredList = req.IdGredList
                        .Split(",")
                        .Select(s => int.TryParse(s.Trim(), out var val) ? val : 0)
                        .ToList();
                    foreach(var gred in GredList)
                    {
                        await _unitOfWork.BeginTransactionAsync();
                        var entity = new PDOButiranPermohonanSkimGredKUJ();
                        entity.IdButiranPermohonan = request.IdButiranPermohonan;
                        entity.IdSkim = req.IdSkimPerkhidmatan;
                        entity.IdGred = gred;
                        entity.IdCipta = request.UserId;
                        entity.TarikhCipta = DateTime.Now; ;
                        await _context.PDOButiranPermohonanSkimGredKUJ.AddAsync(entity);

                        await _unitOfWork.SaveChangesAsync();
                        ++insertedRecords;
                    }
                    await _unitOfWork.CommitAsync();

                }
                return insertedRecords;
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonanSkimGredKUJ");

                throw;
            }

        }



    }

}

