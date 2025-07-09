using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Core.Entities;
using HR.Core.Entities.PDO;
using HR.Core.Enums;
using HR.Core.Interfaces;
using HR.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Services.PDO
{
    public class PengisianJawatanService: IPengisianJawatanService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _dbContext;
        private readonly ILogger<PengisianJawatanService> _logger;

        public PengisianJawatanService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<PengisianJawatanService> logger)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<PengisianJawatanSearchResponseDto?> GetPengisianJawatanAsync(int idSkimPerkhidmatan)
        {
            try
            {
                _logger.LogInformation("Getting PengisianJawatan by ID {Id} using Entity Framework", idSkimPerkhidmatan);
                var result = await (from a in _dbContext.PDOPengisianJawatan
                              join b in _dbContext.PDOJawatan
                                  on a.IdJawatan equals b.Id
                              join c in _dbContext.PDOUnitOrganisasi
                                  on b.IdUnitOrganisasi equals c.Id
                              join d in _dbContext.PDOKekosonganJawatan
                                  on b.Id equals d.IdJawatan
                              join e in _dbContext.PDORujStatusKekosonganJawatan
                                  on d.KodRujStatusKekosonganJawatan equals e.Kod
                              join f in _dbContext.PDOGredSkimJawatan
                                  on a.IdJawatan equals f.IdJawatan
                              where b.StatusAktif == true && f.IdSkimPerkhidmatan == idSkimPerkhidmatan
                              select new PengisianJawatanSearchResponseDto
                              {
                                  Id=a.Id,
                                  KodJawatan = b.Kod,
                                  NamaJawatan = b.Nama,
                                  UnitOrganisasi = c.Nama,
                                  StatusPengisianJawatan = e.Nama,
                                  TarikhKekosonganJawatan = d.TarikhStatusKekosongan
                              }).FirstOrDefaultAsync();
                



                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting MaklumatSkimPerkhidmatan");
                throw;
            }
        }
        public async Task<int> GetPengisianJawatanCountAsync(int idSkimPerkhidmatan)
        {
            try
            {
                _logger.LogInformation("Getting PengisianJawatan by ID {idSkimPerkhidmatan} using Entity Framework", idSkimPerkhidmatan);
                int result = await (from a in _dbContext.PDOPengisianJawatan
                                    join b in _dbContext.PDOJawatan
                                        on a.IdJawatan equals b.Id
                                    join c in _dbContext.PDOUnitOrganisasi
                                        on b.IdUnitOrganisasi equals c.Id
                                    join d in _dbContext.PDOKekosonganJawatan
                                        on b.Id equals d.IdJawatan
                                    join e in _dbContext.PDORujStatusKekosonganJawatan
                                        on d.KodRujStatusKekosonganJawatan equals e.Kod
                                    join f in _dbContext.PDOGredSkimJawatan
                                        on a.IdJawatan equals f.IdJawatan
                                    where b.StatusAktif == true
                                          && f.IdSkimPerkhidmatan == idSkimPerkhidmatan
                                    select a).CountAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting MaklumatSkimPerkhidmatan");
                throw;
            }
        }
        public async Task<bool> CreateAsync(PengisianJawatanDto dto)
        {
            _logger.LogInformation("Service: Creating new PengisianJawatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var userId = Guid.Empty;
                var pengisian = new PDOPengisianJawatan
                {
                    IdPermohonanPengisianSkim = dto.IdPermohonanPengisianSkim,
                    IdJawatan = dto.IdJawatan,
                    IdPemilikKompetensi = Guid.Empty

                };

                pengisian = await _unitOfWork.Repository<PDOPengisianJawatan>().AddAsync(pengisian);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service CreateAsync");
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting Pengisian Jawatan with ID {Id} using Entity Framework", id);
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var result = await _unitOfWork.Repository<PDOPengisianJawatan>().DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting employee with ID {Id} using Entity Framework", id);
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }


    }
}
