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
    public class UnitOrganisasiService : IUnitOrganisasiService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<UnitOrganisasiService> _logger;
        public UnitOrganisasiService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<UnitOrganisasiService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<UnitOrganisasiDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all UnitOrganisasiDto using Entity Framework");
            var result = await _unitOfWork.Repository<PDOUnitOrganisasi>().GetAllAsync();
            result = result.ToList().Where(e => e.StatusAktif);
            return result.Select(MapToDto);
        }

        private UnitOrganisasiDto MapToDto(PDOUnitOrganisasi pDOUnitOrganisasi)
        {
            return new UnitOrganisasiDto
            {
                Id = pDOUnitOrganisasi.Id,
                Nama = pDOUnitOrganisasi.Nama,
                KodCartaOrganisasi = pDOUnitOrganisasi.KodCartaOrganisasi
            };
        }
        public async Task<List<UnitOrganisasiKementerianDto>> GetUnitOrganisasiByKategoriAsync()
        {
            var result = await (from puo in _context.PDOUnitOrganisasi
                                join prkuo in _context.PDORujKategoriUnitOrganisasi
                                    on puo.KodRujKategoriUnitOrganisasi equals prkuo.Kod
                                join prja in _context.PDORujJenisAgensi
                                    on puo.KodRujJenisAgensi equals prja.Kod
                                where puo.StatusAktif && prkuo.Kod == "0001"
                                select new UnitOrganisasiKementerianDto
                                {
                                    Kod = puo.Kod,
                                    Nama = puo.Nama
                                }).ToListAsync();

            return result;
        }

        public async Task<List<UnitOrganisasiAutocompleteDto>> SearchUnitOrganisasiAsync(string keyword)
        {
            return await _context.PDOUnitOrganisasi
                .Where(x => x.Nama.Contains(keyword))
                .Select(x => new UnitOrganisasiAutocompleteDto
                {
                    Kod = x.Kod,
                    Nama = x.Nama
                })
                .ToListAsync();
        }
    }
}
