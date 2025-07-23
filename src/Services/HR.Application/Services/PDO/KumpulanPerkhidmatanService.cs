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
using HR.Core.Entities.PDO;
using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using Grpc.Core;
using Newtonsoft.Json;

namespace HR.Application.Services.PDO
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
        private KumpulanPerkhidmatanDto MapToDto(PDOKumpulanPerkhidmatan pDOKumpulan)
        {
            return new KumpulanPerkhidmatanDto
            {
                Id = pDOKumpulan.Id,
                Nama = pDOKumpulan.Nama.Trim(),
                Kod = pDOKumpulan.Kod.Trim(),
                Keterangan = pDOKumpulan.Keterangan,
                ButiranKemaskini = pDOKumpulan.ButiranKemaskini,
                Ulasan = pDOKumpulan.Ulasan,
                IndikatorSkim = pDOKumpulan.IndikatorSkim,
                IndikatorTanpaSkim = pDOKumpulan.IndikatorTanpaSkim,
                KodJana = pDOKumpulan.KodJana
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

                var data = await query.Where(q => q.b.StatusAktif == true).OrderBy(q => q.a.Kod).ToListAsync();



                var result = data
                    .Select((q, index) => new CarlKumpulanPerkhidmatanDto
                    {
                        Bil = index + 1,
                        Id = q.a.Id,
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

        private string GenerateNextKODFromDb()
        {

            //     int maxId = _dbContext.PDOKumpulanPerkhidmatan
            //    .Select(c => c.Id)  // Or use your primary key name
            //     .AsEnumerable()    // <-- Force client-side evaluation
            //     .DefaultIfEmpty(0)
            //    .Max();

            //return (maxId + 1).ToString("D3"); // Formats to 3-digit KOD like "001"

            // Get the max Kod value as string, parse it to int
            var kodList = _dbContext.PDOKumpulanPerkhidmatan
        .Select(c => c.Kod)
        .ToList();

            int maxKod = kodList
                .Where(k => !string.IsNullOrEmpty(k) && k.All(char.IsDigit))
                .Select(k => int.Parse(k))
                .DefaultIfEmpty(0)
                .Max();

            return (maxKod + 1).ToString("D3");

        }
        public async Task<bool> CreateAsync(KumpulanPerkhidmatanDto perkhidmatanDto)
        {
            _logger.LogInformation("Service: Creating new KumpulanPerkhidmatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Step 1: Insert into PDO_KumpulanPerkhidmatan
                perkhidmatanDto.Kod = GenerateNextKODFromDb();
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
                _logger.LogError(ex, "Error during service CreateAsync:" + ex.InnerException.ToString());
                await _unitOfWork.RollbackAsync();
                throw;
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
                  select new { a, b, b2 }
                    ).FirstOrDefaultAsync();

                PDOKumpulanPerkhidmatan? jsonObj = null;
                if (!string.IsNullOrWhiteSpace(data.a.ButiranKemaskini))
                {
                    jsonObj = JsonConvert.DeserializeObject<PDOKumpulanPerkhidmatan>(data.a.ButiranKemaskini);
                }
                var dtoSource = jsonObj ?? data.a;
                var result = new KumpulanPerkhidmatanDetailDto
                {
                    Kod = dtoSource.Kod,
                    Nama = dtoSource.Nama,
                    Keterangan = dtoSource.Keterangan,
                    StatusPermohonan = data.b2.Nama,
                    TarikhKemaskini = data.b.TarikhKemaskini,
                    UlasanPengesah = dtoSource.UlasanPengesah,
                    Ulasan = dtoSource.Ulasan,
                    IndikatorSkim = dtoSource.IndikatorSkim,
                    IndikatorTanpaSkim = dtoSource.IndikatorTanpaSkim,
                    KodJana = dtoSource.KodJana,
                    StatusAktif = dtoSource.StatusAktif
                };
                return result;
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

                        // x.Kod.Trim() == dto.Kod.Trim() || 
                        x.Nama.Trim() == dto.Nama.Trim());
                }
                else
                {
                    // Update: check for duplicates excluding current record
                    return await _dbContext.PDOKumpulanPerkhidmatan.AnyAsync(x =>

                        (
                        //x.Kod.Trim() == dto.Kod.Trim() || 
                        x.Nama.Trim() == dto.Nama.Trim()) &&
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
                //var statusKumpulan = await _dbContext.PDOKumpulanPerkhidmatan
                //.Where(x => x.Id==perkhidmatanDto.Id &&  x.Kod == perkhidmatanDto.Kod)
                //.Select(x => x.StatusAktif)
                //.FirstOrDefaultAsync();

                var perkhidmatan = await _dbContext.PDOKumpulanPerkhidmatan
                       .Where(x => x.Id == perkhidmatanDto.Id && x.Kod == perkhidmatanDto.Kod)
                       .FirstOrDefaultAsync();
                if (perkhidmatan == null)
                {
                    _logger.LogError("KumpulanPerkhidmatan with ID {Id} not found", perkhidmatanDto.Id);
                    return false; // or throw an exception
                }

                if (!perkhidmatan.StatusAktif)
                {
                    // Step 1: update into PDO_KumpulanPerkhidmatan
                    var perkhidmatanupdate = MapToEntity(perkhidmatanDto);
                    perkhidmatanupdate.StatusAktif = perkhidmatanDto.StatusAktif;
                    perkhidmatanupdate.Ulasan = perkhidmatanDto.Ulasan;


                    var result = await _unitOfWork.Repository<PDOKumpulanPerkhidmatan>().UpdateAsync(perkhidmatanupdate);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();
                }
                else
                {

                    // Step 1: update into PDO_KumpulanPerkhidmatan
                    var ButiranKemaskiniperkhidmatan = MapToEntity(perkhidmatanDto);
                    ButiranKemaskiniperkhidmatan.StatusAktif = perkhidmatanDto.StatusAktif;
                    ButiranKemaskiniperkhidmatan.Ulasan = perkhidmatanDto.Ulasan;

                    perkhidmatan.ButiranKemaskini = JsonConvert.SerializeObject(ButiranKemaskiniperkhidmatan);


                    var result = await _unitOfWork.Repository<PDOKumpulanPerkhidmatan>().UpdateAsync(perkhidmatan);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();

                    //Step 2: Deactivate existing PDO_StatusPermohonanKumpulanPerkhidmatan record
                    var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanKumpulanPerkhidmatan>()
                            .FirstOrDefaultAsync(x => x.IdKumpulanPerkhidmatan == perkhidmatanDto.Id && x.StatusAktif);

                    if (existingStatus != null)
                    {
                        existingStatus.StatusAktif = false;
                        existingStatus.TarikhPinda = DateTime.Now;
                        await _unitOfWork.SaveChangesAsync();
                    }


                    // Step 3: Insert into PDO_StatusPermohonanKumpulanPerkhidmatan
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
                }


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
                                b.TarikhKemaskini,
                                a.Ulasan,
                                a.IndikatorTanpaSkim,
                                a.IndikatorSkim,
                                a.KodJana
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
                    TarikhKemaskini = x.TarikhKemaskini,
                    Ulasan = x.Ulasan,
                    IndikatorTanpaSkim = x.IndikatorTanpaSkim,
                    IndikatorSkim = x.IndikatorSkim,
                    KodJana = x.KodJana

                }).ToList();

                return finalList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting all KumpulanPerkhidmatanDto using EF Core join");
                throw;
            }
        }

        public async Task<KumpulanPerkhidmatanStatusDto> GetMaklumatSediaAda(int id)
        {
            _logger.LogInformation("Getting MaklumatStatus by ID {Id} using Entity Framework", id);
            try
            {

                var result = await (from a in _dbContext.PDOKumpulanPerkhidmatan
                                    join b in _dbContext.PDOStatusPermohonanKumpulanPerkhidmatan
                                        on a.Id equals b.IdKumpulanPerkhidmatan
                                    join b2 in _dbContext.PDORujStatusPermohonan
                                        on b.KodRujStatusPermohonan equals b2.Kod
                                    where b.StatusAktif == true && a.Id == id
                                    select new KumpulanPerkhidmatanStatusDto
                                    {
                                        Id = a.Id,
                                        Kod = a.Kod,
                                        Nama = a.Nama,
                                        Keterangan = a.Keterangan,
                                        StatusAktif = a.StatusAktif,
                                        KodRujStatusPermohonan = b.KodRujStatusPermohonan,
                                        StatusPermohonan = b2.Nama,
                                        TarikhKemaskini = b.TarikhKemaskini,
                                        KodJana = a.KodJana,
                                        IndikatorSkim = a.IndikatorSkim,
                                        IndikatorTanpaSkim = a.IndikatorTanpaSkim,
                                        Ulasan=a.Ulasan,
                                        UlasanPengesah=a.UlasanPengesah


                                    }).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting MaklumatStatus");
                throw;
            }
        }



        public async Task<KumpulanPerkhidmatanRefStatusDto> GetMaklumatBaharuAsync(int id)
        {
            _logger.LogInformation("Getting ButiranKemaskini by ID {Id} using Entity Framework", id);
            try
            {
                var result = await (from a in _dbContext.PDOKumpulanPerkhidmatan
                                    join b in _dbContext.PDOStatusPermohonanKumpulanPerkhidmatan
                                        on a.Id equals b.IdKumpulanPerkhidmatan
                                    join b2 in _dbContext.PDORujStatusPermohonan
                                        on b.KodRujStatusPermohonan equals b2.Kod
                                    where b.StatusAktif == true && a.Id == id
                                    select new KumpulanPerkhidmatanButiranDto
                                    {
                                        //Id = a.Id,
                                        //Kod = a.Kod,
                                        ButiranKemaskini = a.ButiranKemaskini,
                                        //KodRujStatusPermohonan = b.KodRujStatusPermohonan,
                                        //StatusPermohonan = b2.Nama,
                                        //TarikhKemaskini = b.TarikhKemaskini
                                    }).FirstOrDefaultAsync();

                if (String.IsNullOrEmpty(result.ButiranKemaskini))
                {
                    return new KumpulanPerkhidmatanRefStatusDto
                    {
                        Keterangan = "Tiada butiran kemaskini"
                    };
                }

                KumpulanPerkhidmatanRefStatusDto obj = JsonConvert.DeserializeObject<KumpulanPerkhidmatanRefStatusDto>(result.ButiranKemaskini);
                obj.KodRujStatusPermohonan = string.Empty;


                return obj;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting ButiranKemaskini");
                throw;
            }
        }

        public async Task<bool> KemaskiniStatusAsync(KumpulanPerkhidmatanRefStatusDto perkhidmatanDto)
        {
            _logger.LogInformation("Service: Updating KemaskiniStatusAsync");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Step 1: update into PDO_KumpulanPerkhidmatan
                var perkhidmatan = MapToEntity(perkhidmatanDto);
                perkhidmatan.StatusAktif = perkhidmatanDto.StatusAktif;
                perkhidmatan.UlasanPengesah = perkhidmatanDto.UlasanPengesah;
                perkhidmatan.Ulasan = perkhidmatanDto.Ulasan;
                //if (!string.IsNullOrWhiteSpace(perkhidmatan.ButiranKemaskini))
                //{
                //    perkhidmatan.ButiranKemaskini = null;
                //}

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
                var statusEntity = new PDOStatusPermohonanKumpulanPerkhidmatan
                {
                    IdKumpulanPerkhidmatan = perkhidmatan.Id, // use the ID from step 1
                    KodRujStatusPermohonan = perkhidmatanDto.KodRujStatusPermohonan,
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
                throw;
            }
        }
        public async Task<bool> DaftarHantarKumpulanPermohonanAsync(KumpulanPerkhidmatanDto dto)
        {
            _logger.LogInformation("Service: Hantar  KumpulanPermohonan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var idKumpulan = await _dbContext.PDOKumpulanPerkhidmatan
                .Where(x => x.Kod == dto.Kod)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

                if (idKumpulan == 0)
                {
                    // Step 1: Insert into PDO_KumpulanPerkhidmatan
                    dto.Kod = GenerateNextKODFromDb();
                    var perkhidmatan = MapToEntity(dto);
                    perkhidmatan.StatusAktif = false;
                    perkhidmatan = await _unitOfWork.Repository<PDOKumpulanPerkhidmatan>().AddAsync(perkhidmatan);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();

                    // Step 2: Insert into PDO_StatusPermohonanKumpulanPerkhidmatan
                    var statusEntity = new PDOStatusPermohonanKumpulanPerkhidmatan
                    {
                        IdKumpulanPerkhidmatan = perkhidmatan.Id, // use the ID from step 1
                        KodRujStatusPermohonan = "02",
                        TarikhKemaskini = DateTime.Now,
                        StatusAktif = true
                    };
                    await _unitOfWork.Repository<PDOStatusPermohonanKumpulanPerkhidmatan>().AddAsync(statusEntity);
                    await _unitOfWork.SaveChangesAsync();

                    await _unitOfWork.CommitAsync();

                }
                else
                {

                    // Step 1: Update existing active records

                    var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanKumpulanPerkhidmatan>()
                           .FirstOrDefaultAsync(x => x.IdKumpulanPerkhidmatan == idKumpulan && x.StatusAktif);

                    if (existingStatus != null)
                    {
                        existingStatus.StatusAktif = false;
                        existingStatus.TarikhPinda = DateTime.Now;
                        await _unitOfWork.SaveChangesAsync();
                    }

                    // Step 2: Insert new record
                    var newRecord = new PDOStatusPermohonanKumpulanPerkhidmatan
                    {
                        IdKumpulanPerkhidmatan = idKumpulan,
                        KodRujStatusPermohonan = "02",
                        TarikhKemaskini = DateTime.Now,
                        StatusAktif = true
                    };

                    await _unitOfWork.Repository<PDOStatusPermohonanKumpulanPerkhidmatan>().AddAsync(newRecord);
                    await _unitOfWork.SaveChangesAsync();
                }

                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service Daftar HantarKumpulanPermohonanAsync");
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdateHantarKumpulanPermohonanAsync(KumpulanPerkhidmatanDto perkhidmatanDto)
        {
            _logger.LogInformation("Service: Updating Hantar KumpulanPerkhidmatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var perkhidmatan = await _dbContext.PDOKumpulanPerkhidmatan
                      .Where(x => x.Id == perkhidmatanDto.Id && x.Kod == perkhidmatanDto.Kod)
                      .FirstOrDefaultAsync();
                if (perkhidmatan == null)
                {
                    _logger.LogError("KumpulanPerkhidmatan with ID {Id} not found", perkhidmatanDto.Id);
                    return false; // or throw an exception
                }

                if (!perkhidmatan.StatusAktif)
                {

                    // Step 1: update into PDO_KumpulanPerkhidmatan
                    perkhidmatan = MapToEntity(perkhidmatanDto);
                    perkhidmatan.StatusAktif = perkhidmatanDto.StatusAktif;
                    perkhidmatan.Ulasan = perkhidmatanDto.Ulasan;


                    //perkhidmatan.ButiranKemaskini = JsonConvert.SerializeObject(ButiranKemaskiniperkhidmatan);


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
                    var statusEntity = new PDOStatusPermohonanKumpulanPerkhidmatan
                    {
                        IdKumpulanPerkhidmatan = perkhidmatan.Id, // use the ID from step 1
                        KodRujStatusPermohonan = "02",
                        TarikhKemaskini = DateTime.Now,
                        StatusAktif = true
                    };
                    await _unitOfWork.Repository<PDOStatusPermohonanKumpulanPerkhidmatan>().AddAsync(statusEntity);
                    await _unitOfWork.SaveChangesAsync();

                    await _unitOfWork.CommitAsync();
                }
                else
                {

                    // Step 1: update into PDO_KumpulanPerkhidmatan
                    var ButiranKemaskiniperkhidmatan = MapToEntity(perkhidmatanDto);
                    ButiranKemaskiniperkhidmatan.StatusAktif = perkhidmatanDto.StatusAktif;
                    ButiranKemaskiniperkhidmatan.Ulasan = perkhidmatanDto.Ulasan;

                    perkhidmatan.ButiranKemaskini = JsonConvert.SerializeObject(ButiranKemaskiniperkhidmatan);


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
                    var statusEntity = new PDOStatusPermohonanKumpulanPerkhidmatan
                    {
                        IdKumpulanPerkhidmatan = perkhidmatan.Id, // use the ID from step 1
                        KodRujStatusPermohonan = "02",
                        TarikhKemaskini = DateTime.Now,
                        StatusAktif = true
                    };
                    await _unitOfWork.Repository<PDOStatusPermohonanKumpulanPerkhidmatan>().AddAsync(statusEntity);
                    await _unitOfWork.SaveChangesAsync();

                    await _unitOfWork.CommitAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service Updating Hantar ");
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
        private PDOKumpulanPerkhidmatan MapToEntity(KumpulanPerkhidmatanDto dto)
        {
            return new PDOKumpulanPerkhidmatan
            {
                Id = dto.Id,
                Kod = dto.Kod,
                Nama = dto.Nama,
                Keterangan = dto.Keterangan,
                IndikatorSkim = dto.IndikatorSkim,
                IndikatorTanpaSkim = dto.IndikatorTanpaSkim,
                ButiranKemaskini = dto.ButiranKemaskini,
                KodJana = dto.KodJana
            };
        }
        private PDOKumpulanPerkhidmatan MapToHantarEntity(KumpulanPerkhidmatanHantarDto dto)
        {
            return new PDOKumpulanPerkhidmatan
            {
                Id = dto.Id,
                Kod = dto.Kod,
                Nama = dto.Nama,
                Keterangan = dto.Keterangan,
                IndikatorSkim = dto.IndikatorSkim,
                IndikatorTanpaSkim = dto.IndikatorTanpaSkim,
                //ButiranKemaskini =JsonConvert.SerializeObject( dto.ButiranKemaskini),
                KodJana = dto.KodJana
            };
        }
        public async Task<bool> DeleteOrUpdateKumpulanPerkhidmatanAsync(int id)
        {
            _logger.LogInformation("DeleteOrUpdateKumpulanPerkhidmatanAsync by ID {Id} using Entity Framework", id);
            try
            {

                var result = await (from a in _dbContext.PDOKumpulanPerkhidmatan
                                    join b in _dbContext.PDOStatusPermohonanKumpulanPerkhidmatan
                                        on a.Id equals b.IdKumpulanPerkhidmatan
                                    join b2 in _dbContext.PDORujStatusPermohonan
                                        on b.KodRujStatusPermohonan equals b2.Kod
                                    where b.StatusAktif == true && a.Id == id
                                    select new KumpulanPerkhidmatanStatusDto
                                    {
                                        Id = a.Id,
                                        Kod = a.Kod,
                                        Nama = a.Nama,
                                        Keterangan = a.Keterangan,
                                        StatusAktif = a.StatusAktif,
                                        KodRujStatusPermohonan = b.KodRujStatusPermohonan,
                                        StatusPermohonan = b2.Nama,
                                        TarikhKemaskini = b.TarikhKemaskini
                                    }).FirstOrDefaultAsync();
                if (result == null)
                    return false;

                var kumpulanPerkhidmatan = await _unitOfWork.Repository<PDOKumpulanPerkhidmatan>().GetByIdAsync(id);
                if (kumpulanPerkhidmatan == null)
                    return false;

                if (!result.StatusAktif && result.KodRujStatusPermohonan == "01")
                {
                    using var transaction = await _dbContext.Database.BeginTransactionAsync();
                    try
                    {
                        // Delete children first
                        var statusList = await _unitOfWork.Repository<PDOStatusPermohonanKumpulanPerkhidmatan>()
                        .FindByFieldAsync("IdKumpulanPerkhidmatan", id);

                        _dbContext.PDOStatusPermohonanKumpulanPerkhidmatan.RemoveRange(statusList);
                        await _dbContext.SaveChangesAsync();
                        // Then delete parent
                        _dbContext.PDOKumpulanPerkhidmatan.Remove(kumpulanPerkhidmatan);
                        await _dbContext.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        _logger.LogError(ex, "DeleteOrUpdateKumpulanPerkhidmatanAsync failed during transaction");
                        throw;
                    }
                }
                else
                {
                    // Just set to inactive
                    kumpulanPerkhidmatan.StatusAktif = false;
                    await _unitOfWork.Repository<PDOKumpulanPerkhidmatan>().UpdateAsync(kumpulanPerkhidmatan);
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }






            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteOrUpdateKumpulanPerkhidmatanAsync");
                throw;
            }
        }
    }
}


