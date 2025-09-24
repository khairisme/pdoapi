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
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Application.DTOs;

namespace HR.Application.Services.PDO
{
    public class RujukanLaluanKemajuanKerjayaExtService : IRujukanLaluanKemajuanKerjayaExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujStatusJawatanExtService> _logger;

        public RujukanLaluanKemajuanKerjayaExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujStatusJawatanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanLaluanKemajuanKerjaya()
        {
            try

            {

                var result = await (from pdorgj in _context.PDORujLaluanKemajuanKerjaya
                                    select new DropDownDto{
                                        Kod = pdorgj.Kod.Trim(),
                                         Nama = pdorgj.Nama.Trim()
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

