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
                        SkimPerkhidmatan = pdocj != null && pdocj.SkimPerkhidmatan != null ? pdocj.SkimPerkhidmatan : pdosp!.Nama,
                        Gred = pdocj != null && pdocj.Gred != null ? pdocj.Gred : pdog!.Nama,

                        GelaranJawatan = pdocj!.GelaranJawatan,
                        KodRujJenisJawatan = pdocj!.KodRujJenisJawatan,
                        KodRujStatusBekalan = pdocj!.KodRujStatusBekalan,
                        KodRujStatusJawatan = pdocj!.KodRujStatusJawatan,
                        IdAktivitiOrganisasi = pdobp!.IdAktivitiOrganisasi,
                        IdUnitOrganisasi = pdocj!.IdUnitOrganisasi,
                        Pangkat = pdocj!.Pangkat,
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



        public async Task TambahCadanganJawatan(Guid UserId, CadanganJawatanRequestDto request)
        {

            try

            {
                var gelaranJawatan = (from pdorgj in _context.PDORujGelaranJawatan
                                    where pdorgj.Kod==request.KodRujGelaranJawatan
                                    select pdorgj).FirstOrDefault();
                var gred = (from pdog in _context.PDOGred
                            where pdog.KodGred == request.KodGred
                            select pdog).FirstOrDefault();
                var skim = (from pdosp in _context.PDOSkimPerkhidmatan
                            where pdosp.Id == request.IdSkimPerkhidmatan
                            select pdosp).FirstOrDefault();

                for (int i=1; i<=request.BilanganJawatan; i++)
                {
                    await _unitOfWork.BeginTransactionAsync();

                    var entity = new PDOCadanganJawatan();

                    entity.IdAktivitiOrganisasi = request.IdAktivitiOrganisasi;
                    entity.IdButiranPermohonan = request.IdButiranPermohonan;
                    entity.SkimPerkhidmatan = skim.Nama;
                    entity.Gred = gred.Nama;
                    entity.IdButiranPermohonan = request.IdButiranPermohonan;
                    entity.TarikhCipta = DateTime.Now;
                    entity.StatusAktif = false;
                    entity.KodRujJenisJawatan = request.KodRujJenisJawatan;
                    entity.GelaranJawatan = gelaranJawatan.Nama;
                    entity.KodRujStatusBekalan = request.KodRujStatusBekalan;
                    entity.KodRujStatusJawatan = request.KodRujStatusJawatan;
                    entity.IdCipta = UserId;
                    entity.TarikhCipta = DateTime.Now;
                    await _context.PDOCadanganJawatan.AddAsync(entity);

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

        public async Task KemaskiniCadanganJawatan(Guid UserId, int IdButiranPermohonan, int IdUnitOrganisasi)
        {

            try

            {
                var record = (from pdocj in _context.PDOCadanganJawatan
                              where pdocj.IdButiranPermohonan == IdButiranPermohonan
                              select pdocj).ToList();

                foreach (var item in record)
                {
                    await _unitOfWork.BeginTransactionAsync();

                    item.IdUnitOrganisasi=IdUnitOrganisasi;
                    item.IdPinda = UserId;
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


    }

}

