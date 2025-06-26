using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
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
    }
}
