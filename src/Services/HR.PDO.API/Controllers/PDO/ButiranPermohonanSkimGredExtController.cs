using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
/// <summary>
/// Created By: Khairi Abu Bakar  
/// Date: 19/09/2025  
/// Purpose: API controller for managing Butiran Permohonan Skim Gred operations under the PDO module.  
///          Handles routing, delegates business logic to IButiranPermohonanSkimGredExt, and logs operational events.
/// </summary>
namespace HR.PDO.API.Controllers.PDO
{
    [ApiController]
    [Route("api/pdo/v1/butiran-permohonan-skim-gred")]
    public class ButiranPermohonanSkimGredExtController : ControllerBase
    {
        private readonly ILogger<ButiranPermohonanSkimGredExtController> _logger;
        private readonly IButiranPermohonanSkimGredExt _butiranpermohonanskimgredext;

        /// <summary>
        /// Constructor for ButiranPermohonanSkimGredExtController.  
        /// Injects service and logger dependencies for executing business logic and structured logging.
        /// </summary>
        /// <param name="butiranpermohonanskimgredext">Service layer for Butiran Permohonan Skim Gred operations</param>
        /// <param name="logger">Logger instance for diagnostics and traceability</param>
        public ButiranPermohonanSkimGredExtController(
            IButiranPermohonanSkimGredExt butiranpermohonanskimgredext,
            ILogger<ButiranPermohonanSkimGredExtController> logger)
        {
            _butiranpermohonanskimgredext = butiranpermohonanskimgredext;
            _logger = logger;
        }

        /// <summary>
        /// Created By: Khairi Abu Bakar  
        /// Date: 19/09/2025  
        /// Purpose: Adds a new Butiran Permohonan Skim Gred record using the provided request DTO.  
        ///          Executes business logic via IButiranPermohonanSkimGredExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="request">Payload containing details of the Skim Gred application to be added</param>
        /// <returns>HTTP 200 with status and result if successful; HTTP 500 with error details if failed</returns>
        [HttpPost("tambah")]
        public async Task<ActionResult> TambahButiranPermohonanSkimGred([FromBody] TambahButiranPermohonanSkimGredDto request)
        {
            _logger.LogInformation("Calling TambahButiranPermohonanSkimGred");
            try
            {
                var data = await _butiranpermohonanskimgredext.TambahButiranPermohonanSkimGred(request);
                return Ok(new
                {
                    status = data != null ? "Berjaya" : "Gagal",
                    items = data
                });
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in TambahButiranPermohonanSkimGred");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

        /// <summary>
        /// Created By: Khairi Abu Bakar  
        /// Date: 19/09/2025  
        /// Purpose: Retrieves a list of Butiran Permohonan Skim Gred records.  
        ///          Executes business logic via IButiranPermohonanSkimGredExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <returns>HTTP 200 with status and item list if successful; HTTP 500 with error details if failed</returns>
        [HttpGet("senarai")]
        public async Task<ActionResult<IEnumerable<ButiranPermohonanSkimGredDto>>> SenaraiButiranPermohonanSkimGred()
        {
            _logger.LogInformation("Calling SenaraiButiranPermohonanSkimGred");
            try
            {
                var data = await _butiranpermohonanskimgredext.SenaraiButiranPermohonanSkimGred();
                return Ok(new
                {
                    status = data != null ? "Berjaya" : "Gagal",
                    items = data
                });
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in SenaraiButiranPermohonanSkimGred");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

        /// <summary>
        /// Created By: Khairi Abu Bakar  
        /// Date: 19/09/2025  
        /// Purpose: Retrieves a specific Butiran Permohonan Skim Gred record by its unique identifier.  
        ///          Executes business logic via IButiranPermohonanSkimGredExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="Id">Unique identifier for the Butiran Permohonan Skim Gred record</param>
        /// <returns>HTTP 200 with record if found; HTTP 500 with error details if operation fails</returns>
        [HttpGet("baca/{id}")]
        public async Task<ActionResult<ButiranPermohonanSkimGredDto>> BacaButiranPermohonanSkimGred(int Id)
        {
            _logger.LogInformation("Calling BacaButiranPermohonanSkimGred");
            try
            {
                var data = await _butiranpermohonanskimgredext.BacaButiranPermohonanSkimGred(Id);
                return Ok(new
                {
                    status = data != null ? "Berjaya" : "Gagal",
                    items = data
                });
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in BacaButiranPermohonanSkimGred");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

        /// <summary>
        /// Created By: Khairi Abu Bakar  
        /// Date: 19/09/2025  
        /// Purpose: Permanently deletes a Butiran Permohonan Skim Gred record by its unique identifier.  
        ///          Executes deletion logic via IButiranPermohonanSkimGredExt and returns a confirmation response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="Id">Unique identifier for the Skim Gred record to be deleted</param>
        /// <returns>HTTP 200 with success message if deleted; HTTP 500 with error details if operation fails</returns>
        [HttpDelete("hapus-terus/{id}")]
        public async Task<ActionResult> HapusTerusButiranPermohonanSkimGred([FromQuery] int Id)
        {
            _logger.LogInformation("Calling HapusTerusButiranPermohonanSkimGred");
            try
            {
                await _butiranpermohonanskimgredext.HapusTerusButiranPermohonanSkimGred(Id);
                return Ok(new { message = "Berjaya Hapus Butiran Permohonan Skim Gred" });
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in HapusTerusButiranPermohonanSkimGred");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

        /// <summary>
        /// Created By: Khairi Abu Bakar  
        /// Date: 19/09/2025  
        /// Purpose: Updates an existing Butiran Permohonan Skim Gred record using the provided DTO.  
        ///          Executes update logic via IButiranPermohonanSkimGredExt and returns a confirmation response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="request">Payload containing updated details for the Skim Gred record</param>
        /// <returns>HTTP 200 with success message if updated; HTTP 500 with error details if operation fails</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> KemaskiniButiranPermohonanSkimGred([FromBody] ButiranPermohonanSkimGredDto request)
        {
            _logger.LogInformation("Calling KemaskiniButiranPermohonanSkimGred");
            try
            {
                await _butiranpermohonanskimgredext.KemaskiniButiranPermohonanSkimGred(request);
                return Ok(new { message = "Berjaya Kemaskini Butiran Permohonan Skim Gred" });
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in KemaskiniButiranPermohonanSkimGred");
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
