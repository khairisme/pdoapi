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
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using Azure.Core;

namespace HR.Application.Services.PDO
{
    public class UnitOrganisasiExtService : IUnitOrganisasiExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly IRujNegaraExt _rujnegaraext;
        private readonly IRujNegeriExt _rujnegeriext;
        private readonly IRujBandarExt _rujbandarext;
        private readonly IRujukanJenisAgensiExt _rujukanjenisagensiext;
        private readonly IRujukanAgensiExt _rujukanagensiext;
        private readonly IRujukanKlusterExt _rujukanklusterext;


        private readonly PDODbContext _context;
        private readonly ILogger<UnitOrganisasiExtService> _logger;

        public UnitOrganisasiExtService(IRujukanKlusterExt rujukanklusterext, IRujukanAgensiExt rujukanagensiext, IRujukanJenisAgensiExt rujukanjenisagensiext,IRujNegaraExt rujnegaraext, IRujNegeriExt rujnegeriext, IRujBandarExt rujbandarext, IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<UnitOrganisasiExtService> logger)
        {
            _rujukanklusterext = rujukanklusterext;
            _rujukanagensiext = rujukanagensiext;
            _rujukanjenisagensiext = rujukanjenisagensiext;
            _rujnegaraext = rujnegaraext;
            _rujnegeriext = rujnegeriext;
            _rujbandarext = rujbandarext;
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<PagedResult<StrukturUnitOrganisasiDto>> StrukturUnitOrganisasi(StrukturUnitOrganisasiRequestDto request)
        {
            try

            {
                int newParentId = 0;
                bool rootparent = false;
                request.Page = request.Page <= 0 ? 1 : request.Page;
                request.PageSize = request.PageSize <= 0 ? 50 : request.PageSize;
                var query = from pdouo in _context.PDOUnitOrganisasi.AsNoTracking()
                            join pdorkuo in _context.PDORujKategoriUnitOrganisasi.AsNoTracking() on pdouo.KodRujKategoriUnitOrganisasi equals pdorkuo.Kod
                            where (pdouo.KodCartaOrganisasi == request.KodCartaOrganisasi)
                                    && pdouo.StatusAktif == true
                            orderby pdouo.KodCartaOrganisasi
                            select new { pdouo, pdorkuo };

                if (request.ResultChild==false)
                {
                    query = query.Where(x => x.pdouo.KodCartaOrganisasi == request.KodCartaOrganisasi);
                } else
                {
                    var IdInduk = await (from pdoao in _context.PDOUnitOrganisasi
                                         where pdoao.KodCartaOrganisasi == request.KodCartaOrganisasi
                                         select pdoao.Id).FirstOrDefaultAsync();
                    query = query.Where(x => x.pdouo.IdIndukUnitOrganisasi == IdInduk);

                }

                var result = await query
                             .Select(x => new StrukturUnitOrganisasiDto
                             {
                                 Id = x.pdouo.Id,
                                 IdIndukUnitOrganisasi = x.pdouo.IdIndukUnitOrganisasi,
                                 KategoriUnitOrganisasi = x.pdorkuo.Nama,
                                 Kod = x.pdorkuo.Kod,
                                 Tahap = x.pdouo.Tahap,
                                 UnitOrganisasi = x.pdouo.Nama,
                                 KodCartaOrganisasi = x.pdouo.KodCartaOrganisasi,
                                 NamaAgensi = x.pdouo.IndikatorAgensi == true ? x.pdouo.Nama : "",
                                 NamaUnitOrganisasi = x.pdouo.IndikatorAgensi == false ? x.pdouo.Nama : "",
                                 ButiranKemaskini = x.pdouo.ButiranKemaskini,
                                 StatusAktif = x.pdouo.StatusAktif ?? false,
                             })
                             .ToListAsync();
                
                //Get Parent

                foreach (var item in result)
                {

                    if (item.ButiranKemaskini != null)
                    {
                        var butiranKemaskiniList = JsonSerializer.Deserialize<List<ButiranKemaskiniDto>>(item.ButiranKemaskini);
                        if (butiranKemaskiniList != null)
                        {
                            foreach (var butiran in butiranKemaskiniList)
                            {
                                if (item.StatusAktif == false)
                                {
                                    item.StatusUnitOrganisasi = "WujudBaru";
                                }
                                else if (item.StatusAktif == true)
                                {
                                    if (butiran.StatusAktif == false)
                                    {
                                        item.StatusUnitOrganisasi = "Mansuh";
                                    }
                                    else if (butiran.Nama != null)
                                    {
                                        item.StatusUnitOrganisasi = "JenamaSemula";
                                    }
                                    else if (butiran.NewParentId != null)
                                    {
                                        item.StatusUnitOrganisasi = "PindahButiran";
                                    }
                                }
                            }
                        }
                    }
                    var butiranPermohonan = await (from pdobp in _context.PDOButiranPermohonan
                                                   where pdobp.IdPermohonanJawatan == request.IdPermohonanJawatan 
                                                   && pdobp.IdAktivitiOrganisasi == request.IdAktivitiOrganisasi
                                                   
                                                   select pdobp).ToListAsync();
                    item.ButiranPermohonan = butiranPermohonan;
                    item.Children = StrukturUnitOrganisasiGetChildren(item.Id, item.KodCartaOrganisasi, request).Result;
                    var count = (from pdouo in _context.PDOUnitOrganisasi
                                 where pdouo.IdIndukUnitOrganisasi == item.Id
                                 select pdouo.Id).Count();
                    if (count > 0) item.HasChildren = true;
                }


                //if (request.ParentId == 0)
                //{
                //    //foreach (var item in result)
                //    //{

                //    //    if (item.Id == item.IdIndukUnitOrganisasi)
                //    //    {
                //    //        rootparent = true;
                //    //    }
                //    //    else
                //    //    {
                //    //        newParentId = item.IdIndukUnitOrganisasi;
                //    //    }
                //    //}
                //    //result = await StrukturUnitOrganisasiGetParent(newParentId, result);
                //} else
                //{
                //    foreach (var item in result)
                //    {
                //        var children = StrukturUnitOrganisasiGetChildren(item.Id).Result;
                //        if (children.Count() > 0) item.HasChildren = true;
                //    }

                //}

                var total = result.Count();
                var ordered = (request.SortBy ?? "UnitOrganisasi").Trim().ToLowerInvariant() switch

                {
                    "kod"     => request.Desc ? result.OrderByDescending(x => x.Kod)     : result.OrderBy(x => x.Kod),
                    "unitorganisasi"     => request.Desc ? result.OrderByDescending(x => x.UnitOrganisasi)     : result.OrderBy(x => x.UnitOrganisasi),
                };


            var items = ordered
            .Skip((request.Page - 1) * request.PageSize)        
            .Take(request.PageSize)        
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
        public async Task<List<StrukturUnitOrganisasiDto>> StrukturUnitOrganisasiGetParent(int? Id, List<StrukturUnitOrganisasiDto> child)
        {
            bool rootparent = false;
            int? newParentId = 0;
            var result = await (from pdouo in _context.PDOUnitOrganisasi
                                join pdorkuo in _context.PDORujKategoriUnitOrganisasi on pdouo.KodRujKategoriUnitOrganisasi equals pdorkuo.Kod
                                where pdouo.Id == Id
                                select new StrukturUnitOrganisasiDto
                                {
                                    Children = child,
                                    HasChildren = child.Count() > 0 ? true: false,
                                    Id = pdouo.Id,
                                    IdIndukUnitOrganisasi = pdouo.IdIndukUnitOrganisasi,
                                    KategoriUnitOrganisasi = pdorkuo.Nama,
                                    Kod = pdorkuo.Kod,
                                    Tahap = pdouo.Tahap,
                                    UnitOrganisasi = pdouo.Nama

                                }
            ).ToListAsync();

            foreach (var item in result)
            {
                if (item.Id == item.IdIndukUnitOrganisasi)
                {
                    rootparent = true;
                } else
                {
                    newParentId = item.IdIndukUnitOrganisasi;
                }
            }
            if (rootparent)
            {
                return result;
            } else
            {
                return await StrukturUnitOrganisasiGetParent(newParentId, result);
            }
        }

        public async Task<List<StrukturUnitOrganisasiDto>> StrukturUnitOrganisasiGetChildren(int? Id, string? KodCartaOrganisasi, StrukturUnitOrganisasiRequestDto request)
        {
            bool rootparent = false;
            int newParentId = 0;
            var query = from pdouo in _context.PDOUnitOrganisasi
                                join pdorkuo in _context.PDORujKategoriUnitOrganisasi on pdouo.KodRujKategoriUnitOrganisasi equals pdorkuo.Kod
                                orderby pdouo.KodCartaOrganisasi
                                select new { pdouo, pdorkuo };
            query = query.Where(x => x.pdouo.IdIndukUnitOrganisasi == Id && x.pdouo.StatusAktif == true);

            var result = await query.Select(x => new StrukturUnitOrganisasiDto
                    {
                        HasChildren = false,
                        Id = x.pdouo.Id,
                        IdIndukUnitOrganisasi = x.pdouo.IdIndukUnitOrganisasi,
                        KategoriUnitOrganisasi = x.pdorkuo.Nama,
                        Kod = x.pdorkuo.Kod,
                        Tahap = x.pdouo.Tahap,
                        UnitOrganisasi = x.pdouo.Nama,
                        KodCartaOrganisasi = x.pdouo.KodCartaOrganisasi,
                        NamaAgensi = x.pdouo.IndikatorAgensi == true ? x.pdouo.Nama : "",
                        NamaUnitOrganisasi = x.pdouo.IndikatorAgensi == false ? x.pdouo.Nama : "",
                    }).ToListAsync();

            foreach(var item in result)
            {

                var butiranPermohonan = await (from pdobp in _context.PDOButiranPermohonan
                                               where pdobp.IdPermohonanJawatan == request.IdPermohonanJawatan
                                               && pdobp.IdAktivitiOrganisasi == request.IdAktivitiOrganisasi

                                               select pdobp).ToListAsync();
                item.ButiranPermohonan = butiranPermohonan;
                var count = await (from pdouo in _context.PDOUnitOrganisasi
                                       join pdorkuo in _context.PDORujKategoriUnitOrganisasi on pdouo.KodRujKategoriUnitOrganisasi equals pdorkuo.Kod
                                       where pdouo.IdIndukUnitOrganisasi == item.Id
                                   select pdouo.Id).CountAsync();
                if (count > 0) item.HasChildren = true;
            }

            return result;
        }

        public async Task<List<CarianUnitOrganisasiDto>> CarianUnitOrganisasi(UnitOrganisasiCarianRequestDto request)
        {
            try

            {

                var result = await (from pdouo in _context.PDOUnitOrganisasi
                    join pdorja in _context.PDORujJenisAgensi on pdouo.KodRujJenisAgensi equals pdorja.Kod
                    join pdorkuo in _context.PDORujKategoriUnitOrganisasi on pdouo.KodRujKategoriUnitOrganisasi equals pdorkuo.Kod
                    join pdork in _context.PDORujKluster on pdouo.KodRujKluster equals pdork.Kod
                    where (pdouo.Kod.Contains(request.Kod) || request.Kod==null) 
                    && (pdouo.Nama.Contains(request.NamaAgensi) || request.NamaAgensi==null) 
                    select new CarianUnitOrganisasiDto
                    {
                         Id = pdouo.Id,
                         Kod = pdouo.Kod,
                         NamaAgensi = pdouo.Nama,

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

        public async Task<string?> JanaKodAgensi(JanaKodAgensiRequestDto request)
        {
            /*
            public string? KodKementerian { get; set; }
            public string? KodJabatan { get; set; }

             */

            var current = (from pdouo in _context.PDOUnitOrganisasi
                               where pdouo.Id == request.IdUnitOrganisasi
                          select pdouo).FirstOrDefault();

            var currentKodCarta = current.KodCartaOrganisasi;
            string? KodPrefix = current.Kod;
            int count = await _context.PDOUnitOrganisasi
                .CountAsync(puo =>
                    puo.Kod == current.Kod);
            var splitKod = current.Kod.Split("-");
            var running = splitKod[(int)current.Tahap];
            count = Int32.Parse(running);
            count++;
            splitKod[count] = count.ToString("D3");
            return string.Join("-", splitKod);
        }


        public async Task<MuatUnitOrganisasiDto> MuatUnitOrganisasi()
        {
            try
            {
                var muatUnitOrganisasi = new MuatUnitOrganisasiDto();
                muatUnitOrganisasi.JenisAgensiList = await _rujukanjenisagensiext.RujukanJenisAgensi();
                muatUnitOrganisasi.AgensiList = await _rujukanagensiext.RujukanAgensi("");
                muatUnitOrganisasi.KlusterList = await _rujukanklusterext.RujukanKluster();
                return muatUnitOrganisasi;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in BacaUnitOrganisasi");

                throw;
            }

        }
        public async Task<UnitOrganisasiFormDisplayDto> BacaUnitOrganisasi(int? Id)
        {
            try

            {

                var result = await (from pdouo in _context.PDOUnitOrganisasi
                                    join paoparent in _context.PDOUnitOrganisasi on pdouo.IdIndukUnitOrganisasi equals paoparent.Id
                                    join pdorja in _context.PDORujJenisAgensi on pdouo.KodRujJenisAgensi equals pdorja.Kod
                                    where pdouo.Id == Id
                                    select new UnitOrganisasiFormDisplayDto
                                    {
                                        JenisAgensi = pdorja.Nama,
                                        Keterangan = pdouo.Keterangan,
                                        KodUnitOrganisasi = pdouo.Kod,
                                        NamUnitOrganisasi = pdouo.Nama,
                                        Tahap = pdouo.Tahap,
                                        UnitOrganisasiInduk = pdouo.Nama,
                                        KodJabatan = pdouo.KodJabatan,
                                        KodKementerian = pdouo.KodKementerian,
                                        KodRujJenisAgensi = pdorja.Kod
                                    }
                ).FirstOrDefaultAsync();
                string? KodPrefix = result.KodRujJenisAgensi + result.KodKementerian + "-"+result.KodJabatan;
                int count = await _context.PDOUnitOrganisasi
                    .CountAsync(puo =>
                        puo.KodRujJenisAgensi == result.KodRujJenisAgensi &&
                        puo.KodKementerian == result.KodKementerian &&
                        puo.KodJabatan == result.KodJabatan);
                count++;
                string countStr = count.ToString("D7");
                result.KodUnitOrganisasi = KodPrefix + "-" + countStr;
                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in BacaUnitOrganisasi");

                throw;
            }

        }


        public async Task<List<DropDownDto>> RujukanUnitOrganisasi()
        {
            try

            {

                var result = await (from pdouo in _context.PDOUnitOrganisasi
                    select new DropDownDto{
                         Kod = pdouo.Kod.Trim(),
                         Nama = pdouo.Nama.Trim()
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
                         IdIndukUnitOrganisasi = pdouo.IdIndukUnitOrganisasi,
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
                         UnitOrganisasi = pdouo.Nama,
                         SejarahPenubuhan = pdouo.SejarahPenubuhan,
                         Singkatan = pdouo.Singkatan,
                         StatusAktif = pdouo.StatusAktif ?? false,
                         Tahap = pdouo.Tahap,

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



        public async Task KemaskiniUnitOrganisasi(UnitOrganisasiDaftarDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var data = await (from pdouo in _context.PDOUnitOrganisasi
                             where pdouo.Id == request.Id
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
                  data.ButiranKemaskini = request.ButiranKemaskini;
                  data.StatusAktif = request.StatusAktif;
                  data.IdPinda = request.UserId;
                  data.TarikhPinda = DateTime.Now;
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

        public async Task KemaskiniSemakUnitOrganisasi(KemaskiniSemakUnitOrganisasiRequestDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var data = await (from pdouo in _context.PDOUnitOrganisasi
                                  where pdouo.Id == request.Id
                                  select pdouo).FirstOrDefaultAsync();

                var json = JsonSerializer.Serialize(request);
                data.ButiranKemaskini = json;
                data.IdPinda = request.UserId;
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in KemaskiniUnitOrganisasi");

                throw;
            }

        }
        public async Task PenjenamaanSemulaUnitOrganisasi(PenjenamaanUnitOrganisasiDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var data = await (from pdouo in _context.PDOUnitOrganisasi
                             where pdouo.Id == request.Id
                             select pdouo).FirstOrDefaultAsync();
                var newPDOUnitOrganisasi = new PDOUnitOrganisasi
                {
                    // ⚠️ Do not copy Id — let EF generate it if identity
                    KodRujKategoriUnitOrganisasi = data.KodRujKategoriUnitOrganisasi,
                    IdIndukUnitOrganisasi = data.IdIndukUnitOrganisasi, // <-- override parent
                    Kod = data.Kod,
                    Nama = data.Nama,
                    Keterangan = data.Keterangan,
                    Tahap = data.Tahap,
                    KodCartaOrganisasi = data.KodCartaOrganisasi,
                    StatusAktif = data.StatusAktif ?? false,

                    // metadata
                    IdCipta = Guid.NewGuid(),          // new creator? (depends on your logic)
                    TarikhCipta = DateTime.UtcNow,
                    IdPinda = request.UserId,
                    TarikhPinda = DateTime.Now,
                    IdHapus = null,
                    TarikhHapus = null,
                };

                string json = JsonSerializer.Serialize(newPDOUnitOrganisasi);

                data.ButiranKemaskini = json;
                data.IdPinda = request.UserId;
                data.TarikhPinda = DateTime.Now;

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in PenjenamaanSemulaUnitOrganisasi");

                throw;
            }

        }

        public async Task MansuhUnitOrganisasi(MansuhUnitOrganisasiRequestDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await (from pdouo in _context.PDOUnitOrganisasi
                                    where pdouo.Id == request.Id
                                    select pdouo
                              ).FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new Exception("AktivitiOrganisasi not found.");
                }
                else
                {
                    if (entity.StatusAktif != true)
                    {
                        throw new Exception("AktivitiOrganisasi is not valid for Mansuh operation because the StatusAktif is True");
                    }
                    var butiranKemaskiniList = new List<ButiranKemaskiniDto>();

                    if (entity.ButiranKemaskini != null)
                    {
                        butiranKemaskiniList = JsonSerializer.Deserialize<List<ButiranKemaskiniDto>>(entity.ButiranKemaskini);
                    }
                    var newMansuhButiranKemaskini = new ButiranKemaskiniDto
                    {
                        StatusAktif =false,
                        IdHapus = request.UserId,
                        TarikhHapus = DateTime.Now,
                        StatusTindakan = "Mansuh"
                    };
                    butiranKemaskiniList.Add(newMansuhButiranKemaskini);
                    string json = JsonSerializer.Serialize(newMansuhButiranKemaskini);

                    entity.ButiranKemaskini = json;
                    entity.IdPinda = request.UserId;
                    entity.TarikhPinda = DateTime.Now;
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in MansuhAktivitiOrganisasi");

                throw;
            }

        }


        public async Task HapusTerusUnitOrganisasi(HapusTerusUnitOrganisasiRequestDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await (from pdouo in _context.PDOUnitOrganisasi
                             where pdouo.Id == request.Id && pdouo.StatusAktif == true
                                    select pdouo
                              ).FirstOrDefaultAsync();
                if (entity == null)
                {
                    throw new Exception("Tiada Unit Organisasi yang boleh dihapuskan.");
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



        public async Task<UnitOrganisasiFormDisplayDto> WujudUnitOrganisasiBaru(UnitOrganisasiWujudDto request)
            {

                try

                {
                    await _unitOfWork.BeginTransactionAsync();
                    var entity = new PDOUnitOrganisasi();
                    entity.KodRujJenisAgensi = request.KodRujJenisAgensi;
                    entity.KodRujKategoriUnitOrganisasi = request.KodRujKategoriUnitOrganisasi;
                    entity.Kod = request.Kod;
                    entity.Nama = request.Nama;
                    entity.Tahap = request.Tahap;
                    entity.Keterangan = request.Keterangan;
                    entity.StatusAktif = false;
                    entity.IdCipta = request.UserId;
                    entity.TarikhCipta = DateTime.Now;
                    await _context.PDOUnitOrganisasi.AddAsync(entity);

                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();

                    return await BacaUnitOrganisasi(entity.Id);
            }

                catch (Exception ex)

                {

                    _logger.LogError(ex, "Error in WujudUnitOrganisasiBaru");

                    throw;
                }

            }
        public async Task<string?> BacaNamaUnitOrganisasi(int IdUnitOrganisasi)
        {
            try
            {
                var entity = await (from pdouo in _context.PDOUnitOrganisasi
                                   where pdouo.Id == IdUnitOrganisasi
                                   select pdouo
                              ).FirstOrDefaultAsync();
                if (entity == null)
                {
                    throw new Exception("Tiada Unit Organisasi yang boleh dihapuskan.");
                }
                return entity.Nama;

            }
            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in WujudUnitOrganisasiBaru");

                throw;
            }

        }


    }

}

