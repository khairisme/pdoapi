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
using Azure.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HR.Application.Services.PDO
{
    public class ButiranPermohonanExtService : IButiranPermohonanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly IRujGelaranJawatanExt _rujGelaranJawatanExt;
        private readonly ILogger<ButiranPermohonanExtService> _logger;

        public ButiranPermohonanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<ButiranPermohonanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task TambahButiranPermohonan(Guid UserId, TambahButiranPermohonanDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOButiranPermohonan();
                entity.IdCipta = UserId;
                entity.TarikhCipta = DateTime.Now;
                entity.IdPermohonanJawatan = request.IdPermohonanJawatan;
                entity.IdAktivitiOrganisasi = request.IdAktivitiOrganisasi;
                entity.KodRujStatusJawatan = request.KodRujStatusJawatan;
                entity.TarikhMula = request.TarikhMula;
                entity.TarikhTamat = request.TarikhTamat;
                entity.KodRujJenisJawatan = request.KodRujJenisJawatan;
                entity.NoButiran = request.NoButiran;
                entity.IndikatorTBK = request.IndikatorTBK;
                entity.IndikatorHBS = request.IndikatorHBS;
                entity.AnggaranTajukJawatan = request.AnggaranTajukJawatan;
                entity.JumlahKosSebulan = request.JumlahKosSebulan;
                entity.TahunButiran = request.TahunButiran;
                entity.BilanganJawatan = request.BilanganJawatan;
                entity.NamaPemilikKompetensi = request.NamaPemilikKompetensi;
                entity.NoKadPengenalanPemilikKompetensi = request.NoKadPengenalanPemilikKompetensi;
                entity.IndikatorJawatanKritikal = request.IndikatorJawatanKritikal;
                entity.IndikatorJawatanSensitif = request.IndikatorJawatanSensitif;
                entity.IndikatorJawatanStrategik = request.IndikatorJawatanStrategik;
                entity.ButirPerubahan = request.ButirPerubahan;
                entity.TagJawatan = request.TagJawatan;
                await _context.PDOButiranPermohonan.AddAsync(entity); 
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonan");

                throw;
            }

        }

        public async Task TambahJawatanButiranPermohonan(Guid UserId, TambahJawatanButiranPermohonanRequestDto requesButiranPermohonan, CadanganJawatanRequestDto requestCadanganJawatan)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var butiranPermohonan = (from pdobp in _context.PDOButiranPermohonan
                                         where pdobp.IdPermohonanJawatan == requesButiranPermohonan.IdPermohonanJawatan
                                         select pdobp).FirstOrDefault();
                butiranPermohonan.BilanganJawatan = butiranPermohonan.BilanganJawatan + requesButiranPermohonan.BilanganJawatan;
                var cadanganJawatan = new PDOCadanganJawatan();
                cadanganJawatan.GelaranJawatan = await _rujGelaranJawatanExt.BacaGelaranJawatan(requestCadanganJawatan.KodRujGelaranJawatan);
                
                _context.PDOButiranPermohonan.Update(butiranPermohonan);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonan");

                throw;
            }

        }


        public async Task KemaskiniButiranPermohonan(Guid UserId, KemaskiniButiranPermohonanRequestDto request)
        {

            try

            {
                var record = (from pdobp in _context.PDOButiranPermohonan.AsNoTracking()
                                            where pdobp.Id == request.IdButiranPermohonan
                                        select pdobp).FirstOrDefault();
                if (record != null)
                {
                    await _unitOfWork.BeginTransactionAsync();
                    record.IdAktivitiOrganisasi = request.IdAktivitiOrganisasi;
                    record.KodRujStatusJawatan = request.KodRujStatusJawatan;
                    record.TarikhMula = request.TarikhMula;
                    record.TarikhTamat = request.TarikhTamat;
                    record.KodRujJenisJawatan = request.KodRujJenisJawatan;
                    record.NoButiran = request.NoButiran;
                    record.IndikatorTBK = request.IndikatorTBK;
                    record.IndikatorHBS = request.IndikatorHBS;
                    record.AnggaranTajukJawatan = request.AnggaranTajukJawatan;
                    record.JumlahKosSebulan = request.JumlahKosSebulan;
                    record.TahunButiran = request.TahunButiran;
                    record.IdPinda = UserId;
                    record.TarikhPinda = DateTime.Now;
                    record.TahunButiran = (short?)DateTime.Now.Year;
                    record.NamaPemilikKompetensi = request.NamaPemilikKompetensi;
                    record.NoKadPengenalanPemilikKompetensi=request.NoKadPengenalanPemilikKompetensi;
                    record.IdSkimPerkhidmatanPemilikKompetensi = request.IdSkimPerkhidmatanPemilikKompetensi;
                    record.IndikatorJawatanStrategik = request.IndikatorJawatanStrategik;
                    record.IndikatorJawatanSensitif = request.IndikatorJawatanSensitif;
                    record.IndikatorJawatanKritikal = request.IndikatorJawatanKritikal;
                    record.ButirPerubahan = record.ButirPerubahan;


                    _context.PDOButiranPermohonan.Update(record);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();
                }
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonan");

                throw;
            }

        }

        public async Task MansuhButiranPermohonanCadanganJawatan(Guid UserId, int IdButiranPermohonan, int IdCadanganJawatan)
        {
            /*
             * Created by : Khairi bin Abu Bakar
             * Created Date : 31/08/2025
             * Purpose: To save updated record into ButiranPerubahan
             */

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var record = await (from pdocj in _context.PDOCadanganJawatan
                                    where pdocj.IdButiranPermohonan == IdButiranPermohonan
                                    && pdocj.Id == IdCadanganJawatan
                                    select pdocj).FirstOrDefaultAsync();

                record.IdHapus = UserId;
                record.TarikhHapus = DateTime.Now;
                record.StatusAktif = false;

                //There is no Butiran Kemaskini field, so I update direct to the table

                _context.PDOCadanganJawatan.Update(record);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonan");

                throw;
            }

        }


        public async Task KemaskiniButiranPerubahanButiranPermohonan(Guid UserId, KemaskiniButiranPermohonanRequestDto request)
        {
            /*
             * Created by : Khairi bin Abu Bakar
             * Created Date : 31/08/2025
             * Purpose: To save updated record into ButiranPerubahan
             */

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var record = await (from pdobp in _context.PDOButiranPermohonan
                                    where pdobp.Id == request.IdButiranPermohonan
                                    select pdobp).FirstOrDefaultAsync();

                var newRecord = new PDOButiranPermohonan();

                newRecord.IdAktivitiOrganisasi = request.IdAktivitiOrganisasi;
                newRecord.KodRujStatusJawatan = request.KodRujStatusJawatan;
                newRecord.TarikhMula = request.TarikhMula;
                newRecord.TarikhTamat = request.TarikhTamat;
                newRecord.KodRujJenisJawatan = request.KodRujJenisJawatan;
                newRecord.NoButiran = request.NoButiran;
                newRecord.IndikatorTBK = request.IndikatorTBK;
                newRecord.IndikatorHBS = request.IndikatorHBS;
                newRecord.AnggaranTajukJawatan = request.AnggaranTajukJawatan;
                newRecord.JumlahKosSebulan = request.JumlahKosSebulan;
                newRecord.TahunButiran = request.TahunButiran;
                newRecord.IdPinda = UserId;
                newRecord.TarikhPinda = DateTime.Now;
                newRecord.TahunButiran = (short?)DateTime.Now.Year;

                var json = JsonSerializer.Serialize(newRecord);

                record.ButirPerubahan = json;

                _context.PDOButiranPermohonan.Update(record);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonan");

                throw;
            }

        }

        public async Task KiraImplikasiKewanganButiranPermohonan(Guid UserId, KiraImplikasiKewanganRequestDto request)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var resultimplikasiKewangan = (from pdopik in _context.PDOPenetapanImplikasiKewangan
                                               where pdopik.IdSkimPerkhidmatan == request.IdSkimPerkhidmatan
                                               select new KiraImplikasiKewanganButiranPermohonanOuputDto
                                               {
                                                   BilanganJawatan = request.BilanganJawatan,
                                                   ImplikasiKewanganSebulan = pdopik.ImplikasiKosSebulan,
                                                   ImplikasiKewanganSetahun = pdopik.ImplikasiKosSebulan*request.BilanganJawatan*12
                                               }).FirstOrDefault();

                var entity = (from pdobp in _context.PDOButiranPermohonan
                              where pdobp.Id == request.IdButiranPermohonan
                              select pdobp).FirstOrDefault();

                //entity.NoButiran = request.NoButiran;
                entity.AnggaranTajukJawatan = request.AnggaranTajukJawatan;
                entity.JumlahKosSebulan = resultimplikasiKewangan.ImplikasiKewanganSebulan;
                entity.JumlahKosSetahun = resultimplikasiKewangan.ImplikasiKewanganSetahun;
                entity.StatusAktif = false;
                _context.PDOButiranPermohonan.Update(entity);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonan");

                throw;
            }

        }

        public async Task<List<TambahButiranPermohonanDto>> SenaraiButiranPermohonan()
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var result = (from pdobp in _context.PDOButiranPermohonan
                              select new TambahButiranPermohonanDto
                              {
                                  Id = pdobp.Id,
                                  IdPermohonanJawatan = pdobp.IdAktivitiOrganisasi,
                                  IdAktivitiOrganisasi = pdobp.IdAktivitiOrganisasi,
                                  KodRujStatusJawatan = pdobp.KodRujStatusJawatan,
                                  TarikhMula = pdobp.TarikhMula,
                                  TarikhTamat = pdobp.TarikhTamat,
                                  KodRujJenisJawatan = pdobp.KodRujJenisJawatan,
                                  //NoButiran = pdobp.NoButiran,
                                  IndikatorTBK = pdobp.IndikatorTBK,
                                  IndikatorHBS = pdobp.IndikatorHBS,
                                  //AnggaranTajukJawatan = pdobp.AnggaranTajukJawatan,
                                  //JumlahKosSebulan = pdobp.JumlahKosSebulan,
                                  TahunButiran = pdobp.TahunButiran
                              }

                            ).ToList();
                return result;
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonan");

                throw;
            }

        }

        public async Task<TambahButiranPermohonanDto> BacaButiranPermohonan(int IdPermohonanJawatan)
        {

            try

            {
                var result = (from pdobp in _context.PDOButiranPermohonan
                              join pdobps in _context.PDOButiranPermohonanSkimGred on pdobp.Id equals pdobps.IdButiranPermohonan
                              join pdosp in _context.PDOSkimPerkhidmatan on pdobps.IdSkimPerkhidmatan equals pdosp.Id
                              where pdobp.IdPermohonanJawatan == IdPermohonanJawatan
                              select new TambahButiranPermohonanDto
                              {
                                  Id = pdobp.Id,
                                  IdPermohonanJawatan = pdobp.IdAktivitiOrganisasi,
                                  IdAktivitiOrganisasi = pdobp.IdAktivitiOrganisasi,
                                  KodRujStatusJawatan = pdobp.KodRujStatusJawatan,
                                  KodRujJenisJawatan = pdobp.KodRujJenisJawatan,
                                  IdSkimPerkhidmatan = pdosp.Id,
                                  TarikhMula = pdobp.TarikhMula,
                                  TarikhTamat = pdobp.TarikhTamat,
                                  NoButiran = pdobp.NoButiran,
                                  IndikatorTBK = pdobp.IndikatorTBK,
                                  IndikatorHBS = pdobp.IndikatorHBS,
                                  AnggaranTajukJawatan = pdobp.AnggaranTajukJawatan,
                                  JumlahKosSebulan = pdobp.JumlahKosSebulan,
                                  TahunButiran = pdobp.TahunButiran
                              }

                            ).FirstOrDefault();
                return result;
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonan");

                throw;
            }

        }

    }

}

