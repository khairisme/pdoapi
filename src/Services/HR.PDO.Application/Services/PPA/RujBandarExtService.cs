using HR.PDO.Application.Interfaces.PPA;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Contracts.DTOs;
using HR.PDO.Application.Interfaces.PPA;
using HR.PDO.Core.Entities.PPA;

namespace HR.Application.Services.PPA
{
    public class RujBandarExtService : IRujBandarExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PPADbContext _context;
        private readonly ILogger<RujBandarExtService> _logger;

        public RujBandarExtService(IPDOUnitOfWork unitOfWork, PPADbContext dbContext, ILogger<RujBandarExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

    }

}

