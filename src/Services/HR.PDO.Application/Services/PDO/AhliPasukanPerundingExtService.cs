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

namespace HR.Application.Services.PDO
{
    public class AhliPasukanPerundingExtService : IAhliPasukanPerundingExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<AhliPasukanPerundingExtService> _logger;

        public AhliPasukanPerundingExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<AhliPasukanPerundingExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

    }

}

