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
    public class PermohonanPengisianSkimService:IPermohonanPengisianSkimService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<PermohonanPengisianSkimService> _logger;

        public PermohonanPengisianSkimService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<PermohonanPengisianSkimService> logger)
        {
            _context = dbContext;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<List<PegawaiTeknologiMaklumatResponseDto>> GetPegawaiTeknologiMaklumat(int IdSkimPerkhidmatan, int IdPermohonanPengisianSkim)
        {
            _logger.LogInformation("GetPegawaiTeknologiMaklumat: Getting PegawaiTeknologiMaklumat with IdSkimPerkhidmatan: {IdSkimPerkhidmatan}, IdPermohonanPengisian: {IdPermohonanPengisian}", IdSkimPerkhidmatan, IdPermohonanPengisianSkim);
            try
            {
                var query = from ppps in _context.PDOPermohonanPengisianSkim
                            join ppj in _context.PDOPengisianJawatan on ppps.Id equals ppj.IdPermohonanPengisianSkim
                            join b in _context.PDOJawatan on ppj.IdJawatan equals b.Id into bGroup
                            from b in bGroup.Where(x => x.StatusAktif == true)
                            join c in _context.PDOUnitOrganisasi on b.IdUnitOrganisasi equals c.Id
                            join d in _context.PDOKekosonganJawatan on b.Id equals d.IdJawatan
                            join e in _context.PDORujStatusKekosonganJawatan on d.KodRujStatusKekosonganJawatan equals e.Kod
                            join f in _context.PDOGredSkimJawatan on ppj.IdJawatan equals f.IdJawatan
                            where ppps.Id == IdPermohonanPengisianSkim
                                  && ppps.IdSkimPerkhidmatan == IdSkimPerkhidmatan
                            orderby b.Kod
                            select new
                            {
                                ppj.Id,
                                KodJawatan = b.Kod,
                                NamaJawatan = b.Nama,
                                UnitOrganisasi = c.Nama,
                                StatusPengisianJawatan = e.Nama,
                                TarikhKekosonganJawatan = d.TarikhStatusKekosongan
                            };
                _logger.LogInformation("GetPegawaiTeknologiMaklumat: Executing query to fetch PegawaiTeknologiMaklumat data");
                var data = await query.ToListAsync();

                _logger.LogInformation("GetPegawaiTeknologiMaklumat: Retrieved {Count} records from database", data.Count);

                var result = data.Select((x, index) => new PegawaiTeknologiMaklumatResponseDto
                {
                    Bil = index + 1,
                    Id = x.Id,
                    KodJawatan = x.KodJawatan ?? String.Empty,
                    NamaJawatan = x.NamaJawatan ?? String.Empty,
                    UnitOrganisasi = x.UnitOrganisasi ?? String.Empty,
                    StatusPengisianJawatan = x.StatusPengisianJawatan ?? String.Empty,
                    TarikhKekosonganJawatan = x.TarikhKekosonganJawatan
                }).ToList();

                _logger.LogInformation("GetPegawaiTeknologiMaklumat: Successfully processed {Count} PegawaiTeknologiMaklumat records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetPegawaiTeknologiMaklumat: Failed to retrieve PegawaiTeknologiMaklumat data with IdSkimPerkhidmatan: {IdSkimPerkhidmatan}, IdPermohonanPengisian: {IdPermohonanPengisian}", IdSkimPerkhidmatan, IdPermohonanPengisianSkim);
                throw;
            }
        }

        public async Task<BilanganPengisianHadSilingResponseDto> GetBilanganPengisianHadSiling(int IdPermohonanPengisian, int IdPermohonanPengisianSkim)
        {
            _logger.LogInformation("GetBilanganPengisianHadSilingAsync: Getting BilanganPengisian summary with IdPermohonanPengisian: {IdPermohonanPengisian}, IdPermohonanPengisianSkim: {IdPermohonanPengisianSkim}", IdPermohonanPengisian, IdPermohonanPengisianSkim);
            try
            {
                var query = from ppps in _context.PDOPermohonanPengisianSkim
                            where ppps.IdPermohonanPengisian == IdPermohonanPengisian
                                  && ppps.Id == IdPermohonanPengisianSkim
                            select new
                            {
                                ppps.BilanganPengisian,
                                ppps.BilanganHadSIling
                            };

                _logger.LogInformation("GetBilanganPengisianHadSilingAsync: Executing query to fetch BilanganPengisian data");
                var data = await query.ToListAsync();

                _logger.LogInformation("GetBilanganPengisianHadSilingAsync: Retrieved {Count} records from database", data.Count);

                var result = new BilanganPengisianHadSilingResponseDto
                {
                    JumlahBilanganPengisian = data.Sum(x => x.BilanganPengisian),
                    HadSilingDitetapkan = data.Sum(x => x.BilanganHadSIling)
                };

                _logger.LogInformation("GetBilanganPengisianHadSilingAsync: Successfully processed BilanganPengisian summary - JumlahBilanganPengisian: {JumlahBilanganPengisian}, HadSilingDitetapkan: {HadSilingDitetapkan}", result.JumlahBilanganPengisian, result.HadSilingDitetapkan);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBilanganPengisianHadSilingAsync: Failed to retrieve BilanganPengisian summary with IdPermohonanPengisian: {IdPermohonanPengisian}, IdPermohonanPengisianSkim: {IdPermohonanPengisianSkim}", IdPermohonanPengisian, IdPermohonanPengisianSkim);
                throw;
            }
        }
        public async Task<PaparPermohonanDanSilingResponseDto?> GetJumlahDanSilingAsync(PaparPermohonanDanSilingRequestDto request)
        {
            var result = await _context.PDOPermohonanPengisianSkim
                .Where(x => x.IdPermohonanPengisian == request.IdPermohonanPengisian
                         && x.Id == request.IdPermohonanPengisianSkim)
                .GroupBy(x => 1)
                .Select(g => new PaparPermohonanDanSilingResponseDto
                {
                    JumlahBilanganPengisian = g.Sum(x => x.BilanganPengisian),
                    HadSilingDitetapkan = g.Sum(x => x.BilanganHadSIling)
                })
                .FirstOrDefaultAsync();

            return result;
        }



        //Nitya Code Start
        public async Task<int> GetBilanganPengisianByIdAsync(int idPermohonanPengisian)
        {
            return await _context.PDOPermohonanPengisianSkim
                .Where(ppps => ppps.IdPermohonanPengisian == idPermohonanPengisian)
                .CountAsync();
        }
        public async Task<bool> UpdateUlasanAndHadSilingAsync(CombinedUpdateRequestDto request)
        {
            // 1. Update Ulasan
            var ulasanRecord = await _context.PDOPermohonanPengisianSkim
                .FirstOrDefaultAsync(x => x.Id == request.IdPermohonanPengisianSkim &&
                                          x.IdPermohonanPengisian == request.IdPermohonanPengisian);

            if (ulasanRecord == null) return false;

            ulasanRecord.Ulasan = request.Ulasan;

            // 2. Update Grid Items (BilanganHadSiling)
            var ids = request.GridItems.Select(x => x.RecordId).ToList();
            var gridRecords = await _context.PDOPermohonanPengisianSkim
                .Where(x => ids.Contains(x.Id)).ToListAsync();

            foreach (var dto in request.GridItems)
            {
                var record = gridRecords.FirstOrDefault(x => x.Id == dto.RecordId);
                if (record != null)
                {
                    record.BilanganHadSIling = dto.BilanganHadSiling;
                    record.IdPinda = request.UserId;
                    record.TarikhPinda = DateTime.Now;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
        //Nitya Code End
    }
}
