using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Application.DTOs;

namespace HR.Application.Services.PDO
{
    public class SkimPerkhidmatanExtService : ISkimPerkhidmatanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<SkimPerkhidmatanExtService> _logger;

        public SkimPerkhidmatanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<SkimPerkhidmatanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<SkimPerkhidmatanUntukButiranPermohonanDto>> SenaraiSkimPerkhidmatanUntukButiranPermohonan(int IdPermohonanJawatan)
        {
            try

            {

                var result = await (from pdopj in _context.PDOPermohonanJawatan.AsNoTracking()
                    join pdobp in _context.PDOButiranPermohonan.AsNoTracking() on pdopj.Id equals pdobp.IdPermohonanJawatan
                    join pdobpj in _context.PDOButiranPermohonanJawatan.AsNoTracking() on pdobp.Id equals pdobpj.IdButiranPermohonan
                    join pdobpsg in _context.PDOButiranPermohonanSkimGred.AsNoTracking() on pdobp.Id equals pdobpsg.IdButiranPermohonan
                    join pdosp in _context.PDOSkimPerkhidmatan.AsNoTracking() on pdobpsg.IdSkimPerkhidmatan equals  pdosp.Id
                    join pdokp in _context.PDOKlasifikasiPerkhidmatan.AsNoTracking() on pdosp.IdKlasifikasiPerkhidmatan equals pdokp.Id
                    join pdog in _context.PDOGred.AsNoTracking() on new { G = pdobpsg.IdGred, K = pdokp.Id } equals new { G = pdog.Id, K = pdog.IdKlasifikasiPerkhidmatan }
                    join pdoskp in _context.PDOSkimKetuaPerkhidmatan.AsNoTracking() on pdosp.Id equals pdoskp.IdSkimPerkhidmatan
                    join pdoj in _context.PDOJawatan.AsNoTracking() on pdoskp.IdKetuaPerkhidmatan equals pdoj.Id
                    join pdopik in _context.PDOPenetapanImplikasiKewangan.AsNoTracking() on new { G = pdog.Id, S = pdosp.Id } equals new { G = pdopik.IdGred, S = pdopik.IdSkimPerkhidmatan }
                    where pdopj.Id == IdPermohonanJawatan
                    select new SkimPerkhidmatanUntukButiranPermohonanDto{
                         BilanganJawatan = pdobp.BilanganJawatan,
                         Gred = pdog.Nama,
                         ImplikasiKosSebulan = pdopik.ImplikasiKosSebulan*pdobp.BilanganJawatan,
                         ImplikasiKosSetahun = pdopik.ImplikasiKosSetahun*pdobp.BilanganJawatan,
                         SkimPerkhidmatan = pdosp.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiSkimPerkhidmatanUntukButiranPermohonan");

                throw;
            }

        }



    }

}

