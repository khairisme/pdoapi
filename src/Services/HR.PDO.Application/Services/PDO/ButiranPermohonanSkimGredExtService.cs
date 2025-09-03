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

        public async Task<List<ButiranPermohonanSkimGredDto>> SenaraiButiranPermohonanSkimGred()
        {
            try

            {

                var result = await (from pdobpsg in _context.PDOButiranPermohonanSkimGred
                    select new ButiranPermohonanSkimGredDto{
                         IdButiranPermohonan = pdobpsg.IdButiranPermohonan,
                         IdCipta = pdobpsg.IdCipta,
                         IdGred = pdobpsg.IdGred,
                         IdHapus = pdobpsg.IdHapus,
                         IdKetuaPerkhidmatan = pdobpsg.IdKetuaPerkhidmatan,
                         IdPinda = pdobpsg.IdPinda,
                         IdSkimPerkhidmatan = pdobpsg.IdSkimPerkhidmatan,
                         KodBidangPengkhususan = pdobpsg.KodBidangPengkhususan,
                         KodRujLaluanKemajuanKerjaya = pdobpsg.KodRujLaluanKemajuanKerjaya,
                         TarikhCipta = pdobpsg.TarikhCipta,
                         TarikhHapus = pdobpsg.TarikhHapus,
                         TarikhPinda = pdobpsg.TarikhPinda

                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiButiranPermohonanSkimGred");

                throw;
            }

        }



        public async Task<ButiranPermohonanSkimGredDto> BacaButiranPermohonanSkimGred(int Id)
        {
            try

            {

                var result = await (from pdobpsg in _context.PDOButiranPermohonanSkimGred
                    where pdobpsg.Id == Id
                    select new ButiranPermohonanSkimGredDto{
                         IdButiranPermohonan = pdobpsg.IdButiranPermohonan,
                         IdCipta = pdobpsg.IdCipta,
                         IdGred = pdobpsg.IdGred,
                         IdHapus = pdobpsg.IdHapus,
                         IdKetuaPerkhidmatan = pdobpsg.IdKetuaPerkhidmatan,
                         IdPinda = pdobpsg.IdPinda,
                         IdSkimPerkhidmatan = pdobpsg.IdSkimPerkhidmatan,
                         KodBidangPengkhususan = pdobpsg.KodBidangPengkhususan,
                         KodRujLaluanKemajuanKerjaya = pdobpsg.KodRujLaluanKemajuanKerjaya,
                         TarikhCipta = pdobpsg.TarikhCipta,
                         TarikhHapus = pdobpsg.TarikhHapus,
                         TarikhPinda = pdobpsg.TarikhPinda
                    }
                ).FirstOrDefaultAsync();
                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in BacaButiranPermohonanSkimGred");

                throw;
            }

        }



        public async Task HapusTerusButiranPermohonanSkimGred(Guid UserId, int Id)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await (from pdobpsg in _context.PDOButiranPermohonanSkimGred
                             where pdobpsg.Id == Id select pdobpsg
                              ).FirstOrDefaultAsync();
                if (entity == null)
                {
                    throw new Exception("PDOButiranPermohonanSkimGred not found.");
                }
                _context.PDOButiranPermohonanSkimGred.Remove(entity);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in HapusTerusButiranPermohonanSkimGred");

                throw;
            }

        }



        public async Task KemaskiniButiranPermohonanSkimGred(Guid UserId, int Id, ButiranPermohonanSkimGredDto request)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var data = await (from pdobpsg in _context.PDOButiranPermohonanSkimGred
                             where pdobpsg.Id == Id
                             select pdobpsg).FirstOrDefaultAsync();

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in KemaskiniButiranPermohonanSkimGred");

                throw;
            }

        }



        public async Task TambahButiranPermohonanSkimGred(Guid UserId, TambahButiranPermohonanSkimGredDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOButiranPermohonanSkimGred();
                entity.IdCipta = UserId;
                entity.IdButiranPermohonan = request.IdButiranPermohonan;
                entity.IdSkimPerkhidmatan = request.IdSkimPerkhidmatan;
                entity.KodBidangPengkhususan = request.KodBidangPengkhususan;
                entity.KodRujLaluanKemajuanKerjaya = request.KodRujLaluanKemajuanKerjaya;
                entity.IdGred = request.IdGred;
                entity.IdKetuaPerkhidmatan = request.IdKetuaPerkhidmatan;
                entity.IdCipta = UserId;
                entity.TarikhCipta = DateTime.Now;
                await _context.PDOButiranPermohonanSkimGred.AddAsync(entity); 

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

