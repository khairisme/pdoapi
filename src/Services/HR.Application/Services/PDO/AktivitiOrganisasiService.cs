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
    public class AktivitiOrganisasiService : IAktivitiOrganisasiService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<AktivitiOrganisasiService> _logger;
        public AktivitiOrganisasiService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<AktivitiOrganisasiService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<AktivitiOrganisasiDto>> GetAktivitiOrganisasiAsync()
        {
            var result = await (from a in _context.PDOAktivitiOrganisasi
                                join b in _context.PDORujKategoriAktivitiOrganisasi
                                    on a.KodRujKategoriAktivitiOrganisasi equals b.Kod
                                where a.KodCartaAktiviti.StartsWith("01")
                                select new AktivitiOrganisasiDto
                                {
                                    Id = a.Id,
                                    IdIndukAktivitiOrganisasi = a.IdIndukAktivitiOrganisasi,
                                    KodProgram = b.Nama.ToUpper() + " " + a.KodProgram,
                                    Nama = a.Nama,
                                    Tahap = a.Tahap
                                }).ToListAsync();

            return result;
        }

        //Amar Code Start
        public async Task<List<StrukturAktivitiOrganisasiResponseDto>> GetTreeStrukturAktivitiOrganisasi(int IdAktivitiOrganisasi)
        {
            _logger.LogInformation("GetTreeStrukturAktivitiOrganisasi: Getting StrukturAktivitiOrganisasi with IdAktivitiOrganisasi: {IdAktivitiOrganisasi}", IdAktivitiOrganisasi);
            try
            {
                var sql = @"
            WITH ParentChain AS (
                SELECT *
                FROM PDO_AktivitiOrganisasi
                WHERE Id = {0}
                UNION ALL
                SELECT parent.*
                FROM PDO_AktivitiOrganisasi parent
                INNER JOIN ParentChain child ON parent.Id = child.IdIndukAktivitiOrganisasi
            ),
            FullTree AS (
                SELECT
                    Id,
                    IdIndukAktivitiOrganisasi,
                    Nama,
                    0 AS Level,
                    CAST(Nama AS VARCHAR(MAX)) AS FullPath
                FROM PDO_AktivitiOrganisasi
                WHERE Id = (SELECT TOP 1 Id FROM ParentChain WHERE IdIndukAktivitiOrganisasi = 0)
                UNION ALL
                SELECT
                    child.Id,
                    child.IdIndukAktivitiOrganisasi,
                    child.Nama,
                    parent.Level + 1,
                    CAST(parent.FullPath + ' > ' + child.Nama AS VARCHAR(MAX)) AS FullPath
                FROM PDO_AktivitiOrganisasi child
                INNER JOIN FullTree parent ON child.IdIndukAktivitiOrganisasi = parent.Id
            )
            SELECT * FROM FullTree
            ORDER BY FullPath";

                _logger.LogInformation("GetTreeStrukturAktivitiOrganisasi: Executing query to fetch StrukturAktivitiOrganisasi data");
                var data = await _context.Database.SqlQueryRaw<StrukturAktivitiOrganisasiTempDto>(sql, IdAktivitiOrganisasi).ToListAsync();
                _logger.LogInformation("GetTreeStrukturAktivitiOrganisasi: Retrieved {Count} records from database", data.Count);

                var result = data.Select((x, index) => new StrukturAktivitiOrganisasiResponseDto
                {

                    Id = x.Id,
                    IdIndukAktivitiOrganisasi = x.IdIndukAktivitiOrganisasi,
                    Nama = x.Nama ?? String.Empty,
                    Level = x.Level,
                    FullPath = x.FullPath ?? String.Empty
                }).ToList();

                _logger.LogInformation("GetTreeStrukturAktivitiOrganisasi: Successfully processed {Count} StrukturAktivitiOrganisasi records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetTreeStrukturAktivitiOrganisasi: Failed to retrieve StrukturAktivitiOrganisasi data with IdAktivitiOrganisasi: {IdAktivitiOrganisasi}", IdAktivitiOrganisasi);
                throw;
            }
        }

        public async Task<string> GetNamaAktivitiOrganisasi(int IdIndukAktivitiOrganisasi)
        {
            _logger.LogInformation("GetNamaAktivitiOrganisasi: Getting NamaAktivitiOrganisasi with IdIndukAktivitiOrganisasi: {IdIndukAktivitiOrganisasi}", IdIndukAktivitiOrganisasi);
            try
            {
                var result = await _context.PDOAktivitiOrganisasi
                    .Where(pao => pao.Id == IdIndukAktivitiOrganisasi)
                    .Select(pao => pao.Nama)
                    .FirstOrDefaultAsync();

                _logger.LogInformation("GetNamaAktivitiOrganisasi: Successfully retrieved NamaAktivitiOrganisasi");
                return result ?? String.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetNamaAktivitiOrganisasi: Failed to retrieve NamaAktivitiOrganisasi with IdIndukAktivitiOrganisasi: {IdIndukAktivitiOrganisasi}", IdIndukAktivitiOrganisasi);
                throw;
            }
        }

        public async Task<bool> SetPenjenamaanSemula(PenjenamaanSemulaRequestDto penjenamaanSemulaRequestDto)
        {
            _logger.LogInformation("Service: Updating PenjenamaanSemula");
            await _unitOfWork.BeginTransactionAsync();
            try
            {

                // Step 1: Update PDO_AktivitiOrganisasi
                var aktivitiOrganisasi = await _unitOfWork.Repository<PDOAktivitiOrganisasi>()
                    .FirstOrDefaultAsync(x => x.Id == penjenamaanSemulaRequestDto.IdAktivitiOrganisasi);



                if (aktivitiOrganisasi != null)
                {
                    aktivitiOrganisasi.Nama = penjenamaanSemulaRequestDto.NamaAktivitiOrganisasiBaharu;

                    await _unitOfWork.Repository<PDOAktivitiOrganisasi>().UpdateAsync(aktivitiOrganisasi);
                    await _unitOfWork.SaveChangesAsync();
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service SetPenjenamaanSemula");
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }
        public async Task<List<StrukturAktivitiOrganisasiResponseDto>> GetPindahAktivitiOrganisasi(int IdAktivitiOrganisasi)
        {
            _logger.LogInformation("GetPindahAktivitiOrganisasi: Getting StrukturAktivitiOrganisasi with IdAktivitiOrganisasi: {IdAktivitiOrganisasi}", IdAktivitiOrganisasi);
            try
            {
                var sql = @"
            WITH ParentChain AS (
                SELECT *
                FROM PDO_AktivitiOrganisasi
                WHERE Id = {0}
                UNION ALL
                SELECT parent.*
                FROM PDO_AktivitiOrganisasi parent
                INNER JOIN ParentChain child ON parent.Id = child.IdIndukAktivitiOrganisasi
            ),
            FullTree AS (
                SELECT
                    Id,
                    IdIndukAktivitiOrganisasi,
                    Nama,
                    0 AS Level,
                    CAST(Nama AS VARCHAR(MAX)) AS FullPath
                FROM PDO_AktivitiOrganisasi
                WHERE Id = (SELECT TOP 1 Id FROM ParentChain WHERE IdIndukAktivitiOrganisasi = 0)
                UNION ALL
                SELECT
                    child.Id,
                    child.IdIndukAktivitiOrganisasi,
                    child.Nama,
                    parent.Level + 1,
                    CAST(parent.FullPath + ' > ' + child.Nama AS VARCHAR(MAX)) AS FullPath
                FROM PDO_AktivitiOrganisasi child
                INNER JOIN FullTree parent ON child.IdIndukAktivitiOrganisasi = parent.Id
            )
            SELECT * FROM FullTree
            ORDER BY FullPath;";

                _logger.LogInformation("GetPindahAktivitiOrganisasi: Executing query to fetch StrukturAktivitiOrganisasi data");
                var data = await _context.Database.SqlQueryRaw<StrukturAktivitiOrganisasiTempDto>(sql, IdAktivitiOrganisasi).ToListAsync();
                _logger.LogInformation("GetPindahAktivitiOrganisasi: Retrieved {Count} records from database", data.Count);

                var result = data.Select((x, index) => new StrukturAktivitiOrganisasiResponseDto
                {
                    Id = x.Id,
                    IdIndukAktivitiOrganisasi = x.IdIndukAktivitiOrganisasi,
                    Nama = x.Nama ?? String.Empty,
                    Level = x.Level,
                    FullPath = x.FullPath ?? String.Empty
                }).ToList();

                _logger.LogInformation("GetPindahAktivitiOrganisasi: Successfully processed {Count} StrukturAktivitiOrganisasi records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetPindahAktivitiOrganisasi: Failed to retrieve StrukturAktivitiOrganisasi data with IdAktivitiOrganisasi: {IdAktivitiOrganisasi}", IdAktivitiOrganisasi);
                throw;
            }
        }

        public async Task<object> GetNamaKodAktivitiOrganisasi(int IdIndukAktivitiOrganisasi)
        {
            _logger.LogInformation("GetNamaKodAktivitiOrganisasi: Getting NamaKodAktivitiOrganisasi with IdIndukAktivitiOrganisasi: {IdIndukAktivitiOrganisasi}", IdIndukAktivitiOrganisasi);
            try
            {
                var result = await _context.PDOAktivitiOrganisasi
                    .Where(pao => pao.Id == IdIndukAktivitiOrganisasi)
                    .Select(pao => new
                    {
                        KodAktivitiOrganisasi = pao.Kod,
                        NamaAktivitiOrganisasi = pao.Nama
                    })
                    .FirstOrDefaultAsync();

                _logger.LogInformation("GetNamaKodAktivitiOrganisasi: Successfully retrieved NamaKodAktivitiOrganisasi");
                return result ?? new { KodAktivitiOrganisasi = String.Empty, NamaAktivitiOrganisasi = String.Empty };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetNamaKodAktivitiOrganisasi: Failed to retrieve NamaKodAktivitiOrganisasi with IdIndukAktivitiOrganisasi: {IdIndukAktivitiOrganisasi}", IdIndukAktivitiOrganisasi);
                throw;
            }
        }

        public async Task<bool> SetAktivitiOrganisasi(AktivitiOrganisasiRequestDto aktivitiOrganisasiRequestDto)
        {
            _logger.LogInformation("Service: Updating AktivitiOrganisasi");
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Step 1: Get all records with OldParentId using FindByFieldAsync
                var aktivitiOrganisasiList = await _unitOfWork.Repository<PDOAktivitiOrganisasi>()
                    .FindByFieldAsync("IdIndukAktivitiOrganisasi", aktivitiOrganisasiRequestDto.OldParentId);

                if (aktivitiOrganisasiList.Any())
                {
                    // Step 2: Update each record's IdIndukAktivitiOrganisasi
                    foreach (var aktivitiOrganisasi in aktivitiOrganisasiList)
                    {
                        aktivitiOrganisasi.IdIndukAktivitiOrganisasi = aktivitiOrganisasiRequestDto.NewParentId;
                        await _unitOfWork.Repository<PDOAktivitiOrganisasi>().UpdateAsync(aktivitiOrganisasi);
                    }

                    await _unitOfWork.SaveChangesAsync();

                    _logger.LogInformation("SetAktivitiOrganisasi: Successfully updated {Count} AktivitiOrganisasi records", aktivitiOrganisasiList.Count());
                }
                else
                {
                    _logger.LogWarning("SetAktivitiOrganisasi: No records found with OldParentId: {OldParentId}", aktivitiOrganisasiRequestDto.OldParentId);
                    await _unitOfWork.RollbackAsync();
                    return false;
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service SetAktivitiOrganisasi");
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }
        //Amar Code End
    }
}
