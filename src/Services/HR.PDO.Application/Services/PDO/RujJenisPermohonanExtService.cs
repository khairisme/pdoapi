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
    public class RujJenisPermohonanExtService : IRujJenisPermohonanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujJenisPermohonanExtService> _logger;

        public RujJenisPermohonanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujJenisPermohonanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }
        public async Task<List<DropDownDto>> RujukanJenisPermohonan()
        {
            try

            {

                var result = await (from pdorjp in _context.PDORujJenisPermohonan
                                    select new DropDownDto
                                    {
                                        Kod = pdorjp.Kod.Trim(),
                                        Nama = pdorjp.Nama.Trim()
                                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanJenisPermohonan");

                throw;
            }

        }


    }

}

