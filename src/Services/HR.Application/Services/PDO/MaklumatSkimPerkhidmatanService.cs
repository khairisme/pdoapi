using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Core.Entities.PDO;
using HR.Core.Enums;
using HR.Core.Interfaces;
using HR.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Services.PDO
{
    public class MaklumatSkimPerkhidmatanService: IMaklumatSkimPerkhidmatanService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _dbContext;
        private readonly ILogger<MaklumatSkimPerkhidmatanService> _logger;

        public MaklumatSkimPerkhidmatanService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<MaklumatSkimPerkhidmatanService> logger)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<MaklumatSkimPerkhidmatanSearchResponseDto>> GetSenaraiSkimPerkhidmatan(MaklumatSkimPerkhidmatanFilterDto filter)
        {
            try
            {
                _logger.LogInformation("Getting all MaklumatSkimPerkhidmatanDto using EF Core join");

                var query = await (from a in _dbContext.PDOSkimPerkhidmatan
                                   join a2 in _dbContext.PDORujStatusSkim
                                       on a.KodRujStatusSkim equals a2.Kod
                                   join b in _dbContext.PDOStatusPermohonanSkimPerkhidmatan
                                       on a.Id equals b.IdSkimPerkhidmatan
                                   join b2 in _dbContext.PDORujStatusPermohonan
                                       on b.KodRujStatusPermohonan equals b2.Kod
                                   where b.StatusAktif == true
                                   orderby a.Kod
                                   select new { a, a2, b, b2 }).ToListAsync();

                var result = query
                    .Select((q, index) =>
                    {
                        PDOSkimPerkhidmatan? jsonObj = null;
                        if (!string.IsNullOrWhiteSpace(q.a.ButiranKemaskini))
                        {
                            try
                            {
                                jsonObj = JsonConvert.DeserializeObject<PDOSkimPerkhidmatan>(q.a.ButiranKemaskini);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogWarning($"JSON Deserialization failed for SkimPerkhidmatan Id {q.a.Id}: {ex.Message}");
                            }
                        }

                        var dtoSource = jsonObj ?? q.a;

                        return new MaklumatSkimPerkhidmatanSearchResponseDto
                        {
                            Bil = index + 1,
                            Id = dtoSource.Id,
                            Kod = dtoSource.Kod,
                            Nama = dtoSource.Nama,
                            Keterangan = dtoSource.Keterangan,
                            StatusSkimPerkhidmatan = q.a2.Nama,
                            StatusPermohonan = q.b2.Nama,
                            TarikhKemaskini = q.b.TarikhKemasKini,
                            IndikatorSkim = dtoSource.IndikatorSkim,
                            KodRujMatawang = dtoSource.KodRujMatawang,
                            Jumlah = dtoSource.Jumlah,
                            IdKlasifikasiPerkhidmatan = dtoSource.IdKlasifikasiPerkhidmatan,
                            IdKumpulanPerkhidmatan = dtoSource.IdKumpulanPerkhidmatan
                        };
                    });

                // Apply filters
                if (filter.MaklumatKlasifikasiPerkhidmatanId.HasValue)
                    result = result.Where(q => q.IdKlasifikasiPerkhidmatan == filter.MaklumatKlasifikasiPerkhidmatanId);

                if (filter.MaklumatKumpulanPerkhidmatanId.HasValue)
                    result = result.Where(x => x.IdKumpulanPerkhidmatan == filter.MaklumatKumpulanPerkhidmatanId);

                if (!string.IsNullOrWhiteSpace(filter.Kod))
                    result = result.Where(q => q.Kod.Contains(filter.Kod));

                if (!string.IsNullOrWhiteSpace(filter.Nama))
                    result = result.Where(q => q.Nama.Contains(filter.Nama));

                if (!string.IsNullOrWhiteSpace(filter.StatusPermohonan))
                    result = result.Where(q => q.StatusPermohonan == filter.StatusPermohonan);

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve MaklumatSkimPerkhidmatan list.");
                throw new Exception("Failed to retrieve data");
            }
        }

        public async Task<bool> CreateAsync(MaklumatSkimPerkhidmatanCreateRequestDto dto)
        {
            _logger.LogInformation("Service: Creating new MaklumatSkimPerkhidmatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var userId = Guid.Empty;
                var perkhidmatan = new PDOSkimPerkhidmatan
                {
                    IdKlasifikasiPerkhidmatan = dto.IdKlasifikasiPerkhidmatan,
                    IdKumpulanPerkhidmatan = dto.IdKumpulanPerkhidmatan,
                    Kod = await GenerateKodAsync(dto),
                    Nama = dto.Nama,
                    Keterangan = dto.Keterangan,
                    IndikatorSkimKritikal = dto.IndikatorSkimKritikal,
                    IndikatorKenaikanPGT = dto.IndikatorKenaikanPGT,
                    KodRujStatusSkim = "01",
                    IndikatorSkim = dto.IndikatorSkim,
                    KodRujMatawang = dto.KodRujMatawang,
                    Jumlah = dto.Jumlah,
                    ButiranKemaskini = dto.ButiranKemaskini

                };

                perkhidmatan=await _unitOfWork.Repository<PDOSkimPerkhidmatan>().AddAsync(perkhidmatan);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                // Step 2: Insert PDO_StatusPermohonanSkimPerkhidmatan
                var statusPermohonan = new PDOStatusPermohonanSkimPerkhidmatan
                {
                    IdSkimPerkhidmatan = perkhidmatan.Id,
                    KodRujStatusPermohonan = "01",
                    TarikhKemasKini = DateTime.Now,
                    StatusAktif = true
                };

                await _unitOfWork.Repository<PDOStatusPermohonanSkimPerkhidmatan>().AddAsync(statusPermohonan);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();
                // Step 3: Insert PDO_GredSkimPerkhidmatan
                var gredLink = new PDOGredSkimPerkhidmatan
                {
                    IdGred = dto.IdGred,
                    IdSkimPerkhidmatan = perkhidmatan.Id
                };
                await _unitOfWork.Repository<PDOGredSkimPerkhidmatan>().AddAsync(gredLink);
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
        public async Task<bool> CheckDuplicateKodNamaAsync(MaklumatSkimPerkhidmatanCreateRequestDto dto)
        {
            try
            {
                if (dto.IdGred == 0)
                {
                    // Create: check if Kod or Nama already exists
                    return await _dbContext.PDOSkimPerkhidmatan.AnyAsync(x =>

                        x.Nama.Trim() == dto.Nama.Trim());
                }
                else
                {
                    // Update: check for duplicates excluding current record
                    return await _dbContext.PDOSkimPerkhidmatan.AnyAsync(x =>

                        (x.Kod.Trim() == dto.Kod.Trim() || x.Nama.Trim() == dto.Nama.Trim()) &&
                        x.Id != dto.IdGred);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Validate KumpulanPerkhidmatan");
                throw;
            }
        }

        public async Task<MaklumatSkimPerkhidmatanResponseDto?> GetSenaraiSkimPerkhidmatanByIdAsync(string Kod)
        {
            try
            {
                _logger.LogInformation("Getting SenaraiSkimPerkhidmatan by ID {Id} using Entity Framework", Kod);

                var query = await (
                    from a in _dbContext.PDOSkimPerkhidmatan
                    join b in _dbContext.PDOKlasifikasiPerkhidmatan
                        on a.IdKlasifikasiPerkhidmatan equals b.Id
                    join c in _dbContext.PDOKumpulanPerkhidmatan
                        on a.IdKumpulanPerkhidmatan equals c.Id
                    where a.Kod == Kod && b.StatusAktif && c.StatusAktif
                    select new { a, b, c }
                ).FirstOrDefaultAsync();

                if (query == null)
                    return null;

                PDOSkimPerkhidmatan? skimObj = null;
                if (!string.IsNullOrWhiteSpace(query.a.ButiranKemaskini))
                {
                    skimObj = JsonConvert.DeserializeObject<PDOSkimPerkhidmatan>(query.a.ButiranKemaskini);
                }

                var dtoSource = skimObj ?? query.a;

                var result = new MaklumatSkimPerkhidmatanResponseDto
                {
                    Id = dtoSource.Id,
                    Kod = dtoSource.Kod,
                    Nama = dtoSource.Nama,
                    Keterangan = dtoSource.Keterangan,
                    KodKlasifikasiPerkhidmatan = query.b.Kod,
                    KlasifikasiPerkhidmatan = query.b.Nama,
                    KodKumpulanPerkhidmatan = query.c.Kod,
                    KumpulanPerkhidmatan = query.c.Nama,
                    IndikatorSkim = dtoSource.IndikatorSkim,
                    KodRujMatawang = dtoSource.KodRujMatawang,
                    Jumlah = dtoSource.Jumlah
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting MaklumatSkimPerkhidmatan");
                throw;
            }
        }


        public async Task<bool> UpdateAsync(MaklumatSkimPerkhidmatanCreateRequestDto dto)
        {
            _logger.LogInformation("Service: Updating MaklumatSkimPerkhidmatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Step 1: Fetch existing record
                var existingSkim = await _dbContext.PDOSkimPerkhidmatan
                    .Where(x => x.Id == dto.Id && x.Kod == dto.Kod)
                    .FirstOrDefaultAsync();

                if (existingSkim == null)
                {
                    _logger.LogError("MaklumatSkimPerkhidmatan with ID {Id} not found", dto.Id);
                    return false;
                }

                if (!existingSkim.StatusAktif)
                {
                    // Direct update
                    var updatedSkim = MapToEntity(dto);
                    updatedSkim.StatusAktif = dto.StatusAktif;

                    await _unitOfWork.Repository<PDOSkimPerkhidmatan>().UpdateAsync(updatedSkim);
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    // Serialize the update details in ButiranKemaskini
                    var detailsForLog = MapToEntity(dto);
                    detailsForLog.StatusAktif = dto.StatusAktif;
                    existingSkim.ButiranKemaskini = JsonConvert.SerializeObject(detailsForLog);

                    await _unitOfWork.Repository<PDOSkimPerkhidmatan>().UpdateAsync(existingSkim);
                    await _unitOfWork.SaveChangesAsync();

                    // Step 2: Deactivate existing status
                    var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanSkimPerkhidmatan>()
                        .FirstOrDefaultAsync(x => x.IdSkimPerkhidmatan == existingSkim.Id && x.StatusAktif);

                    if (existingStatus != null)
                    {
                        existingStatus.StatusAktif = false;
                        existingStatus.TarikhPinda = DateTime.Now;
                        await _unitOfWork.SaveChangesAsync();
                    }

                    // Step 3: Insert new status
                    var statusEntity = new PDOStatusPermohonanSkimPerkhidmatan
                    {
                        IdSkimPerkhidmatan = existingSkim.Id,
                        KodRujStatusPermohonan = "01",
                        TarikhKemasKini = DateTime.Now,
                        StatusAktif = true
                    };
                    await _unitOfWork.Repository<PDOStatusPermohonanSkimPerkhidmatan>().AddAsync(statusEntity);
                    await _unitOfWork.SaveChangesAsync();
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service UpdateAsync");
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }

        private PDOSkimPerkhidmatan MapToEntity(MaklumatSkimPerkhidmatanCreateRequestDto dto)
        {
            return new PDOSkimPerkhidmatan
            {
               Id=dto.Id,
                Kod = dto.Kod,
                Nama = dto.Nama,
                Keterangan = dto.Keterangan,
                IdKlasifikasiPerkhidmatan = dto.IdKlasifikasiPerkhidmatan,
                IdKumpulanPerkhidmatan=dto.IdKumpulanPerkhidmatan,
                IndikatorSkim = dto.IndikatorSkim,
                KodRujMatawang = dto.KodRujMatawang,
                Jumlah = dto.Jumlah,
                ButiranKemaskini=dto.ButiranKemaskini

            };
        }
        public async Task<IEnumerable<SkimPerkhidmatanDto>> GetActiveSkimPerkhidmatan(SkimPerkhidmatanFilterDto filter)
        {
            var query = from a in _dbContext.PDOSkimPerkhidmatan
                        join b in _dbContext.PDOStatusPermohonanSkimPerkhidmatan
                            on a.Id equals b.IdSkimPerkhidmatan
                        join b2 in _dbContext.PDORujStatusPermohonan
                            on b.KodRujStatusPermohonan equals b2.Kod
                        where b.StatusAktif == true
                        
                        select new
                        {
                            a.Id,
                            a.Kod,
                            a.Nama,
                            a.Keterangan,
                            StatusSkim = b.StatusAktif == true ? "Aktif" : "Tidak Aktif",
                            StatusPermohonan = b2.Nama,
                            b.TarikhKemasKini,
                            b.KodRujStatusPermohonan,
                            a.IndikatorSkim,
                            a.KodRujMatawang,
                            a.Jumlah
                        };

            if (!string.IsNullOrEmpty(filter.Kod))
                query = query.Where(x => x.Kod.Contains(filter.Kod));

            if (!string.IsNullOrEmpty(filter.Nama))
                query = query.Where(x => x.Nama.Contains(filter.Nama));

            if (!string.IsNullOrEmpty(filter.KodRujStatusPermohonan))
                query = query.Where(x => x.KodRujStatusPermohonan == filter.KodRujStatusPermohonan);

            var list = query.OrderBy(x => x.Kod).ToList();

            return list.AsEnumerable().Select((x, index) => new SkimPerkhidmatanDto
            {
                Bil = index + 1,
                Id=x.Id,
                Kod = x.Kod,
                Nama = x.Nama,
                Keterangan = x.Keterangan,
                StatusSkimPerkhidmatan = x.StatusSkim,
                StatusPermohonan = x.StatusPermohonan,
                TarikhKemaskini = x.TarikhKemasKini,
                IndikatorSkim = x.IndikatorSkim,
                KodRujMatawang = x.KodRujMatawang,
                Jumlah = x.Jumlah
            }).ToList();
        }
        public async Task<bool> DaftarHantarSkimPerkhidmatanAsync(MaklumatSkimPerkhidmatanCreateRequestDto dto)
        {
            _logger.LogInformation("Service: Hantar  SkimPerkhidmatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var idPerkhidmatan = await _dbContext.PDOSkimPerkhidmatan
                .Where(x => x.Kod == dto.Kod)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

                if (idPerkhidmatan == 0)
                {
                    var userId = Guid.Empty;
                    var perkhidmatan = new PDOSkimPerkhidmatan
                    {
                        IdKlasifikasiPerkhidmatan = dto.IdKlasifikasiPerkhidmatan,
                        IdKumpulanPerkhidmatan = dto.IdKumpulanPerkhidmatan,
                        Kod = await GenerateKodAsync(dto),
                        Nama = dto.Nama,
                        Keterangan = dto.Keterangan,
                        IndikatorSkimKritikal = dto.IndikatorSkimKritikal,
                        IndikatorKenaikanPGT = dto.IndikatorKenaikanPGT,
                        KodRujStatusSkim = "01",
                        IndikatorSkim = dto.IndikatorSkim,
                        KodRujMatawang = dto.KodRujMatawang,
                        Jumlah = dto.Jumlah,
                        ButiranKemaskini=dto.ButiranKemaskini

                    };

                    perkhidmatan = await _unitOfWork.Repository<PDOSkimPerkhidmatan>().AddAsync(perkhidmatan);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();

                    // Step 2: Insert PDO_StatusPermohonanSkimPerkhidmatan
                    var statusPermohonan = new PDOStatusPermohonanSkimPerkhidmatan
                    {
                        IdSkimPerkhidmatan = perkhidmatan.Id,
                        KodRujStatusPermohonan = "02",
                        TarikhKemasKini = DateTime.Now,
                        StatusAktif = true
                    };

                    await _unitOfWork.Repository<PDOStatusPermohonanSkimPerkhidmatan>().AddAsync(statusPermohonan);
                    await _unitOfWork.SaveChangesAsync();

                    await _unitOfWork.CommitAsync();
                    // Step 3: Insert PDO_GredSkimPerkhidmatan
                    var gredLink = new PDOGredSkimPerkhidmatan
                    {
                        IdGred = dto.IdGred,
                        IdSkimPerkhidmatan = perkhidmatan.Id
                    };
                    await _unitOfWork.Repository<PDOGredSkimPerkhidmatan>().AddAsync(gredLink);
                    await _unitOfWork.SaveChangesAsync();

                    await _unitOfWork.CommitAsync();

                }
                else
                {

                    // Step 1: Update existing active records

                    var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanSkimPerkhidmatan>()
                           .FirstOrDefaultAsync(x => x.IdSkimPerkhidmatan == idPerkhidmatan && x.StatusAktif);

                    if (existingStatus != null)
                    {
                        existingStatus.StatusAktif = false;
                        existingStatus.TarikhPinda = DateTime.Now;
                        await _unitOfWork.SaveChangesAsync();
                    }

                    // Step 2: Insert new record
                    var newRecord = new PDOStatusPermohonanSkimPerkhidmatan
                    {
                        IdSkimPerkhidmatan = idPerkhidmatan,
                        KodRujStatusPermohonan = "02",
                        TarikhKemasKini = DateTime.Now,
                        StatusAktif = true
                    };

                    await _unitOfWork.Repository<PDOStatusPermohonanSkimPerkhidmatan>().AddAsync(newRecord);
                    await _unitOfWork.SaveChangesAsync();
                }

                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service Daftar HantarKumpulanPermohonanAsync");
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }


        public async Task<bool> UpdateHantarSkimPerkhidmatanAsync(MaklumatSkimPerkhidmatanCreateRequestDto perkhidmatanDto)
        {
            _logger.LogInformation("Service: Updating Hantar SkimPerkhidmatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var skimPerkhidmatan = await _dbContext.PDOSkimPerkhidmatan
                    .Where(x => x.Id == perkhidmatanDto.Id && x.Kod == perkhidmatanDto.Kod)
                    .FirstOrDefaultAsync();

                if (skimPerkhidmatan == null)
                {
                    _logger.LogError("SkimPerkhidmatan with ID {Id} not found", perkhidmatanDto.Id);
                    return false;
                }

                if (!skimPerkhidmatan.StatusAktif)
                {
                    // Direct update if not active
                    skimPerkhidmatan = MapToEntity(perkhidmatanDto);
                    skimPerkhidmatan.StatusAktif = perkhidmatanDto.StatusAktif;

                    await _unitOfWork.Repository<PDOSkimPerkhidmatan>().UpdateAsync(skimPerkhidmatan);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    // Serialize updated entity in ButiranKemaskini
                    var butiranKemaskiniSkim = MapToEntity(perkhidmatanDto);
                    butiranKemaskiniSkim.StatusAktif = perkhidmatanDto.StatusAktif;

                    skimPerkhidmatan.ButiranKemaskini = JsonConvert.SerializeObject(butiranKemaskiniSkim);

                    await _unitOfWork.Repository<PDOSkimPerkhidmatan>().UpdateAsync(skimPerkhidmatan);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();
                }

                // Step 2: Deactivate existing status
                var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanSkimPerkhidmatan>()
                    .FirstOrDefaultAsync(x => x.IdSkimPerkhidmatan == skimPerkhidmatan.Id && x.StatusAktif);

                if (existingStatus != null)
                {
                    existingStatus.StatusAktif = false;
                    existingStatus.TarikhPinda = DateTime.Now;
                    await _unitOfWork.SaveChangesAsync();
                }

                // Step 3: Insert new status
                var statusEntity = new PDOStatusPermohonanSkimPerkhidmatan
                {
                    IdSkimPerkhidmatan = skimPerkhidmatan.Id,
                    KodRujStatusPermohonan = "02",
                    TarikhKemasKini = DateTime.Now,
                    StatusAktif = true
                };

                await _unitOfWork.Repository<PDOStatusPermohonanSkimPerkhidmatan>().AddAsync(statusEntity);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service Updating Hantar");
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }


        public async Task<string> GenerateKodAsync(MaklumatSkimPerkhidmatanCreateRequestDto dto)
        {
            // Get max Id from the table
            var maxId = await _dbContext.PDOSkimPerkhidmatan
                                      .MaxAsync(x => (int?)x.Id) ?? 0;

            // Increment ID
            int newId = maxId + 1;

            // Pad the number (e.g., 2 becomes "02")
            string formattedId = newId.ToString("D2");

            // Combine all to return final code
            string finalCode = $"{dto.JenisKod}{dto.KumpulanKod}{dto.KlasifikasiKod}{formattedId}";

            return finalCode;
        }

        public async Task<List<SkimWithJawatanDto>> GetSkimWithJawatanAsync(int idSkim)
        {
            var query = from a in _dbContext.PDOSkimPerkhidmatan
                        where a.Id == idSkim
                        join b in _dbContext.PDOSkimKetuaPerkhidmatan on a.Id equals b.IdSkimPerkhidmatan into skimKetua
                        from b in skimKetua.DefaultIfEmpty()
                        join c in _dbContext.PDOKetuaPerkhidmatan on b.IdKetuaPerkhidmatan equals c.Id into ketua
                        from c in ketua.DefaultIfEmpty()
                        join d in _dbContext.PDOJawatan on c.IdJawatan equals d.Id into jawatan
                        from d in jawatan.DefaultIfEmpty()
                        select new SkimWithJawatanDto
                        {
                            Id = a.Id,
                            Kod = a.Kod,
                            Nama = a.Nama,
                            KodJawatan = d.Kod,
                            NamaJawatan = d.Nama
                        };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<CarianSkimPerkhidmatanResponseDto>> GetCarianSkimPerkhidmatan(CarianSkimPerkhidmatanFilterDto filter)
        {
            try
            {
                _logger.LogInformation("Getting CarianSkimPerkhidmatanResponseDto using EF Core query");

                var query = (from s in _dbContext.PDOSkimPerkhidmatan
                             where s.KodRujStatusSkim == "1"
                             orderby s.Kod
                             select new
                             {
                                 s.Id,
                                 s.Kod,
                                 s.Nama,
                                 s.IdKlasifikasiPerkhidmatan,
                                 s.IdKumpulanPerkhidmatan
                             });

                // Apply filters
                if (filter.KlasifikasiPerkhidmatanId.HasValue)
                    query = query.Where(q => q.IdKlasifikasiPerkhidmatan == filter.KlasifikasiPerkhidmatanId);

                if (filter.KumpulanPerkhidmatanId.HasValue)
                    query = query.Where(q => q.IdKumpulanPerkhidmatan == filter.KumpulanPerkhidmatanId);

                if (!string.IsNullOrWhiteSpace(filter.Kod))
                    query = query.Where(q => q.Kod.Contains(filter.Kod));

                if (!string.IsNullOrWhiteSpace(filter.Nama))
                    query = query.Where(q => q.Nama.Contains(filter.Nama));

                var data = await query.ToListAsync();

                var result = data.Select((q, index) => new CarianSkimPerkhidmatanResponseDto
                {
                    Bil = index + 1,
                    Id = q.Id,
                    Kod = q.Kod,
                    Nama = q.Nama
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve CarianSkimPerkhidmatan data");
                throw new Exception("Failed to retrieve CarianSkimPerkhidmatan data", ex);
            }
        }

        public async Task<IEnumerable<SkimPerkhidmatanResponseDto>> GetAllAsync()
        {
            _logger.LogInformation("GetAllAsync: Getting all SkimPerkhidmatan using LINQ");
            try
            {
                var query = from s in _dbContext.PDOSkimPerkhidmatan
                            orderby s.Kod
                            select new SkimPerkhidmatanResponseDto
                            {
                                Id = s.Id,
                                Kod = s.Kod ?? String.Empty,
                                Nama = s.Nama ?? String.Empty
                            };

                _logger.LogInformation("GetAllAsync: Executing query to fetch SkimPerkhidmatan data");
                var result = await query.ToListAsync();

                _logger.LogInformation("GetAllAsync: Successfully retrieved {Count} SkimPerkhidmatan records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllAsync: Failed to retrieve SkimPerkhidmatan data");
                throw;
            }
        }


    }
}
