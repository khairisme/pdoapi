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
    public class KlasifikasiPerkhidmatanExtService : IKlasifikasiPerkhidmatanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<KlasifikasiPerkhidmatanExtService> _logger;

        public KlasifikasiPerkhidmatanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<KlasifikasiPerkhidmatanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }
        public async Task<List<DropDownDto>> RujukanKlasifikasiPerKhidmatan()
        {
            try

            {

                var result = await (from pdokp in _context.PDOKlasifikasiPerkhidmatan
                    select new DropDownDto{
                        Id = pdokp.Id,
                        Kod = pdokp.Kod,
                        Nama = pdokp.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanKlasifikasiPerKhidmatan");

                throw;
            }

        }



    }

}

