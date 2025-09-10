using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Core.Interfaces;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.PDO.Application.Services.PDO;
using HR.PDO.Core.Entities.PDO;
using HR.PDO.Application.DTOs;

namespace HR.Application.Services.PDO
{
    public class JawatanExtService : IJawatanExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<JawatanExtService> _logger;
        private readonly ISandanganExt _sadanganExt;
        private readonly IProfilPemilikKompetensiExt _profilPemilikKompetensiExt;

        public JawatanExtService(IProfilPemilikKompetensiExt profilPemilikKompetensiExt, ISandanganExt sadanganExt, IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<JawatanExtService> logger)
        {
            _profilPemilikKompetensiExt = profilPemilikKompetensiExt;
            _sadanganExt = sadanganExt;
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }



        public async Task<List<JawatanLinkDto>> SenaraiJawatan(ButiranJawatanRequestDto request)
        {
            /*
             * Created By: Khairi Abu Bakar
             * Date: 30/08/2025
             * Purpose: To get all Jawatan records
             */
            try

            {


                var JawatanList = await (from pdopj in _context.PDOPermohonanJawatan
                                    join pdobp in _context.PDOButiranPermohonan on pdopj.Id equals pdobp.IdPermohonanJawatan
                                    join pdobpj in _context.PDOButiranPermohonanJawatan on pdobp.Id equals pdobpj.IdButiranPermohonan
                                    join pdoj in _context.PDOJawatan on pdobpj.IdJawatan equals pdoj.Id
                                    join pdouo in _context.PDOUnitOrganisasi on pdopj.IdUnitOrganisasi equals pdouo.Id
                                    join pdorjp in _context.PDORujJenisPermohonan on pdopj.KodRujJenisPermohonan equals pdorjp.Kod
                                    //join pdorpp in _context.PDORujPasukanPerunding on pdopj.KodRujPasukanPerunding equals pdorpp.Kod
                                    where pdopj.Id == request.IdPermohonanJawatan
                                    
                                    select new JawatanLinkDto
                                    {
                                        IdJawatan = pdoj.Id,
                                        KodJawatan = pdoj.Kod.Trim(),
                                        NamaJawatan = pdoj.Nama.Trim(),
                                        UnitOrganisasi = pdouo.Nama.Trim(),
                                    }
                                    ).ToListAsync();
                var filterJawatanList = new JawatanListDto();
                filterJawatanList.IdJawatanList = (from j in JawatanList
                                                    select (int?)j.IdJawatan).ToList();

                var ProfilePemilikKompetensiList = await _profilPemilikKompetensiExt.GetExternalSenaraiProfilPemilikKompetensiAsync(filterJawatanList);

                var SandanganList = await _sadanganExt.GetSandanganAsync(filterJawatanList);

                var result = (from j in JawatanList
                              join s in SandanganList on j.IdJawatan equals s.IdJawatan
                              join ppk in ProfilePemilikKompetensiList on s.IdPemilikKompetensi equals ppk.IdPemilikKompetensi  
                              select new JawatanLinkDto
                              {
                                  IdJawatan = j.IdJawatan,
                                  KodJawatan = j.KodJawatan,
                                  NamaJawatan = j.NamaJawatan.Trim(),
                                  UnitOrganisasi = j.UnitOrganisasi,
                                  NamaPenyandang = ppk.NamaPemilikKompetensi.Trim()
                              }).ToList(); // in-memory, no ToListAsync


                return result;

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in SenaraiPermohonanJawatan");

                throw;
            }

        }



        public async Task DaftarPermohonanJawatan(PermohonanJawatanDaftarDto request)
        {

            try

            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOPermohonanJawatan();
                entity.IdCipta = request.UserId;
                entity.IdUnitOrganisasi = request.IdAgensi;
                entity.IdAgensi = request.IdAgensi;
                entity.NomborRujukan = request.NomborRujukan;
                entity.Tajuk = request.Tajuk;
                entity.Keterangan = request.Keterangan;
                entity.KodRujJenisPermohonan = request.KodRujJenisPermohonan;
                entity.IdCipta = request.UserId;
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

        public async Task KemaskiniUlasanPermohonanJawatan(UlasanRequestDto request)
        {

            try

            {
                var recordPermohonan = (from pdopj in _context.PDOPermohonanJawatan
                              where pdopj.Id == request.IdPermohonanJawatan
                              select pdopj).FirstOrDefault();


                var record = (from pdospj in _context.PDOStatusPermohonanJawatan
                              where pdospj.IdPermohonanJawatan == request.IdPermohonanJawatan
                              && pdospj.StatusAktif == true
                              select pdospj).FirstOrDefault();

                if (record != null)
                {
                    await _unitOfWork.BeginTransactionAsync();
                    record.StatusAktif = false;
                    record.IdHapus = request.UserId;
                    record.TarikhHapus = DateTime.Now;
                    _context.PDOStatusPermohonanJawatan.Update(record);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();
                }

                var newStatusPermohonanJawatan = new PDOStatusPermohonanJawatan()
                {
                    IdPermohonanJawatan = request.IdPermohonanJawatan,
                    TarikhStatusPermohonan = DateTime.Now,
                    Ulasan = request.Ulasan,
                    IdCipta = request.UserId,
                    TarikhCipta = DateTime.Now,
                    KodRujStatusPermohonanJawatan = request.KodRujStatusPermohonanJawatan,
                    StatusAktif = true
                };

                await _unitOfWork.BeginTransactionAsync();

                await _context.PDOStatusPermohonanJawatan.AddAsync(newStatusPermohonanJawatan);

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

