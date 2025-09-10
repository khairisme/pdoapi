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
    public class RujukanJenisDokumenExtService : IRujukanJenisDokumenExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujukanJenisDokumenExtService> _logger;

        public RujukanJenisDokumenExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujukanJenisDokumenExtService> logger)
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
                         IdCipta = pdorjd.IdCipta,
                         IdHapus = pdorjd.IdHapus,
                         IdPinda = pdorjd.IdPinda,
                         Keterangan = pdorjd.Keterangan,
                         Kod = pdorjd.Kod,
                         Nama = pdorjd.Nama,
                         StatusAktif = pdorjd.StatusAktif ?? false,
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



        public async Task<List<RujJenisDokumenLinkDto>> SenaraiRujJenisDokumen()
        {
            try

            {

                var result = await (from pdorjd in _context.PDORujJenisDokumen
                    select new RujJenisDokumenLinkDto{
                         IdCipta = pdorjd.IdCipta,
                         IdHapus = pdorjd.IdHapus,
                         IdPinda = pdorjd.IdPinda,
                         Keterangan = pdorjd.Keterangan,
                         Kod = pdorjd.Kod,
                         Nama = pdorjd.Nama,
                         StatusAktif = pdorjd.StatusAktif ?? false,
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



        public async Task<List<DropDownDto>> RujukanJenisDokumen()
        {
            try

            {

                var result = await (from pdorjd in _context.PDORujJenisDokumen
                                    select new DropDownDto
                                    {
                                        Kod = pdorjd.Kod.Trim(),
                                        Nama = pdorjd.Nama.Trim()
                                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanJenisDokumen");

                throw;
            }

        }

        public async Task DaftarRujJenisDokumen(RujJenisDokumenDaftarDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDORujJenisDokumen();
                entity.IdCipta = request.UserId;
                entity.TarikhCipta = DateTime.Now;
                entity.FormatDiterima = request.FormatDiterima;
                entity.Kod = request.Kod;
                entity.Keterangan = request.Keterangan;
                entity.Nama = request.Nama;
                entity.StatusAktif = true;
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



        public async Task KemaskiniRujJenisDokumen(Guid UserId,RujJenisDokumenDaftarDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var data = await (from pdorjd in _context.PDORujJenisDokumen
                             where pdorjd.Id == request.Id
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

