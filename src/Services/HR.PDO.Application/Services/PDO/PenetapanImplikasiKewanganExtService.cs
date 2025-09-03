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
    public class PenetapanImplikasiKewanganExtService : IPenetapanImplikasiKewanganExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<PenetapanImplikasiKewanganExtService> _logger;

        public PenetapanImplikasiKewanganExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<PenetapanImplikasiKewanganExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<PenetapanImplikasiKewanganDto>> SenaraiImplikasiKewangan(int IdPermohonanJawatan)
        {
            try

            {

                var result = await (
                    from pdopj in _context.PDOPermohonanJawatan.AsNoTracking()
                    join pdobp in _context.PDOButiranPermohonan.AsNoTracking()
                        on pdopj.Id equals pdobp.IdPermohonanJawatan
                    join pdobpsg in _context.PDOButiranPermohonanSkimGred.AsNoTracking()
                        on pdobp.Id equals pdobpsg.IdButiranPermohonan
                    join pdosp in _context.PDOSkimPerkhidmatan.AsNoTracking()
                        on pdobpsg.IdSkimPerkhidmatan equals pdosp.Id
                    join pdog in _context.PDOGred.AsNoTracking()
                        on pdobpsg.IdGred equals pdog.Id
                    where pdopj.Id == IdPermohonanJawatan
                    select new PenetapanImplikasiKewanganDto
                    {
                        BilanganJawatan = pdobp.BilanganJawatan,
                        Gred = pdog.Nama,                     // <-- fix (was TarikhCipta)
                        ImplikasiKewanganSebulan = pdobp.JumlahKosSebulan,
                        ImplikasiKewanganSetahun = pdobp.JumlahKosSetahun,
                        SkimPerkhidmatan = pdosp.Nama
                    }
                ).ToListAsync();
                //var result = await (
                //    from pdopj in _context.PDOPermohonanJawatan.AsNoTracking()
                //    where pdopj.Id == IdPermohonanJawatan

                //    // LEFT JOIN PDOButiranPermohonan
                //    from pdobp in _context.PDOButiranPermohonan.AsNoTracking()
                //        .Where(x => x.IdPermohonanJawatan == pdopj.Id)
                //        .DefaultIfEmpty()

                //        // LEFT JOIN PDOButiranPermohonanSkimGred
                //    from pdobpsg in _context.PDOButiranPermohonanSkimGred.AsNoTracking()
                //        .Where(x => pdobp != null && x.IdButiranPermohonan == pdobp.Id)
                //        .DefaultIfEmpty()

                //        // LEFT JOIN PDOSkimPerkhidmatan
                //    from pdosp in _context.PDOSkimPerkhidmatan.AsNoTracking()
                //        .Where(x => pdobpsg != null && x.Id == pdobpsg.IdSkimPerkhidmatan)
                //        .DefaultIfEmpty()

                //        // LEFT JOIN PDOGred
                //    from pdog in _context.PDOGred.AsNoTracking()
                //        .Where(x => pdobpsg != null && x.Id == pdobpsg.IdGred)
                //        .DefaultIfEmpty()
                //    select new PenetapanImplikasiKewanganDto
                //    {
                //        BilanganJawatan = pdobp.BilanganJawatan,
                //        Gred = pdog.Nama,                     // <-- fix (was TarikhCipta)
                //        ImplikasiKewanganSebulan = pdobp.JumlahKosSebulan,
                //        ImplikasiKewanganSetahun = pdobp.JumlahKosSetahun,
                //        SkimPerkhidmatan = pdosp.Nama
                //    }
                //  ).ToListAsync();
                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiImplikasiKewangan");

                throw;
            }

        }



    }

}

