using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Core.Interfaces;
using HR.Infrastructure.Data.EntityFramework;
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

    }
}
