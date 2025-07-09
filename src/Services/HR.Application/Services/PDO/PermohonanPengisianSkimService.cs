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
    }
}
