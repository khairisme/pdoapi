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
    public class RujStatusPermohonanJawatanExtService : IRujStatusPermohonanJawatanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujStatusPermohonanJawatanExtService> _logger;

        public RujStatusPermohonanJawatanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujStatusPermohonanJawatanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }
        public async Task<List<DropDownDto>> RujukanStatusPermohonanJawatan(string KodRujPeranan)
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                                    where pdorspj.KodRujPeranan == KodRujPeranan
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

                _logger.LogError(ex, "Error in RujukanStatusPermohonanJawatan");

                throw;
            }

        }

    }



}

