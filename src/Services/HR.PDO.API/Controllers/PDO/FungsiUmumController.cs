using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
/// <summary>
/// Created By: Khairi Abu Bakar  
/// Date: 19/09/2025  
/// Purpose: API controller for executing general-purpose (Fungsi Umum) operations under the PDO module.  
///          Handles routing, delegates business logic to IFungsiUmum, and logs operational events.
/// </summary>
namespace HR.PDO.API.Controllers.PDO
{
    [ApiController]
    [Route("api/pdo/v1/fungsi-umum")]
    public class FungsiUmumController : ControllerBase
    {
        private readonly ILogger<FungsiUmumController> _logger;
        private readonly IFungsiUmum _fungsiumum;

        /// <summary>
        /// Constructor for FungsiUmumController.  
        /// Injects service layer and logger for executing general-purpose business logic and diagnostics.
        /// </summary>
        /// <param name="fungsiumum">Service layer for Fungsi Umum operations</param>
        /// <param name="logger">Logger instance for structured logging and traceability</param>
        public FungsiUmumController(
            IFungsiUmum fungsiumum,
            ILogger<FungsiUmumController> logger)
        {
            _fungsiumum = fungsiumum;
            _logger = logger;
        }

        /// <summary>
        /// Generate a reference number based on the given organizational unit ID.
        /// This endpoint returns a status and the generated reference number wrapped in a DropDownDto.
        /// </summary>
        /// <param name="IdUnitOrganisasi">The ID of the organizational unit used to generate the reference number.</param>
        /// <returns>
        /// HTTP 200 with status and reference number if successful; 
        /// HTTP 500 with error details if an exception occurs.
        /// </returns>
        /// <remarks>
        /// Author: Khairi Abu Bakar  
        /// Date: 19 September 2025  
        /// Purpose: To provide a GET endpoint for generating a reference number tied to a specific unit,
        /// ensuring traceability and structured logging for operational diagnostics.
        /// </remarks>
        [HttpGet("jana-nombor-rujukan/{id}")]
        public async Task<ActionResult<DropDownDto>> JanaNomborRujukan(int IdUnitOrganisasi)
        {
            _logger.LogInformation("Calling JanaNomborRujukan");

            try
            {
                var data = await _fungsiumum.JanaNomborRujukan(IdUnitOrganisasi);

                return Ok(new
                {
                    status = data != null ? "Berjaya" : "Gagal",
                    items = data
                });
            }
            catch (Exception ex)
            {
                string err = ex.InnerException?.Message ?? string.Empty;
                _logger.LogError(ex, "Error in JanaNomborRujukan");

                return StatusCode(500, $"{ex.Message} - {err}");
            }
        }

    }
}
