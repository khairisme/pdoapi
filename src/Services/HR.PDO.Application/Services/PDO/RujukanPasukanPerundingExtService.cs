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
    public class RujukanPasukanPerundingExtService : IRujukanPasukanPerundingExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujukanPasukanPerundingExtService> _logger;

        public RujukanPasukanPerundingExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujukanPasukanPerundingExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }
        public async Task<List<DropDownDto>> RujukanPasukanPerunding()
        {
            try

            {

                var result = await (from pdorpp in _context.PDORujPasukanPerunding
                                    select new DropDownDto
                                    {
                                        Kod = pdorpp.Kod,
                                        Nama = pdorpp.Nama
                                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanPasukanPerunding");

                throw;
            }

        }

    }

}

