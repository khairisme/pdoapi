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
    public class RujJenisJawatanExtService : IRujJenisJawatanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujJenisJawatanExtService> _logger;

        public RujJenisJawatanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujJenisJawatanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanJenisJawatan()
        {
            try

            {

                var result = await (from pdorjj in _context.PDORujJenisJawatan
                    select new DropDownDto{
                         Kod = pdorjj.Kod,
                         Nama = pdorjj.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanJenisJawatan");

                throw;
            }

        }



    }

}

