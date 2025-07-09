using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
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

                var query = (from a in _dbContext.PDOSkimPerkhidmatan
                             join a2 in _dbContext.PDORujStatusSkim
                                 on a.KodRujStatusSkim equals a2.Kod    
                             join b in _dbContext.PDOStatusPermohonanSkimPerkhidmatan
                                 on a.Id equals b.IdSkimPerkhidmatan
                             join b2 in _dbContext.PDORujStatusPermohonan
                                 on b.KodRujStatusPermohonan equals b2.Kod
                             where b.StatusAktif == true
                             orderby a.Kod
                             select new
                             {
                                 a.Kod,
                                 a.Nama,
                                 a.Keterangan,
                                 a.IdKlasifikasiPerkhidmatan,
                                 a.IdKumpulanPerkhidmatan,
                                 StatusSkimPerkhidmatanNama = a2.Nama,
                                 StatusPermohonanNama = b2.Nama,
                                 b.TarikhKemasKini,
                                 a.Id,
                                 a.IndikatorSkim,
                                 a.KodRujMatawang,
                                 a.Jumlah
                             })
            .AsEnumerable()
            .Select((x, index) => new
            {
                Bil = index + 1,
                x.Kod,
                x.Nama,
                x.Keterangan,
                x.IdKlasifikasiPerkhidmatan,
                x.IdKumpulanPerkhidmatan,
                StatusSkimPerkhidmatan = x.StatusSkimPerkhidmatanNama,
                StatusPermohonan = x.StatusPermohonanNama,
                x.TarikhKemasKini,
                x.Id,
                x.IndikatorSkim,
                x.KodRujMatawang,
                x.Jumlah
            });

                if (filter.MaklumatKlasifikasiPerkhidmatanId.HasValue)
                    query = query.Where(q => q.IdKlasifikasiPerkhidmatan == filter.MaklumatKlasifikasiPerkhidmatanId);
                if (filter.MaklumatKumpulanPerkhidmatanId.HasValue)
                    query = query.Where(x => x.IdKumpulanPerkhidmatan == filter.MaklumatKumpulanPerkhidmatanId);

                if (!string.IsNullOrWhiteSpace(filter.Kod))
                    query = query.Where(q => q.Kod.Contains(filter.Kod));

                if (!string.IsNullOrWhiteSpace(filter.Nama))
                    query = query.Where(q => q.Nama.Contains(filter.Nama));                

                if (!string.IsNullOrWhiteSpace(filter.StatusPermohonan))
                    query = query.Where(q => q.StatusPermohonan == filter.StatusPermohonan);

                var data = query.ToList();
                var result = data
                    .Select((q, index) => new MaklumatSkimPerkhidmatanSearchResponseDto
                    {
                        Bil = index + 1,
                        Id = q.Id,
                        Kod = q.Kod,
                        Nama = q.Nama,
                        Keterangan = q.Keterangan,
                        StatusSkimPerkhidmatan = q.StatusSkimPerkhidmatan,
                        StatusPermohonan = q.StatusPermohonan,
                        TarikhKemaskini=q.TarikhKemasKini,
                        IndikatorSkim=q.IndikatorSkim,
                        KodRujMatawang=q.KodRujMatawang,
                        Jumlah = q.Jumlah
                    })
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrive data");
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
                    KodRujStatusSkim = "",
                    IndikatorSkim = dto.IndikatorSkim,
                    KodRujMatawang = dto.KodRujMatawang,
                    Jumlah = dto.Jumlah,




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
                var result = await (from a in _dbContext.PDOSkimPerkhidmatan
                              join b in _dbContext.PDOKlasifikasiPerkhidmatan
                                  on a.IdKlasifikasiPerkhidmatan equals b.Id
                              join c in _dbContext.PDOKumpulanPerkhidmatan
                                  on a.IdKumpulanPerkhidmatan equals c.Id
                              where a.Kod == Kod && b.StatusAktif && c.StatusAktif
                              select new MaklumatSkimPerkhidmatanResponseDto
                              {
                                  Id=a.Id,
                                  Kod = a.Kod,
                                  Nama = a.Nama,
                                  Keterangan=a.Keterangan,
                                  KodKlasifikasiPerkhidmatan = b.Kod,
                                  KlasifikasiPerkhidmatan = b.Nama,
                                  KodKumpulanPerkhidmatan = c.Kod,
                                  KumpulanPerkhidmatan = c.Nama,
                                  IndikatorSkim = a.IndikatorSkim,
                                  KodRujMatawang = a.KodRujMatawang,
                                  Jumlah = a.Jumlah
                              }).FirstOrDefaultAsync();


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
            try
            {
                // Step 1: update into PDO_SkimPerkhidmatan
                var perkhidmatan = MapToEntity(dto);
                // perkhidmatan.StatusAktif = perkhidmatanDto.StatusAktif;

                var result = await _unitOfWork.Repository<PDOSkimPerkhidmatan>().UpdateAsync(perkhidmatan);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();



                // Step 2: Deactivate existing PDO_StatusPermohonanSkimPerkhidmatan record
                var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanSkimPerkhidmatan>()
                        .FirstOrDefaultAsync(x => x.IdSkimPerkhidmatan == perkhidmatan.Id && x.StatusAktif);

                if (existingStatus != null)
                {
                    existingStatus.StatusAktif = false;
                    existingStatus.TarikhPinda = DateTime.Now;
                    await _unitOfWork.SaveChangesAsync();
                }


                // Step 3: Insert into PDO_StatusPermohonanSkimPerkhidmatan
                var statusEntity = new PDOStatusPermohonanSkimPerkhidmatan
                {
                    IdSkimPerkhidmatan = perkhidmatan.Id, // use the ID from step 1
                    KodRujStatusPermohonan = "01",
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
                _logger.LogError(ex, "Error during service Updating Hantar ");
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
                Jumlah = dto.Jumlah

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
                        KodRujStatusSkim = "",
                        IndikatorSkim = dto.IndikatorSkim,
                        KodRujMatawang = dto.KodRujMatawang,
                        Jumlah = dto.Jumlah

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
                // Step 1: update into PDO_SkimPerkhidmatan
                var perkhidmatan = MapToEntity(perkhidmatanDto);
                // perkhidmatan.StatusAktif = perkhidmatanDto.StatusAktif;

                var result = await _unitOfWork.Repository<PDOSkimPerkhidmatan>().UpdateAsync(perkhidmatan);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();



                // Step 2: Deactivate existing PDO_StatusPermohonanSkimPerkhidmatan record
                var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanSkimPerkhidmatan>()
                        .FirstOrDefaultAsync(x => x.IdSkimPerkhidmatan == perkhidmatan.Id && x.StatusAktif);

                if (existingStatus != null)
                {
                    existingStatus.StatusAktif = false;
                    existingStatus.TarikhPinda = DateTime.Now;
                    await _unitOfWork.SaveChangesAsync();
                }


                // Step 3: Insert into PDO_StatusPermohonanSkimPerkhidmatan
                var statusEntity = new PDOStatusPermohonanSkimPerkhidmatan
                {
                    IdSkimPerkhidmatan = perkhidmatan.Id, // use the ID from step 1
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
                _logger.LogError(ex, "Error during service Updating Hantar ");
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

    }
}
