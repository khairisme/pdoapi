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
    public class RujukanStatusPindaanExtService : IRujukanStatusPindaanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujukanStatusPindaanExtService> _logger;

        public RujukanStatusPindaanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujukanStatusPindaanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanStatusPindaan()
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                                    where new[] { "06", "15" }.Contains(pdorspj.Kod)
                                    select new DropDownDto
                                    {
                                        Kod = pdorspj.Kod.Trim(),
                                        Nama = pdorspj.Nama.Trim()
                                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanStatusPindaan");

                throw;
            }

        }



    }

}

