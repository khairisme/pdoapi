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
    public class RujStatusPersetujuanExtService : IRujStatusPersetujuanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujStatusPersetujuanExtService> _logger;

        public RujStatusPersetujuanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujStatusPersetujuanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanStatusPersetujuan()
        {
            try

            {

                var result = await (from pdorgj in _context.PDORujStatusJawatan
                    select new DropDownDto{
                         Kod = pdorgj.Kod,
                         Nama = pdorgj.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanStatusJawatan");

                throw;
            }

        }



    }

}

