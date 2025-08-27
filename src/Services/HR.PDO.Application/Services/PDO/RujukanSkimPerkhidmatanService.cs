using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Application.DTOs;

namespace HR.Application.Services.PDO
{
    public class RujukanSkimPerkhidmatanService : IRujukanSkimPerkhidmatan
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujukanSkimPerkhidmatanService> _logger;

        public RujukanSkimPerkhidmatanService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujukanSkimPerkhidmatanService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanSkimPerkhidmatan()
        {
            try

            {

                var result = await (from pdosp in _context.PDOSkimPerkhidmatan
                    select new DropDownDto{
                         Kod = pdosp.Kod,
                         Nama = pdosp.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanSkimPerkhidmatan");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanSkimPerkhidmatanIkutKumpulan(int IdKumpulanPerkhidmatan)
        {
            try

            {

                var result = await (from pdosp in _context.PDOSkimPerkhidmatan
                    where pdosp.IdKumpulanPerkhidmatan == IdKumpulanPerkhidmatan
                    select new DropDownDto{
                         Kod = pdosp.Kod,
                         Nama = pdosp.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanSkimPerkhidmatanIkutKumpulan");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanSkimPerkhidmatanIkutKlasifikasi(int IdKlasifikasiPerkhidmatan)
        {
            try

            {

                var result = await (from pdosp in _context.PDOSkimPerkhidmatan
                    where pdosp.IdKlasifikasiPerkhidmatan == IdKlasifikasiPerkhidmatan
                    select new DropDownDto{
                         Kod = pdosp.Kod,
                         Nama = pdosp.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanSkimPerkhidmatanIkutKlasifikasi");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanSkimPerkhidmatanIkutKlasifikasiDanKumpulan(int IdKlasifikasiPerkhidmatan, int IdKumpulanPerkhidmatan)
        {
            try

            {

                var result = await (from pdosp in _context.PDOSkimPerkhidmatan
                    where pdosp.IdKlasifikasiPerkhidmatan == IdKlasifikasiPerkhidmatan && pdosp.IdKumpulanPerkhidmatan == IdKumpulanPerkhidmatan
                    select new DropDownDto{
                         Kod = pdosp.Kod,
                         Nama = pdosp.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanSkimPerkhidmatanIkutKlasifikasiDanKumpulan");

                throw;
            }

        }



    }

}

