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
    public class RujukanGredService : IRujukanGred
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujukanGredService> _logger;

        public RujukanGredService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujukanGredService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanGredIkutKlasifikasiDanKumpulan(int IdKlasifikasiPerkhidmatan, int IdKumpulanPerkhidmatan)
        {
            try

            {

                var result = await (from pdog in _context.PDOGred
                    where pdog.IdKlasifikasiPerkhidmatan == IdKlasifikasiPerkhidmatan && pdog.IdKumpulanPerkhidmatan == IdKumpulanPerkhidmatan
                    select new DropDownDto{
                         Kod = pdog.Kod.Trim(),
                         Nama = pdog.Nama.Trim()
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanGredIkutKlasifikasiDanKumpulan");

                throw;
            }

        }



    }

}

