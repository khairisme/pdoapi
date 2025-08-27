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
    public class RujJenisDokumenExtService : IRujJenisDokumenExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujJenisDokumenExtService> _logger;

        public RujJenisDokumenExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujJenisDokumenExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<RujJenisDokumenLinkDto>> CarianRujJenisDokumen(RujJenisDokumenCarianDto request)
        {
            try

            {

                var result = await (from pdorjd in _context.PDORujJenisDokumen
                    select new RujJenisDokumenLinkDto{
                         FormatDiterima = pdorjd.FormatDiterima,
                         IdCipta = pdorjd.IdCipta,
                         IdHapus = pdorjd.IdHapus,
                         IdPinda = pdorjd.IdPinda,
                         Keterangan = pdorjd.Keterangan,
                         Kod = pdorjd.Kod,
                         Nama = pdorjd.Nama,
                         StatusAktif = pdorjd.StatusAktif,
                         TarikhCipta = pdorjd.TarikhCipta,
                         TarikhHapus = pdorjd.TarikhHapus,
                         TarikhPinda = pdorjd.TarikhPinda
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in CarianRujJenisDokumen");

                throw;
            }

        }



        public async Task<List<RujJenisDokumenLinkDto>> SenaraiRujJenisDokumen(RujJenisDokumenCarianDto request)
        {
            try

            {

                var result = await (from pdorjd in _context.PDORujJenisDokumen
                    select new RujJenisDokumenLinkDto{
                         FormatDiterima = pdorjd.FormatDiterima,
                         IdCipta = pdorjd.IdCipta,
                         IdHapus = pdorjd.IdHapus,
                         IdPinda = pdorjd.IdPinda,
                         Keterangan = pdorjd.Keterangan,
                         Kod = pdorjd.Kod,
                         Nama = pdorjd.Nama,
                         StatusAktif = pdorjd.StatusAktif,
                         TarikhCipta = pdorjd.TarikhCipta,
                         TarikhHapus = pdorjd.TarikhHapus,
                         TarikhPinda = pdorjd.TarikhPinda
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiRujJenisDokumen");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanRujJenisDokumen(RujJenisDokumenDaftarDto request)
        {
            try

            {

                var result = await (from pdorjd in _context.PDORujJenisDokumen
                    select new DropDownDto{
                         Kod = pdorjd.Kod,
                         Nama = pdorjd.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanRujJenisDokumen");

                throw;
            }

        }



        public async Task DaftarRujJenisDokumen(Guid UserId, RujJenisDokumenDaftarDto request)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var entity = new PDORujJenisDokumen();
                entity.IdCipta = UserId;
                await _context.PDORujJenisDokumen.AddAsync(entity); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in DaftarRujJenisDokumen");

                throw;
            }

        }



        public async Task KemaskiniRujJenisDokumen(Guid UserId, int Id, RujJenisDokumenDaftarDto request)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var data = await (from pdorjd in _context.PDORujJenisDokumen
                             where pdorjd.Id == Id
                             select pdorjd).FirstOrDefaultAsync();
                  data.Kod = request.Kod;
                  data.Nama = request.Nama;
                  data.Keterangan = request.Keterangan;
                  data.FormatDiterima = request.FormatDiterima;
                  data.StatusAktif = request.StatusAktif;
                  data.IdPinda = request.IdPinda;
                  data.TarikhPinda = DateTime.Now;
                  data.TarikhHapus = request.TarikhHapus;

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in KemaskiniRujJenisDokumen");

                throw;
            }

        }



    }

}

