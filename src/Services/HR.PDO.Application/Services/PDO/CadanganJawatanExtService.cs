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
using Azure.Core;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace HR.Application.Services.PDO
{
    public class CadanganJawatanExtService : ICadanganJawatanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<CadanganJawatanExtService> _logger;

        public CadanganJawatanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<CadanganJawatanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<CadanganJawatanDto>> SenaraiCadanganJawatan(int IdPermohonanJawatan)
        {
            try

            {

                var result = await (
                    from pdopj in _context.PDOPermohonanJawatan.AsNoTracking()
                    where pdopj.Id == IdPermohonanJawatan

                    // LEFT JOIN PDO_ButiranPermohonan
                    from pdobp in _context.PDOButiranPermohonan.AsNoTracking()
                        .Where(x => x.IdPermohonanJawatan == pdopj.Id)
                        .DefaultIfEmpty()

                        // LEFT JOIN PDO_CadanganJawatan (by IdButiranPermohonan)
                    from pdocj in _context.PDOCadanganJawatan.AsNoTracking()
                        .Where(x => pdobp != null && x.IdButiranPermohonan == pdobp.Id)
                        .DefaultIfEmpty()

                        // LEFT JOIN link table + lookups (for fallbacks)
                    from pdobpsg in _context.PDOButiranPermohonanSkimGred.AsNoTracking()
                        .Where(x => pdobp != null && x.IdButiranPermohonan == pdobp.Id)
                        .DefaultIfEmpty()
                    from pdosp in _context.PDOSkimPerkhidmatan.AsNoTracking()
                        .Where(x => pdobpsg != null && x.Id == pdobpsg.IdSkimPerkhidmatan)
                        .DefaultIfEmpty()
                    from pdog in _context.PDOGred.AsNoTracking()
                        .Where(x => pdobpsg != null && x.Id == pdobpsg.IdGred)
                        .DefaultIfEmpty()

                    select new CadanganJawatanDto
                    {
                        IdButiranPermohonan = pdobp != null ? pdobp.Id : 0,

                        // Prefer values from PDOCadanganJawatan; fallback to the joined masters
                        SkimPerkhidmatan = pdocj != null && pdocj.ButiranSkimPerkhidmatanGred != null ? pdocj.ButiranSkimPerkhidmatanGred.Trim() : pdosp!.Nama.Trim(),
                        KodRujGelaranJawatan = pdocj!.KodRujGelaranJawatan,
                        KodRujJenisJawatan = pdocj!.KodRujJenisJawatan,
                        KodRujStatusBekalan = pdocj!.KodRujStatusBekalan,
                        KodRujStatusJawatan = pdocj!.KodRujStatusJawatan,
                        IdAktivitiOrganisasi = pdobp!.IdAktivitiOrganisasi,
                        IdUnitOrganisasi = pdocj!.IdUnitOrganisasi,
                        KodRujPangkatBadanBeruniform = pdocj!.KodRujPangkatBadanBeruniform,
                        Penyandang = pdocj!.Penyandang,
                    }
                ).ToListAsync();
                
                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiCadanganJawatan");

                throw;
            }

        }
        public async Task<List<PDOCadanganJawatan>> SenaraiRekodCadanganJawatan(int IdButiranPermohonan)
        {
            try

            {

                var result = await (from pdocj in _context.PDOCadanganJawatan.AsNoTracking()
                    where pdocj.IdButiranPermohonan == IdButiranPermohonan
                    select pdocj
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiCadanganJawatan");

                throw;
            }

        }


        public async Task<List<ButiranCadanganJawatanDto>> SenaraiButiranCadanganJawatan(SenaraiCadanganJawatanRequestDto request)
        {
            try

            {
                var gredStr = "";
                foreach (var req in request.SkimPerkhidmatanList)
                {

                    var GredList = req.IdGredList
                      .Split(",")
                      .Select(s => int.TryParse(s.Trim(), out var val) ? val : 0)
                      .ToList();
                    int cnt = 0;
                    foreach (var gred in GredList)
                    {
                        var Nama = (from pdog in _context.PDOGred
                                    where gred == pdog.Id
                                    select pdog.Nama
                                ).FirstOrDefault();
                        if (gredStr == "")
                        {
                            gredStr = Nama.Trim();
                        }
                        else
                        {
                            gredStr = gredStr + "," + Nama.Trim();
                        }
                    }
                    ++cnt;
                    if (cnt == GredList.Count())
                    {
                        gredStr = gredStr + " / ";
                    }
                }

                var gelaranJawatan = (from pdogj in _context.PDORujGelaranJawatan
                                      where pdogj.Kod == request.KodRujGelaranJawatan
                                      select pdogj.Nama).FirstOrDefault();

                var newGred = gredStr.Replace(",", "/");
                var newAnggaran = request.AnggaranBerkenaanTajukJawatan.Replace(newGred, "");

                var result = new List<ButiranCadanganJawatanDto>();

                for (int i = 1; i <= request.BilanganJawatan; i++)
                {
                    var butiranCadangan = new ButiranCadanganJawatanDto
                    {
                        Id = i,
                        NamaJawatan = gelaranJawatan + " " + newAnggaran,
                        NamaUnitOrganisasi = ""
                    };
                    result.Add(butiranCadangan);
                }


                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahCadanganJawatan");

                throw;
            }

            //try

            //{

            //    var result = await (from pdocj in _context.PDOCadanganJawatan.AsNoTracking()
            //                        where pdocj.IdButiranPermohonan == IdButiranPermohonan
            //                        select new ButiranCadanganJawatanDto
            //                        {
            //                            Id = pdocj.Id,
            //                            NamaJawatan = pdocj.ButiranSkimPerkhidmatanGred,
            //                            IdUnitOrganisasi = pdocj.IdUnitOrganisasi,
            //                            NamaUnitOrganisasi = (from pdou in _context.PDOUnitOrganisasi
            //                                                  where pdou.Id == pdocj.IdUnitOrganisasi
            //                                                  select pdou.Nama).FirstOrDefault()
            //                        }
            //    ).ToListAsync();

            //    return result;

            //}

            //catch (Exception ex)

            //{

            //    _logger.LogError(ex, "Error in SenaraiCadanganJawatan");

            //    throw;
            //}

        }

        public async Task<List<PDOCadanganJawatan>> TambahCadanganJawatan(SenaraiCadanganJawatanRequestDto request)
        {

            try

            {
                var gredStr = "";
                foreach (var req in request.SkimPerkhidmatanList)
                {

                    var GredList = req.IdGredList
                      .Split(",")
                      .Select(s => int.TryParse(s.Trim(), out var val) ? val : 0)
                      .ToList();
                    int cnt = 0;
                    foreach (var gred in GredList)
                    {
                        var Nama = (from pdog in _context.PDOGred
                                    where gred == pdog.Id
                                    select pdog.Nama
                                ).FirstOrDefault();
                        if (gredStr == "")
                        {
                            gredStr = Nama.Trim();
                        }
                        else
                        {
                            gredStr = gredStr + "," + Nama.Trim();
                        }
                    }
                    ++cnt;
                    if (cnt == GredList.Count())
                    {
                        gredStr = gredStr + " / ";
                    }
                }

                var gelaranJawatan = (from pdogj in _context.PDORujGelaranJawatan
                                      where pdogj.Kod == request.KodRujGelaranJawatan
                                      select pdogj.Nama).FirstOrDefault();

                var newGred = gredStr.Replace(",", "/");
                var newAnggaran = request.AnggaranBerkenaanTajukJawatan.Replace(newGred, "");

                for (int i=1; i<=request.BilanganJawatan; i++)
                {
                    await _unitOfWork.BeginTransactionAsync();

                    var entity = new PDOCadanganJawatan();

                    entity.IdAktivitiOrganisasi = request.IdAktivitiOrganisasi;
                    entity.IdUnitOrganisasi = request.IdUnitOrganisasi;
                    entity.IdButiranPermohonan = request.IdButiranPermohonan;
                    entity.ButiranSkimPerkhidmatanGred = newAnggaran;
                    entity.IdButiranPermohonan = request.IdButiranPermohonan;
                    entity.TarikhCipta = DateTime.Now;
                    entity.StatusAktif = false;
                    entity.KodRujJenisJawatan = request.KodRujJenisJawatan;
                    entity.KodRujGelaranJawatan = request.KodRujGelaranJawatan;
                    entity.KodRujStatusBekalan = request.KodRujStatusBekalan != null ? (request.KodRujStatusBekalan!="" || request.KodRujStatusBekalan!="string"? request.KodRujStatusBekalan: null) : null ;
                    entity.KodRujStatusJawatan = request.KodRujStatusJawatan;
                    entity.IdCipta = request.UserId;
                    entity.TarikhCipta = DateTime.Now;
                    await _context.PDOCadanganJawatan.AddAsync(entity);

                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();
                }

                var result = await SenaraiRekodCadanganJawatan(request.IdButiranPermohonan);

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahCadanganJawatan");

                throw;
            }

        }

        public async Task KemaskiniCadanganJawatan(KemaskiniCadanganJawatanRequestDto request)
        {

            try

            {
                var record = (from pdocj in _context.PDOCadanganJawatan
                              where pdocj.IdButiranPermohonan == request.IdButiranPermohonan
                              select pdocj).ToList();

                foreach (var item in record)
                {
                    await _unitOfWork.BeginTransactionAsync();

                    item.IdUnitOrganisasi= request.IdUnitOrganisasi;
                    item.IdPinda = request.UserId;
                    item.TarikhPinda = DateTime.Now;

                    _context.PDOCadanganJawatan.Update(item);

                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();
                }


            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahCadanganJawatan");

                throw;
            }

        }

        public async Task<PDOCadanganJawatan> KemaskiniUnitOrganisasiCadanganJawatan(KemaskiniUnitOrganisasiCadanganJawatanRequestDto request)
        {
            try

            {
                var record = await (from pdocj in _context.PDOCadanganJawatan
                              where pdocj.Id == request.IdCadanganJawatan
                              select pdocj).FirstOrDefaultAsync();

                await _unitOfWork.BeginTransactionAsync();

                record.IdUnitOrganisasi = request.IdUnitOrganisasi;
                record.IdPinda = request.UserId;
                record.TarikhPinda = DateTime.Now;

                _context.PDOCadanganJawatan.Update(record);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return record;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahCadanganJawatan");

                throw;
            }
        }


    }

}

