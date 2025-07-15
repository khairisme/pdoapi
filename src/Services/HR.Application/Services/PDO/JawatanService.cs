using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Core.Entities.PDO;
using HR.Core.Interfaces;
using HR.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
            _unitOfWork = unitOfWork;
            _logger = logger;
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
        public async Task<List<CarianJawatanResponseDto>> GetCarianJawatanAsync(CarianJawatanFilterDto filter)
        {
            _logger.LogInformation("GetCarianJawatanAsync: Getting CarianJawatanSebenar with filter: {@Filter}", filter);
            try
            {
                var query = from a in _context.PDOJawatan
                            join b in _context.PDOGredSkimJawatan on a.Id equals b.IdJawatan
                            join c in _context.PDOSkimPerkhidmatan on b.IdSkimPerkhidmatan equals c.Id
                            join d in _context.PDOGred on b.IdGred equals d.Id
                            join e in _context.PDOKekosonganJawatan on a.Id equals e.IdJawatan
                            join e2 in _context.PDORujStatusKekosonganJawatan on e.KodRujStatusKekosonganJawatan equals e2.Kod
                            join f in _context.PDOUnitOrganisasi on a.IdUnitOrganisasi equals f.Id
                            from ppps in _context.PDOPermohonanPengisianSkim
                                .Where(x => x.IdSkimPerkhidmatan == c.Id).DefaultIfEmpty()
                            from ppj in _context.PDOPengisianJawatan
                                .Where(x => x.IdJawatan == a.Id && x.IdPermohonanPengisianSkim == ppps.Id).DefaultIfEmpty()
                            where d.StatusAktif == true
                                && d.IndikatorGredLantikanTerus == true
                                && e.StatusAktif == true
                                && e.KodRujStatusKekosonganJawatan == "01"
                                                  && c.Id == filter.SkimPerkhidmatanId
                                  && (string.IsNullOrEmpty(filter.NamaJawatan) || a.Nama.Contains(filter.NamaJawatan))
                                  && (string.IsNullOrEmpty(filter.UnitOrganisasi) || f.Nama.Contains(filter.UnitOrganisasi))
                            orderby a.Kod
                            select new
                            {
                                a.Id,
                                a.Kod,
                                NamaJawatan = a.Nama,
                                UnitOrganisasi = f.Nama,
                                StatusPengisian = e2.Nama,
                                e.TarikhStatusKekosongan,
                                TickCheckBox = ppj != null
                            };
                _logger.LogInformation("GetCarianJawatanAsync: Executing query to fetch CarianJawatan data");

                var data = await query.ToListAsync();

                _logger.LogInformation("GetCarianJawatanAsync: Retrieved {Count} records from database", data.Count);

                var result = data.Select((x, index) => new CarianJawatanResponseDto
                {
                    Bil = index + 1,
                    Id = x.Id,
                    Kod = x.Kod,
                    NamaJawatan = x.NamaJawatan,
                    UnitOrganisasi = x.UnitOrganisasi,
                    StatusPengisian = x.StatusPengisian,
                    TarikhStatusKekosongan = x.TarikhStatusKekosongan
                }).ToList();

                _logger.LogInformation("GetCarianJawatanAsync: Successfully processed {Count} CarianJawatan records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetCarianJawatanAsync: Failed to retrieve CarianJawatan data with filter: {@Filter}", filter);
                throw new Exception("Failed to retrive data");
            }
        }

        public async Task<List<CarianJawatanSebenarResponseDto>> GetCarianJawatanSebenar(CarianJawatanSebenarFilterDto filter)
        {
            _logger.LogInformation("GetCarianJawatanSebenar: Getting CarianJawatanSebenar with filter: {@Filter}", filter);
            try
            {
                var query = from a in _context.PDOJawatan
                            join b in _context.PDOGredSkimJawatan on a.Id equals b.IdJawatan
                            join c in _context.PDOSkimPerkhidmatan on b.IdSkimPerkhidmatan equals c.Id
                            join d in _context.PDOGred on b.IdGred equals d.Id
                            join e in _context.PDOKekosonganJawatan on a.Id equals e.IdJawatan
                            join e2 in _context.PDORujStatusKekosonganJawatan on e.KodRujStatusKekosonganJawatan equals e2.Kod
                            join f in _context.PDOUnitOrganisasi on a.IdUnitOrganisasi equals f.Id
                            from ppps in _context.PDOPermohonanPengisianSkim
                                .Where(x => x.IdSkimPerkhidmatan == c.Id).DefaultIfEmpty()
                            from ppj in _context.PDOPengisianJawatan
                                .Where(x => x.IdJawatan == a.Id && x.IdPermohonanPengisianSkim == ppps.Id).DefaultIfEmpty()
                            where d.StatusAktif == true
                                && d.IndikatorGredLantikanTerus == true
                                && e.StatusAktif == true
                                && e.KodRujStatusKekosonganJawatan == "01"
                                  && (filter.SkimPerkhidmatanId == null || c.Id == filter.SkimPerkhidmatanId)
                                  && (string.IsNullOrEmpty(filter.KodJawatanSebenar) || a.Kod == filter.KodJawatanSebenar)
                                  && (string.IsNullOrEmpty(filter.NamaJawatanSebenar) || a.Nama.Contains(filter.NamaJawatanSebenar))
                                  && (string.IsNullOrEmpty(filter.StatusKekosonganJawatan) || e2.Kod == filter.StatusKekosonganJawatan)
                                  && (filter.UnitOrganisasiId == null || a.IdUnitOrganisasi == filter.UnitOrganisasiId)
                            orderby a.Kod
                            select new
                            {
                                a.Id,
                                a.Kod,
                                NamaJawatan = a.Nama,
                                UnitOrganisasi = f.Nama,
                                StatusPengisian = e2.Nama,
                                e.TarikhStatusKekosongan,
                                TickCheckBox = ppj != null
                            };

                _logger.LogInformation("GetCarianJawatanSebenar: Executing query to fetch CarianJawatanSebenar data");
                var data = await query.ToListAsync();

                _logger.LogInformation("GetCarianJawatanSebenar: Retrieved {Count} records from database", data.Count);

                var result = data.Select((x, index) => new CarianJawatanSebenarResponseDto
                {
                    Bil = index + 1,
                    Id = x.Id,
                    Kod = x.Kod ?? String.Empty,
                    NamaJawatan = x.NamaJawatan ?? String.Empty,
                    UnitOrganisasi = x.UnitOrganisasi ?? String.Empty,
                    StatusPengisian = x.StatusPengisian ?? String.Empty,
                    TarikhStatusKekosongan = x.TarikhStatusKekosongan
                }).ToList();

                _logger.LogInformation("GetCarianJawatanSebenar: Successfully processed {Count} CarianJawatanSebenar records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetCarianJawatanSebenar: Failed to retrieve CarianJawatanSebenar data with filter: {@Filter}", filter);
                throw;
            }
        }

        public async Task<List<CarianJawatanSebenarRespDto>> SearchCarianawatanSebenarAsync(CarianJawatanSebenarReqDto request)
        {
            var result = await (
                from pj in _context.PDOJawatan
                join pgsj in _context.PDOGredSkimJawatan on pj.Id equals pgsj.IdJawatan
                join psp in _context.PDOSkimPerkhidmatan on pgsj.IdSkimPerkhidmatan equals psp.Id
                join pg in _context.PDOGred on pgsj.IdGred equals pg.Id
                join pkj in _context.PDOKekosonganJawatan on pj.Id equals pkj.IdJawatan
                join e2 in _context.PDORujStatusKekosonganJawatan on pkj.KodRujStatusKekosonganJawatan equals e2.Kod
                join puo in _context.PDOUnitOrganisasi on pj.IdUnitOrganisasi equals puo.Id
                where pg.StatusAktif == true
                    && pg.IndikatorGredLantikanTerus == true
                    && pkj.StatusAktif == true
                    && pkj.KodRujStatusKekosonganJawatan == "01"
                    && psp.Id == request.IdSkimPerkhidmatan
                    && puo.KodCartaOrganisasi.Contains(request.KodCarta)
                    && (string.IsNullOrEmpty(request.KodJawatanSebenar) || pj.Kod == request.KodJawatanSebenar)
                    && (string.IsNullOrEmpty(request.StatusKekosonganJawatan) || e2.Kod == request.StatusKekosonganJawatan)
                    && (request.UnitOrganisasi == null || puo.Id == request.UnitOrganisasi)
                select new CarianJawatanSebenarRespDto
                {
                    Id = pj.Id,
                    Kod = pj.Kod,
                    NamaJawatan = pj.Nama,
                    UnitOrganisasi = puo.Nama,
                    StatusPengisian = e2.Nama,
                    TarikhKekosonganJawatan = pkj.TarikhStatusKekosongan
                }
            ).ToListAsync();

            return result;
        }
    }




}
