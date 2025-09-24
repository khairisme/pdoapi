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
using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Shared.Interfaces;
using HR.PDO.Application.DTOs;

namespace HR.Application.Services.PDO
{
    public class RujukanBidangPengkhusanExtService : IRujukanBidangPengkhususanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly IObjectMapper _mapper;
        private readonly PDODbContext _context;
        private readonly ILogger<RujukanJenisSaraanExtService> _logger;

        public RujukanBidangPengkhusanExtService(IObjectMapper mapper,IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujukanJenisSaraanExtService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }
        public async Task<List<DropDownDto>> RujukanBidangPengkhususan()
        {
            _logger.LogInformation("Getting all RujukanBidangPengkhususan using Entity Framework");
            var result = await _unitOfWork.Repository<PDPBidangPengkhususan>()
                .Query()                          // IQueryable, not IEnumerable
                .Where(p => p.StatusAktif== true)
                .Select(p => new DropDownDto
                {
                    Kod = p.Kod.Trim(),                                                                                                                                                        
                    Nama = p.Nama.Trim(),
                })
                .ToListAsync();
            return result;
        }

    }

}

