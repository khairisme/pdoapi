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
    public class RujukanAgensiExtService : IRujukanAgensiExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujukanAgensiExtService> _logger;

        public RujukanAgensiExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujukanAgensiExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanAgensi(string? NamaAgensi)
        {
            try

            {

                var result = await (from pdouo in _context.PDOUnitOrganisasi
                    where pdouo.IndikatorAgensi == true && pdouo.Nama.Contains(NamaAgensi)
                                    select new DropDownDto{
                         Kod = pdouo.Kod,
                         Nama = pdouo.Nama
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

