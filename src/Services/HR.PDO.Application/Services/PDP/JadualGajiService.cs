using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.DTOs.PDP;
using HR.PDO.Application.Interfaces.PDP;
using HR.PDO.Application.Services.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Core.Entities.PDP;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.Services.PDP
{
    public class JadualGajiService : IJadualGajiService
    {
        private readonly IPDPUnitOfWork _unitOfWork;
        private readonly PDPDbContext _dbContext;
        private readonly ILogger<JadualGajiService> _logger;
        public JadualGajiService(IPDPUnitOfWork unitOfWork, PDPDbContext dbContext, ILogger<JadualGajiService> logger)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<JadualGajiResponseDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all JadualGajiResponseDto using Entity Framework");
            var result = await _unitOfWork.Repository<PDPJadualGaji>().GetAllAsync();
            result = result.ToList().Where(e => e.StatusAktif == true);
            return result.Select(MapToDto);
        }
        private JadualGajiResponseDto MapToDto(PDPJadualGaji pDPJadualGaji)
        {
            return new JadualGajiResponseDto
            {
                Id = pDPJadualGaji.Id,
                KodJenisSaraan = pDPJadualGaji.KodJenisSaraan,
                IdGred = pDPJadualGaji.IdGred,
                NomborGred = pDPJadualGaji.NomborGred,
                GajiMinimum = pDPJadualGaji.GajiMinimum,
                GajiMaksimum = pDPJadualGaji.GajiMaksimum,
                KadarKGT = pDPJadualGaji.KadarKGT,
                KadarKGM = pDPJadualGaji.KadarKGM,
                PeratusKGT = pDPJadualGaji.PeratusKGT,
                KodMataGaji = pDPJadualGaji.KodMataGaji,
                PeringkatMataGaji = pDPJadualGaji.PeringkatMataGaji,
                TingkatMataGaji = pDPJadualGaji.TingkatMataGaji,
                TarikhMulaJadualGaji = pDPJadualGaji.TarikhMulaJadualGaji,
                TarikhTamatJadualGaji = pDPJadualGaji.TarikhTamatJadualGaji,
                ButiranKemasKini = pDPJadualGaji.ButiranKemasKini,
           
            };
        }
    }
}
