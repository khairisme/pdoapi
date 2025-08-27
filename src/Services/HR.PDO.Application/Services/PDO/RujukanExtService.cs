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

        public async Task<List<DropDownDto>> RujukanAgensi()
        {
            try

            {

                var result = await (from pdouo in _context.PDOUnitOrganisasi
                    where pdouo.IndikatorAgensi == true
                    select new DropDownDto{
                         Kod = pdouo.Kod,
                         Nama = pdouo.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanAgensi");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanStatusPermohonanJawatan(string KodRujPeranan)
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                    where pdorspj.KodRujPeranan == KodRujPeranan
                    select new DropDownDto{
                         Kod = pdorspj.Kod,
                         Nama = pdorspj.Nama
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



        public async Task<List<DropDownDto>> RujukanJenisPermohonan()
        {
            try

            {

                var result = await (from pdorjp in _context.PDORujJenisPermohonan
                    select new DropDownDto{
                         Kod = pdorjp.Kod,
                         Nama = pdorjp.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanJenisPermohonan");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanStatusKelulusanJawatan()
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                    where new[] { "10", "11" }.Contains(pdorspj.Kod)
                    select new DropDownDto{
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



        public async Task<List<DropDownDto>> RujukanStatusSemakanPOAPWN()
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                    where new[] { "01", "12", "13" }.Contains(pdorspj.Kod)
                    select new DropDownDto{
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



        public async Task<List<DropDownDto>> RujukanStatusPindaanWPSKP()
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                    where new[] { "15", "06" }.Contains(pdorspj.Kod)
                    select new DropDownDto{
                         Kod = pdorspj.Kod,
                         Nama = pdorspj.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanStatusPindaanWPSKP");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanStatusLengkap()
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                    where new[] { "08", "09" }.Contains(pdorspj.Kod)
                    select new DropDownDto{
                         Kod = pdorspj.Kod,
                         Nama = pdorspj.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanStatusLengkap");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanStatusPindaan()
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                    where new[] { "06", "15" }.Contains(pdorspj.Kod)
                    select new DropDownDto{
                         Kod = pdorspj.Kod,
                         Nama = pdorspj.Nama
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



        public async Task<List<DropDownDto>> RujukanStatusSokongan()
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                    where new[] { "06", "16" , "17" }.Contains(pdorspj.Kod)
                    select new DropDownDto{
                         Kod = pdorspj.Kod,
                         Nama = pdorspj.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanStatusSokongan");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanStatusSemakan()
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                    where new[] { "18", "06" }.Contains(pdorspj.Kod)
                    select new DropDownDto{
                         Kod = pdorspj.Kod,
                         Nama = pdorspj.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanStatusSemakan");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanStatusKajianSemula()
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                    where new[] { "19", "20" }.Contains(pdorspj.Kod)
                    select new DropDownDto{
                         Kod = pdorspj.Kod,
                         Nama = pdorspj.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanStatusKajianSemula");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanStatusPerakuan()
        {
            try

            {

                var result = await (from pdorspj in _context.PDORujStatusPermohonanJawatan
                    where new[] { "21", "22" }.Contains(pdorspj.Kod)
                    select new DropDownDto{
                         Kod = pdorspj.Kod,
                         Nama = pdorspj.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanStatusPerakuan");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanJenisAgensi()
        {
            try

            {

                var result = await (from pdorja in _context.PDORujJenisAgensi
                    select new DropDownDto{
                         Kod = pdorja.Kod,
                         Nama = pdorja.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanJenisAgensi");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanJenisDokumen()
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

                _logger.LogError(ex, "Error in RujukanJenisDokumen");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanPasukanPerunding()
        {
            try

            {

                var result = await (from pdorpp in _context.PDORujPasukanPerunding
                    select new DropDownDto{
                         Kod = pdorpp.Kod,
                         Nama = pdorpp.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanPasukanPerunding");

                throw;
            }

        }



        public async Task<List<DropDownDto>> RujukanKlasifikasiPerKhidmatan()
        {
            try

            {

                var result = await (from pdokp in _context.PDOKlasifikasiPerkhidmatan
                    select new DropDownDto{
                         Kod = pdokp.Kod,
                         Nama = pdokp.Nama
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in RujukanKlasifikasiPerKhidmatan");

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

