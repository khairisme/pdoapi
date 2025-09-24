using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;

/// <summary>
/// Extended controller for managing Jadual Gaji (Salary Schedule) operations within the PDP module.
/// Provides endpoints for retrieving salary data tied to Gred, Skim Perkhidmatan, and other classification filters.
/// </summary>
/// <remarks>
/// Author: Khairi Abu Bakar  
/// Date: 19 September 2025  
/// Purpose: Serves as the external-facing API layer for Jadual Gaji operations, 
/// enabling structured access to salary schedule logic via IJadualGajiExt. 
/// Supports logging, traceability, and modular service delegation.
/// </remarks>
namespace HR.PDO.API.Controllers.PDP {
    [ApiController]
    [Route("api/pdo/v1/jadual-gaji")]
    public class JadualGajiExtController : ControllerBase
    {
        private readonly ILogger<JadualGajiExtController> _logger;
        private readonly IJadualGajiExt _jadualgajiext;

        public JadualGajiExtController(IJadualGajiExt jadualgajiext, ILogger<JadualGajiExtController> logger)
        {
            _jadualgajiext = jadualgajiext;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the full list of Jadual Gaji (Salary Schedule) records based on the provided filter criteria.
        /// </summary>
        /// <param name="request">DTO containing filter parameters for Jadual Gaji retrieval.</param>
        /// <returns>
        /// HTTP 200 with status and list of Jadual Gaji items if successful;  
        /// HTTP 500 with error details if an exception occurs.
        /// </returns>
        /// <remarks>
        /// Author: Khairi Abu Bakar  
        /// Date: 19 September 2025  
        /// Purpose: Provides a GET endpoint to retrieve all relevant Jadual Gaji records, 
        /// supporting dynamic filtering and structured logging for external consumers.
        /// </remarks>
        [HttpGet("senarai-semua")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>> SenaraiSemuaJadualGaji([FromQuery] JadualGajiExtDto request)
        {
            _logger.LogInformation("Calling SenaraiSemuaJadualGaji");

            try
            {
                var data = await _jadualgajiext.SenaraiSemuaJadualGaji(request);

                return Ok(new
                {
                    status = data.Any() ? "Berjaya" : "Gagal",
                    items = data
                });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? string.Empty;
                _logger.LogError(ex, "Error in SenaraiSemuaJadualGaji");

                return StatusCode(500, new
                {
                    status = "Gagal",
                    message = $"{ex.Message} - {innerMessage}"
                });
            }
        }

    }
}
