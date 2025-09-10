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
    public class RujukanStatusLengkapExtService : IRujukanStatusLengkapExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujukanStatusLengkapExtService> _logger;

        public RujukanStatusLengkapExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujukanStatusLengkapExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanStatusLengkap()
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                                    where new[] { "08", "09" }.Contains(pdorspj.Kod)
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

                _logger.LogError(ex, "Error in RujukanStatusLengkap");

                throw;
            }

        }



    }

}

