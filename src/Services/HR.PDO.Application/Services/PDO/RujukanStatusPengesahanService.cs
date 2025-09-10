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
    public class RujukanStatusPengesahanService : IRujukanStatusPengesahan
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujukanStatusPengesahanService> _logger;

        public RujukanStatusPengesahanService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujukanStatusPengesahanService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanStatusPengesahan()
        {
            try

            {

                var result = await (from pdorsp in _context.PDORujStatusPermohonan
                    where new[] { "04", "05" }.Contains(pdorsp.Kod)
                    select new DropDownDto{
                         Kod = pdorsp.Kod.Trim(),
                         Nama = pdorsp.Nama.Trim()
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanStatusPengesahan");

                throw;
            }

        }



    }

}

