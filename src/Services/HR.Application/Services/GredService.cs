using HR.Application.DTOs;
using HR.Application.Interfaces;
using HR.Core.Interfaces;
using HR.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Services
{
    public class GredService : IGredService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<GredService> _logger;
        public GredService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<GredService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }
        public async Task<List<PDOGredDto>> GetGredListAsync(int idKlasifikasi, int idKumpulan)
        {
            _logger.LogInformation("Getting all PDOGredDto using EF Core join");
            try
            {



                var query = from a in _context.PDOGred
                            join b in _context.PDOKlasifikasiPerkhidmatan on a.IdKlasifikasiPerkhidmatan equals b.Id
                            join c in _context.PDOKumpulanPerkhidmatan on a.IdKumpulanPerkhidmatan equals c.Id
                            where a.IdKlasifikasiPerkhidmatan == idKlasifikasi
                                  && a.IdKumpulanPerkhidmatan == idKumpulan
                                  && b.StatusAktif
                                  && c.StatusAktif
                            orderby a.Kod
                            select new
                            {
                                a.Kod,
                                a.Nama,
                                a.Keterangan
                            };

                var result = await query.ToListAsync();

                return result.Select((x, index) => new PDOGredDto
                {
                    Bil = index + 1,
                    Kod = x.Kod,
                    Nama = x.Nama,
                    Keterangan = x.Keterangan
                }).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrive data");
            }
        }
    }
}
