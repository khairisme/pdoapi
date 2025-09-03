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
                         IdCipta = pdobpsgt.IdCipta,
                         IdGred = pdobpsgt.IdGred,
                         IdHapus = pdobpsgt.IdHapus,
                         IdPinda = pdobpsgt.IdPinda,
                         IdSkimPerkhidmatan = pdobpsgt.IdSkimPerkhidmatan,
                         TarikhCipta = pdobpsgt.TarikhCipta,

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



        public async Task<ButiranPermohonanSkimGredTBKDto> BacaButiranPermohonanSkimGredTBK(int Id)
        {
            try

            {

                var result = await (from pdobpsgt in _context.PDOButiranPermohonanSkimGredTBK
                    where pdobpsgt.Id == Id
                    select new ButiranPermohonanSkimGredTBKDto{
                         IdButiranPermohonan = pdobpsgt.IdButiranPermohonan,
                         IdCipta = pdobpsgt.IdCipta,
                         IdGred = pdobpsgt.IdGred,
                         IdHapus = pdobpsgt.IdHapus,
                         IdPinda = pdobpsgt.IdPinda,
                         IdSkimPerkhidmatan = pdobpsgt.IdSkimPerkhidmatan,
                         TarikhCipta = pdobpsgt.TarikhCipta,
                         TarikhHapus = pdobpsgt.TarikhHapus,
                         TarikhPinda = pdobpsgt.TarikhPinda
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



        public async Task HapusTerusButiranPermohonanSkimGredTBK(Guid UserId, int Id)
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



        public async Task KemaskiniButiranPermohonanSkimGredTBK(Guid UserId, int Id, ButiranPermohonanSkimGredTBKDto request)
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

                _logger.LogError(ex, "Error in KemaskiniButiranPermohonanSkimGredTBK");

                throw;
            }

        }



        public async Task TambahButiranPermohonanSkimGredTBK(Guid UserId, TambahButiranPermohonanSkimGredTBKDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOButiranPermohonanSkimGredTBK();
                entity.IdCipta = UserId;
                entity.IdButiranPermohonan = request.IdButiranPermohonan;
                entity.IdSkimPerkhidmatan = request.IdSkimPerkhidmatan;
                entity.IdGred = request.IdGred;
                entity.IdCipta = UserId;
                entity.TarikhCipta = DateTime.Now;
                await _context.PDOButiranPermohonanSkimGredTBK.AddAsync(entity); 

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

