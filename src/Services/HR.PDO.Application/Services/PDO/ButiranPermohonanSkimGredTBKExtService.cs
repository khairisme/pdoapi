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

        public async Task<List<ButiranPermohonanSkimGredTBKDto>> SenaraiButiranPermohonanSkimGredTBK()
        {
            try

            {

                var result = await (from pdobpsgt in _context.PDOButiranPermohonanSkimGredTBK
                    select new ButiranPermohonanSkimGredTBKDto{
                         IdButiranPermohonan = pdobpsgt.IdButiranPermohonan,
                         IdGred = pdobpsgt.IdGred,
                         IdSkimPerkhidmatan = pdobpsgt.IdSkimPerkhidmatan,

                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiButiranPermohonanSkimGredTBK");

                throw;
            }

        }



        public async Task<ButiranPermohonanSkimGredTBKDto> BacaButiranPermohonanSkimGredTBK(int? Id)
        {
            try

            {

                var result = await (from pdobpsgt in _context.PDOButiranPermohonanSkimGredTBK
                    where pdobpsgt.Id == Id
                    select new ButiranPermohonanSkimGredTBKDto{
                         IdButiranPermohonan = pdobpsgt.IdButiranPermohonan,
                         IdSkimPerkhidmatan = pdobpsgt.IdSkimPerkhidmatan,
                    }
                ).FirstOrDefaultAsync();
                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in BacaButiranPermohonanSkimGredTBK");

                throw;
            }

        }



        public async Task HapusTerusButiranPermohonanSkimGredTBK(int Id)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await (from pdobpsgt in _context.PDOButiranPermohonanSkimGredTBK
                             where pdobpsgt.Id == Id select pdobpsgt
                              ).FirstOrDefaultAsync();
                if (entity == null)
                {
                    throw new Exception("PDOButiranPermohonanSkimGredTBK not found.");
                }
                _context.PDOButiranPermohonanSkimGredTBK.Remove(entity);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in HapusTerusButiranPermohonanSkimGredTBK");

                throw;
            }

        }



        public async Task KemaskiniButiranPermohonanSkimGredTBK( ButiranPermohonanSkimGredTBKDto request)
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

                _logger.LogError(ex, "Error in KemaskiniButiranPermohonanSkimGredTBK");

                throw;
            }

        }



        public async Task<List<PDOButiranPermohonanSkimGredTBK>> TambahButiranPermohonanSkimGredTBK(TambahButiranPermohonanSkimGredTBKDto request)
        {

            try
            {
                List<PDOButiranPermohonanSkimGredTBK> insertedRecords = new List<PDOButiranPermohonanSkimGredTBK>();
                foreach (var req in request.ButiranPermohonanSkimGredTBKList)
                {
                    var GredList = req.IdGredList
                        .Split(",")
                        .Select(s => int.TryParse(s.Trim(), out var val) ? val : 0)
                        .ToList();
                    foreach (var gred in GredList)
                    {
                        await _unitOfWork.BeginTransactionAsync();
                        var entity = new PDOButiranPermohonanSkimGredTBK();
                        entity.IdButiranPermohonan = request.IdButiranPermohonan;
                        entity.IdSkimPerkhidmatan = req.IdSkimPerkhidmatan;
                        entity.IdGred = gred;
                        entity.IdCipta = request.UserId;
                        entity.TarikhCipta = DateTime.Now; ;
                        await _context.PDOButiranPermohonanSkimGredTBK.AddAsync(entity);

                        await _unitOfWork.SaveChangesAsync();
                        insertedRecords.Add(entity);
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

