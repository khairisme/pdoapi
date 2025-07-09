using Azure;
using HR.Application.DTOs.PDO;
using HR.Application.DTOs.PDP;
using HR.Application.Interfaces.PDO;
using HR.Core.Entities.PDO;
using HR.Core.Interfaces;
using HR.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HR.Application.Services.PDO
{
    public class PermohonanPengisianService : IPermohonanPengisianService
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<PermohonanPengisianService> _logger;
        private readonly HttpClient _httpClient;
        public PermohonanPengisianService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<PermohonanPengisianService> logger, HttpClient httpClient)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
            _httpClient = httpClient; // This now has BaseAddress pre-configured
        }
        public async Task<List<PermohonanPOAFilterResponseDto>> GetPermohonanListPOAAsync(PermohonanPengisianPOAFilterDto filter)
        {
            var query = from a in _context.PDOPermohonanPengisian
                        join b in _context.PDOStatusPermohonanPengisian on a.Id equals b.IdPermohonanPengisian
                        join c in _context.PDORujStatusPermohonan on b.KodRujStatusPermohonan equals c.Kod
                        orderby a.Id
                        select new
                        {
                            a.Id,
                            a.NomborRujukan,
                            a.Tajuk,
                            a.TarikhPermohonan,
                            b.KodRujStatusPermohonan,
                            Status = c.Nama
                        };

            if (!string.IsNullOrWhiteSpace(filter.NoRujukan))
                query = query.Where(x => x.NomborRujukan.Contains(filter.NoRujukan));

            if (!string.IsNullOrWhiteSpace(filter.Tajuk))
                query = query.Where(x => x.Tajuk.Contains(filter.Tajuk));

            if (!string.IsNullOrWhiteSpace(filter.StatusPermohonan))
                query = query.Where(x => x.KodRujStatusPermohonan == filter.StatusPermohonan);
            
            var result = await query.ToListAsync();

            // Add row number manually
            var withRowNumber = result.Select((x, index) => new PermohonanPOAFilterResponseDto
            {
                Bil = index + 1,
                Id = x.Id,
                NomborRujukan = x.NomborRujukan,
                Tajuk = x.Tajuk,
                TarikhPermohonan = Convert.ToDateTime(x.TarikhPermohonan),
                KodRujStatusPermohonan = x.KodRujStatusPermohonan,
                Status = x.Status
            }).ToList();

            return withRowNumber;
        }
        private async Task<string> GenerateNextKODFromDb(int agensiId)
        {
            // Step 1: Get Agensi info from PDO_UnitOrganisasi using agensiId
            var agensi = await _context.PDOUnitOrganisasi
                .Where(x => x.Id == agensiId)
                .Select(x => new
                {
                    x.KodRujJenisAgensi,
                    x.KodKementerian,
                    x.KodJabatan
                })
                .FirstOrDefaultAsync();

            if (agensi == null)
                throw new Exception("Agensi not found.");

            // Step 2: Get current year
            var tahun = DateTime.Now.Year;

            // Step 3: Calculate next running number
            // You can refine this logic later based on filtering criteria (e.g. year or agensi)
            var existingCount = await _context.PDOPermohonanPengisian.CountAsync();
            var runningNumber = (existingCount + 1).ToString("D3"); // Padded to 3 digits

            // Step 4: Generate NoRujukan
            var noRujukan = $"{agensi.KodRujJenisAgensi}/{agensi.KodKementerian}/{agensi.KodJabatan}/{tahun}/{runningNumber}";

            return noRujukan;
        }
        public async Task<bool> CheckDuplicateKodNamaAsync(SavePermohonanPengisianPOARequestDto dto)
        {
            try
            {
                if (dto.Id == 0)
                {
                    // Create: check if Kod or Nama already exists
                    return await _context.PDOPermohonanPengisian.AnyAsync(x =>

                        // x.Kod.Trim() == dto.Kod.Trim() || 
                        x.Tajuk.Trim() == dto.Tajuk.Trim() && x.IdUnitOrganisasi == dto.IdUnitOrganisasi);
                }
                else
                {
                    // Update: check for duplicates excluding current record
                    return await _context.PDOPermohonanPengisian.AnyAsync(x =>

                        (
                        x.Tajuk.Trim() == dto.Tajuk.Trim()) 
                        && x.IdUnitOrganisasi == dto.IdUnitOrganisasi &&
                        x.Id != dto.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Validate PermohonanPengisianPOA");
                throw;
            }
        }

        public async Task<string> CreateAsync(SavePermohonanPengisianPOARequestDto requestDto)
        {
            _logger.LogInformation("Service: Creating new PermohonanPengisianPOA");
            // Step 1: Insert into PDO_PermohonanPengisian
            requestDto.NomborRujukan = await GenerateNextKODFromDb(requestDto.IdUnitOrganisasi);
            requestDto.TarikhPermohonan = DateTime.Now;
            var permohonanPengisian = MapToEntity(requestDto);
            permohonanPengisian.StatusAktif = false;

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                

                permohonanPengisian = await _unitOfWork.Repository<PDOPermohonanPengisian>().AddAsync(permohonanPengisian);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                // Step 2: Insert into PDO_StatusPermohonanPengisian
                var statusEntity = new PDOStatusPermohonanPengisian
                {
                    IdPermohonanPengisian = permohonanPengisian.Id, // use the ID from step 1
                    KodRujStatusPermohonan = "01",
                    TarikhStatusPermohonan = DateTime.Now,
                    StatusAktif = true
                };
                await _unitOfWork.Repository<PDOStatusPermohonanPengisian>().AddAsync(statusEntity);
                await _unitOfWork.SaveChangesAsync();

                // Step 3: Insert into PDO_PermohonanPengisianSkim
                requestDto.savePermohonanPengisianSkimDtos = requestDto.savePermohonanPengisianSkimDtos ?? new List<SavePermohonanPengisianSkimDto>();
                foreach (var skimDto in requestDto.savePermohonanPengisianSkimDtos)
                {
                    var skimEntity = new PDOPermohonanPengisianSkim
                    {
                        IdPermohonanPengisian = permohonanPengisian.Id,
                        IdSkimPerkhidmatan = skimDto.IdSkimPerkhidmatan,
                        BilanganPengisian = skimDto.BilanganPengisian
                    };
                    await _unitOfWork.Repository<PDOPermohonanPengisianSkim>().AddAsync(skimEntity);
                }
                await _unitOfWork.SaveChangesAsync();


                await _unitOfWork.CommitAsync();


                return permohonanPengisian.NomborRujukan;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during service CreateAsync:" + ex.InnerException.ToString());
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
        public async Task<PermohonanPengisianHeaderResponseDto> GetPermohonanPengisianByAgensiAndId(PermohonanPengisianfilterdto filter)
        {
            var result = await (from ppp in _context.PDOPermohonanPengisian
                                join puo in _context.PDOUnitOrganisasi on ppp.IdUnitOrganisasi equals puo.Id
                                where puo.Id == filter.AgensiId && ppp.Id == filter.IdPermohonanPengisian
                                select new PermohonanPengisianHeaderResponseDto
                                {
                                    Agensi = puo.Nama,
                                    NomborRujukan = ppp.NomborRujukan,
                                    Tajuk = ppp.Tajuk,
                                    Keterangan = ppp.Keterangan
                                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<BilanganPermohonanPengisianResponseDto>> GetBilanganPermohonanPengisianId(int idPermohonanPengisian)
        {
            var result = await (from ppp in _context.PDOPermohonanPengisian
                                join ppps in _context.PDOPermohonanPengisianSkim on ppp.Id equals ppps.IdPermohonanPengisian
                                join psp in _context.PDOSkimPerkhidmatan on ppps.IdSkimPerkhidmatan equals psp.Id
                                //join pssp in _context.PDOStatusSkimPerkhidmatan on psp.Id equals pssp.IdSkimPerkhidmatan
                                //join prsr in _context.PDORujStatusRekod on pssp.KodRujStatusRekod equals prsr.Kod
                                where ppp.Id == idPermohonanPengisian
                                select new BilanganPermohonanPengisianResponseDto
                                {
                                    KodSkim = psp.Kod,
                                    NamaSkimPerkhidmatan = psp.Nama,
                                    Bilangan = ppps.BilanganPengisian
                                }).ToListAsync();

            return result;
        }

       

        public async Task<bool> UpdateAsync(SavePermohonanPengisianPOARequestDto requestDto )
        {
            _logger.LogInformation("Service: Updating PermohonanPengisian POA");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Step 1: update into PDO_PermohonanPengisian
                var permohonanPengisian = MapToEntity(requestDto);
                

                var result = await _unitOfWork.Repository<PDOPermohonanPengisian>().UpdateAsync(permohonanPengisian);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();



                // Step 2: Deactivate existing PDO_StatusPermohonanPengisian record
                var existingStatus = await _unitOfWork.Repository<PDOStatusPermohonanPengisian>()
                        .FirstOrDefaultAsync(x => x.IdPermohonanPengisian == permohonanPengisian.Id );

                if (existingStatus != null)
                {
                    existingStatus.StatusAktif = false;
                    existingStatus.TarikhPinda = DateTime.Now;
                    await _unitOfWork.SaveChangesAsync();
                }


                // Step 3: Insert into PDO_StatusPermohonanPengisian
                var statusEntity = new PDOStatusPermohonanPengisian
                {
                    IdPermohonanPengisian = permohonanPengisian.Id, // use the ID from step 1
                    KodRujStatusPermohonan = "01",
                    TarikhStatusPermohonan = DateTime.Now,
                    StatusAktif = true
                };
                await _unitOfWork.Repository<PDOStatusPermohonanPengisian>().AddAsync(statusEntity);
                await _unitOfWork.SaveChangesAsync();

                var existingSkimList = await _context.PDOPermohonanPengisianSkim
                .Where(x => x.IdPermohonanPengisian == permohonanPengisian.Id)
                .ToListAsync();

                // Step 3: Delete in bulk if records exist
                if (existingSkimList.Any())
                {
                    _context.PDOPermohonanPengisianSkim.RemoveRange(existingSkimList);
                }


                // Step 4: Insert into PDO_PermohonanPengisianSkim
                requestDto.savePermohonanPengisianSkimDtos = requestDto.savePermohonanPengisianSkimDtos ?? new List<SavePermohonanPengisianSkimDto>();
                foreach (var skimDto in requestDto.savePermohonanPengisianSkimDtos)
                {
                    var skimEntity = new PDOPermohonanPengisianSkim
                    {
                        IdPermohonanPengisian = permohonanPengisian.Id,
                        IdSkimPerkhidmatan = skimDto.IdSkimPerkhidmatan,
                        BilanganPengisian = skimDto.BilanganPengisian
                    };
                    await _unitOfWork.Repository<PDOPermohonanPengisianSkim>().AddAsync(skimEntity);
                }
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
        public async Task<SkimNameWithJawatanDto?> GetJawatanBySkimAndAgensiAsync(PenolongPegawaiTeknologiMaklumatFilterDto filterDto)
        {
            // Get Skim Info
            var skimInfo = await (from ppps in _context.PDOPermohonanPengisianSkim
                                  join psp in _context.PDOSkimPerkhidmatan on ppps.IdSkimPerkhidmatan equals psp.Id
                                  where ppps.IdPermohonanPengisian == filterDto.IdPermohonanPengisian
                                        && ppps.Id == filterDto.IdPermohonanPengisianSkim
                                  select new
                                  {
                                      IdSkim = ppps.IdSkimPerkhidmatan,
                                      Nama = psp.Nama
                                  }).FirstOrDefaultAsync();

            if (skimInfo == null)
                return null;

            // Get jawatan data
            var jawatanList = await (from ppp in _context.PDOPermohonanPengisian
                                     join ppps in _context.PDOPermohonanPengisianSkim
                                         on ppp.Id equals ppps.IdPermohonanPengisian
                                     join ppj in _context.PDOPengisianJawatan
                                         on ppp.Id equals ppj.IdPermohonanPengisian
                                     join pj in _context.PDOJawatan
                                         on ppj.IdJawatanSebenar equals pj.Id
                                     join pgsj in _context.PDOGredSkimJawatan
                                         on pj.Id equals pgsj.IdJawatan
                                     join pg in _context.PDOGred
                                         on pgsj.IdGred equals pg.Id
                                     where ppp.IdUnitOrganisasi == filterDto.AgensiId
                                           && ppps.IdSkimPerkhidmatan == skimInfo.IdSkim
                                     select new PenolongPegawaiTeknologiMaklumatResponseDto
                                     {
                                         KodJawatan = pj.Kod,
                                         NamaJawatan = pj.Nama,
                                         Gred = pg.Nama
                                     }).ToListAsync();

            return new SkimNameWithJawatanDto
            {
                IdSkimPerkhidmatan = skimInfo.IdSkim,
                Nama = skimInfo.Nama,
                Data = jawatanList
            };
        }
        public async Task<List<PermohonanPengisianJawatanResponseDto>> GetFilteredPermohonanJawatanAsync(PermohonanPengisianJawatanFilterDto filter)
        {
            var query = from a in _context.PDOPermohonanPengisian
                        join ppps in _context.PDOPermohonanPengisianSkim on a.Id equals ppps.IdPermohonanPengisian
                        join b in _context.PDOStatusPermohonanPengisian on a.Id equals b.IdPermohonanPengisian
                        join c in _context.PDORujStatusPermohonan on b.KodRujStatusPermohonan equals c.Kod
                        join puo in _context.PDOUnitOrganisasi on a.IdUnitOrganisasi equals puo.Id
                        where puo.StatusAktif  && puo.Kod == "0001"
                              && (filter.Kementerian == null || puo.Id == filter.Kementerian)
                              && (filter.StatusPermohonan == null || c.Kod == filter.StatusPermohonan)
                        orderby a.Id
                        select new
                        {
                            a.Id,
                            Kementerian = puo.Nama,
                            ppps.BilanganPengisian,
                            a.TarikhPermohonan,
                            Status = c.Nama
                        };

            var result = await query.ToListAsync();

            return result.Select((x, index) => new PermohonanPengisianJawatanResponseDto
            {
                Id=x.Id,
                Bil = index + 1,
                Kementerian = x.Kementerian,
                BilanganPengisian = x.BilanganPengisian,
                TarikhPermohonan =Convert.ToDateTime( x.TarikhPermohonan),
                Status = x.Status
            }).ToList();
        }


        public async Task<List<AgensiWithJawatanDto>> GetGroupedJawatanByAgensiAsync(PenolongPegawaiTeknologiMaklumatFilterDto filter)
        {
            // Step 1: Get all matching Agensi
            var agensiList = await (from ppp in _context.PDOPermohonanPengisian
                                    join ppps in _context.PDOPermohonanPengisianSkim
                                        on ppp.Id equals ppps.IdPermohonanPengisian
                                    join psp in _context.PDOSkimPerkhidmatan
                                        on ppps.IdSkimPerkhidmatan equals psp.Id
                                    join puo in _context.PDOUnitOrganisasi
                                        on ppp.IdUnitOrganisasi equals puo.Id
                                    where ppps.IdPermohonanPengisian == filter.IdPermohonanPengisian
                                       && ppps.Id == filter.IdPermohonanPengisianSkim
                                    select new
                                    {
                                        AgensiId = puo.Id,
                                        puo.Kod,
                                        NamaAgensi = puo.Nama
                                    })
                                    .Distinct()
                                    .ToListAsync();

            var result = new List<AgensiWithJawatanDto>();

            foreach (var agensi in agensiList)
            {
                // Step 2: Get jawatan for each Agensi
                var jawatanList = await (from ppp in _context.PDOPermohonanPengisian
                                         join puo in _context.PDOUnitOrganisasi
                                             on ppp.IdUnitOrganisasi equals puo.Id
                                         join ppj in _context.PDOPengisianJawatan
                                             on ppp.Id equals ppj.IdPermohonanPengisian
                                         join pj in _context.PDOJawatan
                                             on ppj.IdJawatanSebenar equals pj.Id
                                         join pgsj in _context.PDOGredSkimJawatan
                                             on pj.Id equals pgsj.IdJawatan
                                         join pg in _context.PDOGred
                                             on pgsj.IdGred equals pg.Id
                                         where ppp.IdUnitOrganisasi == agensi.AgensiId
                                         select new
                                         {
                                             pj.Id,
                                             pj.Kod,
                                             pj.Nama,
                                             Gred = pg.Nama,
                                             Agensi = agensi.NamaAgensi
                                         }).ToListAsync();

                var jawatanDtoList = jawatanList
                .Select((x, index) => new PermohonanPengisianJawatanWithAgensiResponseDto
                {
                    Bil = index + 1,
                    Id=x.Id,
                    KodJawatan = x.Kod,
                    NamaJawatan = x.Nama,
                    Gred = x.Gred
                }).ToList();
              

                result.Add(new AgensiWithJawatanDto
                {
                    AgensiId = agensi.AgensiId,
                    Kod = agensi.Kod,
                    NamaAgensi = agensi.NamaAgensi,
                    Data = jawatanDtoList
                });
            }

            return result;
        }

        //public async Task<List<SimulasiKewanganResponseDto>> GetSimulasiByAgensiAsync(int agensiId)
        //{
        //    var query = from ppp in _context.PDOPermohonanPengisian
        //                join ppps in _context.PDOPermohonanPengisianSkim
        //                    on ppp.Id equals ppps.IdPermohonanPengisian
        //                join puo in _context.PDOUnitOrganisasi
        //                    on ppp.IdUnitOrganisasi equals puo.Id
        //                join ppj in _context.PDOPengisianJawatan
        //                    on ppps.Id equals ppj.IdPermohonanPengisianSkim
        //                join pj in _context.PDOJawatan
        //                    on ppj.IdJawatan equals pj.Id
        //                join pgsj in _context.PDOGredSkimJawatan
        //                    on pj.Id equals pgsj.IdJawatan
        //                join pg in _context.PDOGred
        //                    on pgsj.IdGred equals pg.Id
        //                join pjg in _context.PDPJadualGaji
        //                    on pg.Id equals pjg.IdGred
        //                where ppp.IdUnitOrganisasi == agensiId
        //                select new SimulasiKewanganResponseDto
        //                {
        //                    KodJawatan = pj.Kod,
        //                    NamaJawatan = pj.Nama,
        //                    Gred = pg.Nama,
        //                    JumlahImplikasiKewanganSebulan = pjg.GajiMinimum,
        //                    JumlahImplikasiKewanganSetahun = pjg.GajiMinimum * 12
        //                };

        //    return await query.ToListAsync();
        //}
        public async Task<List<SimulasiKewanganByPermohonanDto>> GetSimulasiByPermohonanIdAsync(int idPermohonanPengisian)
        {
            // Step 1: Fetch JadualGaji from PDP API
            
            var response = await _httpClient.GetAsync("api/pdp/JadualGaji/getAll");

            response.EnsureSuccessStatusCode();

            var jadualGajiListapi = await response.Content.ReadFromJsonAsync<JadualGajiApiResponseDto>();
            var jadualGajiList = jadualGajiListapi.Items;
            // Step 2: Query local database
            var result = await (from ppp in _context.PDOPermohonanPengisian
                                join ppps in _context.PDOPermohonanPengisianSkim
                                    on ppp.Id equals ppps.IdPermohonanPengisian
                                join puo in _context.PDOUnitOrganisasi
                                    on ppp.IdUnitOrganisasi equals puo.Id
                                join ppj in _context.PDOPengisianJawatan
                                    on ppps.Id equals ppj.IdPermohonanPengisianSkim
                                join pj in _context.PDOJawatan
                                    on ppj.IdJawatan equals pj.Id
                                join pgsj in _context.PDOGredSkimJawatan
                                    on pj.Id equals pgsj.IdJawatan
                                join pg in _context.PDOGred
                                    on pgsj.IdGred equals pg.Id
                                where ppp.Id == idPermohonanPengisian
                                select new
                                {
                                    pj.Kod,
                                    pj.Nama,
                                    GredNama = pg.Nama,
                                    pg.Id
                                }).ToListAsync();

            // Step 3: Join with remote JadualGaji data
            var finalResult = result
                .Select(x =>
                {
                    var gaji = jadualGajiList.FirstOrDefault(j => j.IdGred == x.Id);
                    return new SimulasiKewanganByPermohonanDto
                    {
                        KodJawatan = x.Kod,
                        NamaJawatan = x.Nama,
                        Gred = x.GredNama,
                        JumlahImplikasiKewanganSebulan = gaji?.GajiMinimum ?? 0,
                        JumlahImplikasiKewanganSetahun = (gaji?.GajiMinimum ?? 0) * 12
                    };
                }).ToList();

            return finalResult;
        }
        public async Task<List<PermohonanPOAIFilterResponseDto>> GetPermohonanListPOAIAsync(PermohonanPengisianPOAIFilterDto filter)
        {
            var query = from a in _context.PDOPermohonanPengisian
                        join b in _context.PDOStatusPermohonanPengisian on a.Id equals b.IdPermohonanPengisian
                        join c in _context.PDORujStatusPermohonan on b.KodRujStatusPermohonan equals c.Kod
                        join d in _context.PDOUnitOrganisasi on a.IdUnitOrganisasi equals d.Id
                        orderby a.Id
                        select new
                        {
                            a.Id,
                            a.NomborRujukan,
                            Agensi = d.Nama,
                            a.Tajuk,
                            a.TarikhPermohonan,
                            b.KodRujStatusPermohonan,
                            Status = c.Nama,
                            AgensiId= d.Id
                        };

            if (filter.AgensiId.HasValue)
                query = query.Where(x => x.AgensiId== filter.AgensiId);

            

            if (!string.IsNullOrWhiteSpace(filter.StatusPermohonan))
                query = query.Where(x => x.KodRujStatusPermohonan == filter.StatusPermohonan);

            var result = await query.ToListAsync();

            // Add row number manually
            var withRowNumber = result.Select((x, index) => new PermohonanPOAIFilterResponseDto
            {
              
                Id = x.Id,
                NomborRujukan = x.NomborRujukan,
                Agensi = x.Agensi,
                Tajuk = x.Tajuk,
                TarikhPermohonan = Convert.ToDateTime(x.TarikhPermohonan),
                KodRujStatusPermohonan = x.KodRujStatusPermohonan,
                Status = x.Status
            }).ToList();

            return withRowNumber;
        }
        private PDOPermohonanPengisian MapToEntity(SavePermohonanPengisianPOARequestDto dto)
        {
            return new PDOPermohonanPengisian
            {
                Id = dto.Id,
                IdUnitOrganisasi = dto.IdUnitOrganisasi,
                Tajuk=dto.Tajuk,
                NomborRujukan = dto.NomborRujukan,
                Keterangan = dto.Keterangan,
                TarikhPermohonan = dto.TarikhPermohonan
            };
        }
    }
}
