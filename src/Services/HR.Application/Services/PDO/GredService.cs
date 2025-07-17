using HR.Application.DTOs;
using HR.Application.DTOs.PDO;
using HR.Application.Extensions;
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

namespace HR.Application.Services
{
    public class GredService : IGredService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<GredService> _logger;
        public GredService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<GredService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }
        public async Task<List<PDOGredDto>> GetGredListAsync(GredFilterDto filter)
        {
            _logger.LogInformation("Getting all PDOGredDto using EF Core join");
            try
            {



                var query = from a in _context.PDOGred
                            join b in _context.PDOKlasifikasiPerkhidmatan on a.IdKlasifikasiPerkhidmatan equals b.Id
                            join c in _context.PDOKumpulanPerkhidmatan on a.IdKumpulanPerkhidmatan equals c.Id
                            where a.IdKlasifikasiPerkhidmatan == filter.IdKlasifikasiPerkhidmatan
                                  && a.IdKumpulanPerkhidmatan == filter.IdKumpulanPerkhidmatan
                                  && b.StatusAktif
                                  && c.StatusAktif
                            orderby a.Kod
                            select new
                            {
                                a.Id,
                                a.Kod,
                                a.Nama,
                                a.Keterangan
                            };

                if (!string.IsNullOrEmpty(filter.Nama))
                    query = query.Where(x => x.Nama.Contains(filter.Nama));
                var result = await query.ToListAsync();

                return result.Select((x, index) => new PDOGredDto
                {
                    Bil = index + 1,
                    Id=x.Id,
                    Kod = x.Kod,
                    Nama = x.Nama,
                    Keterangan = x.Keterangan
                }).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrive data");
            }
        }

        public async Task<List<GredResultDto>> GetFilteredGredList(GredFilterDto filter)
        {
            _logger.LogInformation("Search  Gred using EF Core join");
            try
            {

                var query = from a in _context.PDOGred
                            join b in _context.PDOStatusPermohonanGred on a.Id equals b.IdGred
                            join d in _context.PDOKlasifikasiPerkhidmatan on a.IdKlasifikasiPerkhidmatan equals d.Id
                            join c in _context.PDOKumpulanPerkhidmatan on a.IdKumpulanPerkhidmatan equals c.Id
                            join b2 in _context.PDORujStatusPermohonan on b.KodRujStatusPermohonan equals b2.Kod
                            where b.StatusAktif == true && c.StatusAktif == true
                            select new
                            {
                                a.Id,
                                a.Kod,
                                a.Nama,
                                a.Keterangan,
                                StatusPermohonan = b2.Nama,
                                StatusGred = a.StatusAktif == true ? "Aktif" : "Tidak Aktif",
                                a.IdKumpulanPerkhidmatan,
                                a.IdKlasifikasiPerkhidmatan,
                                b.KodRujStatusPermohonan
                            };

                if (filter.IdKumpulanPerkhidmatan.HasValue)
                    query = query.Where(x => x.IdKumpulanPerkhidmatan == filter.IdKumpulanPerkhidmatan);

                if (filter.IdKlasifikasiPerkhidmatan.HasValue)
                    query = query.Where(x => x.IdKlasifikasiPerkhidmatan == filter.IdKlasifikasiPerkhidmatan);

                if (!string.IsNullOrEmpty(filter.KodRujStatusPermohonan))
                    query = query.Where(x => x.KodRujStatusPermohonan == filter.KodRujStatusPermohonan);

                if (!string.IsNullOrEmpty(filter.Nama))
                    query = query.Where(x => x.Nama.Contains(filter.Nama));

                var data = await query.ToListAsync();

                // Add row number manually
                var result = data.Select((x, index) => new GredResultDto
                {
                    Bil = index + 1,
                    Id=x.Id,
                    Kod = x.Kod,
                    Nama = x.Nama,
                    Keterangan = x.Keterangan,
                    StatusPermohonan = x.StatusPermohonan,
                    StatusGred = x.StatusGred
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrive data");
            }
        }

        public async Task<bool> CreateAsync(CreateGredDto dto)
        {
            _logger.LogInformation("Service: Creating new Gred");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Step 1: Insert into PDO_Gred
                dto.Kod =  $"{dto.KodGred}A{dto.NomborGred}000";
                var gred = MapToEntity(dto);
                gred.StatusAktif = false;

                gred = await _unitOfWork.Repository<PDOGred>().AddAsync(gred);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                // Step 2: Insert into PDO_StatusPermohonanGred
                var statusEntity = new PDOStatusPermohonanGred
                {
                    IdGred = gred.Id, // use the ID from step 1
                    KodRujStatusPermohonan = "01", // Assuming "01" is the default status code
                    TarikhKemasKini = DateTime.Now,
                    StatusAktif = true
                };  
                
                await _unitOfWork.Repository<PDOStatusPermohonanGred>().AddAsync(statusEntity);
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

        public async Task<bool> DaftarGredJawatanAsync(CreateGredDto dto)
        {
            _logger.LogInformation("Service: Hantar  GredJawatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var idGred = await _context.PDOGred
                .Where(x => x.Kod == dto.Kod)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

                if (idGred == 0)
                {
                    // Step 1: Insert into PDO_Gred
                    dto.Kod = dto.Kod = $"{dto.KodGred}A{dto.NomborGred}000";
                    var gred = MapToEntity(dto);
                    gred.StatusAktif = false;
                    gred = await _unitOfWork.Repository<PDOGred>().AddAsync(gred);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();

                    // Step 2: Insert into PDO_StatusPermohonanGred
                    var statusEntity = new PDOStatusPermohonanGred
                    {
                        IdGred = gred.Id, // use the ID from step 1
                        KodRujStatusPermohonan = "02",
                        TarikhKemasKini = DateTime.Now,
                        StatusAktif = true
                    };
                    await _unitOfWork.Repository<PDOStatusPermohonanGred>().AddAsync(statusEntity);
                    await _unitOfWork.SaveChangesAsync();

                    await _unitOfWork.CommitAsync();

                }
                else
                {

                    // Step 1: Update existing active records

                    var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanGred>()
                           .FirstOrDefaultAsync(x => x.IdGred == idGred && x.StatusAktif);

                    if (existingStatus != null)
                    {
                        existingStatus.StatusAktif = false;
                        existingStatus.TarikhPinda = DateTime.Now;
                        await _unitOfWork.SaveChangesAsync();
                    }

                    // Step 2: Insert new record
                    var newRecord = new PDOStatusPermohonanGred
                    {
                        IdGred = idGred,
                        KodRujStatusPermohonan = "02",
                        TarikhKemasKini = DateTime.Now,
                        StatusAktif = true
                    };

                    await _unitOfWork.Repository<PDOStatusPermohonanGred>().AddAsync(newRecord);
                    await _unitOfWork.SaveChangesAsync();
                }

                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service Daftar HantarGredJawatanAsync");
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }

        public async Task<bool> UpdateAsync(CreateGredDto dto)
        {
            _logger.LogInformation("Service: Updating GredJawatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Step 1: update into PDO_Gred
                var gred = MapToEntity(dto);
                gred.StatusAktif = dto.StatusAktif;

                var result = await _unitOfWork.Repository<PDOGred>().UpdateAsync(gred);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();



                // Step 2: Deactivate existing PDO_StatusPermohonanGred record
                var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanGred>()
                        .FirstOrDefaultAsync(x => x.IdGred == dto.Id && x.StatusAktif);

                if (existingStatus != null)
                {
                    existingStatus.StatusAktif = false;
                    existingStatus.TarikhPinda = DateTime.Now;
                    await _unitOfWork.SaveChangesAsync();
                }


                // Step 3: Insert into PDO_StatusPermohonanGred
                var statusEntity = new PDOStatusPermohonanGred
                {
                    IdGred = dto.Id, // use the ID from step 1
                    KodRujStatusPermohonan = "01",
                    TarikhKemasKini = DateTime.Now,
                    StatusAktif = true
                };
                await _unitOfWork.Repository<PDOStatusPermohonanGred>().AddAsync(statusEntity);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();


                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service updateAsyc");
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> UpdateHantarGredJawatanAsync(CreateGredDto dto)
        {
            _logger.LogInformation("Service: Updating Hantar GredJawatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Step 1: update into PDO_Gred
                var gred = MapToEntity(dto);
              
                var result = await _unitOfWork.Repository<PDOGred>().UpdateAsync(gred);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();



                // Step 2: Deactivate existing PDO_StatusPermohonanGred record
                var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanGred>()
                        .FirstOrDefaultAsync(x => x.IdGred == dto.Id && x.StatusAktif);

                if (existingStatus != null)
                {
                    existingStatus.StatusAktif = false;
                    existingStatus.TarikhPinda = DateTime.Now;
                    await _unitOfWork.SaveChangesAsync();
                }


                // Step 3: Insert into PDO_StatusPermohonanKumpulanPerkhidmatan
                var statusEntity = new PDOStatusPermohonanGred
                {
                    IdGred = dto.Id, // use the ID from step 1
                    KodRujStatusPermohonan = "02",
                    TarikhKemasKini = DateTime.Now,
                    StatusAktif = true
                };
                await _unitOfWork.Repository<PDOStatusPermohonanGred>().AddAsync(statusEntity);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();


                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service Updating Hantar ");
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<PaparMaklumatGredDto> GetMaklumatGred(int id)
        {
            _logger.LogInformation("Getting MaklumatGred by ID {Id} using Entity Framework", id);
            try
            {

                var result = await (from a in _context.PDOGred
                                    join b in _context.PDOStatusPermohonanGred on a.Id equals b.IdGred
                                    join d in _context.PDOKlasifikasiPerkhidmatan on a.IdKlasifikasiPerkhidmatan equals d.Id
                                    join c in _context.PDOKumpulanPerkhidmatan on a.IdKumpulanPerkhidmatan equals c.Id
                                    where b.StatusAktif == true && c.StatusAktif == true
                                    where b.StatusAktif == true && a.Id == id
                                    select new PaparMaklumatGredDto
                                    {
                                        Id = a.Id,
                                        KodRujJenisSaraan=a.KodRujJenisSaraan,
                                        Klasifikasi = d.Nama,
                                        IdKlasifikasiPerkhidmatan =a.IdKlasifikasiPerkhidmatan,
                                        IdKumpulanPerkhidmatan = a.IdKumpulanPerkhidmatan,
                                        Kumpulan = c.Nama,
                                        Kod = a.Kod,
                                        Nama = a.Nama,
                                        KodGred=a.KodGred,
                                        NomborGred=a.NomborGred,
                                        IndikatorGredLantikanTerus = a.IndikatorGredLantikanTerus,
                                        IndikatorGredKenaikan = a.IndikatorGredKenaikan,
                                        Keterangan = a.Keterangan,
                                        StatusAktif = a.StatusAktif
                                       
                                    }).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting PaparMaklumatGredDto");
                throw;
            }
        }
        public async Task<bool> CheckDuplicateNamaAsync(CreateGredDto dto)
        {
            try
            {
                if (dto.Id == 0)
                {
                    // Create: check if Nama already exists
                    return await _context.PDOGred.AnyAsync(x =>

                        // x.Kod.Trim() == dto.Kod.Trim() || 
                        x.Nama.Trim() == dto.Nama.Trim() && x.KodRujJenisSaraan==dto.KodRujJenisSaraan);
                }
                else
                {
                    // Update: check for duplicates excluding current record
                    return await _context.PDOGred.AnyAsync(x =>

                        (
                        //x.Kod.Trim() == dto.Kod.Trim() || 
                        x.Nama.Trim() == dto.Nama.Trim()) && x.KodRujJenisSaraan == dto.KodRujJenisSaraan &&
                        x.Id != dto.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Validate PDOGred");
                throw;
            }
        }
        private PDOGred MapToEntity(CreateGredDto dto)
        {
            return new PDOGred
            {
                Id = dto.Id,
                KodRujJenisSaraan = dto.KodRujJenisSaraan,
                IdKlasifikasiPerkhidmatan = dto.IdKlasifikasiPerkhidmatan,
                IdKumpulanPerkhidmatan = dto.IdKumpulanPerkhidmatan,
                Kod = dto.Kod,
                Nama = dto.Nama,
                TurutanGred=dto.TurutanGred,
                KodGred = dto.KodGred,
                NomborGred = dto.NomborGred,
                Keterangan = dto.Keterangan,
                IndikatorGredKenaikan = dto.IndikatorGredKenaikan,
                IndikatorGredLantikanTerus = dto.IndikatorGredLantikanTerus
            };
        }
    }
}
