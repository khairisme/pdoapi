using HR.PDO.Application.DTOs;
using HR.PDO.Application.Interfaces;
using HR.PDO.Core.Entities;
using HR.PDO.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.Services
{
    public class PenggunaService : IPenggunaService
    {
        private readonly IPNSUnitOfWork _unitOfWork;
        private readonly ILogger<PenggunaService> _logger;

        public PenggunaService(IPNSUnitOfWork unitOfWork, ILogger<PenggunaService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<string> mengesahkankelayakanpengguna(string userId, string temporary_password)
        {
            try
            {
                _logger.LogInformation("Getting User by User ID {IdPengguna} using Entity Framework", userId);
                var repository = _unitOfWork.Repository<Pengguna>();
                var users = await repository.FindByFieldAsync("IdPengguna", userId);
                if (users != null && users.Where(x => x.KataLaluanSementara == temporary_password).Count() > 0)
                {
                    return "Sucess";
                }
                return "Failed";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Validating new user using Entity Framework");
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }

        public async Task<string> tetapkankatalaluan(string userId, string password)
        {
            try
            {
                _logger.LogInformation("Getting User by User ID {IdPengguna} using Entity Framework", userId);
                var repository = _unitOfWork.Repository<Pengguna>();
                var users = await repository.FindByFieldAsync("IdPengguna", userId);
                var user = users.FirstOrDefault();
                if (user != null)
                {
                    user.KataLaluanHash = password;

                    var result = await _unitOfWork.Repository<Pengguna>().UpdateAsync(user);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();
                    return "Sucess";

                }
                return "Failed";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error set user password using Entity Framework");
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }

        public async Task<IEnumerable<SoalanKeselamatanDto>> CreateSoalanKeselamatanAsync(List<SoalanKeselamatanDto> soalanKeselamatanDto)
        {
            _logger.LogInformation("Creating user security question using Entity Framework");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                List<PNSSoalanKeselamatan> _SoalanKeselamatans = MapToEntityList<SoalanKeselamatanDto, PNSSoalanKeselamatan>(soalanKeselamatanDto, MapUserSecurityQuestions);
                var soalanKeselamatans = await _unitOfWork.Repository<PNSSoalanKeselamatan>().AddRangeAsync(_SoalanKeselamatans);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
                return soalanKeselamatans.Select(MapUserSQEntityToDto);
               

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Creating user security question using Entity Framework");
                await _unitOfWork.RollbackAsync();
                throw;
            }



        }

        public async Task<IEnumerable<GambarDto>> GetAllGambarAsync()
        {
            _logger.LogInformation("Getting all Gambar using Entity Framework");
            var images = await _unitOfWork.Repository<PNSGambar>().GetAllAsync();
            return images.Select(MapToImageDto);
        }
        public async Task<string> CreatePerkhidmatanAsync(PerkhidmatanLogDto perkhidmatanLog)
        {
            _logger.LogInformation("Creating a PerkhidmatanLogDto using Entity Framework");
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var perkhidmatan = MapPerkhidmatanLogToEntity(perkhidmatanLog);
                perkhidmatan = await _unitOfWork.Repository<PerkhidmatanLog>().AddAsync(perkhidmatan);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
                return "Sucess";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating PerkhidmatanLogDto using Entity Framework");
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
        private PerkhidmatanLog MapPerkhidmatanLogToEntity(PerkhidmatanLogDto dto)
        {
            byte[] varbinaryData = Encoding.UTF8.GetBytes(dto.Gambar);
            return new PerkhidmatanLog
            {
                IdPengguna = dto.IdPengguna,
                Gambar = varbinaryData,
                KataLaluan = dto.KataLaluan
            };


        }


        public List<TEntity> MapToEntityList<TDto, TEntity>(IEnumerable<TDto> dtoList, Func<TDto, TEntity> mapFunc)
        {
            return dtoList.Select(mapFunc).ToList();
        }
        private PNSSoalanKeselamatan MapUserSecurityQuestions(SoalanKeselamatanDto dto)
        {
            return new PNSSoalanKeselamatan
            {
                IdPengguna = dto.IdPengguna,
                KodRujSoalanKeselamatan = dto.KodRujSoalanKeselamatan,
                JawapanSoalan = dto.JawapanSoalan
            };
        }
        private static SoalanKeselamatanDto MapUserSQEntityToDto(PNSSoalanKeselamatan soalanKeselamatan)
        {
            return new SoalanKeselamatanDto
            {
                IdPengguna = soalanKeselamatan.IdPengguna,
                JawapanSoalan= soalanKeselamatan.JawapanSoalan,
                KodRujSoalanKeselamatan=soalanKeselamatan.KodRujSoalanKeselamatan
            };
        }
        private GambarDto MapToImageDto(PNSGambar gambar)
        {
            return new GambarDto
            {
                Id = gambar.Id,
                Lokasi = gambar.Lokasi
            };
        }


    }
}
