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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using HR.PDO.Shared.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace HR.Application.Services.PDO
{
    public class ButiranPermohonanExtService : IButiranPermohonanExt
    {
        // Author: Khairi bin Abu Bakar
        // Date: 16 September 2025
        // Purpose: Initializes the ButiranPermohonanExtService with all required dependencies for processing Butiran Permohonan logic.
        //          These dependencies include reference services, database context, unit of work, object mapping, and logging.

        private readonly IPDOUnitOfWork _unitOfWork; // Handles transactional operations across repositories
        private readonly PDODbContext _context; // Direct access to the PDO database context
        private readonly IRujGelaranJawatanExt _rujGelaranJawatanExt; // Reference service for job titles
        private readonly IRujStatusJawatanExt _rujstatusjawatanExt; // Reference service for job status
        private readonly IRujJenisJawatanExt _rujjenisjawatanExt; // Reference service for job types
        private readonly IKumpulanPerkhidmatanExt _kumpulanperkhidmatanext; // Reference service for service groups
        private readonly IKlasifikasiPerkhidmatanExt _klasifikasiperkhidmatanext; // Reference service for service classifications
        private readonly IRujukanSkimPerkhidmatan _rujukanskimperkhidmatan; // Reference service for service schemes
        private readonly IRujPangkatBadanBeruniformExt _rujpangkatbadanberuniformext; // Reference service for uniformed body ranks
        private readonly IObjectMapper _mapper; // Object mapper for DTO-to-entity transformations
        private readonly ILogger<ButiranPermohonanExtService> _logger; // Logging service for diagnostics and traceability

        /// <summary>
        /// Constructs the ButiranPermohonanExtService with all required dependencies.
        /// </summary>
        /// <param name="mapper">Object mapper for DTO transformations.</param>
        /// <param name="rujGelaranJawatanExt">Service for job title references.</param>
        /// <param name="rujstatusjawatanExt">Service for job status references.</param>
        /// <param name="rujjenisjawatanExt">Service for job type references.</param>
        /// <param name="kumpulanperkhidmatanext">Service for service group references.</param>
        /// <param name="klasifikasiperkhidmatanext">Service for service classification references.</param>
        /// <param name="rujukanskimperkhidmatan">Service for scheme references.</param>
        /// <param name="rujpangkatbadanberuniformext">Service for uniformed rank references.</param>
        /// <param name="unitOfWork">Unit of work for transactional operations.</param>
        /// <param name="dbContext">Database context for direct data access.</param>
        /// <param name="logger">Logger for diagnostics and error tracking.</param>
        public ButiranPermohonanExtService(
            IObjectMapper mapper,
            IRujGelaranJawatanExt rujGelaranJawatanExt,
            IRujStatusJawatanExt rujstatusjawatanExt,
            IRujJenisJawatanExt rujjenisjawatanExt,
            IKumpulanPerkhidmatanExt kumpulanperkhidmatanext,
            IKlasifikasiPerkhidmatanExt klasifikasiperkhidmatanext,
            IRujukanSkimPerkhidmatan rujukanskimperkhidmatan,
            IRujPangkatBadanBeruniformExt rujpangkatbadanberuniformext,
            IPDOUnitOfWork unitOfWork,
            PDODbContext dbContext,
            ILogger<ButiranPermohonanExtService> logger)
        {
            _mapper = mapper;
            _rujGelaranJawatanExt = rujGelaranJawatanExt;
            _rujstatusjawatanExt = rujstatusjawatanExt;
            _rujjenisjawatanExt = rujjenisjawatanExt;
            _kumpulanperkhidmatanext = kumpulanperkhidmatanext;
            _klasifikasiperkhidmatanext = klasifikasiperkhidmatanext;
            _rujukanskimperkhidmatan = rujukanskimperkhidmatan;
            _rujpangkatbadanberuniformext = rujpangkatbadanberuniformext;
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }
        public async Task<string?> ButiranPermohonanTagJawatan(ButiranPermohonanTagJawatanRequestDto request)
        {
            var KetuaPerkhidmatan = await (from pdoskp in _context.PDOSkimKetuaPerkhidmatan
                                           join pdoj in _context.PDOJawatan on pdoskp.IdKetuaPerkhidmatan equals pdoj.Id
                                           where pdoskp.IdSkimPerkhidmatan == request.IdSkimPerkhidmatan
                                           && pdoj.Id == request.IdKetuaPerkhidmatan
                                           select new KetuaJawatanDto
                                           {
                                               IdUnitOrganisasi = pdoj.IdUnitOrganisasi,
                                               IdJawatan = pdoj.Id
                                           }).FirstOrDefaultAsync();

            var idKetuaJabatanPerkhidmatanAwam = await (from pdoj in _context.PDOJawatan
                                                        where pdoj.GelaranJawatan == "KETUA PENGARAH PERKHIDMATAN AWAM"
                                                        select pdoj.Id).FirstOrDefaultAsync();

            var KodRujStatusBekalan = "";
            var TagJawatan = "";
            if (idKetuaJabatanPerkhidmatanAwam == KetuaPerkhidmatan.IdJawatan)
            {
                TagJawatan = "Kader";
                KodRujStatusBekalan = "01";
            }
            else if (request.IdUnitOrganisasi != KetuaPerkhidmatan.IdUnitOrganisasi)
            {
                TagJawatan = "Kader";
                KodRujStatusBekalan = "02";
            }
            else if (request.IdUnitOrganisasi == KetuaPerkhidmatan.IdUnitOrganisasi)
            {
                TagJawatan = "";
                KodRujStatusBekalan = "03";
            }
            return TagJawatan;
        }

        // Author: Khairi bin Abu Bakar
        // Date: 16 September 2025
        /// <summary>
        /// Adds a new Butiran Permohonan record to the database.
        /// </summary>
        /// <param name="request">DTO containing the details of the new Butiran Permohonan.</param>
        /// <returns>The newly created PDOButiranPermohonan entity.</returns>
        public async Task<PDOButiranPermohonan> TambahButiranPermohonan(TambahButiranPermohonanDto request)
        {
            try
            {
                var countAll = await (from pdobp in _context.PDOButiranPermohonan
                                   join pdobpsg in _context.PDOButiranPermohonanSkimGred
                                   on pdobp.Id equals pdobpsg.IdButiranPermohonan
                                   where pdobp.IdPermohonanJawatan == request.IdPermohonanJawatan
                                   select pdobp
                                   ).CountAsync();
                var count = await (from pdobp in _context.PDOButiranPermohonan
                                   join pdobpsg in _context.PDOButiranPermohonanSkimGred
                                   on pdobp.Id equals pdobpsg.IdButiranPermohonan
                                   where pdobp.IdPermohonanJawatan == request.IdPermohonanJawatan
                                   && pdobpsg.IdSkimPerkhidmatan == request.IdSkimPerkhidmatan
                                   select new
                                   {
                                       pdobp.IdPermohonanJawatan,
                                       pdobpsg.IdSkimPerkhidmatan
                                   })
                                   .CountAsync();

                string[] alphabet = Enumerable.Range('a', 26)
                                              .Select(c => ((char)c).ToString())
                                              .ToArray();


                ++count;
                ++countAll;
                string? subCode = "";
                if (count > 0) {
                    subCode = alphabet[count - 1];
                }
                string formatted = string.IsNullOrWhiteSpace(subCode) ? "" : $"({subCode})";

                var newNoButiran = "(" + countAll.ToString() + ")"+ formatted + "(Baharu)";

                var KetuaPerkhidmatan = await (from pdoskp in _context.PDOSkimKetuaPerkhidmatan
                                     join pdoj in _context.PDOJawatan on pdoskp.IdKetuaPerkhidmatan equals pdoj.Id
                                     where pdoskp.IdSkimPerkhidmatan == request.IdSkimPerkhidmatan
                                     && pdoj.Id == request.IdKetuaPerkhidmatan
                                     select new KetuaJawatanDto
                                     {
                                         IdUnitOrganisasi= pdoj.IdUnitOrganisasi,
                                         IdJawatan = pdoj.Id
                                     }).FirstOrDefaultAsync();

                var idKetuaJabatanPerkhidmatanAwam = await (from pdoj in _context.PDOJawatan
                                                            where pdoj.GelaranJawatan == "KETUA PENGARAH PERKHIDMATAN AWAM"
                                                            select pdoj.Id).FirstOrDefaultAsync();
                var KodRujStatusBekalan = "";
                if (idKetuaJabatanPerkhidmatanAwam == KetuaPerkhidmatan.IdJawatan)
                {
                    KodRujStatusBekalan = "01";
                }
                else if (request.IdUnitOrganisasi != KetuaPerkhidmatan.IdUnitOrganisasi)
                {
                    KodRujStatusBekalan = "02";
                }
                else if (request.IdUnitOrganisasi == KetuaPerkhidmatan.IdUnitOrganisasi)
                {
                    KodRujStatusBekalan = "03";
                }

                await _unitOfWork.BeginTransactionAsync();


                var entity = new PDOButiranPermohonan
                {
                    IdCipta = request.UserId,
                    TarikhCipta = DateTime.Now,
                    IdPermohonanJawatan = request.IdPermohonanJawatan,
                    IdAktivitiOrganisasi = request.IdAktivitiOrganisasi,
                    KodRujStatusJawatan = request.KodRujStatusJawatan,
                    NoButiran = request.NoButiran ?? newNoButiran,
                    TarikhMula = request.TarikhMula,
                    TarikhTamat = request.TarikhTamat,
                    KodRujJenisJawatan = request.KodRujJenisJawatan,
                    //IndikatorPermohonan = request.IndikatorPermohonan,
                    IndikatorTBK = request.IndikatorTBK,
                    IndikatorHBS = request.IndikatorHBS,
                    AnggaranTajukJawatan = request.AnggaranTajukJawatan,
                    JumlahKosSebulan = request.JumlahKosSebulan,
                    TahunButiran = request.TahunButiran,
                    BilanganJawatan = request.BilanganJawatan,
                    NamaPemilikKompetensi = request.NamaPemilikKompetensi.Trim(),
                    NoKadPengenalanPemilikKompetensi = request.NoKadPengenalanPemilikKompetensi,
                    IndikatorJawatanKritikal = request.IndikatorJawatanKritikal,
                    IndikatorJawatanSensitif = request.IndikatorJawatanSensitif,
                    IndikatorJawatanStrategik = request.IndikatorJawatanStrategik,
                    IndikatorPemohon = 0,
                    ButirPerubahan = request.ButirPerubahan,
                    IndikatorRekod = request.IndikatorRekod,
                    KodRujStatusBekalan = KodRujStatusBekalan,
                    //KodBidangPengkhususan = request.KodBidangPengkhususan,
                    //KodRujLaluanKemajuanKerjaya = request.KodRujLaluanKemajuanKerjaya,
                    KodRujUrusanPerkhidmatan = request.KodRujUrusanPerkhidmatan=="" ? null : request.KodRujUrusanPerkhidmatan,
                    TagJawatan = request.TagJawatan
                };

                await _context.PDOButiranPermohonan.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TambahButiranPermohonan");
                throw;
            }
        }

        // Author: Khairi bin Abu Bakar
        // Date: 16 September 2025
        /// <summary>
        /// Adds a new Cadangan Jawatan and updates the BilanganJawatan in the related Butiran Permohonan.
        /// </summary>
        /// <param name="requesButiranPermohonan">DTO containing the Butiran Permohonan update info.</param>
        /// <param name="requestCadanganJawatan">DTO containing the Cadangan Jawatan details.</param>
        public async Task TambahJawatanButiranPermohonan(
            TambahJawatanButiranPermohonanRequestDto requesButiranPermohonan,
            CadanganJawatanRequestDto requestCadanganJawatan)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var butiranPermohonan = _context.PDOButiranPermohonan
                    .FirstOrDefault(p => p.IdPermohonanJawatan == requesButiranPermohonan.IdPermohonanJawatan);

                if (butiranPermohonan != null)
                {
                    butiranPermohonan.BilanganJawatan += requesButiranPermohonan.BilanganJawatan;

                    var cadanganJawatan = new PDOCadanganJawatan
                    {
                        KodRujGelaranJawatan = requestCadanganJawatan.KodRujGelaranJawatan
                    };

                    _context.PDOButiranPermohonan.Update(butiranPermohonan);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TambahJawatanButiranPermohonan");
                throw;
            }
        }

        // Author: Khairi bin Abu Bakar
        // Date: 16 September 2025
        /// <summary>
        /// Updates an existing Butiran Permohonan record with new details.
        /// </summary>
        /// <param name="request">DTO containing the updated Butiran Permohonan information.</param>
        public async Task KemaskiniButiranPermohonan(KemaskiniButiranPermohonanRequestDto request)
        {
            try
            {
                var record = _context.PDOButiranPermohonan
                    .AsNoTracking()
                    .FirstOrDefault(p => p.Id == request.IdButiranPermohonan);

                if (record != null)
                {
                    await _unitOfWork.BeginTransactionAsync();

                    record.IdAktivitiOrganisasi = request.IdAktivitiOrganisasi;
                    record.NoButiran = request.NoButiran;
                    record.AnggaranTajukJawatan = request.AnggaranTajukJawatan;
                    record.JumlahKosSebulan = request.JumlahKosSebulan;
                    record.TahunButiran = (short?)DateTime.Now.Year;
                    record.IdPinda = request.UserId;
                    record.TarikhPinda = DateTime.Now;
                    record.NamaPemilikKompetensi = request.NamaPemilikKompetensi.Trim();
                    record.NoKadPengenalanPemilikKompetensi = request.NoKadPengenalanPemilikKompetensi;
                    record.IndikatorJawatanStrategik = request.IndikatorJawatanStrategik;
                    record.IndikatorJawatanSensitif = request.IndikatorJawatanSensitif;
                    record.IndikatorJawatanKritikal = request.IndikatorJawatanKritikal;
                    record.IndikatorRekod = request.IndikatorRekod;
                    record.ButirPerubahan = request.ButirPerubahan;

                    _context.PDOButiranPermohonan.Update(record);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in KemaskiniButiranPermohonan");
                throw;
            }
        }
        public async Task<PDOButiranPermohonan> MansuhButiranButiranJawatan(MansuhButiranJawatanRequestDto request)
        {

            try
            {
                var recordButiran = await (from pdobp in _context.PDOButiranPermohonan
                                    where pdobp.Id == request.IdButiranPermohonan && pdobp.IdPermohonanJawatan==request.IdPermohonanJawatan
                                    select pdobp).FirstOrDefaultAsync();

                var mansuhButiran = new MansuhButiranPermohonanDto();
                //Soft Delete current ButiranPermohonan
                await _unitOfWork.BeginTransactionAsync();
                mansuhButiran.IndikatorRekod = 4; //Mansuh
                mansuhButiran.StatusAktif = false;
                mansuhButiran.IdHapus = request.UserId;
                mansuhButiran.TarikhHapus = DateTime.Now;
                var json = JsonSerializer.Serialize(mansuhButiran);

                recordButiran.ButiranKemaskini = json;

                _context.PDOButiranPermohonan.Update(recordButiran);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return recordButiran;
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahButiranPermohonan");

                throw;
            }

        }


        public async Task KemaskiniButiranPerubahanButiranPermohonan(KemaskiniButiranPermohonanRequestDto request)
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
                newRecord.AnggaranTajukJawatan = request.AnggaranTajukJawatan;
                newRecord.JumlahKosSebulan = request.JumlahKosSebulan;
                newRecord.TahunButiran = request.TahunButiran;
                newRecord.IdPinda = request.UserId;
                newRecord.TarikhPinda = DateTime.Now;
                newRecord.TahunButiran = (short?)DateTime.Now.Year;

                var json = JsonSerializer.Serialize(newRecord);

                record.ButiranKemaskini = json;

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

        public async Task KiraImplikasiKewanganButiranPermohonan(KiraImplikasiKewanganRequestDto request)
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
                                  //TahunButiran = pdobp.TahunButiran
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

        public async Task<ButiranPermohonanLoadOutputDto> MuatButiranPermohonan(int? IdPermohonanJawatan, int? IdButiranPermohonan)
        {
            var latestTarikhCipta = await _context.PDOButiranPermohonan
                                       .Where(p => p.IdPermohonanJawatan == IdPermohonanJawatan && p.Id == IdButiranPermohonan)
                                       .Select(p => p.TarikhCipta)
                                       .DefaultIfEmpty()
                                       .MaxAsync();

            var newOutput = await (from pdobp in _context.PDOButiranPermohonan
                             where pdobp.IdPermohonanJawatan == IdPermohonanJawatan
                             && pdobp.Id == IdButiranPermohonan 
                             orderby pdobp.TarikhCipta descending
                                   //&& pdobp.TarikhCipta == latestTarikhCipta
                             select new ButiranPermohonanLoadOutputDto
                             {
                                IdPermohonanJawatan = pdobp.IdPermohonanJawatan,
                                IdAktivitiOrganisasi = pdobp.IdAktivitiOrganisasi,
                                 //IdKumpulanPerkhidmatan = pdobp.IdKumpulanPerkhidmatan,
                                 KodRujStatusJawatan = pdobp.KodRujStatusJawatan,
                                TarikhMula = pdobp.TarikhMula,
                                TarikhTamat = pdobp.TarikhTamat,
                                KodRujJenisJawatan = pdobp.KodRujJenisJawatan,
                                KodRujGelaranJawatan = pdobp.KodRujGelaranJawatan,
                                KodRujPangkatBadanBeruniform = pdobp.KodRujPangkatBadanBeruniform,
                                TagJawatan = pdobp.TagJawatan,
                                IndikatorJawatanStrategik = pdobp.IndikatorJawatanStrategik,
                                IndikatorJawatanSensitif = pdobp.IndikatorJawatanSensitif,
                                IndikatorJawatanKritikal = pdobp.IndikatorJawatanKritikal,
                                NoButiran = pdobp.NoButiran,
                                AnggaranTajukJawatan = pdobp.AnggaranTajukJawatan,
                                ButirPerubahan = pdobp.ButirPerubahan,
                                BilanganJawatan = pdobp.BilanganJawatan,
                                TahunButiran = pdobp.TahunButiran,
                                IndikatorTBK = pdobp.IndikatorTBK,
                                IndikatorHBS = pdobp.IndikatorHBS,
                                //KodRujJenisSaraan= pdobp.KodRujJenisSaraan,
                                JumlahKosSebulan = pdobp.JumlahKosSebulan,
                                JumlahKosSetahun = pdobp.JumlahKosSetahun,
                                IndikatorPemohon = pdobp.IndikatorPemohon,
                                KodRujUrusanPerkhidmatan = pdobp.KodRujUrusanPerkhidmatan,
                                IdButiranPermohonanLama = pdobp.IdButiranPermohonanLama,
                                IdPemilikKompetensi = pdobp.IdPemilikKompetensi,
                                NamaPemilikKompetensi = pdobp.NamaPemilikKompetensi,
                                NoKadPengenalanPemilikKompetensi = pdobp.NoKadPengenalanPemilikKompetensi,
                                IdSkimPerkhidmatanPemilikKompetensi = pdobp.IdSkimPerkhidmatanPemilikKompetensi,
                                IdGredPemilikKompetensi = pdobp.IdGredPemilikKompetensi,
                                KodRujTujuanTambahSentara = pdobp.KodRujTujuanTambahSentara,
                                IndikatorRekod = pdobp.IndikatorRekod,
                                ButiranKemaskini = pdobp.ButiranKemaskini                               
                             }).FirstOrDefaultAsync();
                if (newOutput!= null)
                {

                var distinctItems = await _context.PDOButiranPermohonanSkimGred
                    .Where(x => x.IdButiranPermohonan == IdButiranPermohonan)
                    .GroupBy(x => new
                    {
                        x.IdButiranPermohonan,
                        x.IdSkimPerkhidmatan,
                        x.KodBidangPengkhususan,
                        x.KodRujLaluanKemajuanKerjaya,
                        x.IdKetuaPerkhidmatan
                    })
                    .Select(g => g.First())
                    .ToListAsync();
                var ButiranPermohonanSkimGredList = new List<ButiranPermohonanSkimGredTableDto>();
                foreach (var item in distinctItems)
                {
                    var GredList = (from pdopsg in _context.PDOButiranPermohonanSkimGred
                                    where pdopsg.IdButiranPermohonan == item.IdButiranPermohonan
                                       && pdopsg.IdSkimPerkhidmatan == item.IdSkimPerkhidmatan
                                       && pdopsg.KodBidangPengkhususan == item.KodBidangPengkhususan
                                       && pdopsg.KodRujLaluanKemajuanKerjaya == item.KodRujLaluanKemajuanKerjaya
                                       && pdopsg.IdKetuaPerkhidmatan == item.IdKetuaPerkhidmatan
                                    select pdopsg.IdGred).ToList();
                    var skimPerkhidmatan = await (from pdosp in _context.PDOSkimPerkhidmatan
                                                  where pdosp.Id == item.IdSkimPerkhidmatan
                                                  select pdosp).FirstOrDefaultAsync();
                    newOutput.IdKumpulanPerkhidmatan = skimPerkhidmatan.IdKumpulanPerkhidmatan;
                    newOutput.KodRujJenisSaraan = skimPerkhidmatan.KodRujJenisSaraan;
                    var bpsg = new ButiranPermohonanSkimGredTableDto
                    {
                        IdButiranPermohonan = item.IdButiranPermohonan,
                        IdSkimPerkhidmatan = item.IdSkimPerkhidmatan,
                        IdKlasifikasiPerkhidmatan = skimPerkhidmatan.IdKlasifikasiPerkhidmatan,
                        GredList = GredList,
                        IdGred = item.IdGred, // This will hold one of the gred values, you can modify as needed
                        KodRujLaluanKemajuanKerjaya = item.KodRujLaluanKemajuanKerjaya,
                        KodBidangPengkhususan = item.KodBidangPengkhususan,
                        IdKetuaPerkhidmatan = item.IdKetuaPerkhidmatan
                    };
                    ButiranPermohonanSkimGredList.Add(bpsg);
                }

                    newOutput.ButiranPermohonanSkimGredList = ButiranPermohonanSkimGredList;

                    foreach (var item in newOutput.ButiranPermohonanSkimGredList)
                    {
                        var additionalItems = await (from pdosp in _context.PDOSkimPerkhidmatan
                                                         where pdosp.Id == item.IdSkimPerkhidmatan
                                                         select pdosp).FirstOrDefaultAsync();
                        newOutput.IdKumpulanPerkhidmatan = additionalItems.IdKumpulanPerkhidmatan;
                        newOutput.KodRujJenisSaraan = additionalItems.KodRujJenisSaraan;
                    }

                    newOutput.ButiranPermohonanSkimGredKUJList = await (from pdobpsgkuj in _context.PDOButiranPermohonanSkimGredKUJ
                                                                 join pdosp in _context.PDOSkimPerkhidmatan on pdobpsgkuj.IdSkim equals pdosp.Id
                                                                 where pdobpsgkuj.IdButiranPermohonan == IdButiranPermohonan
                                                                select new ButiranPermohonanSkimGredKUJTableDto
                                                                {
                                                                    IdButiranPermohonan = pdobpsgkuj.IdButiranPermohonan,
                                                                    IdKlasifikasiPerkhidmatan = pdosp.IdKlasifikasiPerkhidmatan,
                                                                    IdSkimPerkhidmatan = pdobpsgkuj.IdSkim,
                                                                    IdGred = pdobpsgkuj.IdGred,
                                                                }).ToListAsync();
                    newOutput.ButiranPermohonanSkimGredTBKList = await (from pdobpsgtbk in _context.PDOButiranPermohonanSkimGredTBK
                                                                    join pdosp in _context.PDOSkimPerkhidmatan on pdobpsgtbk.IdSkimPerkhidmatan equals pdosp.Id
                                                                    where pdobpsgtbk.IdButiranPermohonan == IdButiranPermohonan
                                                                    select new ButiranPermohonanSkimGredTBKTableDto
                                                                    {
                                                                        IdButiranPermohonan = pdobpsgtbk.IdButiranPermohonan,
                                                                        IdKlasifikasiPerkhidmatan = pdosp.IdKlasifikasiPerkhidmatan,
                                                                        IdSkimPerkhidmatan = pdobpsgtbk.IdSkimPerkhidmatan,
                                                                        IdGred = pdobpsgtbk.IdGred,
                                                                    }).ToListAsync();
                #region Backup maybe needed later
                    //newOutput.GelaranjawatanList = await _rujGelaranJawatanExt.RujukanGelaranJawatan();
                    //newOutput.StatusJawatanList = await _rujstatusjawatanExt.RujukanStatusJawatan();
                    //newOutput.JenisJawatanList = await _rujjenisjawatanExt.RujukanJenisJawatan();
                    //newOutput.KumpulanPerkhidmatanList = await _kumpulanperkhidmatanext.RujukanKumpulanPerkhidmatan();
                    //newOutput.KlasifikasiPerkhidmatanList= await _klasifikasiperkhidmatanext.RujukanKlasifikasiPerKhidmatan();
                    //newOutput.pangkatList = await _rujpangkatbadanberuniformext.RujukanPangkat();
                #endregion Backup maybe needed later
            }
            else
                {
                    newOutput = new ButiranPermohonanLoadOutputDto();
                #region Backup maybe needed later
                //newOutput.GelaranjawatanList = await _rujGelaranJawatanExt.RujukanGelaranJawatan();
                //newOutput.StatusJawatanList = await _rujstatusjawatanExt.RujukanStatusJawatan();
                //newOutput.JenisJawatanList = await _rujjenisjawatanExt.RujukanJenisJawatan();
                //newOutput.KumpulanPerkhidmatanList = await _kumpulanperkhidmatanext.RujukanKumpulanPerkhidmatan();
                //newOutput.KlasifikasiPerkhidmatanList = await _klasifikasiperkhidmatanext.RujukanKlasifikasiPerKhidmatan();
                //newOutput.pangkatList = await _rujpangkatbadanberuniformext.RujukanPangkat();
                #endregion Backup maybe needed later
            }

            return newOutput;
        }
        public async Task PindahButiranPermohonan(PindahButiranPermohonanRequestDto request)
        {
            if (request.IdOldAktivitiOrganisasi == request.IdNewAktivitiOrganisasi)
            {
                throw new Exception("Aktiviti Organisasi Lama dan Baru tidak boleh sama.");
            }

            try {
                await _unitOfWork.BeginTransactionAsync();
                var query = (from pdobp in _context.PDOButiranPermohonan
                             where pdobp.IdPermohonanJawatan == request.IdPermohonanJawatan
                             && pdobp.Id == request.IdButiranPermohonan
                             && pdobp.IdAktivitiOrganisasi == request.IdOldAktivitiOrganisasi
                             select pdobp);

                Console.WriteLine(query.ToQueryString()); // EF Core 5+
                var record = await query.FirstOrDefaultAsync();

                var butiranKemaskiniList = new List<ButiranKemaskiniDto>();

                if (record.ButiranKemaskini != null)
                {
                    butiranKemaskiniList = JsonSerializer.Deserialize<List<ButiranKemaskiniDto>>(record.ButiranKemaskini);
                }
                var newRecord = _mapper.Map<PDOButiranPermohonan>(record);
                var butiranKemaskini = new ButiranKemaskiniDto();
                butiranKemaskini.IdAktivitiOrganisasi = request.IdNewAktivitiOrganisasi;
                butiranKemaskini.IdOldAktivitiOrganisasi = request.IdOldAktivitiOrganisasi;
                butiranKemaskini.IdPermohonanJawatan = request.IdPermohonanJawatan;
                butiranKemaskini.IdButiranPermohonan = request.IdButiranPermohonan;
                butiranKemaskiniList.Add(butiranKemaskini);

                var json = JsonSerializer.Serialize(butiranKemaskiniList);
                newRecord.Id = 0;
                newRecord.ButirPerubahan = json;
                newRecord.IndikatorRekod = 5; //Pindah Butiran
                newRecord.IdPinda = request.UserId;
                newRecord.TarikhPinda = DateTime.Now;
                newRecord.ButiranKemaskini = json;
                _context.PDOButiranPermohonan.Add(newRecord);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            } catch (Exception ex) 
            { 
                Console.WriteLine(ex.ToString());
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.ToString());
                }
            }
            //return record;
        }
        public async Task<TambahButiranPermohonanDto> BacaRekodButiranPermohonan(int? Id)
        {

            try

            {
                var result = (from pdobp in _context.PDOButiranPermohonan
                              join pdobps in _context.PDOButiranPermohonanSkimGred on pdobp.Id equals pdobps.IdButiranPermohonan
                              join pdosp in _context.PDOSkimPerkhidmatan on pdobps.IdSkimPerkhidmatan equals pdosp.Id
                              where pdobp.Id == Id
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
                                  //AnggaranTajukJawatan = pdobp.AnggaranTajukJawatan,
                                  //JumlahKosSebulan = pdobp.JumlahKosSebulan,
                                  //TahunButiran = pdobp.TahunButiran
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
        public async Task<TambahButiranPermohonanDto> BacaButiranPermohonan(int? IdPermohonanJawatan)
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
                                  //AnggaranTajukJawatan = pdobp.AnggaranTajukJawatan,
                                  //JumlahKosSebulan = pdobp.JumlahKosSebulan,
                                  //TahunButiran = pdobp.TahunButiran
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
        public async Task<List<ButiranPermohonanIkutAktivitiOrganisasiDto>> ButiranPermohonanIkutAktivitiOrganisasi(int IdPermohonanJawatan)
        {
            var query =     from pdopao in _context.PDOAktivitiOrganisasi
                            join prkao in _context.PDORujKategoriAktivitiOrganisasi
                                on pdopao.KodRujKategoriAktivitiOrganisasi equals prkao.Kod
                            where (from pdobp in _context.PDOButiranPermohonan
                                   where pdobp.IdPermohonanJawatan == IdPermohonanJawatan
                                   select pdobp.IdAktivitiOrganisasi)
                                  .Contains(pdopao.Id)
                            select new ButiranPermohonanIkutAktivitiOrganisasiDto
                            {
                                NamaKategoriDanAktivitiOrganisasi = prkao.Nama + "-" + pdopao.Nama
                            };

            var AktivitiOrganisasiList = await query.ToListAsync();
            foreach (var item in AktivitiOrganisasiList)
            {
                var butiranPermohonan = await (from pdobp in _context.PDOButiranPermohonan
                                               join pdobpsg in _context.PDOButiranPermohonanSkimGred on pdobp.Id equals pdobpsg.IdButiranPermohonan
                                               join pdosp in _context.PDOSkimPerkhidmatan on pdobpsg.IdSkimPerkhidmatan equals pdosp.Id
                                               join pdog in _context.PDOGred on pdobpsg.IdGred equals pdog.Id
                                               where pdobp.IdPermohonanJawatan == IdPermohonanJawatan
                                               select new ButiranPermohonanSemakDrafWPSKP
                                               {
                                                   NoButiran = pdobp.NoButiran,
                                                   TajukButiran = pdobp.AnggaranTajukJawatan,
                                                   KodGaji = pdog.Nama,
                                                   KodSkim = pdosp.Kod,
                                                   BilanganJawatan = pdobp.BilanganJawatan,
                                                   ButirPerubahan = pdobp.ButirPerubahan

                                               }).ToListAsync();
                item.ButiranPermohonan = butiranPermohonan;
            }

            return AktivitiOrganisasiList;
        }

    }


}

