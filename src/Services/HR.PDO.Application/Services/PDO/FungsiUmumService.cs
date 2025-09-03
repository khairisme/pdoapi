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
    public class FungsiUmumService : IFungsiUmum
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<FungsiUmumService> _logger;

        public FungsiUmumService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<FungsiUmumService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<JanaNomborRujukanDto> JanaNomborRujukan(int IdUnitOrganisasi)
        {
            try

            {

                var result = await (from pdouo in _context.PDOUnitOrganisasi
                    where pdouo.Id == IdUnitOrganisasi
                    select new JanaNomborRujukanDto{
                         IdUnitOrganisasi = pdouo.Id,
                         KodJabatan = pdouo.KodJabatan,
                         KodKementerian = pdouo.KodKementerian,
                         KodRujJenisAgensi = pdouo.KodRujJenisAgensi
                    }
                ).FirstOrDefaultAsync();
                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in JanaNomborRujukan");

                throw;
            }

        }



    }

}

