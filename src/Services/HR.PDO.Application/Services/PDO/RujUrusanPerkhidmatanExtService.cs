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
using HR.PDO.Application.DTOs;
using HR.PDO.Core.Entities.PDO;

namespace HR.Application.Services.PDO
{
    public class RujUrusanPerkhidmatanExtService : IRujUrusanPerkhidmatanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujUrusanPerkhidmatanExtService> _logger;

        public RujUrusanPerkhidmatanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujUrusanPerkhidmatanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanUrusanPerkhidmatan()
        {
            var excludeList = new List<string> { "04" }; // Example exclude list
            var result = await (from pdorup in _context.PDORujUrusanPerkhidmatan
                                where !excludeList.Contains(pdorup.Kod.Trim())
                                select new DropDownDto
                                {
                                    Kod = pdorup.Kod.Trim(),
                                    Nama = pdorup.Nama.Trim()
                                }).ToListAsync();
            return result;
        }

    }

}

