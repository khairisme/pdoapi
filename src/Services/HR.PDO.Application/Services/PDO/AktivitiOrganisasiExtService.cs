using Azure.Core;
using Azure.Identity;
using HR.PDO.Application.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
using Shared.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private  List<ButiranKemaskiniDto> GetButiranKemaskiniObject(string? butiranKemaskiniJson)
        {
            if (string.IsNullOrEmpty(butiranKemaskiniJson))
            {
                return new List<ButiranKemaskiniDto>();
            }
            try
            {
                return JsonSerializer.Deserialize<List<ButiranKemaskiniDto>>(butiranKemaskiniJson) ?? new List<ButiranKemaskiniDto>();
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Failed to deserialize ButiranKemaskini JSON.");
                return new List<ButiranKemaskiniDto>();
            }
        }
        public async Task<PagedResult<StrukturAktivitiOrganisasiDto>> StrukturAktivitiOrganisasi(StrukturAktivitiOrganisasiRequestDto request)
        {
            try
            {
                int? newParentId = 0;
                bool rootparent = false;
                request.page = request.page <= 0 ? 1 : request.page;
                request.pageSize = request.pageSize <= 0 ? 50 : request.pageSize;

                var query = from pdoao in _context.PDOAktivitiOrganisasi
                                    join pdorkao in _context.PDORujKategoriAktivitiOrganisasi
                                    on pdoao.KodRujKategoriAktivitiOrganisasi equals pdorkao.Kod 
                            orderby pdoao.KodProgram
                            select new { pdoao, pdorkao }

                                    ;
                if (!request.ResultChild)
                {
                    query = query.Where(x =>
                        x.pdoao.KodCartaAktiviti == request.KodCartaAktiviti);
                }
                else
                {
                    query = query.Where(x =>
                        x.pdoao.KodCartaAktiviti.StartsWith(request.KodCartaAktiviti) &&
                        x.pdoao.KodCartaAktiviti.Length == request.KodCartaAktiviti.Length + 2 
                        //&& x.pdoao.Tahap == x.pdorkao.Tahap
                        );
                }

                var result = await query
                    .Select(x => new StrukturAktivitiOrganisasiDto
                    {
                        Id = x.pdoao.Id,
                        IdIndukAktivitiOrganisasi = x.pdoao.IdIndukAktivitiOrganisasi,
                        Kod = x.pdoao.Kod!=null ? x.pdoao.Kod.Trim() : "",
                        KodCartaAktiviti = x.pdoao.KodCartaAktiviti!=null? x.pdoao.KodCartaAktiviti.Trim() : "",
                        KodProgram = (x.pdorkao.Nama ?? "").Trim().ToUpper() + " " + (x.pdoao.KodProgram ?? "").Trim(),
                        AktivitiOrganisasi = (x.pdorkao.Nama ?? "").ToUpper() + " - " +
                                             (x.pdoao.KodProgram ?? "").ToUpper() + " - " +
                                             (x.pdoao.Nama ?? "").ToUpper().Trim(),
                        ButiranKemaskiniJson = x.pdoao.ButiranKemaskini,
                        Tahap = x.pdoao.Tahap,
                        StatusAktif = x.pdoao.StatusAktif ?? false,
                        StatusAktivitiOrganisasi = 
                            x.pdoao.IndikatorRekod == 1 ? "WujudBaru" :
                            x.pdoao.IndikatorRekod == 2 ? "JenamaSemula" :
                            x.pdoao.IndikatorRekod == 3 ? "Kemaskini" :
                            x.pdoao.IndikatorRekod == 4 ? "Mansuh" :
                            "None"
                    })
                    .ToListAsync();
                foreach (var item in result)
                {

                    item.Children = await StrukturAktivitiOrganisasiGetChildren(item.KodCartaAktiviti, request);

                    #region StatusAktivitiOrganisasi Logic (Commented Out)
                    #endregion
                    var queryButiranPermohonan = from bp in _context.PDOButiranPermohonan
                                                 where bp.IdPermohonanJawatan == request.IdPermohonanJawatan
                                                 && bp.IdAktivitiOrganisasi == item.Id
                                                 select new { bp };
                //List<ButiranKemaskiniDto>;

                    if (!string.IsNullOrWhiteSpace(item.ButiranKemaskiniJson))
                    {
                        var butirankemaskini = JsonSerializer.Deserialize<List<ButiranKemaskiniDto>>(item.ButiranKemaskiniJson);
                        item.NamaPenjenamaan = butirankemaskini.LastOrDefault()?.Nama;
                    }
                    else
                    {
                        // Handle null or empty case
                        var obj = new List<ButiranKemaskiniDto>(); // or null, depending on your logic
                    }
                    if (item.Children.Count() > 0)
                    {
                        item.HasChildren = true;
                    } else
                    {
                        item.HasChildren = false;
                    }
                }

                var total = result.Count();
                var ordered = (request.sortBy ?? "AktivitiOrganisasi").Trim().ToLowerInvariant() switch

                {
                    "kod" => request.desc ? result.OrderByDescending(x => x.Kod) : result.OrderBy(x => x.Kod),
                    "aktivitiorganisasi" => request.desc ? result.OrderByDescending(x => x.AktivitiOrganisasi) : result.OrderBy(x => x.AktivitiOrganisasi),
                };


                var items = ordered
                .Skip((request.page - 1) * request.pageSize)
                .Take(request.pageSize)
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

                                                                      
        public async Task<PagedResult<StrukturAktivitiOrganisasiDto>> StrukturButiranAktivitiOrganisasi(StrukturAktivitiOrganisasiRequestDto request)
        {
            try
            {
                int? newParentId = 0;
                bool rootparent = false;
                request.page = request.page <= 0 ? 1 : request.page;
                request.pageSize = request.pageSize <= 0 ? 50 : request.pageSize;

                var query = from pdoao in _context.PDOAktivitiOrganisasi
                            join pdorkao in _context.PDORujKategoriAktivitiOrganisasi
                            on pdoao.KodRujKategoriAktivitiOrganisasi equals pdorkao.Kod
                            orderby pdoao.KodProgram
                            select new { pdoao, pdorkao }

                                    ;
                if (!request.ResultChild)
                {
                    query = query.Where(x =>
                        x.pdoao.KodCartaAktiviti == request.KodCartaAktiviti);
                }
                else
                {
                    var IdInduk = await (from pdoao in _context.PDOAktivitiOrganisasi
                                         where pdoao.KodCartaAktiviti == request.KodCartaAktiviti
                                         select pdoao.Id).FirstOrDefaultAsync();

                    query = query.Where(x =>
                      x.pdoao.IdIndukAktivitiOrganisasi == IdInduk);
                    //query = query.Where(x =>
                    //    x.pdoao.KodCartaAktiviti.StartsWith(request.KodCartaAktiviti) &&
                    //    x.pdoao.KodCartaAktiviti.Length == request.KodCartaAktiviti.Length + 2
                    //    //&& x.pdoao.Tahap == x.pdorkao.Tahap
                    //    );
                }

                var result = await query
                    .Select(x => new StrukturAktivitiOrganisasiDto
                    {
                        Id = x.pdoao.Id,
                        IdIndukAktivitiOrganisasi = x.pdoao.IdIndukAktivitiOrganisasi,
                        Kod = x.pdoao.Kod != null ? x.pdoao.Kod.Trim() : "",
                        KodCartaAktiviti = x.pdoao.KodCartaAktiviti != null ? x.pdoao.KodCartaAktiviti.Trim() : "",
                        KodProgram = (x.pdorkao.Nama ?? "").Trim().ToUpper() + " " + (x.pdoao.KodProgram ?? "").Trim(),
                        AktivitiOrganisasi = (x.pdorkao.Nama ?? "").ToUpper() + " - " +
                                             (x.pdoao.KodProgram ?? "").ToUpper() + " - " +
                                             (x.pdoao.Nama ?? "").ToUpper().Trim(),
                        ButiranKemaskiniJson = x.pdoao.ButiranKemaskini,
                        Tahap = x.pdoao.Tahap,
                        StatusAktif = x.pdoao.StatusAktif ?? false,
                        StatusAktivitiOrganisasi =
                            x.pdoao.IndikatorRekod == 1 ? "WujudBaru" :
                            x.pdoao.IndikatorRekod == 2 ? "JenamaSemula" :
                            x.pdoao.IndikatorRekod == 3 ? "Kemaskini" :
                            x.pdoao.IndikatorRekod == 4 ? "Mansuh" :
                            "None"
                    })
                    .ToListAsync();
                foreach (var item in result)
                {

                    item.Children = await StrukturButiranAktivitiOrganisasiGetChildren((int)item.Id,item.KodCartaAktiviti, request);

                    #region StatusAktivitiOrganisasi Logic (Commented Out)
                    #endregion
                    var queryButiranPermohonan = from bp in _context.PDOButiranPermohonan
                                                 where bp.IdPermohonanJawatan == request.IdPermohonanJawatan
                                                 && bp.IdAktivitiOrganisasi == item.Id
                                                 select new { bp };
                    //List<ButiranKemaskiniDto>;
                    var butiranPermohonan = await queryButiranPermohonan
                        .Select(x => new ButiranPermohonanDetailDto
                        {
                            IndikatorRekod = x.bp.IndikatorRekod,
                            Id = x.bp.Id,
                            NamaButiran = x.bp.NoButiran + " " + x.bp.AnggaranTajukJawatan,
                            NamaAktivitiOrganisasi = item.AktivitiOrganisasi,

                            StatusAktivitiOrganisasi =
                                x.bp.IndikatorRekod == 1 ? "WujudBaru" :
                                x.bp.IndikatorRekod == 2 ? "Kemaskini" :
                                x.bp.IndikatorRekod == 3 ? "TambahJawatan" :
                                x.bp.IndikatorRekod == 4 ? "Mansuh" :
                                "None"
                        }
                        )
                        .ToListAsync();

                    if (!string.IsNullOrWhiteSpace(item.ButiranKemaskiniJson))
                    {
                        var butirankemaskini = JsonSerializer.Deserialize<List<ButiranKemaskiniDto>>(item.ButiranKemaskiniJson);
                        item.NamaPenjenamaan = butirankemaskini.LastOrDefault()?.Nama;
                    }
                    else
                    {
                        // Handle null or empty case
                        var obj = new List<ButiranKemaskiniDto>(); // or null, depending on your logic
                    }
                    item.ButiranPermohonan = butiranPermohonan;
                    if (item.Children.Count() > 0)
                    {
                        item.HasChildren = true;
                    }
                    else
                    {
                        item.HasChildren = false;
                    }
                }

                var total = result.Count();
                var ordered = (request.sortBy ?? "AktivitiOrganisasi").Trim().ToLowerInvariant() switch

                {
                    "kod" => request.desc ? result.OrderByDescending(x => x.Kod) : result.OrderBy(x => x.Kod),
                    "aktivitiorganisasi" => request.desc ? result.OrderByDescending(x => x.AktivitiOrganisasi) : result.OrderBy(x => x.AktivitiOrganisasi),
                };


                var items = ordered
                .Skip((request.page - 1) * request.pageSize)
                .Take(request.pageSize)
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

        public async Task<List<StrukturAktivitiOrganisasiDto>> StrukturAktivitiOrganisasiGetChildren(string? KodCartaAktiviti, StrukturAktivitiOrganisasiRequestDto request)
        {
            bool rootparent = false;
            int newParentId = 0;
            var query = from pdoao in _context.PDOAktivitiOrganisasi.AsNoTracking()
                                join pdorkao in _context.PDORujKategoriAktivitiOrganisasi.AsNoTracking()
                                    on pdoao.KodRujKategoriAktivitiOrganisasi equals pdorkao.Kod
                                orderby pdoao.KodProgram
                                select new { pdoao, pdorkao }
                                ;
            query = query.Where(x =>
                x.pdoao.KodCartaAktiviti.StartsWith(request.KodCartaAktiviti) &&
                x.pdoao.KodCartaAktiviti.Length == request.KodCartaAktiviti.Length + 2 
                //&& x.pdoao.Tahap == x.pdorkao.Tahap
                );

            var result = await query
                .Select(x => new StrukturAktivitiOrganisasiDto
                {
                    Id = x.pdoao.Id,
                    IdIndukAktivitiOrganisasi = x.pdoao.IdIndukAktivitiOrganisasi,
                    Kod = x.pdoao.Kod != null ? x.pdoao.Kod.Trim() : "",
                    KodCartaAktiviti = x.pdoao.KodCartaAktiviti != null ? x.pdoao.KodCartaAktiviti.Trim() : "",
                    KodProgram = (x.pdorkao.Nama ?? "").Trim().ToUpper() + " " + (x.pdoao.KodProgram ?? "").Trim(),
                    AktivitiOrganisasi = (x.pdorkao.Nama ?? "").ToUpper() + " - " +
                                         (x.pdoao.KodProgram ?? "").ToUpper() + " - " +
                                         (x.pdoao.Nama ?? "").ToUpper().Trim(),
                    Tahap = x.pdoao.Tahap,
                    ButiranKemaskiniJson = x.pdoao.ButiranKemaskini,
                    StatusAktif = x.pdoao.StatusAktif ?? false,
                    StatusAktivitiOrganisasi =
                        x.pdoao.IndikatorRekod == 1 ? "WujudBaru" :
                        x.pdoao.IndikatorRekod == 2 ? "JenamaSemula" :
                        x.pdoao.IndikatorRekod == 3 ? "Kemaskini" :
                        x.pdoao.IndikatorRekod == 4 ? "Mansuh" :
                        x.pdoao.IndikatorRekod == 5 ? "Pindah" :
                        "None"
                })
                .ToListAsync();
            foreach (var item in result)
            {
                var count = await (from pdoao in _context.PDOAktivitiOrganisasi.AsNoTracking()
                                   join pdorkao in _context.PDORujKategoriAktivitiOrganisasi.AsNoTracking()
                                    on new { KodRujKategoriAktivitiOrganisasi = pdoao.KodRujKategoriAktivitiOrganisasi, Tahap = pdoao.Tahap }
                                    equals new { KodRujKategoriAktivitiOrganisasi = pdorkao.Kod, Tahap = pdorkao.Tahap }
                                   where pdoao.IdIndukAktivitiOrganisasi == item.Id //&& pdoao.KodCartaAktiviti.Contains(request.KodCartaAktiviti)
                                   select pdoao.Id)
                                   .CountAsync();

                if (!string.IsNullOrWhiteSpace(item.ButiranKemaskiniJson))
                {
                    var butirankemaskini = JsonSerializer.Deserialize<List<ButiranKemaskiniDto>>(item.ButiranKemaskiniJson);
                    item.NamaPenjenamaan = butirankemaskini.LastOrDefault()?.Nama;
                }
                else
                {
                    // Handle null or empty case
                    var obj = new List<ButiranKemaskiniDto>(); // or null, depending on your logic
                }


                if (count > 0)
                {
                    item.HasChildren = true;
                }
                else
                {
                    item.HasChildren = false;
                }
            }

            return result;
        }

        public async Task<List<StrukturAktivitiOrganisasiDto>> StrukturButiranAktivitiOrganisasiGetChildren(int Id, string? KodCartaAktiviti, StrukturAktivitiOrganisasiRequestDto request)
        {
            bool rootparent = false;
            int newParentId = 0;
            var query = from pdoao in _context.PDOAktivitiOrganisasi.AsNoTracking()
                        join pdorkao in _context.PDORujKategoriAktivitiOrganisasi.AsNoTracking()
                            on pdoao.KodRujKategoriAktivitiOrganisasi equals pdorkao.Kod
                        orderby pdoao.KodProgram
                        select new { pdoao, pdorkao }
                                ;
            query = query.Where(x =>
                x.pdoao.IdIndukAktivitiOrganisasi == Id
                //x.pdoao.KodCartaAktiviti.StartsWith(request.KodCartaAktiviti) &&
                //x.pdoao.KodCartaAktiviti.Length == request.KodCartaAktiviti.Length + 2
                //&& x.pdoao.Tahap == x.pdorkao.Tahap
                );

            var result = await query
                .Select(x => new StrukturAktivitiOrganisasiDto
                {
                    Id = x.pdoao.Id,
                    IdIndukAktivitiOrganisasi = x.pdoao.IdIndukAktivitiOrganisasi,
                    Kod = x.pdoao.Kod != null ? x.pdoao.Kod.Trim() : "",
                    KodCartaAktiviti = x.pdoao.KodCartaAktiviti != null ? x.pdoao.KodCartaAktiviti.Trim() : "",
                    KodProgram = (x.pdorkao.Nama ?? "").Trim().ToUpper() + " " + (x.pdoao.KodProgram ?? "").Trim(),
                    AktivitiOrganisasi = (x.pdorkao.Nama ?? "").ToUpper() + " - " +
                                         (x.pdoao.KodProgram ?? "").ToUpper() + " - " +
                                         (x.pdoao.Nama ?? "").ToUpper().Trim(),
                    Tahap = x.pdoao.Tahap,
                    ButiranKemaskiniJson = x.pdoao.ButiranKemaskini,
                    StatusAktif = x.pdoao.StatusAktif ?? false,
                    StatusAktivitiOrganisasi =
                        x.pdoao.IndikatorRekod == 1 ? "WujudBaru" :
                        x.pdoao.IndikatorRekod == 2 ? "JenamaSemula" :
                        x.pdoao.IndikatorRekod == 3 ? "Kemaskini" :
                        x.pdoao.IndikatorRekod == 4 ? "Mansuh" :
                        x.pdoao.IndikatorRekod == 5 ? "Pindah" :
                        "None"
                })
                .ToListAsync();
            foreach (var item in result)
            {
                var count = await (from pdoao in _context.PDOAktivitiOrganisasi.AsNoTracking()
                                   join pdorkao in _context.PDORujKategoriAktivitiOrganisasi.AsNoTracking()
                                    on new { KodRujKategoriAktivitiOrganisasi = pdoao.KodRujKategoriAktivitiOrganisasi, Tahap = pdoao.Tahap }
                                    equals new { KodRujKategoriAktivitiOrganisasi = pdorkao.Kod, Tahap = pdorkao.Tahap }
                                   where pdoao.IdIndukAktivitiOrganisasi == item.Id //&& pdoao.KodCartaAktiviti.Contains(request.KodCartaAktiviti)
                                   select pdoao.Id)
                                   .CountAsync();

                var queryButiranPermohonan = from bp in _context.PDOButiranPermohonan
                                             select new { bp }
                                         //where bp.IdPermohonanJawatan == request.IdPermohonanJawatan
                                         //&& bp.IdAktivitiOrganisasi == item.Id
                                         //&& bp.IdAktivitiOrganisasi == request.IdAktivitiOrganisasi
                                         //select new ButiranPermohonanDetailDto
                                         //{
                                         //    Id = bp.Id,
                                         //    NamaButiran = bp.NoButiran + " " + bp.AnggaranTajukJawatan,
                                         //    NamaAktivitiOrganisasi = item.AktivitiOrganisasi
                                         //}
                                         ;

                queryButiranPermohonan = queryButiranPermohonan.Where(x =>
                   x.bp.IdPermohonanJawatan == request.IdPermohonanJawatan
                    && x.bp.IdAktivitiOrganisasi == item.Id);

                var butiranPermohonan = await queryButiranPermohonan
                    .Select(x =>
                        new ButiranPermohonanDetailDto
                        {
                            Id = x.bp.Id,
                            NamaButiran = x.bp.NoButiran + " " + x.bp.AnggaranTajukJawatan,
                            NamaAktivitiOrganisasi = item.AktivitiOrganisasi
                        }
                    )
                    .ToListAsync();
                if (!string.IsNullOrWhiteSpace(item.ButiranKemaskiniJson))
                {
                    var butirankemaskini = JsonSerializer.Deserialize<List<ButiranKemaskiniDto>>(item.ButiranKemaskiniJson);
                    item.NamaPenjenamaan = butirankemaskini.LastOrDefault()?.Nama;
                }
                else
                {
                    // Handle null or empty case
                    var obj = new List<ButiranKemaskiniDto>(); // or null, depending on your logic
                }


                item.ButiranPermohonan = butiranPermohonan;
                if (count > 0)
                {
                    item.HasChildren = true;
                }
                else
                {
                    item.HasChildren = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Creates (wujud) a new Aktiviti Organisasi entity.
        /// </summary>
        /// <param name="request">
        /// DTO containing details such as <c>IdIndukAktivitiOrganisasi</c>, <c>KodProgram</c>,  
        /// <c>Kod</c>, <c>Nama</c>, <c>Tahap</c>, <c>KodRujKategoriAktivitiOrganisasi</c>, <c>Keterangan</c>, and <c>UserId</c>.
        /// </param>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Created: 2025-09-03  
        /// Purpose: Inserts a new record into <c>PDOAktivitiOrganisasi</c> with proper metadata  
        /// for creation tracking and auditing.
        /// </remarks>
        public async Task<AktivitiOrganisasiDto> WujudAktivitiOrganisasiBaru(WujudAktivitiOrganisasiRequestDto request)
        {

            try
            {
                var parentAktivitiOrganisasi = await (from pdoao in _context.PDOAktivitiOrganisasi
                                                where pdoao.Id == request.IdIndukAktivitiOrganisasi
                                                select pdoao).FirstOrDefaultAsync();
                var count = await (from pdoao in _context.PDOAktivitiOrganisasi
                                   where pdoao.Id == request.IdIndukAktivitiOrganisasi
                                   select pdoao).CountAsync();
                string? newKodCarta = "";
                string? newNoButiran = "";

                ++count;
                if (parentAktivitiOrganisasi != null)
                {
                    
                    var parentKodCarta = parentAktivitiOrganisasi.KodCartaAktiviti;
                    newKodCarta = parentKodCarta + count.ToString("D2");
                }

                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOAktivitiOrganisasi
                {
                    IdIndukAktivitiOrganisasi = parentAktivitiOrganisasi.Id,
                    KodProgram = request.KodProgram.Trim(),
                    Kod = request.Kod.Trim(),
                    Nama = request.Nama.Trim(),
                    Tahap = request.Tahap,
                    KodRujKategoriAktivitiOrganisasi = request.KodRujKategoriAktivitiOrganisasi.Trim(),
                    IndikatorRekod = 1,
                    KodCartaAktiviti = newKodCarta,
                    Keterangan = request.Keterangan.Trim(),
                    IdCipta = request.UserId,
                    TarikhCipta = DateTime.Now,
                    StatusAktif = false // usually "baru" means aktif — confirm if you want default = false
                };

                await _context.PDOAktivitiOrganisasi.AddAsync(entity);

                // persist changes
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
                return await BacaAktivitiOrganisasi(entity.Id);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                _logger.LogError(
                    ex,
                    "Error in {Method} for UserId: {UserId}, Nama: {Nama}, Kod: {Kod}, ParentId: {ParentId}",
                    nameof(WujudAktivitiOrganisasiBaru),
                    request.UserId,
                    request.Nama.Trim(),
                    request.Kod.Trim(),
                    request.IdIndukAktivitiOrganisasi
                );

                throw;
            }
        }



        /// <summary>
        /// Renames (penjenamaan) an existing Aktiviti Organisasi entity.
        /// </summary>
        /// <param name="request">
        /// DTO containing the AktivitiOrganisasi Id, UserId of the person performing the update,  
        /// and the new name (<c>Nama</c>).
        /// </param>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Created: 2025-09-03  
        /// Purpose: Archives the current entity state into JSON, updates its name,  
        /// and tracks metadata for auditing purposes.  
        /// </remarks>
        public async Task PenjenamaanAktivitiOrganisasi(PenjenamaanAktivitiOrganisasiRequestDto request)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await _context.PDOAktivitiOrganisasi
                    .FirstOrDefaultAsync(pdoao => pdoao.Id == request.Id);

                if (entity == null)
                {
                    throw new Exception("PDOAktivitiOrganisasi not found.");
                }

                var butiranKemaskiniList = new List<ButiranKemaskiniDto>();

                if (entity.ButiranKemaskini != null)
                {
                    butiranKemaskiniList = JsonSerializer.Deserialize<List<ButiranKemaskiniDto>>(entity.ButiranKemaskini);
                }

                // Archive old state before updating
                var archivedEntity = new ButiranKemaskiniDto
                {
                    Nama = request.Nama.Trim(), // old name for history
                    IdPinda = request.UserId,
                    TarikhPinda = DateTime.Now
                };

                butiranKemaskiniList.Add(archivedEntity);

                string json = JsonSerializer.Serialize(butiranKemaskiniList, new JsonSerializerOptions { WriteIndented = true });
                
                entity.ButiranKemaskini = json;
                entity.IndikatorRekod = 2;
                // Apply the new name
                entity.IdPinda = request.UserId;
                entity.TarikhPinda = DateTime.Now;

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                _logger.LogError(
                    ex,
                    "Error in {Method} for UserId: {UserId}, AktivitiId: {AktivitiId}, NewName: {NewName}",
                    nameof(PenjenamaanAktivitiOrganisasi),
                    request.UserId,
                    request.Id,
                    request.Nama.Trim()
                );

                throw;
            }
        }

        /// <summary>
        /// Moves (pindah) an existing Aktiviti Organisasi to a new parent node.
        /// </summary>
        /// <param name="request">
        /// DTO containing the AktivitiOrganisasi Id, the UserId performing the operation,  
        /// and the new parent Id (<c>NewParentId</c>).
        /// </param>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Created: 2025-09-03  
        /// Purpose: Archives the current AktivitiOrganisasi state into JSON, updates its parent reference,  
        /// and saves the change with metadata (UserId and timestamps).  
        /// </remarks>
        public async Task PindahAktivitiOrganisasi(PindahAktivitiOrganisasiRequestDto request)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await _context.PDOAktivitiOrganisasi
                    .FirstOrDefaultAsync(pdoao => pdoao.Id == request.Id);

                if (entity == null)
                {
                    throw new Exception("PDOAktivitiOrganisasi not found.");
                }

                var butiranKemaskiniList = new List<ButiranKemaskiniDto>();

                if (entity.ButiranKemaskini != null)
                {
                    butiranKemaskiniList = JsonSerializer.Deserialize<List<ButiranKemaskiniDto>>(entity.ButiranKemaskini);
                }

                // Archive old state before updating
                var archivedEntity = new ButiranKemaskiniDto
                {
                    // ⚠️ Do not copy Id — EF will manage it if identity
                    IdIndukAktivitiOrganisasi = request.NewParentId,
                    StatusAktif = entity.StatusAktif ?? false,
                    IdPinda = request.UserId,
                    TarikhPinda = DateTime.Now,
                    NewParentId = request.NewParentId,
                    OldParentId = request.OldParentId
                };


                butiranKemaskiniList.Add(archivedEntity);
                string json = JsonSerializer.Serialize(butiranKemaskiniList, new JsonSerializerOptions { WriteIndented = true });

                // Archive current state into JSON on the original entity
                entity.ButiranKemaskini = json;
                entity.IndikatorRekod = 5;
                entity.IdPinda = request.UserId;
                entity.TarikhPinda = DateTime.Now;

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                _logger.LogError(
                    ex,
                    "Error in {Method} for UserId: {UserId}, AktivitiId: {AktivitiId}, NewParentId: {NewParentId}",
                    nameof(PindahAktivitiOrganisasi),
                    request.UserId,
                    request.Id,
                    request.NewParentId
                );

                throw;
            }
        }


        /// <summary>
        /// Reads an AktivitiOrganisasi by Id, including its parent name,
        /// and prepares metadata for the next potential child (KodProgram, Kod, Tahap).
        /// </summary>
        /// <param name="Id">The identifier of the AktivitiOrganisasi to read.</param>
        /// <returns>An <see cref="AktivitiOrganisasiDto"/> containing details of the activity and next child metadata.</returns>
        public async Task<AktivitiOrganisasiDto> BacaAktivitiOrganisasi(int Id)
        {
            try
            {
                var result = await (
                    from pdoao in _context.PDOAktivitiOrganisasi
                    join paoparent in _context.PDOAktivitiOrganisasi on pdoao.KodCartaAktiviti.Substring(0, pdoao.KodCartaAktiviti.Length-2) equals paoparent.KodCartaAktiviti into parentJoin
                    from paoparent in parentJoin.DefaultIfEmpty() // left join in case root has no parent
                    join pdokao in _context.PDORujKategoriAktivitiOrganisasi on pdoao.KodRujKategoriAktivitiOrganisasi equals pdokao.Kod
                    where pdoao.Id == Id
                    select new AktivitiOrganisasiDto
                    {
                        AktiviOrganisasi = pdoao.Nama.ToUpper().Trim(),
                        AktivitiOrganisasiInduk = paoparent != null ? paoparent.Nama : null,
                        ButiranKemaskini = pdoao.ButiranKemaskini,
                        Id = pdoao.Id,
                        IdIndukAktivitiOrganisasi = pdoao.IdIndukAktivitiOrganisasi,
                        Keterangan = pdoao.Keterangan.Trim(),
                        Kod = pdoao.Kod.Trim(),
                        KodCartaAktiviti = pdoao.KodCartaAktiviti.Trim(),
                        KodProgram = pdoao.KodProgram.Trim(),
                        KodRujKategoriAktivitiOrganisasi = pdoao.KodRujKategoriAktivitiOrganisasi.Trim(),
                        StatusAktif = pdoao.StatusAktif ?? false,
                        Tahap = pdoao.Tahap,
                        KategoriAktivitiOrgansisasi = pdokao.Nama.Trim(),
                        
                    }
                ).FirstOrDefaultAsync();

                if (result == null)
                    throw new InvalidOperationException($"AktivitiOrganisasi with Id={Id} not found.");

                // ✅ Prepare metadata for next child
                var childCount = await _context.PDOAktivitiOrganisasi
                    .CountAsync(x => x.IdIndukAktivitiOrganisasi == result.Id);

                // Generate next KodProgram (append count+1)
                result.Tahap = (result.Tahap ?? 0) + 1;
                var nextProgramCount = childCount + 1;
                result.KodProgram = $"{result.KodProgram}.{nextProgramCount}";

                //var kodArr = result.Kod.Split(".");
                //var cntStr = kodArr[(int)result.Tahap];
                //var count = Int32.Parse(cntStr);
                //++count;
                //kodArr[(int)result.Tahap] = count.ToString("D3");

                //result.Kod = string.Join(".", kodArr);

                // Increase hierarchy level

                // Generate next Kod (increment last numeric part if exists)
                if (!string.IsNullOrWhiteSpace(result.Kod))
                {
                    var parts = result.Kod.Split('.', StringSplitOptions.RemoveEmptyEntries);
                    var lastPart = parts[(int)result.Tahap];

                    if (int.TryParse(lastPart, out int lastNumber))
                    {
                        lastNumber++;
                        parts[(int)result.Tahap] = lastNumber.ToString("D3"); // keep padding to 3 digits
                        result.Kod = string.Join(".", parts);
                    }
                    else
                    {
                        _logger.LogWarning("BacaAktivitiOrganisasi: Last Kod segment is not numeric ({LastPart}) for Id={Id}", lastPart, Id);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BacaAktivitiOrganisasi for Id={Id}", Id);
                throw;
            }
        }



        public async Task<AktivitiOrganisasiAlamatIndukDto> BacaAktivitiOrganisasiAlamatInduk(int IdUnitOrganisasi)
        {
            try
            {
                var unitOrganisasi = await (
                    from pdoao in _context.PDOAktivitiOrganisasi
                    where pdoao.Id == IdUnitOrganisasi
                    select pdoao
                ).FirstOrDefaultAsync();
                var KodCartaOrganisasiParent = unitOrganisasi.KodCartaAktiviti.Substring(0, unitOrganisasi.KodCartaAktiviti.Length - 2);
                var unitOrganisasiParent = await (from pdouo in _context.PDOUnitOrganisasi
                              where pdouo.Id == unitOrganisasi.IdIndukAktivitiOrganisasi
                                                  select pdouo
                              ).FirstOrDefaultAsync();

                var result = await (from pdoauo in _context.PDOAlamatUnitOrganisasi
                              where pdoauo.IdUnitOrganisasi == unitOrganisasiParent.Id
                              select new AktivitiOrganisasiAlamatIndukDto
                              {
                                    Id = pdoauo.Id,
                                    IdUnitOrganisasi = pdoauo.IdUnitOrganisasi,
                                    KodRujPoskod = pdoauo.KodRujPoskod,
                                    Alamat1 = pdoauo.Alamat1,
                                    Alamat2 = pdoauo.Alamat2,
                                    Alamat3 = pdoauo.Alamat3,
                                    KodRujNegara = pdoauo.KodRujNegara,
                                    KodRujNegeri = pdoauo.KodRujNegeri,
                                    KodRujBandar = pdoauo.KodRujBandar,
                                    NomborTelefonPejabat = pdoauo.NomborTelefonPejabat,
                                    NomborFaksPejabat = pdoauo.NomborFaksPejabat
                              }).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BacaAktivitiOrganisasi for Id={IdUnitOrganisasi}", IdUnitOrganisasi);
                throw;
            }
        }
        /// <summary>
        /// Retrieves a list of AktivitiOrganisasi for use in dropdowns.
        /// </summary>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Provides a lightweight reference list of AktivitiOrganisasi  
        ///               with <c>Kod</c> and <c>Nama</c> fields, typically for UI dropdowns.  
        /// </remarks>
        /// <returns>A list of <see cref="DropDownDto"/> containing Kod and Nama.</returns>
        public async Task<List<DropDownDto>> RujukanAktivitiOrganisasi()
        {
            try
            {
                var result = await _context.PDOAktivitiOrganisasi
                    .AsNoTracking()
                    .OrderBy(x => x.Nama) // ✅ predictable order for dropdown
                    .Select(pdoao => new DropDownDto
                    {
                        Kod = pdoao.Kod.Trim(),
                        Nama = pdoao.Nama.Trim()
                    })
                    .ToListAsync();

                return result ?? new List<DropDownDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RujukanAktivitiOrganisasi while fetching dropdown data");
                throw;
            }
        }



        /// <summary>
        /// Registers (daftar) a new Aktiviti Organisasi record into the system.
        /// </summary>
        /// <param name="request">DTO containing user and Aktiviti Organisasi details.</param>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Created: 2025-09-03  
        /// Purpose: Creates a new <c>PDOAktivitiOrganisasi</c> entity from the request payload,  
        /// persists it into the database, and commits the transaction.  
        /// </remarks>
        public async Task DaftarAktivitiOrganisasi(AktivitiOrganisasiDaftarRequestDto request)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOAktivitiOrganisasi
                {
                    IdCipta = request.UserId,
                    Kod = request.KodAktiviti.Trim(),
                    Nama = request.NamaAktiviti.Trim(),
                    KodRujKategoriAktivitiOrganisasi = request.KodRujKategoriAktivitiOrganisasi.Trim(),
                    IdAsal = request.IdAsal
                };

                await _context.PDOAktivitiOrganisasi.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                _logger.LogError(
                    ex,
                    "Error in {Method} for UserId: {UserId}, KodAktiviti: {KodAktiviti}",
                    nameof(DaftarAktivitiOrganisasi),
                    request.UserId,
                    request?.KodAktiviti.Trim()
                );

                throw;
            }
        }



        /// <summary>
        /// Permanently deletes an AktivitiOrganisasi record from the database.  
        /// </summary>
        /// <param name="request">
        /// Request object containing both the UserId (for auditing) and the Id of the entity.  
        /// </param>
        /// <remarks>
        /// Best practice:
        /// - Always wrap in a transaction.  
        /// - Use rollback on failure.  
        /// - Validate existence before delete.  
        /// - Use structured logging for observability.  
        /// </remarks>
        public async Task HapusTerusAktivitiOrganisasi(HapusTerusAktivitiOrganisasiRequestDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "Request payload cannot be null.");


            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await _context.PDOAktivitiOrganisasi
                    .FirstOrDefaultAsync(pdoao => pdoao.Id == request.Id);

                if (entity == null)
                {
                    _logger.LogWarning(
                        "Attempted to delete AktivitiOrganisasi that does not exist. UserId: {UserId}, Id: {Id}",
                        request.UserId, request.Id
                    );
                    throw new KeyNotFoundException($"PDOAktivitiOrganisasi with Id {request.Id} not found.");
                }

                _context.PDOAktivitiOrganisasi.Remove(entity);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                _logger.LogInformation(
                    "AktivitiOrganisasi deleted successfully. UserId: {UserId}, Id: {Id}, Nama: {Nama}",
                    request.UserId, entity.Id, entity.Nama.Trim()
                );
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(
                    ex,
                    "Error in HapusTerusAktivitiOrganisasi for UserId: {UserId}, Id: {Id}",
                    request?.UserId, request?.Id
                );

                throw;
            }
        }

        /// <summary>
        /// Soft delete (mansuh) aktiviti organisasi by archiving details into JSON
        /// and marking entity as updated with deletion metadata.
        /// </summary>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Created: 2025-09-03  
        /// Purpose: Implements business logic to soft-delete (mansuh) an aktiviti organisasi 
        /// by serializing its current state and storing it in <c>ButiranKemaskini</c>, 
        /// while tagging the entity with deletion metadata (UserId & TarikhHapus).
        /// </remarks>
        public async Task MansuhAktivitiOrganisasi(MansuhAktivitiOrganisasiRequestDto request)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await (from pdoao in _context.PDOAktivitiOrganisasi
                                    where pdoao.Id == request.IdAktivitiOrganisasi
                                    select pdoao
                                    ).FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new Exception("PDOAktivitiOrganisasi not found.");
                }

                var butiranKemaskiniList = new List<ButiranKemaskiniDto>();

                if (entity.ButiranKemaskini != null)
                {
                    butiranKemaskiniList = JsonSerializer.Deserialize<List<ButiranKemaskiniDto>>(entity.ButiranKemaskini);
                }

                // Archive old state before updating
                // Archive current state into JSON and set deletion metadata
                var newButiranKemaskini = new ButiranKemaskiniDto
                {
                    Id = entity.Id,
                    StatusAktif = entity.StatusAktif ?? false,
                };

                butiranKemaskiniList.Add(newButiranKemaskini);

                string json = JsonSerializer.Serialize(butiranKemaskiniList, new JsonSerializerOptions { WriteIndented = true });

                int count = json.Length;

                // Apply update on the original entity
                entity.ButiranKemaskini = json;
                entity.IndikatorRekod = 4;
                entity.IdPinda = request.UserId;
                entity.TarikhPinda = DateTime.Now;

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MansuhAktivitiOrganisasi");
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }


    }

}

