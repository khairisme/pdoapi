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
    public class RujTujuanTambahSentaraExtService : IRujTujuanTambahSentaraExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujTujuanTambahSentaraExtService> _logger;

        public RujTujuanTambahSentaraExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujTujuanTambahSentaraExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanTujuanTambahSentara()
        {
            try

            {

                var result = await (from pdortts in _context.PDORujTujuanTambahSentara
                    select new DropDownDto{
                         Kod = pdortts.Kod,
                         Nama = pdortts.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanTujuanTambahSentara");

                throw;
            }

        }



    }

}

