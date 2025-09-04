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
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Azure.Core;

namespace HR.Application.Services.PDO
{
    public class AktivitiOrganisasiExtService : IAktivitiOrganisasiExt
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<AktivitiOrganisasiExtService> _logger;

        public AktivitiOrganisasiExtService(IPDOUnitOfWork unitOfWork, PDODbContext dbContext, ILogger<AktivitiOrganisasiExtService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        public async Task<PagedResult<StrukturAktivitiOrganisasiDto>> StrukturAktivitiOrganisasi(StrukturAktivitiOrganisasiRequestDto request)
        {
            try
            {
                //string? KodCartaAktiviti, int parentId = 0, int page = 1, int pageSize = 50, string? keyword = null, string? sortBy = "AktivitiOrganisasi", bool desc = false, CancellationToken ct = default
                int? newParentId = 0;
                bool rootparent = false;
                request.page = request.page <= 0 ? 1 : request.page;
                request.pageSize = request.pageSize <= 0 ? 50 : request.pageSize;
                var result = await (from pdoao in _context.PDOAktivitiOrganisasi
                    join pdorkao in _context.PDORujKategoriAktivitiOrganisasi  on pdoao.KodRujKategoriAktivitiOrganisasi equals pdorkao.Kod
                    where (request.parentId != 0 ? pdoao.IdIndukAktivitiOrganisasi == request.parentId : pdoao.KodCartaAktiviti == request.KodCartaAktiviti)
                    select new StrukturAktivitiOrganisasiDto{
                         AktivitiOrganisasi = pdoao.Nama,
                         Id = pdoao.Id,
                         IdIndukAktivitiOrganisasi = pdoao.IdIndukAktivitiOrganisasi,
                         Kod = pdoao.Kod,
                         KodCartaAktiviti = pdoao.KodCartaAktiviti,
                         KodProgram = pdorkao.Nama.ToUpper() + ' ' +pdoao.KodProgram,
                         Tahap = pdoao.Tahap

                    }
                ).ToListAsync();
                foreach (var item in result)
                {
                    item.Children = StrukturAktivitiOrganisasiGetChildren(item.Id, request).Result;
                    if (item.Children.Count() > 0) item.HasChildren = true;
                }

                var total = result.Count();
                var ordered = (request.sortBy ?? "AktivitiOrganisasi").Trim().ToLowerInvariant() switch

                {
                    "kod"     => request.desc ? result.OrderByDescending(x => x.Kod)     : result.OrderBy(x => x.Kod),
                    "aktivitiorganisasi"     => request.desc ? result.OrderByDescending(x => x.AktivitiOrganisasi)     : result.OrderBy(x => x.AktivitiOrganisasi),
                };


            var items = ordered
            .Skip((request.page - 1) * request.pageSize)        
            .Take(request.pageSize)        
            .ToList();                                                                                                                                                                                                                                                                            
                return new PagedResult<StrukturAktivitiOrganisasiDto>            
                {
                    Total = total,
                    Items = items   
                };
            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in StrukturAktivitiOrganisasi");

                throw;
            }

        }

        public async Task<List<StrukturAktivitiOrganisasiDto>> StrukturAktivitiOrganisasiGetParent(int? Id, List<StrukturAktivitiOrganisasiDto> child)
        {
            bool rootparent = false;
            int? newParentId = 0;
            var result = await (from pdoao in _context.PDOAktivitiOrganisasi
                                       join pdorkao in _context.PDORujKategoriAktivitiOrganisasi on pdoao.KodRujKategoriAktivitiOrganisasi equals pdorkao.Kod
                                       where pdoao.Id == Id
                                       select new StrukturAktivitiOrganisasiDto
                                        {
                                            Children = child,
                                            HasChildren = child.Count() > 0 ? true : false,
                                            Id = pdoao.Id,
                                            IdIndukAktivitiOrganisasi = pdoao.IdIndukAktivitiOrganisasi,
                                            KodCartaAktiviti = pdoao.KodCartaAktiviti,
                                            KodProgram = pdoao.KodProgram,
                                            Kod = pdoao.Kod,
                                            Tahap = pdoao.Tahap,
                                            AktivitiOrganisasi = pdoao.Nama

                                        }
                            ).ToListAsync();

            foreach (var item in result)
            {
                if (item.Id == item.IdIndukAktivitiOrganisasi)
                {
                    rootparent = true;
                }
                else
                {
                    newParentId = item.IdIndukAktivitiOrganisasi;
                }
            }
            if (rootparent)
            {
                return result;
            }
            else
            {
                return await StrukturAktivitiOrganisasiGetParent(newParentId, result);
            }
        }

        public async Task<List<StrukturAktivitiOrganisasiDto>> StrukturAktivitiOrganisasiGetChildren(int? Id, StrukturAktivitiOrganisasiRequestDto request)
        {
            bool rootparent = false;
            int newParentId = 0;
            var result = await (from pdoao in _context.PDOAktivitiOrganisasi
                                join pdorkao in _context.PDORujKategoriAktivitiOrganisasi on pdoao.KodRujKategoriAktivitiOrganisasi equals pdorkao.Kod
                                where pdoao.IdIndukAktivitiOrganisasi == Id //&& pdoao.KodCartaAktiviti.Contains(request.KodCartaAktiviti)
                                select new StrukturAktivitiOrganisasiDto
                                {
                                    HasChildren = false,
                                    Id = pdoao.Id,
                                    IdIndukAktivitiOrganisasi = pdoao.IdIndukAktivitiOrganisasi,
                                    KodCartaAktiviti = pdoao.KodCartaAktiviti,
                                    KodProgram = pdoao.Kod,
                                    Kod = pdoao.Kod,
                                    Tahap = pdoao.Tahap,
                                    AktivitiOrganisasi = pdoao.Nama

                                }
            ).ToListAsync();
            foreach (var item in result)
            {
                item.Children = await (from pdoao in _context.PDOAktivitiOrganisasi
                                      join pdorkao in _context.PDORujKategoriAktivitiOrganisasi on pdoao.KodRujKategoriAktivitiOrganisasi equals pdorkao.Kod
                                      where pdoao.IdIndukAktivitiOrganisasi == item.Id //&& pdoao.KodCartaAktiviti.Contains(request.KodCartaAktiviti)
                                       select new StrukturAktivitiOrganisasiDto
                                      {
                                          HasChildren = false,
                                          Id = pdoao.Id,
                                          IdIndukAktivitiOrganisasi = pdoao.IdIndukAktivitiOrganisasi,
                                          KodCartaAktiviti = pdoao.KodCartaAktiviti,
                                          KodProgram = pdoao.KodProgram,
                                          Kod = pdoao.Kod,
                                          Tahap = pdoao.Tahap,
                                          AktivitiOrganisasi = pdoao.Nama

                                      }
                                ).ToListAsync();
                foreach(var child in item.Children)
                {
                    var children = await (from pdoao in _context.PDOAktivitiOrganisasi
                                           join pdorkao in _context.PDORujKategoriAktivitiOrganisasi on pdoao.KodRujKategoriAktivitiOrganisasi equals pdorkao.Kod
                                           where pdoao.IdIndukAktivitiOrganisasi == child.Id //&& pdoao.KodCartaAktiviti.Contains(request.KodCartaAktiviti)
                                          select new StrukturAktivitiOrganisasiDto
                                           {
                                               HasChildren = false,
                                               Id = pdoao.Id,
                                               IdIndukAktivitiOrganisasi = pdoao.IdIndukAktivitiOrganisasi,
                                               KodCartaAktiviti = pdoao.KodCartaAktiviti,
                                               KodProgram = pdoao.KodProgram,
                                               Kod = pdoao.Kod,
                                               Tahap = pdoao.Tahap,
                                               AktivitiOrganisasi = pdoao.Nama

                                           }
                                    ).ToListAsync();
                    if (children.Count() > 0) child.HasChildren = true;
                }
                if (item.Children.Count() > 0) item.HasChildren = true;
            }

            return result;
        }


        /// <summary>
        /// Creates (wujud) a new Aktiviti Organisasi entity.
        /// </summary>
        /// <param name="request">
        /// DTO containing details such as <c>IdIndukAktivitiOrganisasi</c>, <c>KodProgram</c>,  
        /// <c>Kod</c>, <c>Nama</c>, <c>Tahap</c>, <c>KodRujKategoriAktivitiOrganisasi</c>, <c>Keterangan</c>, and <c>UserId</c>.
        /// </param>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Created: 2025-09-03  
        /// Purpose: Inserts a new record into <c>PDOAktivitiOrganisasi</c> with proper metadata  
        /// for creation tracking and auditing.
        /// </remarks>
        public async Task WujudAktivitiOrganisasiBaru(WujudAktivitiOrganisasiRequestDto request)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOAktivitiOrganisasi
                {
                    IdIndukAktivitiOrganisasi = request.IdIndukAktivitiOrganisasi,
                    KodProgram = request.KodProgram,
                    Kod = request.Kod,
                    Nama = request.Nama,
                    Tahap = request.Tahap,
                    KodRujKategoriAktivitiOrganisasi = request.KodRujKategoriAktivitiOrganisasi,
                    Keterangan = request.Keterangan,
                    IdCipta = request.UserId,
                    TarikhCipta = DateTime.Now,
                    StatusAktif = true // usually "baru" means aktif — confirm if you want default = false
                };

                await _context.PDOAktivitiOrganisasi.AddAsync(entity);

                // persist changes
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                _logger.LogError(
                    ex,
                    "Error in {Method} for UserId: {UserId}, Nama: {Nama}, Kod: {Kod}, ParentId: {ParentId}",
                    nameof(WujudAktivitiOrganisasiBaru),
                    request.UserId,
                    request.Nama,
                    request.Kod,
                    request.IdIndukAktivitiOrganisasi
                );

                throw;
            }
        }



        /// <summary>
        /// Renames (penjenamaan) an existing Aktiviti Organisasi entity.
        /// </summary>
        /// <param name="request">
        /// DTO containing the AktivitiOrganisasi Id, UserId of the person performing the update,  
        /// and the new name (<c>Nama</c>).
        /// </param>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Created: 2025-09-03  
        /// Purpose: Archives the current entity state into JSON, updates its name,  
        /// and tracks metadata for auditing purposes.  
        /// </remarks>
        public async Task PenjenamaanAktivitiOrganisasi(PenjenamaanAktivitiOrganisasiRequestDto request)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await _context.PDOAktivitiOrganisasi
                    .FirstOrDefaultAsync(pdoao => pdoao.Id == request.Id);

                if (entity == null)
                {
                    throw new Exception("PDOAktivitiOrganisasi not found.");
                }

                // Archive old state before updating
                var archivedEntity = new PDOAktivitiOrganisasi
                {
                    KodRujKategoriAktivitiOrganisasi = entity.KodRujKategoriAktivitiOrganisasi,
                    IdIndukAktivitiOrganisasi = entity.IdIndukAktivitiOrganisasi,
                    Kod = entity.Kod,
                    Nama = entity.Nama, // old name for history
                    Keterangan = entity.Keterangan,
                    KodProgram = entity.KodProgram,
                    Tahap = entity.Tahap,
                    KodCartaAktiviti = entity.KodCartaAktiviti,
                    ButiranKemaskini = entity.ButiranKemaskini,
                    StatusAktif = entity.StatusAktif ?? false,

                    IdPinda = request.UserId,
                    TarikhPinda = DateTime.Now
                };

                string json = JsonSerializer.Serialize(archivedEntity);
                entity.ButiranKemaskini = json;

                // Apply the new name
                entity.Nama = request.Nama;
                entity.IdPinda = request.UserId;
                entity.TarikhPinda = DateTime.Now;

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                _logger.LogError(
                    ex,
                    "Error in {Method} for UserId: {UserId}, AktivitiId: {AktivitiId}, NewName: {NewName}",
                    nameof(PenjenamaanAktivitiOrganisasi),
                    request.UserId,
                    request.Id,
                    request.Nama
                );

                throw;
            }
        }

        /// <summary>
        /// Moves (pindah) an existing Aktiviti Organisasi to a new parent node.
        /// </summary>
        /// <param name="request">
        /// DTO containing the AktivitiOrganisasi Id, the UserId performing the operation,  
        /// and the new parent Id (<c>NewParentId</c>).
        /// </param>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Created: 2025-09-03  
        /// Purpose: Archives the current AktivitiOrganisasi state into JSON, updates its parent reference,  
        /// and saves the change with metadata (UserId and timestamps).  
        /// </remarks>
        public async Task PindahAktivitiOrganisasi(PindahAktivitiOrganisasiRequestDto request)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await _context.PDOAktivitiOrganisasi
                    .FirstOrDefaultAsync(pdoao => pdoao.Id == request.Id);

                if (entity == null)
                {
                    throw new Exception("PDOAktivitiOrganisasi not found.");
                }

                var newPDOAktivitiOrganisasi = new PDOAktivitiOrganisasi
                {
                    // ⚠️ Do not copy Id — EF will manage it if identity
                    KodRujKategoriAktivitiOrganisasi = entity.KodRujKategoriAktivitiOrganisasi,
                    IdIndukAktivitiOrganisasi = request.NewParentId, // <-- override parent
                    Kod = entity.Kod,
                    Nama = entity.Nama,
                    Keterangan = entity.Keterangan,
                    KodProgram = entity.KodProgram,
                    Tahap = entity.Tahap,
                    KodCartaAktiviti = entity.KodCartaAktiviti,
                    ButiranKemaskini = entity.ButiranKemaskini,
                    StatusAktif = entity.StatusAktif ?? false,

                    // metadata
                    IdCipta = Guid.NewGuid(), // 🔎 check business rules, might need to keep original IdCipta instead
                    TarikhCipta = DateTime.UtcNow,
                    IdPinda = request.UserId,
                    TarikhPinda = DateTime.Now,
                    IdHapus = null,
                    TarikhHapus = null,
                };

                string json = JsonSerializer.Serialize(newPDOAktivitiOrganisasi);

                // Archive current state into JSON on the original entity
                entity.ButiranKemaskini = json;
                entity.IdPinda = request.UserId;
                entity.TarikhPinda = DateTime.Now;

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                _logger.LogError(
                    ex,
                    "Error in {Method} for UserId: {UserId}, AktivitiId: {AktivitiId}, NewParentId: {NewParentId}",
                    nameof(PindahAktivitiOrganisasi),
                    request.UserId,
                    request.Id,
                    request.NewParentId
                );

                throw;
            }
        }


        /// <summary>
        /// Reads an AktivitiOrganisasi by Id, including its parent name,
        /// and prepares metadata for the next potential child (KodProgram, Kod, Tahap).
        /// </summary>
        /// <param name="Id">The identifier of the AktivitiOrganisasi to read.</param>
        /// <returns>An <see cref="AktivitiOrganisasiDto"/> containing details of the activity and next child metadata.</returns>
        public async Task<AktivitiOrganisasiDto> BacaAktivitiOrganisasi(int Id)
        {
            try
            {
                var result = await (
                    from pdoao in _context.PDOAktivitiOrganisasi
                    join paoparent in _context.PDOAktivitiOrganisasi
                        on pdoao.IdIndukAktivitiOrganisasi equals paoparent.Id into parentJoin
                    from paoparent in parentJoin.DefaultIfEmpty() // left join in case root has no parent
                    where pdoao.Id == Id
                    select new AktivitiOrganisasiDto
                    {
                        AktiviOrganisasi = pdoao.Nama,
                        AktivitiOrganisasiInduk = paoparent != null ? paoparent.Nama : null,
                        ButiranKemaskini = pdoao.ButiranKemaskini,
                        Id = pdoao.Id,
                        IdCipta = pdoao.IdCipta,
                        IdHapus = pdoao.IdHapus,
                        IdIndukAktivitiOrganisasi = pdoao.IdIndukAktivitiOrganisasi,
                        IdPinda = pdoao.IdPinda,
                        Keterangan = pdoao.Keterangan,
                        Kod = pdoao.Kod,
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

                if (result == null)
                    throw new InvalidOperationException($"AktivitiOrganisasi with Id={Id} not found.");

                // ✅ Prepare metadata for next child
                var childCount = await _context.PDOAktivitiOrganisasi
                    .CountAsync(x => x.IdIndukAktivitiOrganisasi == result.Id);

                // Generate next KodProgram (append count+1)
                var nextProgramCount = childCount + 1;
                result.KodProgram = $"{result.KodProgram}.{nextProgramCount}";

                // Increase hierarchy level
                result.Tahap = (result.Tahap ?? 0) + 1;

                // Generate next Kod (increment last numeric part if exists)
                if (!string.IsNullOrWhiteSpace(result.Kod))
                {
                    var parts = result.Kod.Split('.', StringSplitOptions.RemoveEmptyEntries);
                    var lastPart = parts.Last();

                    if (int.TryParse(lastPart, out int lastNumber))
                    {
                        lastNumber++;
                        parts[^1] = lastNumber.ToString("D3"); // keep padding to 3 digits
                        result.Kod = string.Join(".", parts);
                    }
                    else
                    {
                        _logger.LogWarning("BacaAktivitiOrganisasi: Last Kod segment is not numeric ({LastPart}) for Id={Id}", lastPart, Id);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in BacaAktivitiOrganisasi for Id={Id}", Id);
                throw;
            }
        }



        /// <summary>
        /// Retrieves a list of AktivitiOrganisasi for use in dropdowns.
        /// </summary>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Provides a lightweight reference list of AktivitiOrganisasi  
        ///               with <c>Kod</c> and <c>Nama</c> fields, typically for UI dropdowns.  
        /// </remarks>
        /// <returns>A list of <see cref="DropDownDto"/> containing Kod and Nama.</returns>
        public async Task<List<DropDownDto>> RujukanAktivitiOrganisasi()
        {
            try
            {
                var result = await _context.PDOAktivitiOrganisasi
                    .AsNoTracking()
                    .OrderBy(x => x.Nama) // ✅ predictable order for dropdown
                    .Select(pdoao => new DropDownDto
                    {
                        Kod = pdoao.Kod,
                        Nama = pdoao.Nama
                    })
                    .ToListAsync();

                return result ?? new List<DropDownDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RujukanAktivitiOrganisasi while fetching dropdown data");
                throw;
            }
        }



        /// <summary>
        /// Registers (daftar) a new Aktiviti Organisasi record into the system.
        /// </summary>
        /// <param name="request">DTO containing user and Aktiviti Organisasi details.</param>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Created: 2025-09-03  
        /// Purpose: Creates a new <c>PDOAktivitiOrganisasi</c> entity from the request payload,  
        /// persists it into the database, and commits the transaction.  
        /// </remarks>
        public async Task DaftarAktivitiOrganisasi(AktivitiOrganisasiDaftarRequestDto request)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new PDOAktivitiOrganisasi
                {
                    IdCipta = request.UserId,
                    Kod = request.KodAktiviti,
                    Nama = request.NamaAktiviti,
                    KodRujKategoriAktivitiOrganisasi = request.KodRujKategoriAktivitiOrganisasi,
                    IdAsal = request.IdAsal
                };

                await _context.PDOAktivitiOrganisasi.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                _logger.LogError(
                    ex,
                    "Error in {Method} for UserId: {UserId}, KodAktiviti: {KodAktiviti}",
                    nameof(DaftarAktivitiOrganisasi),
                    request.UserId,
                    request?.KodAktiviti
                );

                throw;
            }
        }



        /// <summary>
        /// Permanently deletes an AktivitiOrganisasi record from the database.  
        /// </summary>
        /// <param name="request">
        /// Request object containing both the UserId (for auditing) and the Id of the entity.  
        /// </param>
        /// <remarks>
        /// Best practice:
        /// - Always wrap in a transaction.  
        /// - Use rollback on failure.  
        /// - Validate existence before delete.  
        /// - Use structured logging for observability.  
        /// </remarks>
        public async Task HapusTerusAktivitiOrganisasi(HapusTerusAktivitiOrganisasiRequestDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "Request payload cannot be null.");


            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await _context.PDOAktivitiOrganisasi
                    .FirstOrDefaultAsync(pdoao => pdoao.Id == request.Id);

                if (entity == null)
                {
                    _logger.LogWarning(
                        "Attempted to delete AktivitiOrganisasi that does not exist. UserId: {UserId}, Id: {Id}",
                        request.UserId, request.Id
                    );
                    throw new KeyNotFoundException($"PDOAktivitiOrganisasi with Id {request.Id} not found.");
                }

                _context.PDOAktivitiOrganisasi.Remove(entity);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                _logger.LogInformation(
                    "AktivitiOrganisasi deleted successfully. UserId: {UserId}, Id: {Id}, Nama: {Nama}",
                    request.UserId, entity.Id, entity.Nama
                );
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(
                    ex,
                    "Error in HapusTerusAktivitiOrganisasi for UserId: {UserId}, Id: {Id}",
                    request?.UserId, request?.Id
                );

                throw;
            }
        }

        /// <summary>
        /// Soft delete (mansuh) aktiviti organisasi by archiving details into JSON
        /// and marking entity as updated with deletion metadata.
        /// </summary>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Created: 2025-09-03  
        /// Purpose: Implements business logic to soft-delete (mansuh) an aktiviti organisasi 
        /// by serializing its current state and storing it in <c>ButiranKemaskini</c>, 
        /// while tagging the entity with deletion metadata (UserId & TarikhHapus).
        /// </remarks>
        public async Task MansuhAktivitiOrganisasi(MansuhAktivitiOrganisasiRequestDto request)
        {

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = await (from pdoao in _context.PDOAktivitiOrganisasi
                                    where pdoao.Id == request.IdAktivitiOrganisasi
                                    select pdoao
                                    ).FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new Exception("PDOAktivitiOrganisasi not found.");
                }

                // Archive current state into JSON and set deletion metadata
                var newButiranKemaskini = new MansuhAktivitiOrganisasiButiranKemaskiniDto
                {
                    Id = entity.Id,
                    StatusAktif = entity.StatusAktif ?? false,
                };

                string json = JsonSerializer.Serialize(newButiranKemaskini);

                int count = json.Length;

                // Apply update on the original entity
                entity.ButiranKemaskini = json;
                entity.IdPinda = request.UserId;
                entity.TarikhPinda = DateTime.Now;

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in MansuhAktivitiOrganisasi");
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }


    }

}

