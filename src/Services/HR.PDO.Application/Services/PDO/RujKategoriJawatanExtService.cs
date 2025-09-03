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
    public class RujKategoriJawatanExtService : IRujKategoriJawatanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujKategoriJawatanExtService> _logger;

        public RujKategoriJawatanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujKategoriJawatanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanAgensi()
        {
            try

            {

                var result = await (from pdorkj in _context.PDORujKategoriJawatan
                    where pdorkj.StatusAktif == true
                    select new DropDownDto{
                         Kod = pdorkj.Kod,
                         Nama = pdorkj.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanAgensi");

                throw;
            }

        }



    }

}

