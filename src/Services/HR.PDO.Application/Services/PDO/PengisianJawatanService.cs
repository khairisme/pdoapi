using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Core.Enums;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HR.PDO.Application.Services.PDO
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
        public async Task<List<PengisianJawatanSearchResponseDto>> GetPengisianJawatanAsync(int idSkimPerkhidmatan)
        {
            try
            {
                _logger.LogInformation("Getting PengisianJawatan by ID {Id} using Entity Framework", idSkimPerkhidmatan);
                var query = from a in _dbContext.PDOPengisianJawatan
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
                                 Id = a.Id,
                                 KodJawatan = b.Kod,
                                 NamaJawatan = b.Nama,
                                 UnitOrganisasi = c.Nama,
                                 StatusPengisianJawatan = e.Nama,
                                 TarikhKekosonganJawatan = d.TarikhStatusKekosongan
                             };

                var data = await query.ToListAsync();

                var result = data.Select((x, index) => new PengisianJawatanSearchResponseDto
                {
                    Bil = index + 1,
                    Id = x.Id,
                    KodJawatan = x.KodJawatan,
                    NamaJawatan = x.NamaJawatan,
                    UnitOrganisasi = x.UnitOrganisasi,
                    StatusPengisianJawatan = x.StatusPengisianJawatan,
                    TarikhKekosonganJawatan = x.TarikhKekosonganJawatan
                }).ToList();


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
        // Nitya Code Start
        public async Task<bool> CreateAsync(List<PengisianJawatanDto> dtos)
        {
            _logger.LogInformation("Service: Creating {Count} new PengisianJawatan records", dtos?.Count ?? 0);

            try
            {
                // Validate input parameters
                if (dtos == null || !dtos.Any())
                {
                    _logger.LogWarning("Service: Cannot create PengisianJawatan - empty or null list provided");
                    return false;
                }

                await _unitOfWork.BeginTransactionAsync();

                try
                {
                    var createdEntities = new List<PDOPengisianJawatan>();

                    // Process each DTO in the list
                    foreach (var dto in dtos)
                    {
                        // Validate individual DTO
                        if (dto == null)
                        {
                            _logger.LogWarning("Service: Skipping null DTO in PengisianJawatan creation");
                            continue;
                        }

                        // Create pengisian jawatan entity
                        var pengisian = new PDOPengisianJawatan
                        {
                            IdPermohonanPengisianSkim = dto.IdPermohonanPengisianSkim,
                            IdJawatan = dto.IdJawatan,
                            IdPemilikKompetensi = Guid.Empty

                        };

                        // Add entity to repository
                        var createdEntity = await _unitOfWork.Repository<PDOPengisianJawatan>().AddAsync(pengisian);
                        createdEntities.Add(createdEntity);

                        _logger.LogDebug("Service: Added PengisianJawatan entity for IdJawatan: {IdJawatan}", dto.IdJawatan);
                    }

                    // Check if any entities were created
                    if (!createdEntities.Any())
                    {
                        _logger.LogWarning("Service: No valid PengisianJawatan entities to create");
                        await _unitOfWork.RollbackAsync();
                        return false;
                    }

                    // Save all changes in a single transaction
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();

                    _logger.LogInformation("Service: Successfully created {Count} PengisianJawatan records", createdEntities.Count);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Service: Error during PengisianJawatan bulk creation - rolling back transaction");
                    await _unitOfWork.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Unexpected error during CreateAsync operation");
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
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

        public async Task<List<AgensiWithSkimDto>> GetAgensiWithSkimPengisianAsync()
        {
            var agensiList = await (from a in _dbContext.PDOPengisianJawatan
                                    join b in _dbContext.PDOJawatan on a.IdJawatan equals b.Id
                                    join c in _dbContext.PDOUnitOrganisasi on b.IdUnitOrganisasi equals c.Id
                                    where b.StatusAktif == true
                                    group c by new { c.Id, c.Kod, c.Nama } into g
                                    select new AgensiWithSkimDto
                                    {
                                        AgensiId = g.Key.Id,
                                        Kod = g.Key.Kod,
                                        Agensi = g.Key.Nama
                                    }).ToListAsync();

            foreach (var agensi in agensiList)
            {
                var skimList = await (from ppp in _dbContext.PDOPermohonanPengisian
                                      join ppps in _dbContext.PDOPermohonanPengisianSkim on ppp.Id equals ppps.IdPermohonanPengisian
                                      join psp in _dbContext.PDOSkimPerkhidmatan on ppps.IdSkimPerkhidmatan equals psp.Id
                                      where ppp.IdUnitOrganisasi == agensi.AgensiId
                                      group ppps by new { psp.Kod, psp.Nama } into g
                                      select new SkimPerkhidmatanDataDto
                                      {
                                          KodSkim = g.Key.Kod,
                                          NamaSkimPerkhidmatan = g.Key.Nama,
                                          BilanganPengisian = g.Sum(x => x.BilanganPengisian)
                                      }).ToListAsync();

                agensi.SkimList = skimList;
            }

            return agensiList;
        }
        public async Task<PermohonanDetailDto> GetPermohonanDetailByIdAsync(int idPermohonan)
        {
            var result = await (from ppp in _dbContext.PDOPermohonanPengisian
                                join ppps in _dbContext.PDOPermohonanPengisianSkim
                                    on ppp.Id equals ppps.IdPermohonanPengisian
                                join puo in _dbContext.PDOUnitOrganisasi
                                    on ppp.IdUnitOrganisasi equals puo.Id
                                join prkuo in _dbContext.PDORujKategoriUnitOrganisasi
                                    on puo.KodRujKategoriUnitOrganisasi equals prkuo.Kod
                                join prja in _dbContext.PDORujJenisAgensi
                                    on puo.KodRujJenisAgensi equals prja.Kod
                                where puo.StatusAktif == true
                                   && prkuo.Kod == "0001"
                                   && ppp.Id == idPermohonan
                                select new PermohonanDetailDto
                                {
                                    Kementerian = prja.Nama,
                                    NomborRujukan = ppp.NomborRujukan,
                                    TarikhPermohonan = ppp.TarikhPermohonan,
                                    Keterangan = ppp.Keterangan,
                                    BilanganPermohonan = _dbContext.PDOPermohonanPengisianSkim
                                        .Where(a => a.IdPermohonanPengisian == ppp.Id)
                                        .Sum(a => (int?)a.BilanganPengisian) ?? 0,
                                    Ulasan = ppps.Ulasan
                                }).FirstOrDefaultAsync();

            return result;
        }
        public async Task<List<SenaraiJawatanPengisianDto>> GetSenaraiJawatanUntukPengisian(int idSkimPerkhidmatan)
        {
            var result = await (
                from a in _dbContext.PDOPengisianJawatan
                join b in _dbContext.PDOJawatan on a.IdJawatan equals b.Id
                join c in _dbContext.PDOUnitOrganisasi on b.IdUnitOrganisasi equals c.Id
                join d in _dbContext.PDOKekosonganJawatan on b.Id equals d.IdJawatan
                join e in _dbContext.PDORujStatusKekosonganJawatan on d.KodRujStatusKekosonganJawatan equals e.Kod
                join f in _dbContext.PDOGredSkimJawatan on a.IdJawatan equals f.IdJawatan
                where b.StatusAktif == true && f.IdSkimPerkhidmatan == idSkimPerkhidmatan
                select new SenaraiJawatanPengisianDto
                {
                    Id = a.Id,
                    KodJawatan = b.Kod,
                    NamaJawatan = b.Nama,
                    UnitOrganisasi = c.Nama,
                    StatusPengisianJawatan = e.Nama,
                    TarikhKekosonganJawatan = d.TarikhStatusKekosongan
                }
            ).ToListAsync();

            return result;
        }
        public async Task<List<UnitOrganisasiDataDto>> GetSenaraiJawatanSebenarAsync()
        {
            var unitList = await (
                from a in _dbContext.PDOPengisianJawatan
                join b in _dbContext.PDOJawatan on a.IdJawatan equals b.Id
                join c in _dbContext.PDOUnitOrganisasi on b.IdUnitOrganisasi equals c.Id
                where b.StatusAktif == true
                group c by new { c.Id, c.Kod, c.Nama } into grp
                select new UnitOrganisasiDataDto
                {
                    IdUnitOrganisasi = grp.Key.Id,
                    Kod = grp.Key.Kod,
                    Agensi = grp.Key.Nama,
                    JawatanList = new List<JawatanDto>()
                }
            ).ToListAsync();

            foreach (var unit in unitList)
            {
                var jawatanList = await (
                    from ppp in _dbContext.PDOPermohonanPengisian
                    join puo in _dbContext.PDOUnitOrganisasi on ppp.IdUnitOrganisasi equals puo.Id
                    join ppj in _dbContext.PDOPengisianJawatan on ppp.Id equals ppj.IdPermohonanPengisian
                    join pj in _dbContext.PDOJawatan on ppj.IdJawatanSebenar equals pj.Id
                    join pgsj in _dbContext.PDOGredSkimJawatan on pj.Id equals pgsj.IdJawatan
                    join pg in _dbContext.PDOGred on pgsj.IdGred equals pg.Id
                    where ppp.IdUnitOrganisasi == unit.IdUnitOrganisasi
                    select new JawatanDto
                    {
                        KodJawatan = pj.Kod,
                        NamaJawatan = pj.Nama,
                        Gred = pg.Nama
                    }
                ).ToListAsync();

                unit.JawatanList = jawatanList;
            }

            return unitList;
        }
        // Nitya Code End

    }
}
