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
    public class RujKategoriUnitOrganisasiExtService : IRujKategoriUnitOrganisasiExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujKategoriUnitOrganisasiExtService> _logger;

        public RujKategoriUnitOrganisasiExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujKategoriUnitOrganisasiExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanKategoriUnitOrganisasi()
        {
            try

            {

                var result = await (from pdorkuo in _context.PDORujKategoriUnitOrganisasi
                    select new DropDownDto{
                         Kod = pdorkuo.Kod,
                         Nama = pdorkuo.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanKategoriUnitOrganisasi");

                throw;
            }

        }



    }

}

