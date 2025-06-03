using HR.Application.DTOs;
using HR.Application.Interfaces;
using HR.Core.Entities;
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
using HR.Application.Extensions;

namespace HR.Application.Services
{
    public class KumpulanPerkhidmatanService : IKumpulanPerkhidmatanService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _dbContext;
        private readonly ILogger<KumpulanPerkhidmatanService> _logger;

        public KumpulanPerkhidmatanService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<KumpulanPerkhidmatanService> logger)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<KumpulanPerkhidmatanDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all KumpulanPerkhidmatanDto using Entity Framework");
            var result = await _unitOfWork.Repository<PDOKumpulanPerkhidmatan>().GetAllAsync();
            result = result.ToList().Where(e => e.StatusAktif);
            return result.Select(MapToDto);
        }
        private KumpulanPerkhidmatanDto MapToDto(PDOKumpulanPerkhidmatan  pDOKumpulan)
        {
            return new KumpulanPerkhidmatanDto
            {
                Id = pDOKumpulan.Id,
                Nama = pDOKumpulan.Nama,
                Kod= pDOKumpulan.Kod,
                Keterangan=pDOKumpulan.Keterangan,
                ButiranKemaskini= pDOKumpulan.ButiranKemaskini
            };
        }

        public async Task<IEnumerable<CarlKumpulanPerkhidmatanDto>> GetKumpulanPerkhidmatanAsync(KumpulanPerkhidmatanFilterDto filter)
        {

            try
            {
                _logger.LogInformation("Getting all KumpulanPerkhidmatanDto using EF Core join");

                var query = from a in _dbContext.PDOKumpulanPerkhidmatan
                            join b in _dbContext.PDOStatusPermohonanKumpulanPerkhidmatan on a.Id equals b.IdKumpulanPerkhidmatan
                            join b2 in _dbContext.PDORujStatusPermohonan on b.KodRujStatusPermohonan equals b2.Kod
                            select new { a, b2, b };

                // Apply filters
                if (!string.IsNullOrWhiteSpace(filter.Kod))
                    query = query.Where(q => q.a.Kod.Contains(filter.Kod.Trim()));

                if (!string.IsNullOrWhiteSpace(filter.Nama))
                    query = query.Where(q => q.a.Nama.Contains(filter.Nama));

                if (filter.StatusKumpulan.HasValue)
                    query = query.Where(q => Convert.ToInt32(q.a.StatusAktif) == filter.StatusKumpulan.Value);

                if (!string.IsNullOrWhiteSpace(filter.StatusPermohonan))
                    query = query.Where(q => q.b2.Kod == filter.StatusPermohonan);

                var data = await query.OrderBy(q => q.a.Kod).ToListAsync();



                var result = data
                    .Select((q, index) => new CarlKumpulanPerkhidmatanDto
                    {
                        Bil = index + 1,
                        Kod = q.a.Kod?.Trim(),
                        Nama = q.a.Nama,
                        Keterangan = q.a.Keterangan,
                        StatusKumpulanPerkhidmatan = (q.a.StatusAktif
                            ? StatusKumpulanPerkhidmatanEnum.Aktif
                            : StatusKumpulanPerkhidmatanEnum.TidakAktif).ToDisplayString(),
                        StatusPermohonan = q.b2.Nama,
                        TarikhKemaskini = q.b.TarikhKemaskini
                    })
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting all KumpulanPerkhidmatanDto using EF Core join");
                throw;
            }
        }

        public async Task<bool> CreateAsync(KumpulanPerkhidmatanDto perkhidmatanDto)
        {
            _logger.LogInformation("Service: Creating new KumpulanPerkhidmatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Step 1: Insert into PDO_KumpulanPerkhidmatan
                var perkhidmatan = MapToEntity(perkhidmatanDto);
                perkhidmatan.StatusAktif = false;

                perkhidmatan = await _unitOfWork.Repository<PDOKumpulanPerkhidmatan>().AddAsync(perkhidmatan);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                // Step 2: Insert into PDO_StatusPermohonanKumpulanPerkhidmatan
                var statusEntity = new PDOStatusPermohonanKumpulanPerkhidmatan
                {
                    IdKumpulanPerkhidmatan = perkhidmatan.Id, // use the ID from step 1
                    KodRujStatusPermohonan = "01",
                    TarikhKemaskini = DateTime.Now,
                    StatusAktif = true
                };
                await _unitOfWork.Repository<PDOStatusPermohonanKumpulanPerkhidmatan>().AddAsync(statusEntity);
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
        public async Task<KumpulanPerkhidmatanDetailDto?> GetKumpulanPerkhidmatanByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Getting KumpulanPerkhidmatan by ID {Id} using Entity Framework", id);
                var data = await (
                  from a in _dbContext.PDOKumpulanPerkhidmatan
                  join b in _dbContext.PDOStatusPermohonanKumpulanPerkhidmatan
                      on a.Id equals b.IdKumpulanPerkhidmatan
                  join b2 in _dbContext.PDORujStatusPermohonan
                      on b.KodRujStatusPermohonan equals b2.Kod
                  where b.StatusAktif == true && a.Id == id
                  select new KumpulanPerkhidmatanDetailDto
                  {
                      Kod = a.Kod,
                      Nama = a.Nama,
                      Keterangan = a.Keterangan,
                      StatusPermohonan = b2.Nama,
                      TarikhKemaskini = b.TarikhKemaskini
                  }).FirstOrDefaultAsync();

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting KumpulanPerkhidmatan");
                throw;
            }
        }
        public async Task<bool> CheckDuplicateKodNamaAsync(KumpulanPerkhidmatanDto dto)
        {
            try
            {
                if (dto.Id == 0)
                {
                    // Create: check if Kod or Nama already exists
                    return await _dbContext.PDOKumpulanPerkhidmatan.AnyAsync(x =>
                        
                        (x.Kod.Trim() == dto.Kod.Trim() || x.Nama.Trim() == dto.Nama.Trim()));
                }
                else
                {
                    // Update: check for duplicates excluding current record
                    return await _dbContext.PDOKumpulanPerkhidmatan.AnyAsync(x =>
                       
                        (x.Kod.Trim() == dto.Kod.Trim() || x.Nama.Trim() == dto.Nama.Trim()) &&
                        x.Id != dto.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Validate KumpulanPerkhidmatan");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(KumpulanPerkhidmatanDto perkhidmatanDto)
        {
            _logger.LogInformation("Service: Updating KumpulanPerkhidmatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Step 1: update into PDO_KumpulanPerkhidmatan
                var perkhidmatan = MapToEntity(perkhidmatanDto);
                perkhidmatan.StatusAktif = perkhidmatanDto.StatusAktif;

                var result = await _unitOfWork.Repository<PDOKumpulanPerkhidmatan>().UpdateAsync(perkhidmatan);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                

                // Step 2: Deactivate existing PDO_StatusPermohonanKumpulanPerkhidmatan record
                var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanKumpulanPerkhidmatan>()
                        .FirstOrDefaultAsync(x => x.IdKumpulanPerkhidmatan == perkhidmatan.Id && x.StatusAktif);

                if (existingStatus != null)
                {
                    existingStatus.StatusAktif = false;
                    existingStatus.TarikhPinda = DateTime.Now;
                    await _unitOfWork.SaveChangesAsync();
                }


                // Step 3: Insert into PDO_StatusPermohonanKumpulanPerkhidmatan
              var  statusEntity = new PDOStatusPermohonanKumpulanPerkhidmatan
                {
                    IdKumpulanPerkhidmatan = perkhidmatan.Id, // use the ID from step 1
                    KodRujStatusPermohonan = "01",
                    TarikhKemaskini = DateTime.Now,
                    StatusAktif = true
                };
                await _unitOfWork.Repository<PDOStatusPermohonanKumpulanPerkhidmatan>().AddAsync(statusEntity);
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

        public async Task<IEnumerable<CarlStatusKumpulanPerkhidmatanDto>> GetStatusKumpulanPerkhidmatan(KumpulanPerkhidmatanFilterDto filter)
        {

            try
            {
                _logger.LogInformation("Getting all StatusKumpulanPerkhidmatan using EF Core join");

                var query = from a in _dbContext.PDOKumpulanPerkhidmatan
                            join b in _dbContext.PDOStatusPermohonanKumpulanPerkhidmatan
                                on a.Id equals b.IdKumpulanPerkhidmatan
                            join b2 in _dbContext.PDORujStatusPermohonan
                                on b.KodRujStatusPermohonan equals b2.Kod
                            where b.StatusAktif == true
                            select new
                            {
                                a.Id,
                                a.Kod,
                                a.Nama,
                                a.Keterangan,
                                b.KodRujStatusPermohonan,
                                StatusPermohonan = b2.Nama,
                                b.TarikhKemaskini
                            };

                // Apply filters only if values are provided
                if (!string.IsNullOrEmpty(filter.Kod))
                {
                    query = query.Where(x => x.Kod.Contains(filter.Kod.Trim()));
                }

                if (!string.IsNullOrEmpty(filter.Nama))
                {
                    query = query.Where(x => x.Nama.Contains(filter.Nama));
                }

                if (!string.IsNullOrEmpty(filter.StatusPermohonan))
                {
                    query = query.Where(x => x.StatusPermohonan.Contains(filter.StatusPermohonan));
                }

                var resultList = await query
                    .OrderBy(x => x.Kod)
                    .ToListAsync();

                // Add row number ("Bil.") and map to DTO
                var finalList = resultList.Select((x, index) => new CarlStatusKumpulanPerkhidmatanDto
                {
                    Bil = index + 1,
                    Id = x.Id,
                    Kod = x.Kod?.Trim(),
                    Nama = x.Nama,
                    Keterangan = x.Keterangan,
                    KodRujStatusPermohonan = x.KodRujStatusPermohonan,
                    StatusPermohonan = x.StatusPermohonan,
                    TarikhKemaskini = x.TarikhKemaskini
                }).ToList();

                return finalList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting all KumpulanPerkhidmatanDto using EF Core join");
                throw;
            }
        }

        private PDOKumpulanPerkhidmatan MapToEntity(KumpulanPerkhidmatanDto dto)
        {
            return new PDOKumpulanPerkhidmatan
            {
                Id = dto.Id,
                Kod=dto.Kod,
                Nama=dto.Nama,
                Keterangan= dto.Keterangan,
                ButiranKemaskini= dto.ButiranKemaskini
            };
        }
    }
}


