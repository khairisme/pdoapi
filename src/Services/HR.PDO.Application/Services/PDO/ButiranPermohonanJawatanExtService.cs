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

        public async Task<List<ButiranPermohonanJawatanDto>> SenaraiButiranPermohonanJawatan()
        {
            try

            {

                var result = await (from pdobpj in _context.PDOButiranPermohonanJawatan
                    select new ButiranPermohonanJawatanDto{
                         IdButiranPermohonan = pdobpj.IdButiranPermohonan,
                         IdCipta = pdobpj.IdCipta,
                         IdHapus = pdobpj.IdHapus,
                         IdJawatan = pdobpj.IdJawatan,
                         IdPinda = pdobpj.IdPinda,
                         StatusAktif = pdobpj.StatusAktif ?? false,
                         TarikhCipta = pdobpj.TarikhCipta,
                         TarikhHapus = pdobpj.TarikhHapus,
                         TarikhPinda = pdobpj.TarikhPinda

                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiButiranPermohonanJawatan");

                throw;
            }

        }



        public async Task<ButiranPermohonanJawatanDto> BacaButiranPermohonanJawatan(int Id)
        {
            try

            {

                var result = await (from pdobpj in _context.PDOButiranPermohonanJawatan
                    where pdobpj.Id == Id
                    select new ButiranPermohonanJawatanDto{
                         IdButiranPermohonan = pdobpj.IdButiranPermohonan,
                         IdCipta = pdobpj.IdCipta,
                         IdHapus = pdobpj.IdHapus,
                         IdJawatan = pdobpj.IdJawatan,
                         IdPinda = pdobpj.IdPinda,
                         StatusAktif = pdobpj.StatusAktif ?? false,
                         TarikhCipta = pdobpj.TarikhCipta,
                         TarikhHapus = pdobpj.TarikhHapus,
                         TarikhPinda = pdobpj.TarikhPinda
                    }
                ).FirstOrDefaultAsync();
                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in BacaButiranPermohonanJawatan");

                throw;
            }

        }



        public async Task HapusTerusPermohonanJawatan(Guid UserId, int Id)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await (from pdobpj in _context.PDOButiranPermohonanJawatan
                             where pdobpj.Id == Id select pdobpj
                              ).FirstOrDefaultAsync();
                if (entity == null)
                {
                    throw new Exception("PDOButiranPermohonanJawatan not found.");
                }
                _context.PDOButiranPermohonanJawatan.Remove(entity);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in HapusTerusPermohonanJawatan");

                throw;
            }

        }



        public async Task TambahButiranPermohonanJawatan(Guid UserId, TambahButiranPermohonanJawatanDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOButiranPermohonanJawatan();
                entity.IdCipta = UserId;
                entity.IdButiranPermohonan = request.IdButiranPermohonan;
                entity.IdJawatan = request.IdJawatan;
                entity.StatusAktif = request.StatusAktif;
                entity.IdCipta = request.IdCipta;
                entity.TarikhCipta = DateTime.Now;
                entity.StatusAktif = false;
                await _context.PDOButiranPermohonanJawatan.AddAsync(entity); 

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

