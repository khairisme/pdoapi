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
    public class JadualGajiExtService : IJadualGajiExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<JadualGajiExtService> _logger;

        public JadualGajiExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<JadualGajiExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<JadualGajiExtDto>> SenaraiSemuaJadualGaji(JadualGajiExtDto request)
        {
            try

            {

                var result = await (from pdojg in _context.PDPJadualGaji
                    select new JadualGajiExtDto{
                         ButiranKemasKini = request.ButiranKemasKini,
                         GajiMaksimum = request.GajiMaksimum,
                         GajiMinimum = request.GajiMinimum,
                         Id = request.Id,
                         IdGred = request.IdGred,
                         KadarKGM = request.KadarKGM,
                         KadarKGT = request.KadarKGT,
                         KodJenisSaraan = request.KodJenisSaraan,
                         KodMataGaji = request.KodMataGaji,
                         NomborGred = request.NomborGred,
                         PeratusKGT = request.PeratusKGT,
                         PeringkatMataGaji = request.PeringkatMataGaji,
                         TarikhMulaJadualGaji = request.TarikhMulaJadualGaji,
                         TarikhTamatJadualGaji = request.TarikhTamatJadualGaji,
                         TingkatMataGaji = request.TingkatMataGaji
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiSemuaJadualGaji");

                throw;
            }

        }



    }

}

