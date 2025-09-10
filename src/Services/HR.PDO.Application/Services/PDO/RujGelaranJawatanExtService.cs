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
    public class RujGelaranJawatanExtService : IRujGelaranJawatanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujGelaranJawatanExtService> _logger;

        public RujGelaranJawatanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujGelaranJawatanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanGelaranJawatan()
        {
            try

            {

                var result = await (from pdorgj in _context.PDORujGelaranJawatan
                    select new DropDownDto{
                         Kod = pdorgj.Kod.Trim(),
                         Nama = pdorgj.Nama.Trim()
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanGelaranJawatan");

                throw;
            }

        }

        public async Task<string?> BacaGelaranJawatan(string? KodRujGelaranJawatan)
        {
            try

            {

                var NamaGelaran = await (from pdorgj in _context.PDORujGelaranJawatan
                                    where pdorgj.Kod == KodRujGelaranJawatan
                                    select pdorgj.Nama).FirstOrDefaultAsync();

                return NamaGelaran;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanGelaranJawatan");

                throw;
            }

        }


    }

}

