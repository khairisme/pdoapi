using HR.PDO.Application.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Application.Services.PDO
{
    public class RujStatusKelulusanExtService : IRujukanStatusKelulusanJawatanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujStatusKekosonganJawatanExtService> _logger;

        public RujStatusKelulusanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujStatusKekosonganJawatanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }
        public async Task<List<DropDownDto>> RujukanStatusKelulusanJawatan()
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                                    where new[] { "10", "11" }.Contains(pdorspj.Kod)
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

                _logger.LogError(ex, "Error in RujukanStatusKelulusanJawatan");

                throw;
            }

        }


    }

}

