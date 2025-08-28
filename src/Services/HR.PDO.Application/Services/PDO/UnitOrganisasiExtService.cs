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
    public class UnitOrganisasiExtService : IUnitOrganisasiExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<UnitOrganisasiExtService> _logger;

        public UnitOrganisasiExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<UnitOrganisasiExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<PagedResult<StrukturUnitOrganisasiDto>> StrukturUnitOrganisasi(string? KodCartaOrganisasi, int parentId = 0, int page = 1, int pageSize = 50, string? keyword = null, string? sortBy = "UnitOrganisasi", bool desc = false, CancellationToken ct = default)
        {
            try

            {

                page = page <= 0 ? 1 : page;
                pageSize = pageSize <= 0 ? 50 : pageSize;
                var result = await (from pdouo in _context.PDOUnitOrganisasi
                    join pdorkuo in _context.PDORujKategoriUnitOrganisasi  on pdouo.KodRujKategoriUnitOrganisasi equals pdorkuo.Kod
                    where EF.Functions.Like(pdouo.KodCartaOrganisasi, KodCartaOrganisasi + "%")
                    select new StrukturUnitOrganisasiDto{
                         Id = pdouo.Id,
                         IdIndukUnitOrganisasi = pdouo.IdIndukUnitOrganisasi,
                         KategoriUnitOrganisasi = pdorkuo.Nama,
                         Kod = pdorkuo.Kod,
                         Tahap = pdouo.Tahap,
                         UnitOrganisasi = pdouo.Nama

                    }
                ).ToListAsync();
                var total = result.Count();
                var ordered = (sortBy ?? "UnitOrganisasi").Trim().ToLowerInvariant() switch

                {
                    "kod"     => desc ? result.OrderByDescending(x => x.Kod)     : result.OrderBy(x => x.Kod),
                    "unitorganisasi"     => desc ? result.OrderByDescending(x => x.UnitOrganisasi)     : result.OrderBy(x => x.UnitOrganisasi),
                };


            var items = ordered
            .Skip((page - 1) * pageSize)        
            .Take(pageSize)        
            .ToList();                                                                                                                                                                                                                                                                            
                return new PagedResult<StrukturUnitOrganisasiDto>            
                {
                    Total = total,
                    Items = items   
                };
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in StrukturUnitOrganisasi");

                throw;
            }

        }



        public async Task<List<UnitOrganisasiLinkDto>> CarianUnitOrganisasi(UnitOrganisasiCarianDto request)
        {
            try

            {

                var result = await (from pdouo in _context.PDOUnitOrganisasi
                    join pdorja in _context.PDORujJenisAgensi on pdouo.KodRujJenisAgensi equals pdorja.Kod
                    join pdorkuo in _context.PDORujKategoriUnitOrganisasi on pdouo.KodRujKategoriUnitOrganisasi equals pdorkuo.Kod
                    join pdork in _context.PDORujKluster on pdouo.KodRujKluster equals pdork.Kod
                    where (pdouo.Kod == request.KodUnitOrganisasi || request.KodUnitOrganisasi==null) && (pdouo.Nama == request.NamaUnitOrganisasi || request.NamaUnitOrganisasi==null) && (pdouo.Keterangan == request.Keterangan || request.Keterangan==null)
                    select new UnitOrganisasiLinkDto{
                         ButiranKemaskini = pdouo.ButiranKemaskini,
                         Id = pdouo.Id,
                         IdAsal = pdouo.IdAsal,
                         IdCipta = pdouo.IdCipta,
                         IdHapus = pdouo.IdHapus,
                         IdIndukUnitOrganisasi = pdouo.IdIndukUnitOrganisasi,
                         IdPinda = pdouo.IdPinda,
                         IndikatorAgensi = pdouo.IndikatorAgensi,
                         IndikatorAgensiRasmi = pdouo.IndikatorAgensiRasmi,
                         IndikatorJabatanDiKerajaanNegeri = pdouo.IndikatorJabatanDiKerajaanNegeri,
                         IndikatorPemohonPerjawatan = pdouo.IndikatorPemohonPerjawatan,
                         JenisAgensi = pdorja.Nama,
                         KategoriUnitOrganisasi = pdorkuo.Nama,
                         Keterangan = pdouo.Keterangan,
                         Kluster = pdork.Nama,
                         Kod = pdouo.Kod,
                         KodCartaOrganisasi = pdouo.KodCartaOrganisasi,
                         KodJabatan = pdouo.KodJabatan,
                         KodKementerian = pdouo.KodKementerian,
                         Nama = pdouo.Nama,
                         SejarahPenubuhan = pdouo.SejarahPenubuhan,
                         Singkatan = pdouo.Singkatan,
                         StatusAktif = pdouo.StatusAktif,
                         Tahap = pdouo.Tahap,
                         TarikhAkhirPengukuhan = pdouo.TarikhAkhirPengukuhan,
                         TarikhAkhirPenyusunan = pdouo.TarikhAkhirPenyusunan,
                         TarikhCipta = pdouo.TarikhCipta,
                         TarikhHapus = pdouo.TarikhHapus,
                         TarikhPenubuhan = pdouo.TarikhPenubuhan,
                         TarikhPinda = pdouo.TarikhPinda

                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in CarianUnitOrganisasi");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanUnitOrganisasi()
        {
            try

            {

                var result = await (from pdouo in _context.PDOUnitOrganisasi
                    select new DropDownDto{
                         Kod = pdouo.Kod,
                         Nama = pdouo.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanUnitOrganisasi");

                throw;
            }

        }



        public async Task<List<UnitOrganisasiLinkDto>> SenaraiUnitOrganisasi(UnitOrganisasiCarianDto request)
        {
            try

            {

                var result = await (from pdouo in _context.PDOUnitOrganisasi
                    join pdorja in _context.PDORujJenisAgensi on pdouo.KodRujJenisAgensi equals pdorja.Kod
                    join pdorkuo in _context.PDORujKategoriUnitOrganisasi on pdouo.KodRujKategoriUnitOrganisasi equals pdorkuo.Kod
                    join pdork in _context.PDORujKluster on pdouo.KodRujKluster equals pdork.Kod
                    select new UnitOrganisasiLinkDto{
                         ButiranKemaskini = pdouo.ButiranKemaskini,
                         Id = pdouo.Id,
                         IdAsal = pdouo.IdAsal,
                         IdCipta = pdouo.IdCipta,
                         IdHapus = pdouo.IdHapus,
                         IdIndukUnitOrganisasi = pdouo.IdIndukUnitOrganisasi,
                         IdPinda = pdouo.IdPinda,
                         IndikatorAgensi = pdouo.IndikatorAgensi,
                         IndikatorAgensiRasmi = pdouo.IndikatorAgensiRasmi,
                         IndikatorJabatanDiKerajaanNegeri = pdouo.IndikatorJabatanDiKerajaanNegeri,
                         IndikatorPemohonPerjawatan = pdouo.IndikatorPemohonPerjawatan,
                         JenisAgensi = pdorja.Nama,
                         KategoriUnitOrganisasi = pdorkuo.Nama,
                         Keterangan = pdouo.Keterangan,
                         Kluster = pdork.Nama,
                         Kod = pdouo.Kod,
                         KodCartaOrganisasi = pdouo.KodCartaOrganisasi,
                         KodJabatan = pdouo.KodJabatan,
                         KodKementerian = pdouo.KodKementerian,
                         Nama = pdouo.Nama,
                         SejarahPenubuhan = pdouo.SejarahPenubuhan,
                         Singkatan = pdouo.Singkatan,
                         StatusAktif = pdouo.StatusAktif,
                         Tahap = pdouo.Tahap,
                         TarikhAkhirPengukuhan = pdouo.TarikhAkhirPengukuhan,
                         TarikhAkhirPenyusunan = pdouo.TarikhAkhirPenyusunan,
                         TarikhCipta = pdouo.TarikhCipta,
                         TarikhHapus = pdouo.TarikhHapus,
                         TarikhPenubuhan = pdouo.TarikhPenubuhan,
                         TarikhPinda = pdouo.TarikhPinda

                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiUnitOrganisasi");

                throw;
            }

        }



        public async Task KemaskiniUnitOrganisasi(Guid UserId, int Id, UnitOrganisasiDaftarDto request)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var data = await (from pdouo in _context.PDOUnitOrganisasi
                             where pdouo.Id == Id
                             select pdouo).FirstOrDefaultAsync();
                  data.Id = request.Id;
                  data.KodRujKategoriUnitOrganisasi = request.KodRujKategoriUnitOrganisasi;
                  data.KodRujJenisAgensi = request.KodRujJenisAgensi;
                  data.KodRujKluster = request.KodRujKluster;
                  data.IdIndukUnitOrganisasi = request.IdIndukUnitOrganisasi;
                  data.KodKementerian = request.KodKementerian;
                  data.KodJabatan = request.KodJabatan;
                  data.Kod = request.Kod;
                  data.Nama = request.Nama;
                  data.Singkatan = request.Singkatan;
                  data.Keterangan = request.Keterangan;
                  data.Tahap = request.Tahap;
                  data.KodCartaOrganisasi = request.KodCartaOrganisasi;
                  data.IndikatorAgensi = request.IndikatorAgensi;
                  data.IndikatorAgensiRasmi = request.IndikatorAgensiRasmi;
                  data.IndikatorPemohonPerjawatan = request.IndikatorPemohonPerjawatan;
                  data.IndikatorJabatanDiKerajaanNegeri = request.IndikatorJabatanDiKerajaanNegeri;
                  data.TarikhPenubuhan = request.TarikhPenubuhan;
                  data.SejarahPenubuhan = request.SejarahPenubuhan;
                  data.TarikhAkhirPenyusunan = request.TarikhAkhirPenyusunan;
                  data.TarikhAkhirPengukuhan = request.TarikhAkhirPengukuhan;
                  data.ButiranKemaskini = request.ButiranKemaskini;
                  data.StatusAktif = request.StatusAktif;
                  data.IdCipta = request.IdCipta;
                  data.TarikhCipta = request.TarikhCipta;
                  data.IdPinda = request.IdPinda;
                  data.TarikhPinda = request.TarikhPinda;
                  data.IdHapus = request.IdHapus;
                  data.TarikhHapus = request.TarikhHapus;
                  data.IdAsal = request.IdAsal;

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in KemaskiniUnitOrganisasi");

                throw;
            }

        }



        public async Task PenjenamaanSemulaUnitOrganisasi(Guid UserId, int Id, string? Nama)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var data = await (from pdouo in _context.PDOUnitOrganisasi
                             where pdouo.Id == Id
                             select pdouo).FirstOrDefaultAsync();
     data.Nama = Nama;

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in PenjenamaanSemulaUnitOrganisasi");

                throw;
            }

        }



        public async Task HapusTerusUnitOrganisasi(Guid UserId, int Id)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var entity = await (from pdouo in _context.PDOUnitOrganisasi
                             where pdouo.Id == Id select pdouo
                              ).FirstOrDefaultAsync();
                if (entity == null)
                {
                    throw new Exception("PDOUnitOrganisasi not found.");
                }
                _context.PDOUnitOrganisasi.Remove(entity);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in HapusTerusUnitOrganisasi");

                throw;
            }

        }



    }

}

