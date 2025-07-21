using HR.Application.DTOs.PDO;
using HR.Application.Extensions;
using HR.Application.Interfaces.PDO;
using HR.Core.Entities.PDO;
using HR.Core.Enums;
using HR.Core.Interfaces;
using HR.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.Application.Services.PDO
{
    public class MaklumatKlasifikasiPerkhidmatanService : IMaklumatKlasifikasiPerkhidmatanService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _dbContext;
        private readonly ILogger<MaklumatKlasifikasiPerkhidmatanService> _logger;

        public MaklumatKlasifikasiPerkhidmatanService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<MaklumatKlasifikasiPerkhidmatanService> logger)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<MaklumatKlasifikasiPerkhidmatanSearchResponseDto>> GetSearchMaklumatKlasifikasiPerkhidmatan(PenapisMaklumatKlasifikasiPerkhidmatanDto filter)
        {
            try
            {


                _logger.LogInformation("Getting all MaklumatKlasifikasiPerkhidmatanDto using EF Core join");

                var query = (from a in _dbContext.PDOKlasifikasiPerkhidmatan
                             join b in _dbContext.PDOStatusPermohonanKlasifikasiPerkhidmatan
                                           on a.Id equals b.IdKlasifikasiPerkhidmatan
                             join b2 in _dbContext.PDORujStatusPermohonan
                                           on b.KodRujStatusPermohonan equals b2.Kod
                             where b.StatusAktif == true
                             orderby a.Kod
                             select new
                             {
                                 a.Id,
                                 a.Kod,
                                 a.Nama,
                                 a.Keterangan,
                                 a.StatusAktif,
                                 a.FungsiUmum,
                                 a.FungsiUtama,
                                 StatusPermohonan = b2.Nama
                             })
                  .AsEnumerable()
                 .Select((x, index) => new
                 {
                     Bil = index + 1,
                     x.Id,
                     x.Kod,
                     x.Nama,
                     x.Keterangan,
                     x.StatusAktif,
                     x.FungsiUmum,
                     x.FungsiUtama,
                     x.StatusPermohonan
                 });


                // Apply filters
                if (!string.IsNullOrWhiteSpace(filter.Kod))
                    query = query.Where(q => q.Kod.Contains(filter.Kod));

                if (!string.IsNullOrWhiteSpace(filter.Nama))
                    query = query.Where(q => q.Nama.Contains(filter.Nama));

                if (filter.StatusKumpulan.HasValue)
                    query = query.Where(q => Convert.ToInt16(q.StatusAktif) == filter.StatusKumpulan.Value);

                if (!string.IsNullOrWhiteSpace(filter.StatusPermohonan))
                    query = query.Where(q => q.Kod == filter.StatusPermohonan);

                var data = query.ToList();



                var result = data
                    .Select((q, index) => new MaklumatKlasifikasiPerkhidmatanSearchResponseDto
                    {
                        Bil = index + 1,
                        Id = q.Id,
                        Kod = q.Kod,
                        Nama = q.Nama,
                        Keterangan = q.Keterangan,
                        StatusKumpulanPerkhidmatan = (q.StatusAktif
                            ? StatusKumpulanPerkhidmatanEnum.Aktif
                            : StatusKumpulanPerkhidmatanEnum.TidakAktif).ToDisplayString(),
                        FungsiUmum=q.FungsiUmum,
                        FungsiUtama=q.FungsiUtama,
                        StatusPermohonan = q.StatusPermohonan
                    })
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrive data");
            }
        }

        private string GenerateNextKODFromDb()
        {

            int maxId = _dbContext.PDOKumpulanPerkhidmatan
           .Select(c => c.Id)  // Or use your primary key name
            .AsEnumerable()    // <-- Force client-side evaluation
            .DefaultIfEmpty(0)
           .Max();

            return (maxId + 1).ToString("D3"); // Formats to 3-digit KOD like "001"

        }
        public async Task<bool> NewAsync(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto CreateRequestDto)
        {
            _logger.LogInformation("Service: Creating new KumpulanPerkhidmatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Step 1: Insert into PDO_KlasifikasiPerkhidmatan
               // CreateRequestDto.Kod = GenerateNextKODFromDb();
                var KlasifikasiPerkhidmatan = MapToEntity(CreateRequestDto);
                KlasifikasiPerkhidmatan.StatusAktif = false;

                KlasifikasiPerkhidmatan = await _unitOfWork.Repository<PDOKlasifikasiPerkhidmatan>().AddAsync(KlasifikasiPerkhidmatan);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                // Step 2: Insert into PDO_StatusPermohonanKumpulanPerkhidmatan
                var statusEntity = new PDOStatusPermohonanKlasifikasiPerkhidmatan
                {
                    IdKlasifikasiPerkhidmatan = KlasifikasiPerkhidmatan.Id, // use the ID from step 1
                    KodRujStatusPermohonan = "01",
                    TarikhKemaskini = DateTime.Now,
                    StatusAktif = true
                };
                await _unitOfWork.Repository<PDOStatusPermohonanKlasifikasiPerkhidmatan>().AddAsync(statusEntity);
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

        public async Task<bool> CheckDuplicateKodNamaAsync(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto dto)
        {
            try
            {
                if (dto.Id == 0)
                {
                    // Create: check if Kod or Nama already exists
                    return await _dbContext.PDOKlasifikasiPerkhidmatan.AnyAsync(x =>
                     //x.Nama.Trim() == dto.Nama.Trim());
                     x.Kod.Trim() == dto.Kod.Trim() || x.Nama.Trim() == dto.Nama.Trim());
                }
                else
                {
                    // Update: check for duplicates excluding current record
                    return await _dbContext.PDOKlasifikasiPerkhidmatan.AnyAsync(x =>

                        (x.Kod.Trim() == dto.Kod.Trim() || x.Nama.Trim() == dto.Nama.Trim()) &&
                        x.Id != dto.Id);
                }

            
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Validate KlasifikasiPerkhidmatan");
                throw;
            }
        }
        public async Task<MaklumatKlasifikasiPerkhidmatanResponseDto> GetMaklumatKlasifikasiPerkhidmatan(int id)
        {
            try
            {

                _logger.LogInformation("Getting all MaklumatKlasifikasiPerkhidmatanDto using EF Core join");

                var query = await (from a in _dbContext.PDOKlasifikasiPerkhidmatan
                                   join b in _dbContext.PDOStatusPermohonanKlasifikasiPerkhidmatan
                                                 on a.Id equals b.IdKlasifikasiPerkhidmatan
                                   join b2 in _dbContext.PDORujStatusPermohonan
                                                 on b.KodRujStatusPermohonan equals b2.Kod
                                   where b.StatusAktif == true && a.Id == id
                                   select new { a, b, b2 }
                                ).FirstOrDefaultAsync();
                PDOKlasifikasiPerkhidmatan? jsonObj = null;
                if (!string.IsNullOrWhiteSpace(query.a.ButiranKemaskini))
                {
                    jsonObj = JsonConvert.DeserializeObject<PDOKlasifikasiPerkhidmatan>(query.a.ButiranKemaskini);
                }
                var dtoSource = jsonObj ?? query.a;

                var result = new MaklumatKlasifikasiPerkhidmatanResponseDto
                {
                    Id = dtoSource.Id,
                    Kod = dtoSource.Kod,
                    Nama = dtoSource.Nama,
                    Keterangan = dtoSource.Keterangan,
                    FungsiUtama = dtoSource.FungsiUtama ?? "",
                    FungsiUmum = dtoSource.FungsiUmum ?? "",
                    StatusKlasifikasiPerkhidmatan = query.b.StatusAktif == true ? "Aktif" : "Tidak Aktif",
                    Status = query.b2.Nama,
                    TarikhKemaskini = query.b.TarikhKemaskini,
                    IndikatorSkim = dtoSource.IndikatorSkim,
                    StatusAktif = dtoSource.StatusAktif
                };
                return result;
                //orderby a.Kod
                //select new MaklumatKlasifikasiPerkhidmatanResponseDto
                //{
                //    Id = a.Id,
                //    Kod = a.Kod,
                //    Nama = a.Nama,
                //    Keterangan = a.Keterangan,
                //    FungsiUtama = a.FungsiUtama ?? "",
                //    FungsiUmum = a.FungsiUmum ?? "",
                //    StatusKlasifikasiPerkhidmatan = b.StatusAktif == true ? "Aktif" : "Tidak Aktif",
                //    Status = b2.Nama,
                //    TarikhKemaskini = b.TarikhKemaskini,
                //    IndikatorSkim = a.IndikatorSkim,
                //    StatusAktif = a.StatusAktif

                //    // IndSkimPerkhidmatan = a.IndSkimPerkhidmatan
                //}).FirstOrDefaultAsync();

                
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrive data");
            }
        }

        public async Task<MaklumatKlasifikasiPerkhidmatanResponseDto> GetMaklumatKlasifikasiPerkhidmatanOld(int id)
        {
            try
            {

                _logger.LogInformation("Getting all MaklumatKlasifikasiPerkhidmatanDto using EF Core join");

                var query = await (from a in _dbContext.PDOKlasifikasiPerkhidmatan
                                   join b in _dbContext.PDOStatusPermohonanKlasifikasiPerkhidmatan
                                                 on a.Id equals b.IdKlasifikasiPerkhidmatan
                                   join b2 in _dbContext.PDORujStatusPermohonan
                                                 on b.KodRujStatusPermohonan equals b2.Kod
                                   where b.StatusAktif == true && a.Id == id
                                   //orderby a.Kod
                                   select new MaklumatKlasifikasiPerkhidmatanResponseDto
                                   {
                                       Id = a.Id,
                                       Kod = a.Kod,
                                       Nama = a.Nama,
                                       Keterangan = a.Keterangan,
                                       FungsiUtama = a.FungsiUtama ?? "",
                                       FungsiUmum = a.FungsiUmum ?? "",
                                       StatusKlasifikasiPerkhidmatan = b.StatusAktif == true ? "Aktif" : "Tidak Aktif",
                                       Status = b2.Nama,
                                       TarikhKemaskini = b.TarikhKemaskini,
                                       IndikatorSkim = a.IndikatorSkim,
                                       StatusAktif = a.StatusAktif

                                       // IndSkimPerkhidmatan = a.IndSkimPerkhidmatan
                                   }).FirstOrDefaultAsync();

                return query;
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrive data");
            }
        }
        public async Task<ButiranKemaskiniKlasifikasiPerkhidmatanResponseDto> GetMaklumatKlasifikasiPerkhidmatanView(int id)
        {
            try
            {

                _logger.LogInformation("Getting all MaklumatKlasifikasiPerkhidmatanDto using EF Core join");

                var query = await (from a in _dbContext.PDOKlasifikasiPerkhidmatan
                                   join b in _dbContext.PDOStatusPermohonanKlasifikasiPerkhidmatan
                                                 on a.Id equals b.IdKlasifikasiPerkhidmatan
                                   join b2 in _dbContext.PDORujStatusPermohonan
                                                 on b.KodRujStatusPermohonan equals b2.Kod
                                   where b.StatusAktif == true && a.Id == id
                                   orderby a.Kod
                                   select new MaklumatKlasifikasiPerkhidmatanResponseDto
                                   {
                                       ButiranKemaskini = a.ButiranKemaskini

                                       // IndSkimPerkhidmatan = a.IndSkimPerkhidmatan
                                   }).FirstOrDefaultAsync();

                if (System.String.IsNullOrEmpty(query.ButiranKemaskini))
                {
                    return new ButiranKemaskiniKlasifikasiPerkhidmatanResponseDto
                    {
                        Keterangan = "Tiada butiran kemaskini"
                    };
                }

                ButiranKemaskiniKlasifikasiPerkhidmatanResponseDto obj = JsonConvert.DeserializeObject<ButiranKemaskiniKlasifikasiPerkhidmatanResponseDto>(query.ButiranKemaskini);
                return obj;
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrive data");
            }
        }

        public async Task<bool> SetAsync(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto updateRequestDto)
        {
            _logger.LogInformation("Service: Updating MaklumatKlasifikasiPerkhidmatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var klasifikasiPerkhidmatan = await _dbContext.PDOKlasifikasiPerkhidmatan
                      .Where(x => x.Id == updateRequestDto.Id && x.Kod == updateRequestDto.Kod)
                      .FirstOrDefaultAsync();

                if (klasifikasiPerkhidmatan == null)
                {
                    _logger.LogError("KlasifikasiPerkhidmatan with ID {Id} not found", updateRequestDto.Id);
                    return false; // or throw an exception
                }
                if (!klasifikasiPerkhidmatan.StatusAktif)
                {
                    // Step 1: update into PDO_KlasifikasiPerkhidmatan
                    var klasifikasiPerkhidmatanupdate = MapToEntity(updateRequestDto);
                    klasifikasiPerkhidmatanupdate.StatusAktif = updateRequestDto.StatusAktif;
                    klasifikasiPerkhidmatanupdate.Ulasan = updateRequestDto.Ulasan;


                    var result = await _unitOfWork.Repository<PDOKlasifikasiPerkhidmatan>().UpdateAsync(klasifikasiPerkhidmatanupdate);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    // Step 1: update into PDO_KlasifikasiPerkhidmatan
                    var ButiranKemaskiniklasifikasiPerkhidmatan = MapToEntity(updateRequestDto);
                    ButiranKemaskiniklasifikasiPerkhidmatan.StatusAktif = updateRequestDto.StatusAktif;
                    ButiranKemaskiniklasifikasiPerkhidmatan.Ulasan = updateRequestDto.Ulasan;

                    klasifikasiPerkhidmatan.ButiranKemaskini = JsonConvert.SerializeObject(ButiranKemaskiniklasifikasiPerkhidmatan);


                    var result = await _unitOfWork.Repository<PDOKlasifikasiPerkhidmatan>().UpdateAsync(klasifikasiPerkhidmatan);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();

                    // Step 2: Deactivate existing PDO_StatusPermohonanKlasifikasiPerkhidmatan record
                    var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanKlasifikasiPerkhidmatan>()
                            .FirstOrDefaultAsync(x => x.IdKlasifikasiPerkhidmatan == klasifikasiPerkhidmatan.Id && x.StatusAktif);

                    if (existingStatus != null)
                    {
                        existingStatus.StatusAktif = false;
                        existingStatus.TarikhPinda = DateTime.Now;
                        await _unitOfWork.SaveChangesAsync();
                    }


                    // Step 3: Insert into PDO_StatusPermohonanKlasifikasiPerkhidmatan
                    var statusEntity = new PDOStatusPermohonanKlasifikasiPerkhidmatan
                    {
                        IdKlasifikasiPerkhidmatan = klasifikasiPerkhidmatan.Id, // use the ID from step 1
                        KodRujStatusPermohonan = "01",
                        TarikhKemaskini = DateTime.Now,
                        StatusAktif = true
                    };
                    await _unitOfWork.Repository<PDOStatusPermohonanKlasifikasiPerkhidmatan>().AddAsync(statusEntity);
                    await _unitOfWork.SaveChangesAsync();

                    await _unitOfWork.CommitAsync();

                }
                  
                    return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service UpdateAsync");
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }

        public async Task<IEnumerable<PengesahanPerkhidmatanKlasifikasiResponseDto>> GetSenaraiPengesahanPerkhidmatanKlasifikasi(PenapisPerkhidmatanKlasifikasiDto filter)
        {
            try
            {
                _logger.LogInformation("Getting all MaklumatKlasifikasiPerkhidmatanDto using EF Core join");

                var query = (from a in _dbContext.PDOKlasifikasiPerkhidmatan
                              join b in _dbContext.PDOStatusPermohonanKlasifikasiPerkhidmatan
                                  on a.Id equals b.IdKlasifikasiPerkhidmatan
                              join b2 in _dbContext.PDORujStatusPermohonan
                                  on b.KodRujStatusPermohonan equals b2.Kod
                              where b.StatusAktif == true
                              orderby a.Kod
                              select new
                              {
                                  a.Id,
                                  a.Kod,
                                  a.Nama,
                                  a.Keterangan,
                                  b.KodRujStatusPermohonan,
                                  StatusPermohonan = b2.Nama,
                                  b.TarikhKemaskini,
                                  a.IndikatorSkim
                                  //a.IndSkimPerkhidmatan
                              })
                .AsEnumerable() // Switch to in-memory to simulate row_number
                .Select((x, index) => new
                {
                    Bil = index + 1,
                    x.Id,
                    x.Kod,
                    x.Nama,
                    x.Keterangan,
                    x.KodRujStatusPermohonan,
                    x.StatusPermohonan,
                    x.TarikhKemaskini,
                    x.IndikatorSkim
                    //x.IndSkimPerkhidmatan
                });



                // Apply filters
                if (!string.IsNullOrWhiteSpace(filter.Kod))
                    query = query.Where(q => q.Kod.Contains(filter.Kod));

                if (!string.IsNullOrWhiteSpace(filter.Nama))
                    query = query.Where(q => q.Nama.Contains(filter.Nama));

                if (!string.IsNullOrWhiteSpace(filter.StatusPermohonan))
                    query = query.Where(q => q.StatusPermohonan == filter.StatusPermohonan);

                var data = query.ToList();


                var result = data
                    .Select((q, index) => new PengesahanPerkhidmatanKlasifikasiResponseDto
                    {
                        Id= q.Id,
                        Bil = q.Bil,
                        Kod = q.Kod,
                        Nama = q.Nama,
                        Keterangan = q.Keterangan,
                        StatusPermohonan = q.StatusPermohonan
                     
                    })
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrive data");
            }
        }

        public async Task<IEnumerable<MaklumatKlasifikasiPerkhidmatanDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all MaklumatKlasifikasiPerkhidmatanCreateRequestDto using Entity Framework");
            var result = await _unitOfWork.Repository<PDOKlasifikasiPerkhidmatan>().GetAllAsync();
            result = result.ToList().Where(e => e.StatusAktif);
            return result.Select(MapToDto);
        }

        public async Task<bool> KemaskiniStatusAsync(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto dto)
        {
            _logger.LogInformation("Service: Updating KemaskiniStatusAsync");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Step 1: update into PDO_KlasifikasiPerkhidmatan
                var klasifikasiPerkhidmatan = MapToEntity(dto);
                klasifikasiPerkhidmatan.StatusAktif = dto.StatusAktif;
                klasifikasiPerkhidmatan.Ulasan = dto.Ulasan;
                //if (!string.IsNullOrWhiteSpace(perkhidmatan.ButiranKemaskini))
                //{
                //    perkhidmatan.ButiranKemaskini = null;
                //}

                var result = await _unitOfWork.Repository<PDOKlasifikasiPerkhidmatan>().UpdateAsync(klasifikasiPerkhidmatan);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();



                // Step 2: Deactivate existing PDO_StatusPermohonanKlasifikasiPerkhidmatan record
                var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanKlasifikasiPerkhidmatan>()
                        .FirstOrDefaultAsync(x => x.IdKlasifikasiPerkhidmatan == klasifikasiPerkhidmatan.Id && x.StatusAktif);

                if (existingStatus != null)
                {
                    existingStatus.StatusAktif = false;
                    existingStatus.TarikhPinda = DateTime.Now;
                    await _unitOfWork.SaveChangesAsync();
                }


                // Step 3: Insert into PDO_StatusPermohonanKlasifikasiPerkhidmatan
                var statusEntity = new PDOStatusPermohonanKlasifikasiPerkhidmatan
                {
                    IdKlasifikasiPerkhidmatan = klasifikasiPerkhidmatan.Id, // use the ID from step 1
                    KodRujStatusPermohonan = dto.KodRujStatusPermohonan,
                    TarikhKemaskini = DateTime.Now,
                    StatusAktif = true
                };
                await _unitOfWork.Repository<PDOStatusPermohonanKlasifikasiPerkhidmatan>().AddAsync(statusEntity);
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

        public async Task<bool> DaftarHanatarMaklumatKlasifikasiPerkhidmatanAsync(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto dto)
        {
            _logger.LogInformation("Service: Hantar  MaklumatKlasifikasiPerkhidmatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var idKlasifikasi = await _dbContext.PDOKlasifikasiPerkhidmatan
                .Where(x => x.Kod == dto.Kod)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
                if (idKlasifikasi == 0)
                {

                    // Step 1: Insert into PDO_KlasifikasiPerkhidmatan
                    var KlasifikasiPerkhidmatan = MapToEntity(dto);
                    KlasifikasiPerkhidmatan.StatusAktif = false;

                    KlasifikasiPerkhidmatan = await _unitOfWork.Repository<PDOKlasifikasiPerkhidmatan>().AddAsync(KlasifikasiPerkhidmatan);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();

                    // Step 2: Insert into PDO_StatusPermohonanKumpulanPerkhidmatan
                    var statusEntity = new PDOStatusPermohonanKlasifikasiPerkhidmatan
                    {
                        IdKlasifikasiPerkhidmatan = KlasifikasiPerkhidmatan.Id, // use the ID from step 1
                        KodRujStatusPermohonan = "02",
                        TarikhKemaskini = DateTime.Now,
                        StatusAktif = true
                    };
                    await _unitOfWork.Repository<PDOStatusPermohonanKlasifikasiPerkhidmatan>().AddAsync(statusEntity);
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    // Step 1: Update existing active records

                    var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanKlasifikasiPerkhidmatan>()
                           .FirstOrDefaultAsync(x => x.IdKlasifikasiPerkhidmatan == idKlasifikasi && x.StatusAktif);

                    if (existingStatus != null)
                    {
                        existingStatus.StatusAktif = false;
                        existingStatus.TarikhPinda = DateTime.Now;
                        await _unitOfWork.SaveChangesAsync();
                    }

                    // Step 2: Insert new record
                    var newRecord = new PDOStatusPermohonanKlasifikasiPerkhidmatan
                    {
                        IdKlasifikasiPerkhidmatan = idKlasifikasi,
                        KodRujStatusPermohonan = "02",
                        TarikhKemaskini = DateTime.Now,
                        StatusAktif = true
                    };

                    await _unitOfWork.Repository<PDOStatusPermohonanKlasifikasiPerkhidmatan>().AddAsync(newRecord);
                    await _unitOfWork.SaveChangesAsync();

                }

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

        public async Task<bool> SetHanatarMaklumatKlasifikasiPerkhidmatanAsync(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto updateRequestDto)
        {
            _logger.LogInformation("Service: Updating Hantar KumpulanPerkhidmatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var klasifikasiPerkhidmatan = await _dbContext.PDOKlasifikasiPerkhidmatan
                     .Where(x => x.Id == updateRequestDto.Id && x.Kod == updateRequestDto.Kod)
                     .FirstOrDefaultAsync();
                if (klasifikasiPerkhidmatan == null)
                {
                    _logger.LogError("KlasifikasiPerkhidmatan with ID {Id} not found", updateRequestDto.Id);
                    return false; // or throw an exception
                }

                if (!klasifikasiPerkhidmatan.StatusAktif)
                {

                    // Step 1: update into PDO_KumpulanPerkhidmatan
                    klasifikasiPerkhidmatan = MapToEntity(updateRequestDto);
                    klasifikasiPerkhidmatan.StatusAktif = updateRequestDto.StatusAktif;
                    klasifikasiPerkhidmatan.Ulasan = updateRequestDto.Ulasan;


                    //perkhidmatan.ButiranKemaskini = JsonConvert.SerializeObject(ButiranKemaskiniperkhidmatan);


                    var result = await _unitOfWork.Repository<PDOKlasifikasiPerkhidmatan>().UpdateAsync(klasifikasiPerkhidmatan);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();


                    // Step 2: Deactivate existing PDO_StatusPermohonanKlasifikasiPerkhidmatan record
                    var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanKlasifikasiPerkhidmatan>()
                            .FirstOrDefaultAsync(x => x.IdKlasifikasiPerkhidmatan == klasifikasiPerkhidmatan.Id && x.StatusAktif);

                    if (existingStatus != null)
                    {
                        existingStatus.StatusAktif = false;
                        existingStatus.TarikhPinda = DateTime.Now;
                        await _unitOfWork.SaveChangesAsync();
                    }


                    // Step 3: Insert into PDO_StatusPermohonanKlasifikasiPerkhidmatan
                    var statusEntity = new PDOStatusPermohonanKlasifikasiPerkhidmatan
                    {
                        IdKlasifikasiPerkhidmatan = klasifikasiPerkhidmatan.Id, // use the ID from step 1
                        KodRujStatusPermohonan = "02",
                        TarikhKemaskini = DateTime.Now,
                        StatusAktif = true
                    };
                    await _unitOfWork.Repository<PDOStatusPermohonanKlasifikasiPerkhidmatan>().AddAsync(statusEntity);
                    await _unitOfWork.SaveChangesAsync();

                    await _unitOfWork.CommitAsync();
                }
                else
                {

                    // Step 1: update into PDO_KlasifikasiPerkhidmatan
                    var ButiranKemaskiniKlasifikasiPerkhidmatan = MapToEntity(updateRequestDto);
                    ButiranKemaskiniKlasifikasiPerkhidmatan.StatusAktif = updateRequestDto.StatusAktif;
                    ButiranKemaskiniKlasifikasiPerkhidmatan.Ulasan = updateRequestDto.Ulasan;

                    klasifikasiPerkhidmatan.ButiranKemaskini = JsonConvert.SerializeObject(ButiranKemaskiniKlasifikasiPerkhidmatan);


                    var result = await _unitOfWork.Repository<PDOKlasifikasiPerkhidmatan>().UpdateAsync(klasifikasiPerkhidmatan);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();

                    // Step 2: Deactivate existing PDO_StatusPermohonanKlasifikasiPerkhidmatan record
                    var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanKlasifikasiPerkhidmatan>()
                            .FirstOrDefaultAsync(x => x.IdKlasifikasiPerkhidmatan == klasifikasiPerkhidmatan.Id && x.StatusAktif);

                    if (existingStatus != null)
                    {
                        existingStatus.StatusAktif = false;
                        existingStatus.TarikhPinda = DateTime.Now;
                        await _unitOfWork.SaveChangesAsync();
                    }


                    // Step 3: Insert into PDO_StatusPermohonanKlasifikasiPerkhidmatan
                    var statusEntity = new PDOStatusPermohonanKlasifikasiPerkhidmatan
                    {
                        IdKlasifikasiPerkhidmatan = klasifikasiPerkhidmatan.Id, // use the ID from step 1
                        KodRujStatusPermohonan = "02",
                        TarikhKemaskini = DateTime.Now,
                        StatusAktif = true
                    };
                    await _unitOfWork.Repository<PDOStatusPermohonanKlasifikasiPerkhidmatan>().AddAsync(statusEntity);
                    await _unitOfWork.SaveChangesAsync();

                    await _unitOfWork.CommitAsync();
                }

                return true;

                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service Updating Hantar ");
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }

        private PDOKlasifikasiPerkhidmatan MapToEntity(MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto dto)
        {
            return new PDOKlasifikasiPerkhidmatan
            {
                Id = dto.Id,
                Kod = dto.Kod,
                Nama = dto.Nama,
                Keterangan = dto.Keterangan,
                FungsiUmum = dto.FungsiUmum,
                FungsiUtama = dto.FungsiUtama,
                StatusAktif = dto.StatusAktif,
                IndikatorSkim = dto.IndikatorSkim,
                ButiranKemaskini = dto.ButiranKemaskini,
                //IndSkimPerkhidmatan = dto.IndSkimPerkhidmatan
            };
        }
        private MaklumatKlasifikasiPerkhidmatanDto MapToDto(PDOKlasifikasiPerkhidmatan entity)
        {
            return new MaklumatKlasifikasiPerkhidmatanDto
            {
                Id = entity.Id,
                Nama = entity.Nama.Trim(),
                Kod = entity.Kod.Trim(),
                Keterangan = entity.Keterangan,
                FungsiUtama = entity.FungsiUtama,
                FungsiUmum = entity.FungsiUmum,
                ButiranKemaskini = entity.ButiranKemaskini
            };
        }

        public async Task<bool> DeleteOrUpdateKlasifikasiPerkhidmatanAsync(int id)
        {
            _logger.LogInformation("DeleteOrUpdateKlasifikasiPerkhidmatanAsync by ID {Id} using Entity Framework", id);
            try
            {

                var result = await (from a in _dbContext.PDOKlasifikasiPerkhidmatan
                                    join b in _dbContext.PDOStatusPermohonanKlasifikasiPerkhidmatan
                                        on a.Id equals b.IdKlasifikasiPerkhidmatan
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

                var kumpulanPerkhidmatan = await _unitOfWork.Repository<PDOKlasifikasiPerkhidmatan>().GetByIdAsync(id);
                if (kumpulanPerkhidmatan == null)
                    return false;

                if (!result.StatusAktif && result.KodRujStatusPermohonan == "01")
                {
                    using var transaction = await _dbContext.Database.BeginTransactionAsync();
                    try
                    {
                        // Delete children first
                        var statusList = await _unitOfWork.Repository<PDOStatusPermohonanKlasifikasiPerkhidmatan>()
                        .FindByFieldAsync("IdKlasifikasiPerkhidmatan", id);

                        _dbContext.PDOStatusPermohonanKlasifikasiPerkhidmatan.RemoveRange(statusList);
                        await _dbContext.SaveChangesAsync();
                        // Then delete parent
                        _dbContext.PDOKlasifikasiPerkhidmatan.Remove(kumpulanPerkhidmatan);
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
                    await _unitOfWork.Repository<PDOKlasifikasiPerkhidmatan>().UpdateAsync(kumpulanPerkhidmatan);
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }






            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteOrUpdateKlasifikasiPerkhidmatanAsync");
                throw;
            }
        }
    }
}


