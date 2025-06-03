using HR.Application.DTOs;
using HR.Application.Extensions;
using HR.Application.Interfaces;
using HR.Core.Enums;
using HR.Core.Interfaces;
using HR.Infrastructure.Data.EntityFramework;
using Microsoft.Extensions.Logging;

namespace HR.Application.Services
{
    public class MaklumatKlasifikasiPerkhidmatanService : IMaklumatKlasifikasiPerkhidmatanService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _dbContext;
        private readonly ILogger<MaklumatKlasifikasiPerkhidmatanService> _logger;

        public MaklumatKlasifikasiPerkhidmatanService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<MaklumatKlasifikasiPerkhidmatanService> logger)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<MaklumatKlasifikasiPerkhidmatanDto>> GetMaklumatKlasifikasiPerkhidmatan(MaklumatKlasifikasiPerkhidmatanFilterDto filter)
        {
            try
            {


                _logger.LogInformation("Getting all MaklumatKlasifikasiPerkhidmatanDto using EF Core join");

                var query = (from a in _dbContext.PDOKlasifikasiPerkhidmatan
                             join b in _dbContext.PDOStatusPermohonanKlasifikasiPerkhidmatan
                                           on a.Id equals b.IdKlasifikasiPerkhidmatan
                             join b2 in _dbContext.PDORujStatusPermohonan
                                           on b.KodRujStatusPermohonan equals b2.Kod
                             where b.StatusAktif == true
                             orderby a.Kod
                             select new
                             {
                                 a.Kod,
                                 a.Nama,
                                 a.Keterangan,
                                 a.StatusAktif,
                                 StatusPermohonan = b2.Nama
                             })
                  .AsEnumerable()
                 .Select((x, index) => new
                 {
                     Bil = index + 1,
                     x.Kod,
                     x.Nama,
                     x.Keterangan,
                     x.StatusAktif,
                     x.StatusPermohonan
                 });


                // Apply filters
                if (!string.IsNullOrWhiteSpace(filter.Kod))
                    query = query.Where(q => q.Kod.Contains(filter.Kod));

                if (!string.IsNullOrWhiteSpace(filter.Nama))
                    query = query.Where(q => q.Nama.Contains(filter.Nama));

                if (filter.StatusKumpulan.HasValue)
                    query = query.Where(q => Convert.ToInt16(q.StatusAktif) == filter.StatusKumpulan.Value);

                if (!string.IsNullOrWhiteSpace(filter.StatusPermohonan))
                    query = query.Where(q => q.Kod == filter.StatusPermohonan);

                var data = query.ToList();



                var result = data
                    .Select((q, index) => new MaklumatKlasifikasiPerkhidmatanDto
                    {
                        Bil = index + 1,
                        Kod = q.Kod,
                        Nama = q.Nama,
                        Keterangan = q.Keterangan,
                        StatusKumpulanPerkhidmatan = (q.StatusAktif
                            ? StatusKumpulanPerkhidmatanEnum.Aktif
                            : StatusKumpulanPerkhidmatanEnum.TidakAktif).ToDisplayString(),
                        StatusPermohonan = q.StatusPermohonan
                    })
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrive data");
            }
        }


    }
}


