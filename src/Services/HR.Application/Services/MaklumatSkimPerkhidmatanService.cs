using HR.Application.DTOs;
using HR.Application.Interfaces;
using HR.Core.Entities;
using HR.Core.Enums;
using HR.Core.Interfaces;
using HR.Infrastructure.Data.EntityFramework;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Services
{
    public class MaklumatSkimPerkhidmatanService: IMaklumatSkimPerkhidmatanService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _dbContext;
        private readonly ILogger<MaklumatSkimPerkhidmatanService> _logger;

        public MaklumatSkimPerkhidmatanService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<MaklumatSkimPerkhidmatanService> logger)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<MaklumatSkimPerkhidmatanSearchResponseDto>> GetSenaraiSkimPerkhidmatan(MaklumatSkimPerkhidmatanFilterDto filter)
        {
            try
            {


                _logger.LogInformation("Getting all MaklumatSkimPerkhidmatanDto using EF Core join");

                var query = (from a in _dbContext.PDOKumpulanPerkhidmatan
                             join b in _dbContext.PDOStatusPermohonanKumpulanPerkhidmatan
                                 on a.Id equals b.IdKumpulanPerkhidmatan
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
                                 StatusPermohonanNama = b2.Nama,
                                 b.TarikhKemaskini,
                                 a.Id
                             })
    .AsEnumerable()
    .Select((x, index) => new
    {
        Bil = index + 1,
        x.Kod,
        x.Nama,
        x.Keterangan,
        StatusSkimPerkhidmatan = x.StatusAktif ? "Aktif" : "Tidak Aktif",
        StatusPermohonan = x.StatusPermohonanNama,
        x.TarikhKemaskini,
        x.Id
    });
                // Apply filters
                //if (!string.IsNullOrWhiteSpace(filter.MaklumatKumpulanPerkhidmatanId))
                //    query = query.Where(q => q.Nama.Contains(filter.Nama));

                //if (!string.IsNullOrWhiteSpace(filter.MaklumatKlasifikasiPerkhidmatanId))
                //    query = query.Where(q => q.Nama.Contains(filter.Nama));
                if (filter.MaklumatKumpulanPerkhidmatanId.HasValue)
                    query = query.Where(x => x.Id == filter.MaklumatKumpulanPerkhidmatanId);

                if (!string.IsNullOrWhiteSpace(filter.Kod))
                    query = query.Where(q => q.Kod.Contains(filter.Kod));

                if (!string.IsNullOrWhiteSpace(filter.Nama))
                    query = query.Where(q => q.Nama.Contains(filter.Nama));                

                if (!string.IsNullOrWhiteSpace(filter.StatusPermohonan))
                    query = query.Where(q => q.Kod == filter.StatusPermohonan);

                var data = query.ToList();
                var result = data
                    .Select((q, index) => new MaklumatSkimPerkhidmatanSearchResponseDto
                    {
                        Bil = index + 1,
                        Kod = q.Kod,
                        Nama = q.Nama,
                        Keterangan = q.Keterangan,
                        StatusSkimPerkhidmatan = q.StatusSkimPerkhidmatan,
                        StatusPermohonan = q.StatusPermohonan,
                        TarikhKemaskini=q.TarikhKemaskini
                    })
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to retrive data");
            }
        }

        public async Task<bool> CreateAsync(MaklumatSkimPerkhidmatanCreateRequestDto dto)
        {
            _logger.LogInformation("Service: Creating new MaklumatSkimPerkhidmatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var userId = Guid.Empty;
                var perkhidmatan = new PDOSkimPerkhidmatan
                {
                    IdKlasifikasiPerkhidmatan = dto.IdKlasifikasiPerkhidmatan,
                    IdKumpulanPerkhidmatan = dto.IdKumpulanPerkhidmatan,
                    Kod = dto.Kod,
                    Nama = dto.Nama,
                    Keterangan = dto.Keterangan,
                    IndikatorSkimKritikal = dto.IndikatorSkimKritikal,
                    IndikatorKenaikanPGT = dto.IndikatorKenaikanPGT,
                    KodRujStatusSkim = 0
                    
                };

                perkhidmatan=await _unitOfWork.Repository<PDOSkimPerkhidmatan>().AddAsync(perkhidmatan);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                // Step 2: Insert PDO_StatusPermohonanSkimPerkhidmatan
                var statusPermohonan = new PDOStatusPermohonanSkimPerkhidmatan
                {
                    IdSkimPerkhidmatan = perkhidmatan.Id,
                    KodRujStatusPermohonan = "01",
                    TarikhKemasKini = DateTime.Now,
                    StatusAktif = true
                };

                await _unitOfWork.Repository<PDOStatusPermohonanSkimPerkhidmatan>().AddAsync(statusPermohonan);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();
                // Step 3: Insert PDO_GredSkimPerkhidmatan
                var gredLink = new PDOGredSkimPerkhidmatan
                {
                    IdGred = dto.IdGred,
                    IdSkimPerkhidmatan = perkhidmatan.Id
                };
                await _unitOfWork.Repository<PDOGredSkimPerkhidmatan>().AddAsync(gredLink);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service CreateAsync");
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }
        
    }
}
