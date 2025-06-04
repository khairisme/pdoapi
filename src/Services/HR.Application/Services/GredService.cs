using HR.Application.DTOs;
using HR.Application.Extensions;
using HR.Application.Interfaces;
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

        public async Task<List<GredSearchResultDTO>> SearchGredAsync(int? idKlasifikasi, int? idKumpulan)
        {
            _logger.LogInformation("Search  Gred using EF Core join");
            try
            {

                var query = from a in _context.PDOKumpulanPerkhidmatan
                            join b in _context.PDOStatusPermohonanGred on a.Id equals b.IdGred
                            join b2 in _context.PDORujStatusPermohonan on b.KodRujStatusPermohonan equals b2.Kod
                            where b.StatusAktif == true
                            select new
                            {
                                a.Kod,
                                a.Nama,
                                a.Keterangan,
                                a.StatusAktif,
                                StatusPermohonan = b2.Nama,
                                b.TarikhKemasKini,
                                // a.IdKlasifikasiPerkhidmatan,
                                a.Id
                            };

                //if (idKlasifikasi.HasValue)
                //    query = query.Where(x => x.IdKlasifikasiPerkhidmatan == idKlasifikasi);

                if (idKumpulan.HasValue)
                    query = query.Where(x => x.Id == idKumpulan);

                var result = await query.OrderBy(x => x.Kod).ToListAsync();

                return result.Select((x, index) => new GredSearchResultDTO
                {
                    Bil = index + 1,
                    Kod = x.Kod,
                    Nama = x.Nama,
                    Keterangan = x.Keterangan,
                    StatusGred = (x.StatusAktif
                                ? StatusKumpulanPerkhidmatanEnum.Aktif
                                : StatusKumpulanPerkhidmatanEnum.TidakAktif).ToDisplayString(),
                    StatusPermohonan = x.StatusPermohonan,
                    TarikhKemaskini = x.TarikhKemasKini
                }).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrive data");
            }
        }
    }
}
