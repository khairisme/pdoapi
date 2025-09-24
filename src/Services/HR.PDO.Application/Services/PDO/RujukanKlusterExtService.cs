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
    public class RujukanKlusterExtService : IRujukanKlusterExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujukanKlusterExtService> _logger;

        public RujukanKlusterExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujukanKlusterExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }
        public async Task<List<DropDownDto>> RujukanKluster()
        {
            try

            {

                var result = await (from pdouo in _context.PDORujKluster
                                    select new DropDownDto
                                    {
                                        Kod = pdouo.Kod.Trim(),
                                        Nama = pdouo.Nama.Trim()
                                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanUnitOrganisasi");

                throw;
            }

        }

    }

}

