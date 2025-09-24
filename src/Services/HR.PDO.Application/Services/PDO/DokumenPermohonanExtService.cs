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
using System.Runtime.Intrinsics.Arm;

namespace HR.Application.Services.PDO
{
    public class DokumenPermohonanExtService : IDokumenPermohonanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<DokumenPermohonanExtService> _logger;

        public DokumenPermohonanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<DokumenPermohonanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<RujJenisDokumenLinkDto>> SenaraiDokumenPermohonan(int IdPermohonanJawatan)
        {
            try

            {

                var result = await (from pdodp in _context.PDODokumenPermohonan
                    join pdorjd in _context.PDORujJenisDokumen  on  pdodp.KodRujJenisDokumen equals pdorjd.Kod
                    where pdodp.IdPermohonanJawatan==IdPermohonanJawatan
                    select new RujJenisDokumenLinkDto
                    {
                        Id = pdodp.Id,
                        StatusAktif = pdodp.StatusAktif ?? false,
                        FormatDokumen = pdorjd.Nama,
                        JenisDokumen = pdodp.KodRujJenisDokumen,
                        PautanDokumen = pdodp.PautanDokumen,
                        IdPermohonanJawatan = pdodp.IdPermohonanJawatan,
                        NamaDokumen = pdodp.NamaDokumen.Trim(),
                        Saiz = pdodp.Saiz

                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiDokumenPermohonan");

                throw;
            }

        }
        public async Task<PDODokumenPermohonan> BacaDokumenPermohonan(long? Id)
        {
            try

            {

                var result = await (from pdodp in _context.PDODokumenPermohonan
                                    join pdorjd in _context.PDORujJenisDokumen on pdodp.KodRujJenisDokumen equals pdorjd.Kod
                                    where pdodp.IdPermohonanJawatan == Id
                                    select pdodp
                ).FirstOrDefaultAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiDokumenPermohonan");

                throw;
            }

        }



        public async Task<PDODokumenPermohonan> WujudDokumenPermohonanBaru(WujudDokumenPermohonanRequestDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDODokumenPermohonan();
                entity.IdPermohonanJawatan = request.IdPermohonanJawatan;
                entity.KodRujJenisDokumen = request.KodRujJenisDokumen;
                entity.NamaDokumen = request.NamaDokumen.Trim();
                entity.PautanDokumen = request.PautanDokumen;
                entity.FormatDokumen = request.FormatDokumen;
                entity.Saiz = request.Saiz;
                entity.IdCipta = request.UserId;
                entity.TarikhCipta = DateTime.Now;
                await _context.PDODokumenPermohonan.AddAsync(entity); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return await BacaDokumenPermohonan(entity.Id);
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in WujudDokumenPermohonanBaru");

                throw;
            }

        }



        public async Task<string?> HapusTerusDokumenPermohonan(HapusTerusDokumenPermohonanRequest request)
        {

            try

            {
                var statusAktif = await (from pdospj in _context.PDOStatusPermohonanJawatan
                                        where pdospj.IdPermohonanJawatan == request.IdPermohonanJawatan
                                        select pdospj.StatusAktif).FirstOrDefaultAsync();
                await _unitOfWork.BeginTransactionAsync();
                var entity = await (from pdodp in _context.PDODokumenPermohonan
                             where pdodp.Id == request.IdDokumenPermhonan 
                             select pdodp
                              ).FirstOrDefaultAsync();
                if (statusAktif==true)
                {
                    throw new Exception("Permohonan Jawatan telah dilulukan (StatusAktif=1). Tidak boleh hapus terus rekod ini.");
                }
                if (entity == null)
                {
                    throw new Exception("Dokumen Permohonan tiada rekod berkaitan.");
                }
                _context.PDODokumenPermohonan.Remove(entity);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return "Dokumen Permohonan {request.IdDokumenPermhonan} berjaya dihapus terus.";
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in HapusTerusDokumenPermohonan");

                throw;
            }

        }



    }

}

