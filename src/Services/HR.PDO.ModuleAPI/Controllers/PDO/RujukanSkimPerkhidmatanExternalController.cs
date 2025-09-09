using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    /// <summary>
    /// API Controller for managing and retrieving Skim Perkhidmatan reference data.
    /// </summary>
    /// <remarks>
    /// Author: Khairi bin Abu Bakar  
    /// Date: 2025-09-04  
    /// Purpose: Provides endpoints to retrieve Skim Perkhidmatan information for dropdowns 
    /// and filtering by Klasifikasi or Kumpulan Perkhidmatan. Ensures consistent logging 
    /// and structured error handling across all reference endpoints.
    /// </remarks>
    [ApiController]
    [Route("api/pdo/v1/external/rujukan/skim-perkhidmatan")]
    public class RujukanSkimPerkhidmatanExternalController : ControllerBase
    {
        private readonly ILogger<RujukanSkimPerkhidmatanExternalController> _logger;
        private readonly IRujukanSkimPerkhidmatan _rujukanskimperkhidmatan;

        public RujukanSkimPerkhidmatanExternalController(
            IRujukanSkimPerkhidmatan rujukanskimperkhidmatan,
            ILogger<RujukanSkimPerkhidmatanExternalController> logger)
        {
            _rujukanskimperkhidmatan = rujukanskimperkhidmatan;
            _logger = logger;
        }
        /// <summary>
        /// Retrieves a list of Skim Perkhidmatan filtered by both 
        /// Klasifikasi Perkhidmatan and Kumpulan Perkhidmatan.
        /// </summary>
        /// <param name="request">The request DTO containing IdKlasifikasiPerkhidmatan and IdKumpulanPerkhidmatan.</param>
        /// <returns>A list of <see cref="DropDownDto"/> wrapped in an <see cref="ActionResult"/>.</returns>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Date: 2025-09-04  
        /// Purpose: Provides an API endpoint to supply filtered Skim Perkhidmatan data for dropdown selections, 
        /// ensuring proper error handling and logging for maintainability.
        /// </remarks>
        /// <summary>
        /// Get Skim Perkhidmatan by Klasifikasi and Kumpulan using request payload.
        /// </summary>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Date: 2025-09-04  
        /// Purpose: Allows frontend to pass Klasifikasi and Kumpulan IDs in request body payload 
        /// instead of query string or route parameters.
        /// </remarks>
        [HttpPost("klasifikasi/kumpulan")]
        [ProducesResponseType(typeof(IEnumerable<DropDownDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DropDownDto>>> RujukanSkimPerkhidmatanIkutKlasifikasiDanKumpulan(
            [FromBody] RujukanSkimPerkhidmatanRequestDto request)
        {
            _logger.LogInformation(
                "Calling RujukanSkimPerkhidmatanIkutKlasifikasiDanKumpulan with IdKlasifikasi={IdKlasifikasi}, IdKumpulan={IdKumpulan}",
                request.IdKlasifikasiPerkhidmatan, request.IdKumpulanPerkhidmatan);

            try
            {
                var data = await _rujukanskimperkhidmatan.RujukanSkimPerkhidmatanIkutKlasifikasiDanKumpulan(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var err = ex.InnerException?.Message ?? string.Empty;
                _logger.LogError(ex, "Error in RujukanSkimPerkhidmatanIkutKlasifikasiDanKumpulan");
                return StatusCode(500, ex.Message + "-" + err);
            }
        }

        /// <summary>
        /// Retrieves the complete list of Skim Perkhidmatan without applying any filters.
        /// </summary>
        /// <returns>A list of <see cref="DropDownDto"/> wrapped in an <see cref="ActionResult"/>.</returns>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Date: 2025-09-04  
        /// Purpose: Provides an API endpoint to supply all Skim Perkhidmatan data for dropdown selections, 
        /// ensuring proper logging and structured error handling.
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DropDownDto>>> RujukanSkimPerkhidmatan()
        {
            _logger.LogInformation("Calling RujukanSkimPerkhidmatan");

            try
            {
                var data = await _rujukanskimperkhidmatan.RujukanSkimPerkhidmatan();
                return Ok(data);
            }
            catch (Exception ex)
            {
                string err = string.Empty;

                if (ex != null)
                {
                    _logger.LogError(ex, "Error in RujukanSkimPerkhidmatan");

                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

        /// <summary>
        /// Retrieves the list of Skim Perkhidmatan filtered by the specified Klasifikasi Perkhidmatan ID.
        /// </summary>
        /// <param name="IdKlasifikasiPerkhidmatan">The ID of the Klasifikasi Perkhidmatan to filter by.</param>
        /// <returns>A filtered list of <see cref="DropDownDto"/> wrapped in an <see cref="ActionResult"/>.</returns>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Date: 2025-09-04  
        /// Purpose: Provides an API endpoint to fetch Skim Perkhidmatan based on a specific 
        /// Klasifikasi Perkhidmatan, ensuring proper logging and structured error handling.
        /// </remarks>
        [HttpGet("klasifikasi/{IdKlasifikasiPerkhidmatan:int}")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>> RujukanSkimPerkhidmatanIkutKlasifikasi(int IdKlasifikasiPerkhidmatan)
        {
            _logger.LogInformation("Calling RujukanSkimPerkhidmatanIkutKlasifikasi");

            try
            {
                var data = await _rujukanskimperkhidmatan.RujukanSkimPerkhidmatanIkutKlasifikasi(IdKlasifikasiPerkhidmatan);
                return Ok(data);
            }
            catch (Exception ex)
            {
                string err = string.Empty;

                if (ex != null)
                {
                    _logger.LogError(ex, "Error in RujukanSkimPerkhidmatanIkutKlasifikasi");

                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

        /// <summary>
        /// Retrieves the list of Skim Perkhidmatan filtered by the specified Kumpulan Perkhidmatan ID.
        /// </summary>
        /// <param name="IdKumpulanPerkhidmatan">The ID of the Kumpulan Perkhidmatan to filter by.</param>
        /// <returns>A filtered list of <see cref="DropDownDto"/> wrapped in an <see cref="ActionResult"/>.</returns>
        /// <remarks>
        /// Author: Khairi bin Abu Bakar  
        /// Date: 2025-09-04  
        /// Purpose: Provides an API endpoint to fetch Skim Perkhidmatan based on a specific 
        /// Kumpulan Perkhidmatan, ensuring proper logging and structured error handling.
        /// </remarks>
        [HttpGet("kumpulan/{IdKumpulanPerkhidmatan:int}")]
        public async Task<ActionResult<IEnumerable<DropDownDto>>> RujukanSkimPerkhidmatanIkutKumpulan(int IdKumpulanPerkhidmatan)
        {
            _logger.LogInformation("Calling RujukanSkimPerkhidmatanIkutKumpulan");

            try
            {
                var data = await _rujukanskimperkhidmatan.RujukanSkimPerkhidmatanIkutKumpulan(IdKumpulanPerkhidmatan);
                return Ok(data);
            }
            catch (Exception ex)
            {
                string err = string.Empty;

                if (ex != null)
                {
                    _logger.LogError(ex, "Error in RujukanSkimPerkhidmatanIkutKumpulan");

                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

    }
}
