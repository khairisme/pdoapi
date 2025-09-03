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
                        StatusAktif = pdodp.StatusAktif ?? false,
                        TarikhCipta = pdodp.TarikhCipta,
                        TarikhHapus  = pdodp.TarikhHapus,
                        TarikhPinda = pdodp.TarikhPinda,
                        IdCipta = pdodp.IdCipta,
                        IdHapus = pdodp.IdHapus,
                        IdPinda = pdodp.IdPinda,
                        FormatDokumen = pdorjd.Nama,
                        JenisDokumen = pdodp.KodRujJenisDokumen,
                        PautanDokumen = pdodp.PautanDokumen,
                        IdPermohonanJawatan = pdodp.IdPermohonanJawatan,
                        NamaDokumen = pdodp.NamaDokumen

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



        public async Task WujudDokumenPermohonanBaru(Guid UserId, int IdPermohonanJawatan, string? KodRujJenisDokumen, string? NamaDokumen, string? PautanDokumen, string? FormatDokumen, int Saiz)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDODokumenPermohonan();
                entity.IdPermohonanJawatan = IdPermohonanJawatan;
                entity.KodRujJenisDokumen = KodRujJenisDokumen;
                entity.NamaDokumen = NamaDokumen;
                entity.PautanDokumen = PautanDokumen;
                entity.FormatDokumen = FormatDokumen;
                entity.Saiz = Saiz;
                entity.IdCipta = UserId;
                entity.TarikhCipta = DateTime.Now;
                await _context.PDODokumenPermohonan.AddAsync(entity); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in WujudDokumenPermohonanBaru");

                throw;
            }

        }



        public async Task HapusTerusDokumenPermohonan(Guid UserId, int Id)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await (from pdodp in _context.PDODokumenPermohonan
                             where pdodp.Id == Id 
                             select pdodp
                              ).FirstOrDefaultAsync();
                if (entity == null)
                {
                    throw new Exception("PDODokumenPermohonan not found.");
                }
                _context.PDODokumenPermohonan.Remove(entity);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in HapusTerusDokumenPermohonan");

                throw;
            }

        }



    }

}

