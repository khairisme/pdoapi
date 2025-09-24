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



        public async Task<ButiranPermohonanJawatanDto> BacaButiranPermohonanJawatan(AddButiranPermohonanJawatanRequestDto request)
        {
            try

            {

                var result = await (from pdobpj in _context.PDOButiranPermohonanJawatan
                    where pdobpj.IdButiranPermohonan == request.IdButiranPermohonan
                    && pdobpj.IdJawatan == request.IdJawatan
                    select new ButiranPermohonanJawatanDto{
                         IdButiranPermohonan = pdobpj.IdButiranPermohonan,
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


        public async Task<ButiranPermohonanJawatanDto> BacaButiranPermohonanJawatan(int? IdButiranPermohonan, int? IdJawatan)
        {
            try

            {

                var result = await (from pdobpj in _context.PDOButiranPermohonanJawatan
                                    where pdobpj.IdButiranPermohonan == IdButiranPermohonan
                                    && pdobpj.IdJawatan == IdJawatan
                                    select new ButiranPermohonanJawatanDto
                                    {
                                        IdButiranPermohonan = pdobpj.IdButiranPermohonan,
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

        public async Task HapusTerusPermohonanJawatan(AddButiranPermohonanJawatanRequestDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await (from pdobpj in _context.PDOButiranPermohonanJawatan
                             where pdobpj.IdButiranPermohonan == request.IdButiranPermohonan
                             && pdobpj.IdJawatan == request.IdJawatan
                                    select pdobpj
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



        public async Task<ButiranPermohonanJawatanDto> TambahButiranPermohonanJawatan(TambahButiranPermohonanJawatanDto request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOButiranPermohonanJawatan
                {
                    IdButiranPermohonan = request.IdButiranPermohonan,
                    IdJawatan = request.IdJawatan,
                    IdCipta = request.UserId,
                    TarikhCipta = DateTime.UtcNow,   // use UTC for consistency
                    StatusAktif = false
                };

                await _context.PDOButiranPermohonanJawatan.AddAsync(entity);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
                return await BacaButiranPermohonanJawatan(entity.IdButiranPermohonan, entity.IdJawatan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TambahButiranPermohonanJawatan");
                await _unitOfWork.RollbackAsync();  // rollback on error
                throw;
            }
        }



    }

}

