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
        public async Task<List<CarianJawatanResponseDto>> GetCarianJawatanAsync(CarianJawatanFilterDto filter)
        {
            _logger.LogInformation("Getting all CarianJawatanResponseDto using EF Core join");
            try
            {
                var query = from a in _context.PDOJawatan
                            join b in _context.PDOGredSkimJawatan on a.Id equals b.IdJawatan
                            join c in _context.PDOSkimPerkhidmatan on b.IdSkimPerkhidmatan equals c.Id
                            join d in _context.PDOGred on b.IdGred equals d.Id into dGroup
                            from d in dGroup.Where(x => x.StatusAktif == true && x.IndikatorGredLantikanTerus == true)
                            join e in _context.PDOKekosonganJawatan on a.Id equals e.IdJawatan into eGroup
                            from e in eGroup.Where(x => x.StatusAktif == true)
                            join e2 in _context.PDORujStatusKekosonganJawatan on e.KodRujStatusKekosonganJawatan equals e2.Kod
                            join f in _context.PDOUnitOrganisasi on a.IdUnitOrganisasi equals f.Id
                            where e.KodRujStatusKekosonganJawatan == "01"
                                  && c.Id == filter.Id
                               
                            select new
                            {
                                a.Id,
                                a.Kod,
                                NamaJawatan = a.Nama,
                                UnitOrganisasi = f.Nama,
                                StatusPengisian = e2.Nama,
                                e.TarikhStatusKekosongan
                            };

                if (!string.IsNullOrEmpty(filter.Nama))
                    query = query.Where(x => x.NamaJawatan.Contains(filter.Nama));

                if (!string.IsNullOrEmpty(filter.UnitOrganisasi))
                    query = query.Where(x => x.UnitOrganisasi.Contains(filter.UnitOrganisasi));

                var data = await query.ToListAsync();

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

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrive data");
            }
        }
    }


}
