using HR.PDO.Application.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Application.Services.PDO
{
    public class RujukanJenisAgensiExtService : IRujukanJenisAgensiExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujukanJenisAgensiExtService> _logger;

        public RujukanJenisAgensiExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujukanJenisAgensiExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }
        public async Task<List<DropDownDto>> RujukanJenisAgensi()
        {
            try

            {

                var result = await (from pdorja in _context.PDORujJenisAgensi
                                    select new DropDownDto
                                    {
                                        Kod = pdorja.Kod.Trim(),
                                        Nama = pdorja.Nama.Trim()
                                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanJenisAgensi");

                throw;
            }

        }

    }

}

