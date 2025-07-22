using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Core.Entities.PDO;
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
    public class UnitOrganisasiService : IUnitOrganisasiService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<UnitOrganisasiService> _logger;
        public UnitOrganisasiService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<UnitOrganisasiService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<UnitOrganisasiDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all UnitOrganisasiDto using Entity Framework");
            var result = await _unitOfWork.Repository<PDOUnitOrganisasi>().GetAllAsync();
            result = result.ToList().Where(e => e.StatusAktif);
            return result.Select(MapToDto);
        }

        private UnitOrganisasiDto MapToDto(PDOUnitOrganisasi pDOUnitOrganisasi)
        {
            return new UnitOrganisasiDto
            {
                Id = pDOUnitOrganisasi.Id,
                Nama = pDOUnitOrganisasi.Nama,
                KodCartaOrganisasi = pDOUnitOrganisasi.KodCartaOrganisasi
            };
        }
        public async Task<List<UnitOrganisasiKementerianDto>> GetUnitOrganisasiByKategoriAsync()
        {
            var result = await (from puo in _context.PDOUnitOrganisasi
                                join prkuo in _context.PDORujKategoriUnitOrganisasi
                                    on puo.KodRujKategoriUnitOrganisasi equals prkuo.Kod
                                join prja in _context.PDORujJenisAgensi
                                    on puo.KodRujJenisAgensi equals prja.Kod
                                where puo.StatusAktif && prkuo.Kod == "0001"
                                select new UnitOrganisasiKementerianDto
                                {
                                    Kod = puo.Kod,
                                    Nama = puo.Nama
                                }).ToListAsync();

            return result;
        }

        public async Task<List<UnitOrganisasiAutocompleteDto>> SearchUnitOrganisasiAsync(string keyword)
        {
            return await _context.PDOUnitOrganisasi
                .Where(x => x.Nama.Contains(keyword))
                .Select(x => new UnitOrganisasiAutocompleteDto
                {
                    Kod = x.Kod,
                    Nama = x.Nama
                })
                .ToListAsync();
        }

        //Amar Code 17/07/25
        public async Task<string> GetNamaUnitOrganisasi(int IdUnitOrganisasi)
        {
            _logger.LogInformation("GetNamaUnitOrganisasi: Getting NamaUnitOrganisasi with IdUnitOrganisasi: {IdUnitOrganisasi}", IdUnitOrganisasi);
            try
            {
                var result = await _context.PDOUnitOrganisasi
                    .Where(puo => puo.Id == IdUnitOrganisasi)
                    .Select(puo => puo.Nama)
                    .FirstOrDefaultAsync();
                _logger.LogInformation("GetNamaUnitOrganisasi: Successfully retrieved NamaUnitOrganisasi");
                return result ?? String.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetNamaUnitOrganisasi: Failed to retrieve NamaUnitOrganisasi with IdUnitOrganisasi: {IdUnitOrganisasi}", IdUnitOrganisasi);
                throw;
            }
        }
        public async Task<bool> SetPenjenamaanSemula(UnitOrganisasiPenjenamaanSemulaRequestDto penjenamaanSemulaRequestDto)
        {
            _logger.LogInformation("Service: Updating PenjenamaanSemula");
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Step 1: Update PDO_UnitOrganisasi
                var unitOrganisasi = await _unitOfWork.Repository<PDOUnitOrganisasi>()
                    .FirstOrDefaultAsync(x => x.Id == penjenamaanSemulaRequestDto.IdUnitOrganisasi);
                if (unitOrganisasi != null)
                {
                    unitOrganisasi.Nama = penjenamaanSemulaRequestDto.NamaUnitOrganisasiBaharu;
                    await _unitOfWork.Repository<PDOUnitOrganisasi>().UpdateAsync(unitOrganisasi);
                    await _unitOfWork.SaveChangesAsync();
                }
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service SetPenjenamaanSemula");
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }
        //End

        //code added by amar 220725

        public async Task<List<UnitOrganisasiCarianKetuaPerkhidmatanResponseDto>> GetAgency()
        {
            _logger.LogInformation("GetAgency: Fetching agencies with IndikatorAgensiRasmi = 1 and StatusAktif = 1");

            try
            {
                var agencies = await _context.PDOUnitOrganisasi
                    .Where(x => x.IndikatorAgensiRasmi == true && x.StatusAktif == true)
                    .Select(x => new UnitOrganisasiCarianKetuaPerkhidmatanResponseDto
                    {
                        Id = x.Id,
                        Nama = x.Nama ?? string.Empty,
                        KodCartaOrganisasi = x.KodCartaOrganisasi ?? string.Empty
                    })
                    .ToListAsync();

                _logger.LogInformation("GetAgency: Retrieved {Count} agencies", agencies.Count);
                return agencies;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAgency: Error occurred while fetching agency list");
                throw;
            }
        }

        //end
    }
}
