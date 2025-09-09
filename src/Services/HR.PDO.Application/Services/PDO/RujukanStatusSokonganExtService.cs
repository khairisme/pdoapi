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
    public class RujukanStatusSokonganExtService : IRujukanStatusSokonganExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujukanStatusSokonganExtService> _logger;

        public RujukanStatusSokonganExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujukanStatusSokonganExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanStatusSokongan()
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                                    where new[] { "06", "16", "17" }.Contains(pdorspj.Kod)
                                    select new DropDownDto
                                    {
                                        Kod = pdorspj.Kod,
                                        Nama = pdorspj.Nama
                                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanStatusSokongan");

                throw;
            }

        }




    }

}

