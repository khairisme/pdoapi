using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Application.DTOs;

namespace HR.Application.Services.PDO
{
    public class AktivitiOrganisasiExtService : IAktivitiOrganisasiExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<AktivitiOrganisasiExtService> _logger;

        public AktivitiOrganisasiExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<AktivitiOrganisasiExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<PagedResult<StrukturAktivitiOrganisasiDto>> StrukturAktivitiOrganisasi(string? KodCartaOrganisasi, int parentId = 0, int page = 1, int pageSize = 50, string? keyword = null, string? sortBy = "UnitOrganisasi", bool desc = false, CancellationToken ct = default)
        {
            try

            {

                page = page <= 0 ? 1 : page;
                pageSize = pageSize <= 0 ? 50 : pageSize;
                var result = await (from pdoao in _context.PDOAktivitiOrganisasi
                    join pdorkao in _context.PDORujKategoriAktivitiOrganisasi  on pdoao.KodRujKategoriAktivitiOrganisasi equals pdorkao.Kod
                    where EF.Functions.Like(pdoao.KodCartaAktiviti, KodCartaOrganisasi + "%")
                    select new StrukturAktivitiOrganisasiDto{
                         AktivitiOrganisasi = pdoao.Nama,
                         Id = pdoao.Id,
                         IdIndukAktivitiOrganisasi = pdoao.IdIndukAktivitiOrganisasi,
                         Kod = pdoao.Kod,
                         KodCartaAktiviti = pdoao.KodCartaAktiviti,
                         KodProgram = pdorkao.Nama.ToUpper() + ' ' +pdoao.KodProgram,
                         Tahap = pdoao.Tahap

                    }
                ).ToListAsync();
                var total = result.Count();
                var ordered = (sortBy ?? "AktivitiOrganisasi").Trim().ToLowerInvariant() switch

                {
                    "kod"     => desc ? result.OrderByDescending(x => x.Kod)     : result.OrderBy(x => x.Kod),
                    "aktivitiorganisasi"     => desc ? result.OrderByDescending(x => x.AktivitiOrganisasi)     : result.OrderBy(x => x.AktivitiOrganisasi),
                };


            var items = ordered
            .Skip((page - 1) * pageSize)        
            .Take(pageSize)        
            .ToList();                                                                                                                                                                                                                                                                            
                return new PagedResult<StrukturAktivitiOrganisasiDto>            
                {
                    Total = total,
                    Items = items   
                };
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in StrukturAktivitiOrganisasi");

                throw;
            }

        }



        public async Task WujudAktivitiOrganisasiBaru(Guid UserId, int IdIndukAktivitiOrganisasi, string? KodProgram, string? Kod, string? Nama, int Tahap, string? KodRujKategoriAktivitiOrganisasi, string? Keterangan)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var entity = new PDOAktivitiOrganisasi();
                entity.IdIndukAktivitiOrganisasi = IdIndukAktivitiOrganisasi;
                entity.KodProgram = KodProgram;
                entity.Kod = Kod;
                entity.Nama = Nama;
                entity.Tahap = Tahap;
                entity.KodRujKategoriAktivitiOrganisasi = KodRujKategoriAktivitiOrganisasi;
                entity.Keterangan = Keterangan;
                entity.Keterangan = Keterangan;
                entity.IdCipta = UserId;
                entity.TarikhCipta = DateTime.Now;
                await _context.PDOAktivitiOrganisasi.AddAsync(entity); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in WujudAktivitiOrganisasiBaru");

                throw;
            }

        }



        public async Task PenjenamaanAktivitiOrganisasi(Guid UserId, int Id, string? Nama)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var data = await (from pdoao in _context.PDOAktivitiOrganisasi
                             where pdoao.Id == Id
                             select pdoao).FirstOrDefaultAsync();
     data.Nama = Nama;

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in PenjenamaanAktivitiOrganisasi");

                throw;
            }

        }



        public async Task<AktivitiOrganisasiDto> BacaAktivitiOrganisasi(int Id)
        {
            try

            {

                var result = await (from pdoao in _context.PDOAktivitiOrganisasi
                    where pdoao.Id == Id
                    select new AktivitiOrganisasiDto{
                         ButiranKemaskini = pdoao.ButiranKemaskini,
                         Id = pdoao.Id,
                         IdCipta = pdoao.IdCipta,
                         IdHapus = pdoao.IdHapus,
                         IdIndukAktivitiOrganisasi = pdoao.IdIndukAktivitiOrganisasi,
                         IdPinda = pdoao.IdPinda,
                         Keterangan = pdoao.Keterangan,
                         Kod = pdoao.Kod,
                         KodCartaAktiviti = pdoao.KodCartaAktiviti,
                         KodProgram = pdoao.KodProgram,
                         KodRujKategoriAktivitiOrganisasi = pdoao.KodRujKategoriAktivitiOrganisasi,
                         Nama = pdoao.Nama,
                         StatusAktif = pdoao.StatusAktif,
                         Tahap = pdoao.Tahap,
                         TarikhCipta = pdoao.TarikhCipta,
                         TarikhHapus = pdoao.TarikhHapus,
                         TarikhPinda = pdoao.TarikhPinda
                    }
                ).FirstOrDefaultAsync();
                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in BacaAktivitiOrganisasi");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanAktivitiOrganisasi()
        {
            try

            {

                var result = await (from pdoao in _context.PDOAktivitiOrganisasi
                    select new DropDownDto{
                         Kod = pdoao.Kod,
                         Nama = pdoao.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanAktivitiOrganisasi");

                throw;
            }

        }



        public async Task DaftarAktivitiOrganisasi(Guid UserId, AktivitiOrganisasiDaftarDto request)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var entity = new PDOAktivitiOrganisasi();
                entity.IdCipta = UserId;
                entity.Kod = request.KodAktiviti;
                entity.Nama = request.NamaAktiviti;
                entity.KodRujKategoriAktivitiOrganisasi = request.KodRujKategoriAktivitiOrganisasi;
                entity.IdAsal = request.IdAsal;
                await _context.PDOAktivitiOrganisasi.AddAsync(entity); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in DaftarAktivitiOrganisasi");

                throw;
            }

        }



        public async Task HapusTerusAktivitiOrganisasi(Guid UserId, int Id)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var entity = await (from pdoao in _context.PDOAktivitiOrganisasi
                             where pdoao.Id == Id select pdoao
                              ).FirstOrDefaultAsync();
                if (entity == null)
                {
                    throw new Exception("PDOAktivitiOrganisasi not found.");
                }
                _context.PDOAktivitiOrganisasi.Remove(entity);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in HapusTerusAktivitiOrganisasi");

                throw;
            }

        }



    }

}

