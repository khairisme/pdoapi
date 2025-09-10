using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.Services.PDO
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
                        where a.StatusAktif == true && b.StatusAktif == true &&
                              a.Nama.Contains(namaJwtn.Trim()) &&
                              b.KodCartaOrganisasi.Contains(kodCartaOrganisasi)
                        select new JawatanWithAgensiDto
                        {
                            Id = a.Id,
                            Kod = a.Kod.Trim(),
                            Nama = a.Nama.Trim(),
                            Agensi = b.Nama.Trim()
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
                                  && (string.IsNullOrEmpty(filter.NamaJawatan.Trim()) || a.Nama.Contains(filter.NamaJawatan.Trim()))
                                  && (string.IsNullOrEmpty(filter.UnitOrganisasi.Trim()) || f.Nama.Contains(filter.UnitOrganisasi.Trim()))
                            orderby a.Kod
                            select new
                            {
                                a.Id,
                                a.Kod,
                                NamaJawatan = a.Nama.Trim(),
                                UnitOrganisasi = f.Nama.Trim(),
                                StatusPengisian = e2.Nama.Trim(),
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
                                  && (string.IsNullOrEmpty(filter.NamaJawatanSebenar.Trim()) || a.Nama.Contains(filter.NamaJawatanSebenar.Trim()))
                                  && (string.IsNullOrEmpty(filter.StatusKekosonganJawatan) || e2.Kod == filter.StatusKekosonganJawatan)
                                  && (filter.UnitOrganisasiId == null || a.IdUnitOrganisasi == filter.UnitOrganisasiId)
                            orderby a.Kod
                            select new
                            {
                                a.Id,
                                Kod=a.Kod.Trim(),
                                NamaJawatan = a.Nama.Trim(),
                                UnitOrganisasi = f.Nama.Trim(),
                                StatusPengisian = e2.Nama.Trim(),
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

        // added by amar 220725
        public async Task<List<CarianKetuaPerkhidmatanResponseDto>> GetNamaJawatan(string KodCarta)
        {
            _logger.LogInformation("GetNamaJawatan: Fetching Ketua Perkhidmatan Jawatan for KodCarta = {KodCarta}", KodCarta);

            try
            {
                var query = from a in _context.PDOJawatan
                            join b in _context.PDOUnitOrganisasi
                                on a.IdUnitOrganisasi equals b.Id
                            where a.StatusAktif == true
                                  && a.IndikatorKetuaPerkhidmatan == true
                                  && b.StatusAktif == true
                                  && b.KodCartaOrganisasi.Contains(KodCarta)
                            select new CarianKetuaPerkhidmatanResponseDto
                            {
                                Id = a.Id,
                                Nama = (a.Nama ?? string.Empty).Trim()
                            };

                var result = await query.ToListAsync();

                _logger.LogInformation("GetNamaJawatan: Retrieved {Count} Ketua Perkhidmatan records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetNamaJawatan: Error occurred while fetching Ketua Perkhidmatan for KodCarta = {KodCarta}", KodCarta);
                throw;
            }
        }



        public async Task<List<SenaraiKetuaPerkhidmatanResponseDto>> GetSenaraiKetuaPerkhidmatan(string? NamaJawatan, string KodCartaOrganisasi)
        {
            _logger.LogInformation("GetSenaraiKetuaPerkhidmatan: Fetching Ketua Perkhidmatan for KodCartaOrganisasi = {KodCartaOrganisasi}", KodCartaOrganisasi);
            var kodCartaOrganisasi = KodCartaOrganisasi.Substring(0,KodCartaOrganisasi.Length - 2);

            try
            {
                var query = from a in _context.PDOJawatan
                            join b in _context.PDOUnitOrganisasi
                                on a.IdUnitOrganisasi equals b.Id
                            where a.StatusAktif == true
                                  && a.IndikatorKetuaPerkhidmatan == true
                                  && b.StatusAktif == true
                                  && b.KodCartaOrganisasi.StartsWith(kodCartaOrganisasi)
                                  && (string.IsNullOrEmpty(NamaJawatan.Trim()) || a.Nama.Contains(NamaJawatan.Trim()))
                            select new SenaraiKetuaPerkhidmatanResponseDto
                            {
                                Id = a.Id,
                                Kod = a.Kod ?? string.Empty,
                                Jawatan = (a.Nama ?? string.Empty).Trim(),
                                Agensi = (b.Nama ?? string.Empty).Trim()
                            };

                var result = await query.ToListAsync();

                _logger.LogInformation("GetSenaraiKetuaPerkhidmatan: Retrieved {Count} records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetSenaraiKetuaPerkhidmatan: Error occurred while fetching data for KodCartaOrganisasi = {KodCartaOrganisasi}", KodCartaOrganisasi);
                throw;
            }
        }


        //end
    }




}
