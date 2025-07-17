using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Core.Entities.PDO;
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
    public class PermohonanJawatanService : IPermohonanJawatanService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _dbContext;
        private readonly ILogger<PermohonanJawatanService> _logger;
        public PermohonanJawatanService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<PermohonanJawatanService> logger)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _logger = logger;
        }
        public IQueryable<PermohonanJawatanSearchResponseDto> Search(PermohonanJawatanFilterDto filter)
        {
            try
            {
                _logger.LogInformation("Getting all PermohonanJawatanSearch using EF Core join");
                var query = from a in _dbContext.PDOPermohonanJawatan
                            join b in _dbContext.PDOStatusPermohonanJawatan on a.Id equals b.IdPermohonanJawatan
                            join c in _dbContext.PDORujStatusPermohonan on b.KodRujStatusPermohonan equals c.Kod
                            where b.StatusAktif == true
                            select new PermohonanJawatanSearchResponseDto
                            {
                                Id = a.Id,
                                NomborRujukan = a.NomborRujukan,
                                Tajuk = a.Tajuk,
                                TarikhPermohonan = a.TarikhPermohonan,
                                KodRujStatusPermohonan = b.KodRujStatusPermohonan,
                                Status = c.Nama
                            };

                if (!string.IsNullOrWhiteSpace(filter.NomborRujukan))
                    query = query.Where(x => x.NomborRujukan.Contains(filter.NomborRujukan));

                if (!string.IsNullOrWhiteSpace(filter.Tajuk))
                    query = query.Where(x => x.Tajuk.Contains(filter.Tajuk));

                if (!string.IsNullOrWhiteSpace(filter.KodRujStatusPermohonan))
                    query = query.Where(x => x.KodRujStatusPermohonan == filter.KodRujStatusPermohonan);

                return query;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting all PermohonanJawatanSearch using EF Core join");
                throw;
            }
        }
        public async Task<List<PermohonanJawatanResponseDto>> GetSenaraiPermohonanJawatanAsync(PermohonanJawatanFilterDto2 filter)
        {
            var query = from ppj in _dbContext.PDOPermohonanJawatan
                        join pspj in _dbContext.PDOStatusPermohonanJawatan on ppj.Id equals pspj.IdPermohonanJawatan
                        join prsp in _dbContext.PDORujStatusPermohonan on pspj.KodRujStatusPermohonan equals prsp.Kod
                        where
                            (string.IsNullOrEmpty(filter.NomborRujukan) || ppj.NomborRujukan == filter.NomborRujukan)
                            && (string.IsNullOrEmpty(filter.TajukPermohonan) || ppj.Tajuk.Contains(filter.TajukPermohonan))
                            && (string.IsNullOrEmpty(filter.KodStatusPermohonan) || prsp.Kod == filter.KodStatusPermohonan)
                        select new PermohonanJawatanResponseDto
                        {
                            RecordId = ppj.Id,
                            NomborRujukan = ppj.NomborRujukan,
                            TajukPermohonan = ppj.Tajuk,
                            TarikhPermohonan = ppj.TarikhPermohonan,
                            Status = prsp.Nama
                        };

            return await query.ToListAsync();
        }
        public async Task<List<PermohonanPindaanResponseDto>> GetSenaraiPermohonanPindaanAsync(PermohonanPindaanFilterDto filter)
        {
            var query = from ppj in _dbContext.PDOPermohonanJawatan
                        join pspj in _dbContext.PDOStatusPermohonanJawatan on ppj.Id equals pspj.IdPermohonanJawatan
                        join prsp in _dbContext.PDORujStatusPermohonan on pspj.KodRujStatusPermohonan equals prsp.Kod
                        where (string.IsNullOrEmpty(filter.NomborRujukan) || ppj.NomborRujukan.Contains(filter.NomborRujukan))
                              && (string.IsNullOrEmpty(filter.TajukPermohonan) || ppj.Tajuk.Contains(filter.TajukPermohonan))
                              && (string.IsNullOrEmpty(filter.KodStatusPermohonan) || prsp.Kod == filter.KodStatusPermohonan)
                        select new PermohonanPindaanResponseDto
                        {
                            RecordId = ppj.Id,
                            NomborRujukan = ppj.NomborRujukan,
                            TajukPermohonan = ppj.Tajuk,
                            TarikhPermohonan = ppj.TarikhPermohonan,
                            Status = prsp.Nama
                        };

            return await query.ToListAsync();
        }


        public async Task<List<PermohonanJawatanDto>> GetSenaraiAsalAsync(int agensiId, string? noRujukan, string? tajuk, string? statusKod)
        {
            var result = await (from ppj in _dbContext.PDOPermohonanJawatan
                                join puo in _dbContext.PDOUnitOrganisasi on ppj.IdUnitOrganisasi equals puo.Id
                                join pspj in _dbContext.PDOStatusPermohonanJawatan on ppj.Id equals pspj.IdPermohonanJawatan
                                join prsp in _dbContext.PDORujStatusPermohonan on pspj.KodRujStatusPermohonan equals prsp.Kod
                                where ppj.IdUnitOrganisasi == agensiId
                                   && (string.IsNullOrEmpty(noRujukan) || ppj.NomborRujukan.Contains(noRujukan))
                                   && (string.IsNullOrEmpty(tajuk) || ppj.Tajuk.Contains(tajuk))
                                   && prsp.Kod == statusKod
                                select new PermohonanJawatanDto
                                {
                                    Agensi = puo.Nama,
                                    NomborRujukan = ppj.NomborRujukan,
                                    Tajuk = ppj.Tajuk,
                                    TarikhPermohonan =Convert.ToDateTime( ppj.TarikhPermohonan),
                                    Status = prsp.Nama
                                }).ToListAsync();

            return result;
        }

        public async Task<PermohonanJawatanDto?> GetSenaraiBaruByIdAsync(int idPermohonanJawatan)
        {
            var result = await (from ppj in _dbContext.PDOPermohonanJawatan
                                join puo in _dbContext.PDOUnitOrganisasi on ppj.IdUnitOrganisasi equals puo.Id
                                join pspj in _dbContext.PDOStatusPermohonanJawatan on ppj.Id equals pspj.IdPermohonanJawatan
                                join prsp in _dbContext.PDORujStatusPermohonan on pspj.KodRujStatusPermohonan equals prsp.Kod
                                where ppj.Id == idPermohonanJawatan
                                select new PermohonanJawatanDto
                                {
                                    Agensi = puo.Nama,
                                    NomborRujukan = ppj.NomborRujukan,
                                    Tajuk = ppj.Tajuk,
                                    TarikhPermohonan = Convert.ToDateTime(ppj.TarikhPermohonan),
                                    Status = prsp.Nama
                                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<PermohonanJawatanListDto>> GetPermohonanListAsync( int agensiId, string? noRujukan,string? tajuk,string? kodStatus)
        {
            var query = from ppj in _dbContext.PDOPermohonanJawatan
                        join puo in _dbContext.PDOUnitOrganisasi on ppj.IdUnitOrganisasi equals puo.Id
                        join prjp in _dbContext.PDORujJenisPermohonan on ppj.KodRujJenisPermohonan equals prjp.Kod
                        join pspj in _dbContext.PDOStatusPermohonanJawatan on ppj.Id equals pspj.IdPermohonanJawatan
                        join prspj in _dbContext.PDORujStatusPermohonanJawatan on pspj.KodRujStatusPermohonan equals prspj.Kod
                        where ppj.IdAgensi == agensiId
                            && (string.IsNullOrEmpty(noRujukan) || ppj.NomborRujukan.Contains(noRujukan))
                            && (string.IsNullOrEmpty(tajuk) || ppj.Tajuk.Contains(tajuk))
                            && (string.IsNullOrEmpty(kodStatus) || prspj.Kod == kodStatus)
                        select new PermohonanJawatanListDto
                        {
                            IdPermohonanJawatan = ppj.Id,
                            IdUnitOrganisasi = ppj.IdUnitOrganisasi,
                            IdAgensi = ppj.IdAgensi,
                            Agensi = puo.Nama,
                            NomborRujukan = ppj.NomborRujukan,
                            JenisPermohonan = prjp.Nama,
                            TajukPermohonan = ppj.Tajuk,
                            TarikhPermohonan =Convert.ToDateTime( ppj.TarikhPermohonan),
                            Status = prspj.Nama
                        };

            return await query.ToListAsync();
        }



        //Amar Code Start
        public async Task<List<SalinanAsaResponseDto>> GetSalinanAsa(SalinanAsaFilterDto filter)
        {
            _logger.LogInformation("GetSalinanAsa: Getting SalinanAsa with filter: {@Filter}", filter);
            try
            {
                var query = from ppj in _dbContext.PDOPermohonanJawatan
                            join puo in _dbContext.PDOUnitOrganisasi on ppj.IdUnitOrganisasi equals puo.Id
                            join pspj in _dbContext.PDOStatusPermohonanJawatan on ppj.Id equals pspj.IdPermohonanJawatan
                            join prsp in _dbContext.PDORujStatusPermohonan on pspj.KodRujStatusPermohonan equals prsp.Kod
                            where ppj.IdUnitOrganisasi == filter.AgensiId
                                && (string.IsNullOrEmpty(filter.NoRujukan) || ppj.NomborRujukan.Contains(filter.NoRujukan))
                                && (string.IsNullOrEmpty(filter.TajukPermohonan) || ppj.Tajuk.Contains(filter.TajukPermohonan))
                                && prsp.Kod == filter.StatusPermohonan
                            orderby ppj.TarikhPermohonan
                            select new
                            {
                                Agensi = puo.Nama,
                                ppj.NomborRujukan,
                                ppj.Tajuk,
                                ppj.TarikhPermohonan,
                                Status = prsp.Nama
                            };

                _logger.LogInformation("GetSalinanAsa: Executing query to fetch SalinanAsa data");
                var data = await query.ToListAsync();
                _logger.LogInformation("GetSalinanAsa: Retrieved {Count} records from database", data.Count);

                var result = data.Select((x, index) => new SalinanAsaResponseDto
                {
                    Bil = index + 1,
                    Agensi = x.Agensi ?? String.Empty,
                    NomborRujukan = x.NomborRujukan ?? String.Empty,
                    Tajuk = x.Tajuk ?? String.Empty,
                    TarikhPermohonan = x.TarikhPermohonan,
                    Status = x.Status ?? String.Empty
                }).ToList();

                _logger.LogInformation("GetSalinanAsa: Successfully processed {Count} SalinanAsa records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetSalinanAsa: Failed to retrieve SalinanAsa data with filter: {@Filter}", filter);
                throw;
            }
        }

        public async Task<List<SalinanBaharuResponseDto>> GetSalinanBaharu(int IdPermohonanJawatanSelected)
        {
            _logger.LogInformation("GetSalinanBaharu: Getting SalinanBaharu with IdPermohonanJawatanSelected: {IdPermohonanJawatanSelected}", IdPermohonanJawatanSelected);

            try
            {
                var query = from ppj in _dbContext.PDOPermohonanJawatan
                            join puo in _dbContext.PDOUnitOrganisasi on ppj.IdUnitOrganisasi equals puo.Id
                            join pspj in _dbContext.PDOStatusPermohonanJawatan on ppj.Id equals pspj.IdPermohonanJawatan
                            join prsp in _dbContext.PDORujStatusPermohonan on pspj.KodRujStatusPermohonanJawatan equals prsp.Kod
                            where ppj.Id == IdPermohonanJawatanSelected
                            orderby ppj.TarikhPermohonan
                            select new
                            {
                                Agensi = puo.Nama ?? String.Empty,
                                ppj.NomborRujukan,
                                Tajuk = ppj.Tajuk,
                                ppj.TarikhPermohonan,
                                Status = prsp.Nama ?? String.Empty
                            };

                _logger.LogInformation("GetSalinanBaharu: Executing query to fetch SalinanBaharu data");

                var data = await query.ToListAsync();

                _logger.LogInformation("GetSalinanBaharu: Retrieved {Count} records from database", data.Count);

                var result = data.Select((x, index) => new SalinanBaharuResponseDto
                {
                    Bil = index + 1,
                    Agensi = x.Agensi,
                    NomborRujukan = x.NomborRujukan ?? String.Empty,
                    Tajuk = x.Tajuk ?? String.Empty,
                    TarikhPermohonan = x.TarikhPermohonan,
                    Status = x.Status
                }).ToList();

                _logger.LogInformation("GetSalinanBaharu: Successfully processed {Count} SalinanBaharu records", result.Count);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetSalinanBaharu: Failed to retrieve SalinanBaharu data with IdPermohonanJawatanSelected: {IdPermohonanJawatanSelected}", IdPermohonanJawatanSelected);
                throw;
            }
        }

        public async Task<bool> SetUlasanPasukanPerunding(UlasanPasukanPerundingRequestDto ulasanPasukanPerundingRequestDto)
        {
            _logger.LogInformation("Service: Updating UlasanPasukanPerunding");
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Step 1: Update PDO_PermohonanJawatan
                var permohonanJawatan = await _unitOfWork.Repository<PDOPermohonanJawatan>()
                    .FirstOrDefaultAsync(x => x.IdUnitOrganisasi == ulasanPasukanPerundingRequestDto.AgensiId && x.Id == ulasanPasukanPerundingRequestDto.IdPermohonanJawatan);

                if (permohonanJawatan != null)
                {
                    permohonanJawatan.KodRujJenisPermohonan = ulasanPasukanPerundingRequestDto.KodRujJenisPermohonan;
                    permohonanJawatan.KodRujJenisPermohonanJPA = ulasanPasukanPerundingRequestDto.KodRujJenisPermohonanPP;
                   

                    await _unitOfWork.Repository<PDOPermohonanJawatan>().UpdateAsync(permohonanJawatan);
                    await _unitOfWork.SaveChangesAsync();
                }

                // Step 2: Deactivate existing PDO_StatusPermohonanJawatan record
                var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanJawatan>()
                    .FirstOrDefaultAsync(x => x.IdPermohonanJawatan == ulasanPasukanPerundingRequestDto.IdPermohonanJawatan && x.StatusAktif == true);

                if (existingStatus != null)
                {
                    existingStatus.StatusAktif = false;
                   

                    await _unitOfWork.Repository<PDOStatusPermohonanJawatan>().UpdateAsync(existingStatus);
                    await _unitOfWork.SaveChangesAsync();
                }

                // Step 3: Insert new PDO_StatusPermohonanJawatan record
                var newStatusEntity = new PDOStatusPermohonanJawatan
                {
                    IdPermohonanJawatan = ulasanPasukanPerundingRequestDto.IdPermohonanJawatan,
                    KodRujStatusPermohonanJawatan = "02",
                    TarikhStatusPermohonan = DateTime.Now,
                    UlasanStatusPermohonan = ulasanPasukanPerundingRequestDto.Ulasan,
                    StatusAktif = true,
                };

                await _unitOfWork.Repository<PDOStatusPermohonanJawatan>().AddAsync(newStatusEntity);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service SetUlasanPasukanPerunding");
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }

        public async Task<List<SenaraiPermohonanPerjawatanResponseDto>> GetSenaraiPermohonanPerjawatan(SenaraiPermohonanPerjawatanFilterDto filter)
        {
            _logger.LogInformation("GetSenaraiPermohonanPerjawatan: Getting SenaraiPermohonanPerjawatan with filter: {@Filter}", filter);
            try
            {
                var query = from a in _dbContext.PDOPermohonanJawatan
                            join b in _dbContext.PDOStatusPermohonanJawatan on a.Id equals b.IdPermohonanJawatan
                            join c in _dbContext.PDORujStatusPermohonan on b.KodRujStatusPermohonan equals c.Kod
                            where (string.IsNullOrEmpty(filter.NomborRujukan) || a.NomborRujukan.Contains(filter.NomborRujukan))
                                && (string.IsNullOrEmpty(filter.TajukPermohonan) || a.Tajuk.Contains(filter.TajukPermohonan))
                                && (string.IsNullOrEmpty(filter.StatusPermohonan) || c.Kod == filter.StatusPermohonan)
                           
                            select new
                            {
                                a.Id,
                                a.NomborRujukan,
                                a.Tajuk,
                                a.TarikhPermohonan,
                                b.KodRujStatusPermohonan,
                                Status = c.Nama
                            };

                _logger.LogInformation("GetSenaraiPermohonanPerjawatan: Executing query to fetch SenaraiPermohonanPerjawatan data");
                var data = await query.ToListAsync();
                _logger.LogInformation("GetSenaraiPermohonanPerjawatan: Retrieved {Count} records from database", data.Count);

                var result = data.Select((x, index) => new SenaraiPermohonanPerjawatanResponseDto
                {
                    Bil = index + 1,
                    Id = x.Id,
                    NomborRujukan = x.NomborRujukan ?? String.Empty,
                    Tajuk = x.Tajuk ?? String.Empty,
                    TarikhPermohonan = x.TarikhPermohonan,
                    KodRujStatusPermohonan = x.KodRujStatusPermohonan ?? String.Empty,
                    Status = x.Status ?? String.Empty
                }).ToList();

                _logger.LogInformation("GetSenaraiPermohonanPerjawatan: Successfully processed {Count} SenaraiPermohonanPerjawatan records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetSenaraiPermohonanPerjawatan: Failed to retrieve SenaraiPermohonanPerjawatan data with filter: {@Filter}", filter);
                throw;
            }
        }
        //Amar Code End

    }
}
