using HR.PDO.Application.DTOs;
using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Services.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Application.Services.PDO
{
    public class PermohonanJawatanExtService : IPermohonanJawatanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<PermohonanJawatanExtService> _logger;

        public PermohonanJawatanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<PermohonanJawatanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<BacaPermohonanJawatanDto> TambahPermohonanJawatanBaru(TambahPermohonanJawatanBaruDto request)
        {

            try

            {

                var UnitOrganisasi = (from pdouo in _context.PDOUnitOrganisasi
                                      where pdouo.Id == request.IdUnitOrganisasi
                                      select pdouo).FirstOrDefault();
                await _unitOfWork.BeginTransactionAsync();

                var PermohonanJawatan = new PDOPermohonanJawatan();
                PermohonanJawatan.IdUnitOrganisasi = request.IdUnitOrganisasi;
                PermohonanJawatan.NomborRujukan = request.NomborRujukan;
                PermohonanJawatan.Tajuk = request.Tajuk;
                PermohonanJawatan.Keterangan = request.Keterangan;
                PermohonanJawatan.KodRujJenisPermohonan= request.KodRujJenisPermohonan;
                PermohonanJawatan.IdCipta = request.UserId;
                PermohonanJawatan.TarikhCipta = DateTime.Now;
                await _context.PDOPermohonanJawatan.AddAsync(PermohonanJawatan); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                await _unitOfWork.BeginTransactionAsync();

                var StatusPermohonanJawatan = new PDOStatusPermohonanJawatan();
                StatusPermohonanJawatan.IdPermohonanJawatan = PermohonanJawatan.Id;
                StatusPermohonanJawatan.KodRujStatusPermohonanJawatan = "01";
                StatusPermohonanJawatan.TarikhStatusPermohonan = DateTime.Now;
                StatusPermohonanJawatan.StatusAktif = true;
                StatusPermohonanJawatan.IdCipta = request.UserId;
                StatusPermohonanJawatan.TarikhCipta = DateTime.Now;
                await _context.PDOStatusPermohonanJawatan.AddAsync(StatusPermohonanJawatan);
                await _context.SaveChangesAsync();

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return await BacaPermohonanJawatan(PermohonanJawatan.Id);
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahPermohonanJawatanBaru");

                throw;
            }

        }



        public async Task<BacaPermohonanJawatanDto> BacaPermohonanJawatan(int Id)
        {
            try

            {

                var PermohonanJawatan = await (from pdopj in _context.PDOPermohonanJawatan
                    where pdopj.Id == Id
                    select new BacaPermohonanJawatanDto{
                        Id = pdopj.Id,
                        IdUnitOrganisasi = pdopj.IdUnitOrganisasi,
                        IdAgensi = pdopj.IdAgensi,
                        KodRujJenisPermohonan = pdopj.KodRujJenisPermohonan,
                        KodRujJenisPermohonanJPA = pdopj.KodRujJenisPermohonanJPA,
                        NomborRujukan = pdopj.NomborRujukan,
                        Tajuk = pdopj.Tajuk,
                        Keterangan = pdopj.Keterangan,
                        KodRujPasukanPerunding = pdopj.KodRujPasukanPerunding,
                        NoWaranPerjawatan = pdopj.NoWaranPerjawatan,
                        TarikhPermohonan = pdopj.TarikhPermohonan,
                        TarikhCadanganWaran = pdopj.TarikhCadanganWaran,
                        TarikhWaranDiluluskan = pdopj.TarikhWaranDiluluskan,
                        IdCipta = pdopj.IdCipta
                    }
                ).FirstOrDefaultAsync();
                if (PermohonanJawatan == null)
                {
                    throw new Exception("Permohonan Jawatan not found.");
                }
                var StatusPermohonanJawatan = await (from pdospj in _context.PDOStatusPermohonanJawatan
                                    where pdospj.IdPermohonanJawatan == PermohonanJawatan.Id
                                    select pdospj
                ).FirstOrDefaultAsync();

                if (StatusPermohonanJawatan == null)
                {
                    throw new Exception("Status Permohonan Jawatan not found.");
                }

                PermohonanJawatan.StatusPermohonanJawatan = StatusPermohonanJawatan;
                return PermohonanJawatan;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in BacaPermohonanJawatan");

                throw;
            }

        }
        public async Task<MuatPermohonanJawatanOutputDto> MuatPermohonanJawatan(int IdUnitOrganisasi)
        {
            try
            {
                var UnitOrganisasi = (from pdouo in _context.PDOUnitOrganisasi
                                      where pdouo.Id == IdUnitOrganisasi
                                      select pdouo).FirstOrDefault();
                if (UnitOrganisasi == null)
                {
                    throw new Exception("Unit Organisasi not found.");
                }
                var newNomborRujukan = UnitOrganisasi.KodRujJenisAgensi + "/" + UnitOrganisasi.KodKementerian + "/" + UnitOrganisasi.KodJabatan + "/" + DateTime.Now.Year;

                int count = await _context.PDOPermohonanJawatan
                    .CountAsync(p => p.NomborRujukan.StartsWith(newNomborRujukan));
                count++;
                newNomborRujukan = newNomborRujukan + "/" + count.ToString("D3");

                var MuatPermohonanJawatanOutput = new MuatPermohonanJawatanOutputDto
                {
                    IdUnitOrganisasi = UnitOrganisasi.Id,
                    UnitOrganisasi = UnitOrganisasi.Nama ?? string.Empty,
                    NomborRujukan = newNomborRujukan
                };

                return MuatPermohonanJawatanOutput;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in BacaPermohonanJawatan");

                throw;
            }

        }



        public async Task HapusTerusPermohonanJawatan(Guid UserId, int Id)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await (from pdopj in _context.PDOPermohonanJawatan
                             where pdopj.Id == Id select pdopj
                              ).FirstOrDefaultAsync();
                if (entity == null)
                {
                    throw new Exception("PDOPermohonanJawatan not found.");
                }
                _context.PDOPermohonanJawatan.Remove(entity);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in HapusTerusPermohonanJawatan");

                throw;
            }

        }



        public async Task KemaskiniPermohonanJawatan(Guid UserId, int Id, PermohonanJawatanDaftarDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var data = await (from pdopj in _context.PDOPermohonanJawatan
                             where pdopj.Id == Id
                             select pdopj).FirstOrDefaultAsync();
                  data.IdUnitOrganisasi = request.IdAgensi;
                  data.IdAgensi = request.IdAgensi;
                  data.NomborRujukan = request.NomborRujukan;
                  data.Tajuk = request.Tajuk;
                  data.Keterangan = request.Keterangan;
                  data.KodRujJenisPermohonan = request.KodRujJenisPermohonan;
                  data.IdCipta = request.UserId;
                  data.TarikhCipta = DateTime.Now;

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in KemaskiniPermohonanJawatan");

                throw;
            }

        }



        public async Task<List<PermohonanJawatanCarianDto>> CarianSenaraiPermohonanJawatan(CarianSenaraiPermohonanJawatanRequestDto filter)
        {
            try

            {

                var result = await (from pdopj in _context.PDOPermohonanJawatan
                    join pdospj in _context.PDOStatusPermohonanJawatan on pdopj.Id equals pdospj.IdPermohonanJawatan
                    join pdouo in _context.PDOUnitOrganisasi on pdopj.IdUnitOrganisasi equals pdouo.Id
                    join pdorjp in _context.PDORujJenisPermohonan on pdopj.KodRujJenisPermohonan equals pdorjp.Kod
                    join pdorspj in _context.PDORujStatusPermohonanJawatan on pdospj.KodRujStatusPermohonanJawatan equals pdorspj.Kod
                    where pdouo.IndikatorAgensi == true && pdouo.StatusAktif == true && pdorspj.Kod == filter.KodRujStatusPermohonanJawatan
                    select new PermohonanJawatanCarianDto{
                         AgensiId = pdopj.IdUnitOrganisasi,
                         KodRujJenisPermohonan = pdopj.KodRujJenisPermohonan,
                         KodRujStatusPermohonanJawatan = pdorspj.Kod,
                         NomborRujukan = pdopj.NomborRujukan,
                         TajukPermohonan = pdopj.Tajuk

                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in CarianSenaraiPermohonanJawatan");

                throw;
            }

        }



        public async Task<PagedResult<PermohonanJawatanLinkDto>> CarianPermohonanJawatan(PermohonanJawatanCarianDto request)
        {
            var page = request.Page <= 0 ? 1 : request.Page;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;
            var sortBy = (request.SortBy ?? "TarikhPermohonan").ToLowerInvariant();
            var desc = request.Desc;

            try
            {
                // Build the query (do NOT call ToListAsync yet)
                IQueryable<PermohonanJawatanLinkDto> q =
                    from pdopj in _context.PDOPermohonanJawatan.AsNoTracking()
                        // LEFT JOIN PDO_StatusPermohonanJawatan
                    join spj0 in _context.PDOStatusPermohonanJawatan.AsNoTracking()
                        on pdopj.Id equals spj0.IdPermohonanJawatan into pdopjGroup
                    from pdospj in pdopjGroup.DefaultIfEmpty()
                        // LEFT JOIN PDO_RujStatusPermohonanJawatan (spj can be null)
                    join ruj0 in _context.PDORujStatusPermohonanJawatan.AsNoTracking()
                         on pdospj.KodRujStatusPermohonanJawatan equals ruj0.Kod into rujGroup
                    from ruj in rujGroup.DefaultIfEmpty()

                    where pdopj.IdUnitOrganisasi == request.IdUnitOrganisasi 
                    //&&
                    //    (pdopj.NomborRujukan.Contains(request.NomborRujukan) || request.NomborRujukan == null) &&
                    //    (pdopj.Tajuk.Contains(request.TajukPermohonan) || request.TajukPermohonan == null) &&
                    //    (pdopj.KodRujJenisPermohonan == request.KodRujJenisPermohonan || request.KodRujJenisPermohonan == null) 
                   select new PermohonanJawatanLinkDto
                    {
                        Id = pdopj.Id,
                        NomborRujukan = pdopj.NomborRujukan,
                        TajukPermohonan = pdopj.Tajuk,
                        TarikhPermohonan = pdopj.TarikhPermohonan,
                        Status = ruj.Nama
                    };

                // Optional keyword filter
                //if (!string.IsNullOrWhiteSpace(request.Keyword))
                //{
                //    var kw = request.Keyword.Trim();
                //    q = q.Where(x =>
                //        ((x.NomborRujukan != null && x.NomborRujukan.Contains(kw)) ||
                //        (x.TajukPermohonan != null && EF.Functions.Like(x.TajukPermohonan!, $"%{kw}%")))
                //    );
                //}
                var total = await q.CountAsync();
                // Sorting
                q = sortBy switch
                {
                    "nomborrujukan" or "rujukan" or "nombor"
                        => desc ? q.OrderByDescending(x => x.NomborRujukan) : q.OrderBy(x => x.NomborRujukan),
                    "tajuk"
                        => desc ? q.OrderByDescending(x => x.TajukPermohonan) : q.OrderBy(x => x.TajukPermohonan),
                    "status"
                        => desc ? q.OrderByDescending(x => x.Status) : q.OrderBy(x => x.Status),
                    _ // default: TarikhPermohonan
                        => desc ? q.OrderByDescending(x => x.TarikhPermohonan) : q.OrderBy(x => x.TarikhPermohonan),
                };

                // Pagination (materialize here)
                var items = await q
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                .ToListAsync();
                return new PagedResult<PermohonanJawatanLinkDto>
                {
                    Items = items,
                    Page = page,
                    PageSize = pageSize,
                    Total = total
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CarianPermohonanJawatan");
                throw;
            }
        }


        public async Task<List<PermohonanJawatanLinkDto>> SenaraiPermohonanJawatan(PermohonanJawatanCarianDto request)
        {
            /*
             * Created By: Khairi Abu Bakar
             * Date: 30/08/2025
             * Purpose: To get all Permohonanan Jawatan records for the current Unit Organisasi
             */
            try

            {

                var result = await (from pdopj in _context.PDOPermohonanJawatan
                    join pdouo in _context.PDOUnitOrganisasi on pdopj.IdUnitOrganisasi equals pdouo.Id
                    join pdorjp in _context.PDORujJenisPermohonan on pdopj.KodRujJenisPermohonan equals pdorjp.Kod
                    join pdorpp in _context.PDORujPasukanPerunding on pdopj.KodRujPasukanPerunding equals pdorpp.Kod
                    select new PermohonanJawatanLinkDto{
                         Agensi = pdouo.Nama,
                         Id = pdopj.Id,
                         IdAgensi = pdopj.IdAgensi,
                         IdUnitOrganisasi = pdopj.IdUnitOrganisasi,
                         JenisPermohonan = pdorjp.Nama,
                         Keterangan = pdopj.Keterangan,
                         NomborRujukan = pdopj.NomborRujukan,
                         PasukanPerunding = pdorpp.Nama,

                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiPermohonanJawatan");

                throw;
            }

        }



        public async Task DaftarPermohonanJawatan(PermohonanJawatanDaftarDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOPermohonanJawatan();
                entity.IdCipta = request.UserId;
                entity.IdUnitOrganisasi = request.IdAgensi;
                entity.IdAgensi = request.IdAgensi;
                entity.NomborRujukan = request.NomborRujukan;
                entity.Tajuk = request.Tajuk;
                entity.Keterangan = request.Keterangan;
                entity.KodRujJenisPermohonan = request.KodRujJenisPermohonan;
                entity.IdCipta = request.UserId;
                entity.TarikhCipta = DateTime.Now;
                await _context.PDOPermohonanJawatan.AddAsync(entity); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in DaftarPermohonanJawatan");

                throw;
            }

        }

        public async Task KemaskiniUlasanStatusPermohonanJawatan(UlasanRequestDto request)
        {

            try

            {
                var recordPermohonan = (from pdopj in _context.PDOPermohonanJawatan
                              where pdopj.Id == request.IdPermohonanJawatan
                              select pdopj).FirstOrDefault();


                var record = (from pdospj in _context.PDOStatusPermohonanJawatan
                              where pdospj.IdPermohonanJawatan == request.IdPermohonanJawatan
                              && pdospj.StatusAktif == true
                              select pdospj).FirstOrDefault();

                if (record != null)
                {
                    await _unitOfWork.BeginTransactionAsync();
                    record.StatusAktif = false;
                    record.IdHapus = request.UserId;
                    record.TarikhHapus = DateTime.Now;
                    _context.PDOStatusPermohonanJawatan.Update(record);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();
                }

                var newStatusPermohonanJawatan = new PDOStatusPermohonanJawatan()
                {
                    IdPermohonanJawatan = request.IdPermohonanJawatan,
                    TarikhStatusPermohonan = DateTime.Now,
                    Ulasan = request.Ulasan,
                    IdCipta = request.UserId,
                    TarikhCipta = DateTime.Now,
                    KodRujStatusPermohonanJawatan = request.KodRujStatusPermohonanJawatan,
                    StatusAktif = true
                };

                await _unitOfWork.BeginTransactionAsync();

                await _context.PDOStatusPermohonanJawatan.AddAsync(newStatusPermohonanJawatan);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in DaftarPermohonanJawatan");

                throw;
            }

        }

        public async Task KemaskiniUlasanPermohonanJawatan(UlasanRequestDto request)
        {

            try

            {
                var recordPermohonan = (from pdopj in _context.PDOPermohonanJawatan
                                        where pdopj.Id == request.IdPermohonanJawatan
                                        select pdopj).FirstOrDefault();


                var record = (from pdospj in _context.PDOStatusPermohonanJawatan
                              where pdospj.IdPermohonanJawatan == request.IdPermohonanJawatan
                              && pdospj.StatusAktif == true
                              select pdospj).FirstOrDefault();

                if (record != null)
                {
                    await _unitOfWork.BeginTransactionAsync();
                    record.StatusAktif = false;
                    record.IdHapus = request.UserId;
                    record.TarikhHapus = DateTime.Now;
                    _context.PDOStatusPermohonanJawatan.Update(record);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();
                }

                var newStatusPermohonanJawatan = new PDOStatusPermohonanJawatan()
                {
                    IdPermohonanJawatan = request.IdPermohonanJawatan,
                    TarikhStatusPermohonan = DateTime.Now,
                    Ulasan = request.Ulasan,
                    IdCipta = request.UserId,
                    TarikhCipta = DateTime.Now,
                    StatusAktif = true
                };

                await _unitOfWork.BeginTransactionAsync();

                await _context.PDOStatusPermohonanJawatan.AddAsync(newStatusPermohonanJawatan);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in DaftarPermohonanJawatan");

                throw;
            }

        }

    }

}

