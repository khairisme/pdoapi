using HR.PDO.Application.DTOs;
using HR.PDO.Application.DTOs.PDO;
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
    public class AktivitiOrganisasiPasukanPerundingExtService : IAktivitiOrganisasiPasukanPerundingExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<AktivitiOrganisasiPasukanPerundingExtService> _logger;

        public AktivitiOrganisasiPasukanPerundingExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<AktivitiOrganisasiPasukanPerundingExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<AktivitiOrganisasiPasukanPerundingOutputDto>> SenaraiAktivitiOrganisasiPasukanPerunding()
        {
            try

            {

                var result = await (from pdoaorpp in _context.PDOAktivitiOrganisasiRujPasukanPerunding
                                    join pdoao in _context.PDOAktivitiOrganisasi on pdoaorpp.IdAktivitiOrganisasi equals pdoao.Id
                                    join pdorpp in _context.PDORujPasukanPerunding on pdoaorpp.KodRujPasukanPerunding equals pdorpp.Kod
                                    select new AktivitiOrganisasiPasukanPerundingOutputDto
                                    {
                                        PasukanPerunding= pdorpp.Nama,
                                        NamaOrganisasi = pdoao.Nama.Trim()
                                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in AktivitiOrganisasiPasukanPerundingExtIkutKlasifikasiDanKumpulan");

                throw;
            }

        }

        public async Task<PDOAktivitiOrganisasiRujPasukanPerunding> TambahAktivitiOrganisasiPasukanPerunding(TambahAktivitiOrganisasiPasukanPerundingRequestDto request)
        {
            try

            {

                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOAktivitiOrganisasiRujPasukanPerunding
                {
                    IdAktivitiOrganisasi = request.IdAktivitiOrganisasi,
                    KodRujPasukanPerunding = request.KodRujPasukanPerunding
                };

                await _context.PDOAktivitiOrganisasiRujPasukanPerunding.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return entity;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in AktivitiOrganisasiPasukanPerundingExtIkutKlasifikasiDanKumpulan");

                throw;
            }

        }

        public async Task<bool> HapusTerusAktivitiOrganisasiPasukanPerunding(TambahAktivitiOrganisasiPasukanPerundingRequestDto request)
        {
            try

            {

                var result = await (from pdoaorpp in _context.PDOAktivitiOrganisasiRujPasukanPerunding
                                    where pdoaorpp.KodRujPasukanPerunding == request.KodRujPasukanPerunding
                                    && pdoaorpp.IdAktivitiOrganisasi == request.IdAktivitiOrganisasi
                                   select pdoaorpp
                ).FirstOrDefaultAsync();

                _context.PDOAktivitiOrganisasiRujPasukanPerunding.Remove(result);
                _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitAsync();


                return true;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in AktivitiOrganisasiPasukanPerundingExtIkutKlasifikasiDanKumpulan");

                throw;

            }

        }

    }

}

