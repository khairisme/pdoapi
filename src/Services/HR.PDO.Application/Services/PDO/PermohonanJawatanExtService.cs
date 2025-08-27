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
    public class PermohonanJawatanExtService : IPermohonanJawatanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<PermohonanJawatanExtService> _logger;

        public PermohonanJawatanExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<PermohonanJawatanExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task TambahPermohonanJawatanBaru(Guid UserId, string? NomborRujukan, string? Tajuk, string? Keterangan, string? KodRujJenisPermohonan)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var entity = new PDOPermohonanJawatan();
                entity.NomborRujukan = NomborRujukan;
                entity.Tajuk = Tajuk;
                entity.Keterangan = Keterangan;
                entity.KodRujJenisPermohonan = KodRujJenisPermohonan;
                entity.KodRujJenisPermohonan = KodRujJenisPermohonan;
                entity.KodRujJenisPermohonan = KodRujJenisPermohonan;
                entity.KodRujJenisPermohonan = KodRujJenisPermohonan;
                entity.KodRujJenisPermohonan = KodRujJenisPermohonan;
                entity.IdCipta = UserId;
                entity.TarikhCipta = DateTime.Now;
                await _context.PDOPermohonanJawatan.AddAsync(entity); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in TambahPermohonanJawatanBaru");

                throw;
            }

        }



        public async Task<BacaPermohonanJawatanDto> BacaPermohonanJawatan(int Id)
        {
            try

            {

                var result = await (from pdopj in _context.PDOPermohonanJawatan
                    where pdopj.Id == Id
                    select new BacaPermohonanJawatanDto{
                         Id = pdopj.Id
                    }
                ).FirstOrDefaultAsync();
                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in BacaPermohonanJawatan");

                throw;
            }

        }



        public async Task HapusTerusPermohonanJawatan(Guid UserId, int Id)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var entity = await (from pdopj in _context.PDOPermohonanJawatan
                             where pdopj.Id == Id select pdopj
                              ).FirstOrDefaultAsync();
                if (entity == null)
                {
                    throw new Exception("PDOPermohonanJawatan not found.");
                }
                _context.PDOPermohonanJawatan.Remove(entity);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in HapusTerusPermohonanJawatan");

                throw;
            }

        }



        public async Task KemaskiniPermohonanJawatan(Guid UserId, int Id, PermohonanJawatanDaftarDto request)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var data = await (from pdopj in _context.PDOPermohonanJawatan
                             where pdopj.Id == Id
                             select pdopj).FirstOrDefaultAsync();
                  data.IdUnitOrganisasi = request.IdUnitOrganisasi;
                  data.IdAgensi = request.IdAgensi;
                  data.NomborRujukan = request.NomborRujukan;
                  data.Tajuk = request.Tajuk;
                  data.Keterangan = request.Keterangan;
                  data.KodRujJenisPermohonan = request.KodRujJenisPermohonan;
                  data.StatusAktif = request.StatusAktif;
                  data.IdCipta = request.IdCipta;
                  data.TarikhCipta = DateTime.Now;

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in KemaskiniPermohonanJawatan");

                throw;
            }

        }



        public async Task<List<PermohonanJawatanCarianDto>> CarianSenaraiPermohonanJawatan(CarianSenaraiPermohonanJawatanRequestDto filter)
        {
            try

            {

                var result = await (from pdopj in _context.PDOPermohonanJawatan
                    join pdospj in _context.PDOStatusPermohonanJawatan on pdopj.Id equals pdospj.IdPermohonanJawatan
                    join pdouo in _context.PDOUnitOrganisasi on pdopj.IdUnitOrganisasi equals pdouo.Id
                    join pdorjp in _context.PDORujJenisPermohonan on pdopj.KodRujJenisPermohonan equals pdorjp.Kod
                    join pdorspj in _context.PDORujStatusPermohonanJawatan on pdospj.KodRujStatusPermohonanJawatan equals pdorspj.Kod
                    where pdouo.IndikatorAgensi == true && pdouo.StatusAktif == true && pdorspj.Kod == filter.KodRujStatusPermohonanJawatan
                    select new PermohonanJawatanCarianDto{
                         AgensiId = pdopj.IdUnitOrganisasi,
                         IdUnitOrganisasi = pdopj.IdUnitOrganisasi,
                         KodRujJenisPermohonan = pdopj.KodRujJenisPermohonan,
                         KodRujStatusPermohonanJawatan = pdorspj.Kod,
                         NomborRujukan = pdopj.NomborRujukan,
                         TajukPermohonan = pdopj.Tajuk
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in CarianSenaraiPermohonanJawatan");

                throw;
            }

        }



        public async Task<List<SenaraiPermohonanJawatanDto>> SenaraiPermohonanJawatanCarianAgensiNoRujukanTajukStatus(string KodAgensi, string NoRujukan, string TajukPermohonan, string KodRujStatusPermohonanJawatan)
        {
            try

            {

                var result = await (from pdopj in _context.PDOPermohonanJawatan
                    join pdospj in _context.PDOStatusPermohonanJawatan on pdopj.Id equals pdospj.IdPermohonanJawatan
                    join pdouo in _context.PDOUnitOrganisasi on pdopj.IdUnitOrganisasi equals pdouo.Id
                    join pdorjp in _context.PDORujJenisPermohonan on pdopj.KodRujJenisPermohonan equals pdorjp.Kod
                    join pdorspj in _context.PDORujStatusPermohonanJawatan on pdospj.KodRujStatusPermohonanJawatan equals pdorspj.Kod
                    where (pdouo.Kod == KodAgensi || KodAgensi == null)
                    && (pdorspj.Kod == KodRujStatusPermohonanJawatan || KodRujStatusPermohonanJawatan == null)
                    && (pdopj.NomborRujukan == NoRujukan || NoRujukan == null)
                    && (pdopj.Tajuk == TajukPermohonan || TajukPermohonan == null)
                    select new SenaraiPermohonanJawatanDto{
                         NomborRujukan = pdopj.NomborRujukan,
                         Status = pdorspj.Nama,
                         TarikhPermohonan = pdopj.TarikhPermohonan,
                         Agensi = pdouo.Nama,
                         Id = pdopj.Id,
                         JenisPermohonan = pdorjp.Nama,
                         TajukPermohonan = pdopj.Tajuk
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiPermohonanJawatanCarianAgensiNoRujukanTajukStatus");

                throw;
            }

        }



        public async Task<List<SenaraiPermohonanJawatanDto>> SenaraiPermohonanJawatanCarianNoRujukanJenisTajukStatus(string NoRujukan, string KodRujJenisPermohonan, string TajukPermohonan, string KodRujStatusPermohonanJawatan)
        {
            try

            {

                var result = await (from pdopj in _context.PDOPermohonanJawatan
                    join pdospj in _context.PDOStatusPermohonanJawatan on pdopj.Id equals pdospj.IdPermohonanJawatan
                    join pdouo in _context.PDOUnitOrganisasi on pdopj.IdUnitOrganisasi equals pdouo.Id
                    join pdorjp in _context.PDORujJenisPermohonan on pdopj.KodRujJenisPermohonan equals pdorjp.Kod
                    join pdorspj in _context.PDORujStatusPermohonanJawatan on pdospj.KodRujStatusPermohonanJawatan equals pdorspj.Kod
                    where (pdouo.Kod == KodRujJenisPermohonan || KodRujJenisPermohonan == null)
                    && (pdorspj.Kod == KodRujStatusPermohonanJawatan || KodRujStatusPermohonanJawatan == null)
                    && (pdopj.NomborRujukan == NoRujukan || NoRujukan == null)
                    && (pdopj.Tajuk == TajukPermohonan || TajukPermohonan == null)
                    select new SenaraiPermohonanJawatanDto{
                         NomborRujukan = pdopj.NomborRujukan,
                         Status = pdorspj.Nama,
                         TarikhPermohonan = pdopj.TarikhPermohonan,
                         Agensi = pdouo.Nama,
                         Id = pdopj.Id,
                         JenisPermohonan = pdorjp.Nama,
                         TajukPermohonan = pdopj.Tajuk
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiPermohonanJawatanCarianNoRujukanJenisTajukStatus");

                throw;
            }

        }



        public async Task<List<PermohonanJawatanLinkDto>> CarianPermohonanJawatan(int Id, PermohonanJawatanCarianDto request)
        {
            try

            {

                var result = await (from pdopj in _context.PDOPermohonanJawatan
                    join pdobp in _context. PDOButiranPermohonan  on pdopj.Id equals pdobp.IdPermohonanJawatan 
                    join pdospj in _context.PDOStatusPermohonanJawatan on pdopj.Id equals pdospj.IdPermohonanJawatan
                    join pdouo in _context.PDOUnitOrganisasi on pdopj.IdUnitOrganisasi equals pdouo.Id
                    join pdobpj  in _context. PDOButiranPermohonanJawatan  on pdobp.Id equals pdobpj.IdButiranPermohonan 
join pdorspj in _context.PDORujStatusPermohonanJawatan on pdospj.KodRujStatusPermohonanJawatan equals pdorspj.Kod
                    join pdobps  in _context.PDOButiranPermohonanSkimGred  on pdobp.Id equals pdobps.IdButiranPermohonan 
join pdorjp in _context.PDORujJenisPermohonan on pdopj.KodRujJenisPermohonan equals pdorjp.Kod
                    join pdosp in _context.PDOSkimPerkhidmatan  on pdobps.IdSkimPerkhidmatan equals  pdosp.Id
join pdorpp in _context.PDORujPasukanPerunding  on pdopj.KodRujPasukanPerunding equals pdorpp.Kod
                    join pdokp   in _context.PDOKlasifikasiPerkhidmatan  on pdosp.IdKlasifikasiPerkhidmatan equals pdokp.Id
                    join pdog   in _context.PDOGred  on new { GredId = pdobps.IdGred, KpId = pdokp.Id }
  equals new { GredId = pdog.Id,    KpId = pdog.IdKlasifikasiPerkhidmatan }
                    join pdoskp in _context.PDOSkimKetuaPerkhidmatan  on pdosp.Id equals pdoskp.IdSkimPerkhidmatan
                    join pdoj  in _context.PDOJawatan  on pdoskp.IdKetuaPerkhidmatan equals pdoj.Id
                    join pdopik in _context.PDOPenetapanImplikasiKewangan  on new { GredId = pdog.Id, SkimId = pdosp.Id }  equals new { GredId = pdopik.IdGred, SkimId = pdopik.IdSkimPerkhidmatan }
                    where (pdopj.IdUnitOrganisasi == request.IdUnitOrganisasi || request.IdUnitOrganisasi == null) && (pdopj.NomborRujukan == request.NomborRujukan || request.NomborRujukan == null) && (pdopj.Tajuk == request.TajukPermohonan || request.TajukPermohonan == null) && (pdorspj.Kod == request.KodRujStatusPermohonanJawatan || request.KodRujStatusPermohonanJawatan == null) && (pdorjp.Kod == request.KodRujJenisPermohonan || request.KodRujJenisPermohonan == null)
                    select new PermohonanJawatanLinkDto{
                         Agensi = pdouo.Nama,
                         Id = pdopj.Id,
                         IdAgensi = pdopj.IdAgensi,
                         IdCipta = pdopj.IdCipta,
                         IdHapus = pdopj.IdHapus,
                         IdPinda = pdopj.IdPinda,
                         IdUnitOrganisasi = pdopj.IdUnitOrganisasi,
                         JenisPermohonan = pdorjp.Nama,
                         Keterangan = pdopj.Keterangan,
                         NomborRujukan = pdopj.NomborRujukan,
                         NoWaranPerjawatan = pdopj.NoWaranPerjawatan,
                         PasukanPerunding = pdorpp.Nama,
                         Tajuk = pdopj.Tajuk,
                         TarikhCadanganWaran = pdopj.TarikhCadanganWaran,
                         TarikhCipta = pdopj.TarikhCipta,
                         TarikhHapus = pdopj.TarikhHapus,
                         TarikhPermohonan = pdopj.TarikhPermohonan,
                         TarikhPinda = pdopj.TarikhPinda,
                         TarikhWaranDiluluskan = pdopj.TarikhWaranDiluluskan
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in CarianPermohonanJawatan");

                throw;
            }

        }



        public async Task<List<PermohonanJawatanLinkDto>> SenaraiPermohonanJawatan(PermohonanJawatanCarianDto request)
        {
            try

            {

                var result = await (from pdopj in _context.PDOPermohonanJawatan
                    join pdouo in _context.PDOUnitOrganisasi on pdopj.IdUnitOrganisasi equals pdouo.Id
                    join pdorjp in _context.PDORujJenisPermohonan on pdopj.KodRujJenisPermohonan equals pdorjp.Kod
                    join pdorpp in _context.PDORujPasukanPerunding on pdopj.KodRujPasukanPerunding equals pdorpp.Kod
                    select new PermohonanJawatanLinkDto{
                         Agensi = pdouo.Nama,
                         Id = pdopj.Id,
                         IdAgensi = pdopj.IdAgensi,
                         IdCipta = pdopj.IdCipta,
                         IdHapus = pdopj.IdHapus,
                         IdPinda = pdopj.IdPinda,
                         IdUnitOrganisasi = pdopj.IdUnitOrganisasi,
                         JenisPermohonan = pdorjp.Nama,
                         Keterangan = pdopj.Keterangan,
                         NomborRujukan = pdopj.NomborRujukan,
                         NoWaranPerjawatan = pdopj.NoWaranPerjawatan,
                         PasukanPerunding = pdorpp.Nama,
                         Tajuk = pdopj.Tajuk,
                         TarikhCadanganWaran = pdopj.TarikhCadanganWaran,
                         TarikhCipta = pdopj.TarikhCipta,
                         TarikhHapus = pdopj.TarikhHapus,
                         TarikhPermohonan = pdopj.TarikhPermohonan,
                         TarikhPinda = pdopj.TarikhPinda,
                         TarikhWaranDiluluskan = pdopj.TarikhWaranDiluluskan
                    }
                ).ToListAsync();

                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiPermohonanJawatan");

                throw;
            }

        }



        public async Task DaftarPermohonanJawatan(Guid UserId, PermohonanJawatanDaftarDto request)
        {
            await _unitOfWork.BeginTransactionAsync();

            try

            {

                var entity = new PDOPermohonanJawatan();
                entity.IdCipta = UserId;
                entity.IdUnitOrganisasi = request.IdUnitOrganisasi;
                entity.IdAgensi = request.IdAgensi;
                entity.NomborRujukan = request.NomborRujukan;
                entity.Tajuk = request.Tajuk;
                entity.Keterangan = request.Keterangan;
                entity.KodRujJenisPermohonan = request.KodRujJenisPermohonan;
                entity.StatusAktif = request.StatusAktif;
                entity.IdCipta = request.IdCipta;
                entity.TarikhCipta = DateTime.Now;
                await _context.PDOPermohonanJawatan.AddAsync(entity); 
                await _context.SaveChangesAsync(); 

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in DaftarPermohonanJawatan");

                throw;
            }

        }



    }

}

