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
    /// <summary>
    /// Service class responsible for handling operations related to Rujukan Skim Perkhidmatan 
    /// such as retrieving and managing service scheme references.
    /// </summary>
    /// <remarks>
    /// Author: Khairi bin Abu Bakar  
    /// Date: 2025-09-04  
    /// Purpose: Encapsulates business logic for accessing and managing Rujukan Skim Perkhidmatan data, 
    /// providing a clean abstraction for controllers and ensuring consistent error logging.
    /// </remarks>
    public class RujukanSkimPerkhidmatanService : IRujukanSkimPerkhidmatan
    {
        private readonly IPDOUnitOfWork _unitOfWork;
        private readonly PDODbContext _context;
        private readonly ILogger<RujukanSkimPerkhidmatanService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RujukanSkimPerkhidmatanService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The Unit of Work for transaction management and repository coordination.</param>
        /// <param name="dbContext">The database context for direct EF Core operations.</param>
        /// <param name="logger">The logger for error and process tracking.</param>
        public RujukanSkimPerkhidmatanService(
            IPDOUnitOfWork unitOfWork,
            PDODbContext dbContext,
            ILogger<RujukanSkimPerkhidmatanService> logger)
        {
            _unitOfWork = unitOfWork;
            _context = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the complete list of Skim Perkhidmatan (service schemes) without any filters.
        /// </summary>
        /// <returns>A list of DropDownDto containing Id, Kod, and Nama of all service schemes.</returns>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Date: 2025-09-04  
        /// Purpose: Provide a full dropdown list of Skim Perkhidmatan for use in UI components such as selection menus.
        /// </remarks>
        public async Task<List<DropDownDto>> RujukanSkimPerkhidmatan()
        {
            try
            {
                var result = await (from pdosp in _context.PDOSkimPerkhidmatan
                                    select new DropDownDto
                                    {
                                        Id = pdosp.Id,
                                        Kod = pdosp.Kod.Trim(),
                                        Nama = pdosp.Nama.Trim()
                                    }
                                   ).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RujukanSkimPerkhidmatan");
                throw;
            }
        }



        /// <summary>
        /// Retrieves a list of Skim Perkhidmatan (service schemes) filtered by Kumpulan Perkhidmatan (service group).
        /// </summary>
        /// <param name="IdKumpulanPerkhidmatan">The unique identifier of the Kumpulan Perkhidmatan.</param>
        /// <returns>A list of DropDownDto containing Kod and Nama of matching service schemes.</returns>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Date: 2025-09-04  
        /// Purpose: Provide filtered dropdown options of Skim Perkhidmatan based on Kumpulan Perkhidmatan for use in UI selections.
        /// </remarks>
        public async Task<List<DropDownDto>> RujukanSkimPerkhidmatanIkutKumpulan(int IdKumpulanPerkhidmatan)
        {
            try
            {
                var result = await (from pdosp in _context.PDOSkimPerkhidmatan
                                    where pdosp.IdKumpulanPerkhidmatan == IdKumpulanPerkhidmatan
                                    select new DropDownDto
                                    {
                                        Id = pdosp.Id,
                                        Kod = pdosp.Kod.Trim(),
                                        Nama = pdosp.Nama.Trim()
                                    }
                                   ).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RujukanSkimPerkhidmatanIkutKumpulan");
                throw;
            }
        }



        /// <summary>
        /// Retrieves a list of Skim Perkhidmatan (Service Schemes) filtered by Klasifikasi Perkhidmatan.
        /// Author: Khairi bin Abu Bakar
        /// Date: 2025-09-04
        /// Purpose: Provides a dropdown list of Skim Perkhidmatan for UI binding 
        ///          or business logic, filtered by a given Klasifikasi ID. 
        ///          Includes structured logging and error handling.
        /// </summary>
        /// <param name="IdKlasifikasiPerkhidmatan">Identifier for Klasifikasi Perkhidmatan used in filtering.</param>
        /// <returns>List of <see cref="DropDownDto"/> representing Skim Perkhidmatan options.</returns>
        public async Task<List<DropDownDto>> RujukanSkimPerkhidmatanIkutKlasifikasi(int IdKlasifikasiPerkhidmatan)
        {
            try
            {
                // ✅ Best Practice: Structured logging of input parameter
                _logger.LogInformation(
                    "Fetching SkimPerkhidmatan for IdKlasifikasiPerkhidmatan={IdKlasifikasi}",
                    IdKlasifikasiPerkhidmatan
                );

                var result = await (from pdosp in _context.PDOSkimPerkhidmatan
                                    where pdosp.IdKlasifikasiPerkhidmatan == IdKlasifikasiPerkhidmatan
                                    select new DropDownDto
                                    {
                                        Id = pdosp.Id,
                                        Kod = pdosp.Kod.Trim(),
                                        Nama = pdosp.Nama.Trim()
                                    }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                // ✅ Best Practice: Log exception with parameter context
                _logger.LogError(
                    ex,
                    "Error in RujukanSkimPerkhidmatanIkutKlasifikasi with IdKlasifikasiPerkhidmatan={IdKlasifikasi}",
                    IdKlasifikasiPerkhidmatan
                );

                throw; // rethrow so upstream handler can manage it
            }
        }

        /// <summary>
        /// Retrieves a list of Skim Perkhidmatan (Service Schemes) based on Klasifikasi and Kumpulan.
        /// Author: Khairi bin Abu Bakar
        /// Date: 2025-09-04
        /// Purpose: Provides a filtered dropdown list of Skim Perkhidmatan for use in UI selections.
        ///          Accepts a DTO parameter for cleaner method signature and easier extensibility.
        /// </summary>
        public async Task<List<DropDownDto>> RujukanSkimPerkhidmatanIkutKlasifikasiDanKumpulan(RujukanSkimPerkhidmatanRequestDto request)
        {
            try
            {
                // ✅ Best Practice: Log key parameters using structured logging
                _logger.LogInformation(
                    "Fetching SkimPerkhidmatan with IdKlasifikasiPerkhidmatan={IdKlasifikasi}, IdKumpulanPerkhidmatan={IdKumpulan}",
                    request.IdKlasifikasiPerkhidmatan,
                    request.IdKumpulanPerkhidmatan
                );

                var result = await (from pdosp in _context.PDOSkimPerkhidmatan
                                    where pdosp.IdKlasifikasiPerkhidmatan == request.IdKlasifikasiPerkhidmatan
                                       && pdosp.IdKumpulanPerkhidmatan == request.IdKumpulanPerkhidmatan
                                       && pdosp.KodRujJenisSaraan == request.KodRujJenisSaraan
                                    select new DropDownDto
                                    {
                                        Id = pdosp.Id,
                                        Kod = pdosp.Kod.Trim(),
                                        Nama = pdosp.Nama.Trim()
                                    }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                // ✅ Best Practice: Always log exceptions with context
                _logger.LogError(ex,
                    "Error in RujukanSkimPerkhidmatanIkutKlasifikasiDanKumpulan with IdKlasifikasiPerkhidmatan={IdKlasifikasi}, IdKumpulanPerkhidmatan={IdKumpulan}",
                    request.IdKlasifikasiPerkhidmatan,
                    request.IdKumpulanPerkhidmatan
                );

                throw; // Re-throw so upstream handler can deal with it
            }
        }



    }

}

