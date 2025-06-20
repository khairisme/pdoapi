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
    public class JawatanService : IJawatanService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<GredService> _logger;

        public JawatanService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<GredService> logger)
        {
            _context = dbContext;
        }

        public async Task<List<JawatanWithAgensiDto>> GetJawatanWithAgensiAsync(string namaJwtn, string kodCartaOrganisasi)
        {
            var query = from a in _context.PDOJawatan
                        join b in _context.PDOUnitOrganisasi on a.IdUnitOrganisasi equals b.Id
                        where a.StatusAktif && b.StatusAktif &&
                              a.Nama.Contains(namaJwtn) &&
                              b.KodCartaOrganisasi.Contains(kodCartaOrganisasi)
                        select new JawatanWithAgensiDto
                        {
                            Id = a.Id,
                            Kod = a.Kod,
                            Nama = a.Nama,
                            Agensi = b.Nama
                        };

            return await query.ToListAsync();
        }
    }

}
