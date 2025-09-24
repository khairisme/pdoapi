using HR.PDO.Application.DTOs;
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
    public class AlamatUnitOrganisasiExtService : IAlamatUnitOrganisasiExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<AlamatUnitOrganisasiExtService> _logger;

        public AlamatUnitOrganisasiExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<AlamatUnitOrganisasiExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }
        public async Task<PDOAlamatUnitOrganisasi> BacaAlamatUnitOrganisasi(int? Id)
        {
            try

            {
                var alamatUnitUnitOrganisasi = await (from pdoauo in _context.PDOAlamatUnitOrganisasi
                                               where pdoauo.IdUnitOrganisasi == Id
                                               select pdoauo).FirstOrDefaultAsync();
                if (alamatUnitUnitOrganisasi == null)
                {
                    throw new KeyNotFoundException("Tiada Rekod Alamat Unit Organisasi");
                }

                return alamatUnitUnitOrganisasi;
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in WujudUnitOrganisasiBaru");

                throw;
            }
        }
        public async Task<PDOAlamatUnitOrganisasi> TambahAlamatUnitOrganisasi(AlamatUnitOrganisasiDto request)
        {
            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOAlamatUnitOrganisasi();
                entity.IdUnitOrganisasi = request.IdUnitOrganisasi;
                entity.KodRujPoskod = request.KodRujPoskod;
                entity.Alamat1 = request.Alamat1;
                entity.Alamat2 = request.Alamat2;
                entity.Alamat3 = request.Alamat3;
                entity.KodRujNegara = request.KodRujNegara;
                entity.KodRujNegeri = request.KodRujNegeri;
                entity.KodRujBandar = request.KodRujBandar;
                entity.NomborTelefonPejabat = request.NomborTelefonPejabat;
                entity.NomborFaksPejabat = request.NomborFaksPejabat;
                entity.LamanWeb = request.LamanWeb;
                entity.KoordinatUnitOrganisasi = request.KoordinatUnitOrganisasi;
                entity.StatusAktif = request.StatusAktif;
                entity.IdCipta = request.UserId;
                entity.TarikhCipta = DateTime.Now;
                await _context.PDOAlamatUnitOrganisasi.AddAsync(entity);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
                return await BacaAlamatUnitOrganisasi(entity.Id);
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in WujudUnitOrganisasiBaru");

                throw;
            }
        }

    }

}

