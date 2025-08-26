using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.Services.PDO
{
    public class PermohonanPeriwatanService: IPermohonanPeriwatanService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _dbContext;
        private readonly ILogger<PermohonanPeriwatanService> _logger;
        public PermohonanPeriwatanService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<PermohonanPeriwatanService> logger)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(PermohonanPeriwatanCreateRequestDto dto)
        {
            _logger.LogInformation("Service: Creating new PermohonanPeriwatan");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var userId = Guid.Empty;
                var Periwatan = new PDOPermohonanJawatan
                {
                    IdUnitOrganisasi =1, //dto.IdUnitOrganisasi,
                    IdAgensi = dto.AgensiId,
                    KodRujJenisPermohonan = dto.JenisPermohonan,
                    NomborRujukan = dto.NoRujukan,
                    Tajuk = dto.TajukPermohonan,
                    StatusAktif=true

                };

                Periwatan = await _unitOfWork.Repository<PDOPermohonanJawatan>().AddAsync(Periwatan);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                // Step 2: Insert PDO_StatusPermohonanJawatan
                var statusPeriwatan = new PDOStatusPermohonanJawatan
                {
                    IdPermohonanJawatan = Periwatan.Id,
                    KodRujStatusPermohonan = "01",
                    TarikhStatusPermohonan = DateTime.Now,
                    StatusAktif = true
                };

                await _unitOfWork.Repository<PDOStatusPermohonanJawatan>().AddAsync(statusPeriwatan);
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
        public async Task<bool> CreateAktivitiOrganisasiAsync(AktivitiOrganisasiCreateRequestDto dto)
        {
            _logger.LogInformation("Service: Creating new AktivitiOrganisasi");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var userId = Guid.Empty;
                var aktiviti = new PDOAktivitiOrganisasi
                {
                    IdIndukAktivitiOrganisasi = dto.IdIndukAktivitiOrganisasi,
                    KodProgram = dto.KodProgram,
                    Kod = dto.Kod, // If this is auto-generated, generate it before this line
                    Nama = dto.Nama,
                    Tahap = dto.Tahap,
                    Keterangan = dto.Keterangan,
                    KodRujKategoriAktivitiOrganisasi = "04",
                    StatusAktif = false,
                    IdCipta = Guid.NewGuid(),
                    TarikhCipta = DateTime.Now
                };

                aktiviti = await _unitOfWork.Repository<PDOAktivitiOrganisasi>().AddAsync(aktiviti);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during  CreateAktivitiOrganisasiAsync");
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }


        public async Task<string?> GetUlasanStatusAsync(int idPermohonanJawatan)
        {
            return await _dbContext.PDOStatusPermohonanJawatan
                .Where(p => p.IdPermohonanJawatan == idPermohonanJawatan && p.StatusAktif == true)
                .Select(p => p.UlasanStatusPermohonan)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SimpanStatusPermohonanAsync(SimpanStatusPermohonanDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // 1. Set existing statuses to inactive
                var existingStatuses = await _dbContext.PDOStatusPermohonanJawatan
                    .Where(x => x.IdPermohonanJawatan == dto.IdPermohonanJawatan && x.StatusAktif == true)
                    .ToListAsync();

                foreach (var status in existingStatuses)
                {
                    status.StatusAktif = false;
                  
                    status.TarikhPinda = DateTime.Now;
                }

                await _unitOfWork.Repository<PDOStatusPermohonanJawatan>().AddRangeAsync(existingStatuses);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();



                // 2. Insert new status
                var newStatus = new PDOStatusPermohonanJawatan
                {

                    IdPermohonanJawatan = dto.IdPermohonanJawatan,
                    KodRujStatusPermohonan = "02", // Hardcoded as per requirement

                    UlasanStatusPermohonan = dto.Ulasan,
                    StatusAktif = true
                };

                await _unitOfWork.Repository<PDOStatusPermohonanJawatan>().AddAsync(newStatus);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }

    }
}
