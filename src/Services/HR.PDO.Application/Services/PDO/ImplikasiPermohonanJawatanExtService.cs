using Azure.Core;
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
    public class ImplikasiPermohonanJawatanExtService : IImplikasiPermohonanJawatanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<ImplikasiPermohonanJawatanExtService> _logger;
        private readonly IButiranPermohonanExt _butiranPermohonanExt;

        public ImplikasiPermohonanJawatanExtService(IButiranPermohonanExt butiranPermohonanExt, IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<ImplikasiPermohonanJawatanExtService> logger)
        {
            _butiranPermohonanExt = butiranPermohonanExt;
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<PDOImplikasiPermohonanJawatan> TambahImplikasiPermohonanJawatan(TambahImplikadiPermohonanJawatanRequestDto request)
        {

            try

            {

                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOImplikasiPermohonanJawatan();
                entity.IdPermohonanJawatan = request.IdPermohonanJawatan;
                entity.SumberKewanganBersepadu = request.SumberKewanganBersepadu;
                entity.IdCipta = request.UserId;
                entity.TarikhCipta = DateTime.Now;
                entity.TarikhHapus = null;
                await _context.PDOImplikasiPermohonanJawatan.AddAsync(entity); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return await BacaImplikasiPermohonanJawatan(entity.Id);

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahImplikasiPermohonanJawatan");

                throw;
            }

        }



        public async Task<TambahButiranPermohonanDto> TambahButiranPermohonan(TambahButiranPermohonanDto request)
        {

            try

            {

                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOButiranPermohonan();
                entity.IdCipta = request.UserId;
                entity.IdPermohonanJawatan = request.IdPermohonanJawatan;
                entity.IdAktivitiOrganisasi = request.IdAktivitiOrganisasi;
                entity.KodRujStatusJawatan = request.KodRujStatusJawatan;
                entity.TarikhMula = request.TarikhMula;
                entity.TarikhTamat = request.TarikhTamat;
                entity.KodRujJenisJawatan = request.KodRujJenisJawatan;
                entity.NoButiran = request.NoButiran;
                entity.IndikatorTBK = request.IndikatorTBK;
                entity.IndikatorHBS = request.IndikatorHBS;
                await _context.PDOButiranPermohonan.AddAsync(entity); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return await _butiranPermohonanExt.BacaButiranPermohonan(entity.Id);


            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonan");

                throw;
            }

        }

        public Task<MansuhWujudImplikasiPermohonanOutputDto> SenaraiMansuhWujudImplikasiPermohonanJawatan(int IdPermohonanJawatan)
        {
            try
            {
                // Query for Butiran Mansuh
                var queryButiranMansuh = from pdobp in _context.PDOButiranPermohonan
                                         join pbpsg in _context.PDOButiranPermohonanSkimGred on pdobp.Id equals pbpsg.IdButiranPermohonan
                                         join pdog in _context.PDOGred on pbpsg.IdGred equals pdog.Id
                                         where pdobp.IdPermohonanJawatan == IdPermohonanJawatan && pdobp.IndikatorRekod == 4 // Mansuh
                                         select new MansuhmplikasiPermohonanDto
                                         {
                                             PerjawatanMansuh = pdobp.AnggaranTajukJawatan,
                                             Gred = pdog.Nama,
                                             KosTerlibat = pdobp.JumlahKosSebulan,
                                             JumlahJawatanMansuh = pdobp.BilanganJawatan,
                                             JumlahKeseluruhanSetahun = pdobp.JumlahKosSetahun
                                         };
                var butiranMansuhList = queryButiranMansuh.ToList();

                var IndikatorRekodList = new int?[] { 1, 3 };

                //Query for Butiran Wujud Baru
                var queryButiranWujudBaru = from pdobp in _context.PDOButiranPermohonan
                                            join pbpsg in _context.PDOButiranPermohonanSkimGred on pdobp.Id equals pbpsg.IdButiranPermohonan
                                            join pdog in _context.PDOGred on pbpsg.IdGred equals pdog.Id
                                            where pdobp.IdPermohonanJawatan == IdPermohonanJawatan 
                                            //&& IndikatorRekodList.Contains(pdobp.IndikatorRekod)
                                            select new WujudImplikasiPermohonanDto
                                            {
                                                PerjawatanDipohon = pdobp.AnggaranTajukJawatan,
                                                GredDipohon = pdog.Nama,
                                                KosTerlibat = pdobp.JumlahKosSebulan,
                                                JumlahJawatanDipohon = pdobp.BilanganJawatan,
                                                JumlahKeseluruhanSetahun = pdobp.JumlahKosSetahun
                                            };
                var butiranWujudBaruList = queryButiranWujudBaru.ToList();

                var result = new MansuhWujudImplikasiPermohonanOutputDto
                {
                    ButiranMansuh = butiranMansuhList,
                    ButiranWujudBaru = butiranWujudBaruList
                };

                return Task.FromResult(result);
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonan");

                throw;
            }



        }

        public async Task<PDOImplikasiPermohonanJawatan> BacaImplikasiPermohonanJawatan(int? Id)
        {
            try
            {
                // Query for Butiran Mansuh
                var implikasiPermohonanJawatan = await  (from pdobp in _context.PDOImplikasiPermohonanJawatan
                                         where pdobp.Id == Id 
                                         select pdobp).FirstOrDefaultAsync();
                return implikasiPermohonanJawatan;
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in BacaImplikasiPermohonanJawatan");

                throw;
            }



        }

    }

}

