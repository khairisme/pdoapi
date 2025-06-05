using HR.Application.DTOs;
using HR.Application.DTOs.PDO;
using HR.Application.Extensions;
using HR.Application.Interfaces.PDO;
using HR.Core.Enums;
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
        public async Task<List<PDOGredDto>> GetGredListAsync(GredFilterDto filter)
        {
            _logger.LogInformation("Getting all PDOGredDto using EF Core join");
            try
            {



                var query = from a in _context.PDOGred
                            join b in _context.PDOKlasifikasiPerkhidmatan on a.IdKlasifikasiPerkhidmatan equals b.Id
                            join c in _context.PDOKumpulanPerkhidmatan on a.IdKumpulanPerkhidmatan equals c.Id
                            where a.IdKlasifikasiPerkhidmatan == filter.IdKlasifikasiPerkhidmatan
                                  && a.IdKumpulanPerkhidmatan == filter.IdKumpulanPerkhidmatan
                                  && b.StatusAktif
                                  && c.StatusAktif
                            orderby a.Kod
                            select new
                            {
                                a.Kod,
                                a.Nama,
                                a.Keterangan
                            };

                if (!string.IsNullOrEmpty(filter.Nama))
                    query = query.Where(x => x.Nama.Contains(filter.Nama));
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

        public async Task<List<GredResultDto>> GetFilteredGredList(GredFilterDto filter)
        {
            _logger.LogInformation("Search  Gred using EF Core join");
            try
            {

                var query = from a in _context.PDOGred
                            join b in _context.PDOStatusPermohonanGred on a.Id equals b.IdGred
                            join d in _context.PDOKlasifikasiPerkhidmatan on a.IdKlasifikasiPerkhidmatan equals d.Id
                            join c in _context.PDOKumpulanPerkhidmatan on a.IdKumpulanPerkhidmatan equals c.Id
                            join b2 in _context.PDORujStatusPermohonan on b.KodRujStatusPermohonan equals b2.Kod
                            where b.StatusAktif == true && c.StatusAktif == true
                            select new
                            {
                                a.Kod,
                                a.Nama,
                                a.Keterangan,
                                StatusPermohonan = b2.Nama,
                                StatusGred = a.StatusAktif == true ? "Aktif" : "Tidak Aktif",
                                a.IdKumpulanPerkhidmatan,
                                a.IdKlasifikasiPerkhidmatan,
                                b.KodRujStatusPermohonan
                            };

                if (filter.IdKumpulanPerkhidmatan.HasValue)
                    query = query.Where(x => x.IdKumpulanPerkhidmatan == filter.IdKumpulanPerkhidmatan);

                if (filter.IdKlasifikasiPerkhidmatan.HasValue)
                    query = query.Where(x => x.IdKlasifikasiPerkhidmatan == filter.IdKlasifikasiPerkhidmatan);

                if (!string.IsNullOrEmpty(filter.KodRujStatusPermohonan))
                    query = query.Where(x => x.KodRujStatusPermohonan == filter.KodRujStatusPermohonan);

                if (!string.IsNullOrEmpty(filter.Nama))
                    query = query.Where(x => x.Nama.Contains(filter.Nama));

                var data = await query.ToListAsync();

                // Add row number manually
                var result = data.Select((x, index) => new GredResultDto
                {
                    Bil = index + 1,
                    Kod = x.Kod,
                    Nama = x.Nama,
                    Keterangan = x.Keterangan,
                    StatusPermohonan = x.StatusPermohonan,
                    StatusGred = x.StatusGred
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrive data");
            }
        }
    }
}
