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
    public class RujukanExtService : IRujukanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujukanExtService> _logger;

        public RujukanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<RujukanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<DropDownDto>> RujukanStatusPersetujuan()
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                    where new[] { "03", "05", "14" }.Contains(pdorspj.Kod)
                    select new DropDownDto{
                         Kod = pdorspj.Kod,
                         Nama = pdorspj.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanStatusPersetujuan");

                throw;
            }

        }











        public async Task<List<DropDownDto>> RujukanStatusSemakanPOAPWN()
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                                    where new[] { "01", "12", "13" }.Contains(pdorspj.Kod)
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

                _logger.LogError(ex, "Error in RujukanStatusSemakanPOAPWN");

                throw;
            }

        }






        public async Task<List<DropDownDto>> RujukanPangkat()
        {
            try

            {

                var result = await (from pparpbb in _context.PPARujPangkatBadanBeruniform
                    select new DropDownDto{
                         Kod = pparpbb.Kod,
                         Nama = pparpbb.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanPangkat");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanGelaranJawatan()
        {
            try

            {

                var result = await (from pdorgj in _context.PDORujGelaranJawatan
                    select new DropDownDto{
                         Kod = pdorgj.Kod,
                         Nama = pdorgj.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanGelaranJawatan");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanUrusanPerkhidmatan()
        {
            try

            {

                var result = await (from pdorup in _context.PDORujUrusanPerkhidmatan
                    select new DropDownDto{
                         Kod = pdorup.Kod,
                         Nama = pdorup.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanUrusanPerkhidmatan");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanJenisMesyuarat()
        {
            try

            {

                var result = await (from pdorjm in _context.PDORujJenisMesyuarat
                    select new DropDownDto{
                         Kod = pdorjm.Kod,
                         Nama = pdorjm.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanJenisMesyuarat");

                throw;
            }

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



        public async Task<List<DropDownDto>> RujukanJenisJawatan()
        {
            try

            {

                var result = await (from pdorjw in _context.PDORujJenisJawatan
                    select new DropDownDto{
                         Kod = pdorjw.Kod,
                         Nama = pdorjw.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanJenisJawatan");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanKetuaPerkhidmatan(int IdSkimPerkhidmatan)
        {
            try

            {

                var result = await (from pdoj in _context.PDOJawatan
                    join pdoskp in _context.PDOSkimKetuaPerkhidmatan on pdoj.Id equals pdoskp.IdKetuaPerkhidmatan
                    select new DropDownDto{
                         Kod = pdoj.Kod,
                         Nama = pdoj.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanKetuaPerkhidmatan");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanKumpulanPerkhidmatan()
        {
            try

            {

                var result = await (from pdokp in _context.PDOKumpulanPerkhidmatan
                    select new DropDownDto{
                         Kod = pdokp.Kod,
                         Nama = pdokp.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanKumpulanPerkhidmatan");

                throw;
            }

        }



    }

}

