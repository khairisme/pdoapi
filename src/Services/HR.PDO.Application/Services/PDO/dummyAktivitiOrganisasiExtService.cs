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
    public class dummyAktivitiOrganisasiExtService : IdummyAktivitiOrganisasiExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<dummyAktivitiOrganisasiExtService> _logger;

        public dummyAktivitiOrganisasiExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<dummyAktivitiOrganisasiExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<UnitOrganisasiFormDisplayDto> BacaUnitOrganisasi(int Id)
        {
            try

            {

                var result = await (from pdouo in _context.PDOUnitOrganisasi
                    join paoparent in _context.PDOUnitOrganisasi on pdouo.IdIndukUnitOrganisasi equals paoparent.Id
                    join pdorja in _context.PDORujJenisAgensi on pdouo.KodRujJenisAgensi equals pdorja.Kod
                    where pdouo.Id == Id
                    select new UnitOrganisasiFormDisplayDto{
                         JenisAgensi = pdorja.Nama.Trim(),
                         Keterangan = pdouo.Keterangan,
                         KodUnitOrganisasi = pdouo.Kod.Trim(),
                         NamUnitOrganisasi = pdouo.Nama.Trim(),
                         Tahap = pdouo.Tahap,
                         UnitOrganisasiInduk = pdouo.Nama.Trim()
                    }
                ).FirstOrDefaultAsync();
                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in BacaUnitOrganisasi");

                throw;
            }

        }



        public async Task WujudUnitOrganisasiBaru(Guid UserId, UnitOrganisasiWujudDto  request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOUnitOrganisasi();
                entity.IdCipta = UserId;
                entity.KodRujJenisAgensi = request.KodRujJenisAgensi;
                entity.Kod = request.Kod;
                entity.Nama = request.Nama.Trim();
                entity.Tahap = request.Tahap;
                entity.Keterangan = request.Keterangan;
                await _context.PDOUnitOrganisasi.AddAsync(entity); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in WujudUnitOrganisasiBaru");

                throw;
            }

        }



        public async Task<AktivitiOrganisasiDto> BacaAktivitiOrganisasi(int Id)
        {
            try

            {

                var result = await (from pdoao in _context.PDOAktivitiOrganisasi
                    join paoparent in _context.PDOAktivitiOrganisasi on pdoao.IdIndukAktivitiOrganisasi equals paoparent.Id
                    where pdoao.Id == Id
                    select new AktivitiOrganisasiDto{
                         AktiviOrganisasi = pdoao.Nama.Trim(),
                         AktivitiOrganisasiInduk = paoparent.Nama.Trim(),
                         ButiranKemaskini = pdoao.ButiranKemaskini,
                         Id = pdoao.Id,
                         IdCipta = pdoao.IdCipta,
                         IdHapus = pdoao.IdHapus,
                         IdIndukAktivitiOrganisasi = pdoao.IdIndukAktivitiOrganisasi,
                         IdPinda = pdoao.IdPinda,
                         Keterangan = pdoao.Keterangan,
                         Kod = pdoao.Kod.Trim(),
                         KodCartaAktiviti = pdoao.KodCartaAktiviti,
                         KodProgram = pdoao.KodProgram,
                         KodRujKategoriAktivitiOrganisasi = pdoao.KodRujKategoriAktivitiOrganisasi,
                         StatusAktif = pdoao.StatusAktif ?? false,
                         Tahap = pdoao.Tahap,
                         TarikhCipta = pdoao.TarikhCipta,
                         TarikhHapus = pdoao.TarikhHapus,
                         TarikhPinda = pdoao.TarikhPinda
                    }
                ).FirstOrDefaultAsync();
                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in BacaAktivitiOrganisasi");

                throw;
            }

        }



    }

}

