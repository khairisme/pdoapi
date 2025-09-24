using Azure.Core;
using HR.PDO.Application.DTOs;
using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Infrastructure.Data.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using  Shared.Contracts.DTOs;
using Swashbuckle.AspNetCore.Annotations;

/// <summary>
/// Extended controller for Gred-related reference operations within the PDO module.
/// Handles external-facing endpoints that interact with Gred data via IGredExt service.
/// </summary>
/// <remarks>
/// Author: Khairi Abu Bakar  
/// Date: 19 September 2025  
/// Purpose: Provides specialized endpoints for Gred reference logic, supporting external consumers
/// and ensuring structured routing, logging, and service delegation.
/// </remarks>
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/gred/rujukan")]
    public class GredExtController : ControllerBase
    {
        private readonly ILogger<RujukanExtController> _logger;
        private readonly IGredExt _gredext;
        private readonly PDODbContext _context;

        public GredExtController(IGredExt gredext, ILogger<RujukanExtController> logger)
        {
            _gredext = gredext;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of Gred references for dropdown consumption.
        /// </summary>
        /// <returns>
        /// HTTP 200 with status and list of Gred items if successful;  
        /// HTTP 500 with error details if an exception occurs.
        /// </returns>
        /// <remarks>
        /// Author: Khairi Abu Bakar  
        /// Date: 19 September 2025  
        /// Purpose: Provides a GET endpoint to expose Gred reference data for external consumers, 
        /// typically used in dropdowns or selection lists. Includes structured logging and error handling.
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DropDownDto>>> RujukanGred()
        {
            _logger.LogInformation("Calling RujukanGred");

            try
            {
                var data = await _gredext.RujukanGred();

                return Ok(new
                {
                    status = data.Any() ? "Berjaya" : "Gagal",
                    items = data
                });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? string.Empty;
                _logger.LogError(ex, "Error in RujukanGred");

                return StatusCode(500, new
                {
                    status = "Gagal",
                    message = $"{ex.Message} - {innerMessage}"
                });
            }
        }


        /// <summary>
        /// Retrieves Gred reference data filtered by classification and service group.
        /// </summary>
        /// <param name="IdKlasifikasiPerkhidmatan">The ID of the service classification.</param>
        /// <param name="IdKumpulanPerkhidmatan">The ID of the service group.</param>
        /// <returns>
        /// HTTP 200 with a filtered list of Gred references;  
        /// HTTP 500 if an exception occurs.
        /// </returns>
        /// <remarks>
        /// Author: Khairi Abu Bakar  
        /// Date: 19 September 2025  
        /// Purpose: Provides a filtered Gred reference endpoint based on classification and group parameters, 
        /// supporting dynamic dropdowns and contextual data retrieval for external consumers.
        /// </remarks>
        [HttpGet("ikut-klasifikasi-kumpulan")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>> RujukanGredIkutKlasifikasiDanKumpulan(
            int IdKlasifikasiPerkhidmatan,
            int IdKumpulanPerkhidmatan)
        {
            try
            {
                var result = await _gredext.RujukanGredIkutKlasifikasiDanKumpulan(
                    IdKlasifikasiPerkhidmatan,
                    IdKumpulanPerkhidmatan);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RujukanGredIkutKlasifikasiDanKumpulan");
                return StatusCode(500, new
                {
                    status = "Gagal",
                    message = ex.Message
                });
            }
        }

        [HttpPost("ikut-skim-perkhidmatan")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>> RujukanGredIkutSkimPerkhidmatan(
        [FromBody] GredSkimRequestDto request)
        {
            try
            {
                var result = await _gredext.RujukanGredIkutSkimPerkhidmatan(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RujukanGredIkutSkimPerkhidmatab");
                return StatusCode(500, new
                {
                    status = "Gagal",
                    message = ex.Message
                });
            }
        }
        /// <summary>
        /// Retrieves Gred reference data specifically for KUP (Khas Untuk Penyandang) positions.
        /// </summary>
        /// <returns>
        /// HTTP 200 with a list of Gred items for KUP;  
        /// HTTP 500 with error details if an exception occurs.
        /// </returns>
        /// <remarks>
        /// Author: Khairi Abu Bakar  
        /// Date: 19 September 2025  
        /// Purpose: Provides a dedicated endpoint to expose Gred data relevant to KUP roles, 
        /// supporting dropdowns and filtered UI components for external consumers.
        /// </remarks>
        [HttpGet("kup")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>> RujukanGredKUP()
        {
            try
            {
                var result = await _gredext.RujukanGredKUP();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RujukanGredKUP");

                return StatusCode(500, new
                {
                    status = "Gagal",
                    message = ex.Message
                });
            }
        }

    }
}
