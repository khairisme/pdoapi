using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
/// <summary>
/// Created By: Khairi Abu Bakar  
/// Date: 19/09/2025  
/// Purpose: API controller for managing Butiran Permohonan Skim Gred KUJ operations under the PDO module.  
///          Handles routing, delegates business logic to IButiranPermohonanSkimGredKUJExt, and logs operational events.
/// </summary>
namespace HR.PDO.API.Controllers.PDO
{
    [ApiController]
    [Route("api/pdo/v1/butiran-permohonan-skim-gred-kuj")]
    public class ButiranPermohonanSkimGredKUJExtController : ControllerBase
    {
        private readonly ILogger<ButiranPermohonanSkimGredKUJExtController> _logger;
        private readonly IButiranPermohonanSkimGredKUJExt _butiranpermohonanskimgredkujext;

        /// <summary>
        /// Constructor for ButiranPermohonanSkimGredKUJExtController.  
        /// Injects service and logger dependencies for executing business logic and structured logging.
        /// </summary>
        /// <param name="butiranpermohonanskimgredkujext">Service layer for Skim Gred KUJ operations</param>
        /// <param name="logger">Logger instance for diagnostics and traceability</param>
        public ButiranPermohonanSkimGredKUJExtController(
            IButiranPermohonanSkimGredKUJExt butiranpermohonanskimgredkujext,
            ILogger<ButiranPermohonanSkimGredKUJExtController> logger)
        {
            _butiranpermohonanskimgredkujext = butiranpermohonanskimgredkujext;
            _logger = logger;
        }

        /// <summary>
        /// Created By: Khairi Abu Bakar  
        /// Date: 19/09/2025  
        /// Purpose: Adds a new Butiran Permohonan Skim Gred KUJ record using the provided request DTO.  
        ///          Executes business logic via IButiranPermohonanSkimGredKUJExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="request">Payload containing details of the Skim Gred KUJ application to be added</param>
        /// <returns>HTTP 200 with status and result if successful; HTTP 500 with error details if failed</returns>
        [HttpPost("tambah")]
        public async Task<ActionResult> TambahButiranPermohonanSkimGredKUJ([FromBody] TambahButiranPermohonanSkimGredKUJDto request)
        {
            _logger.LogInformation("Calling TambahButiranPermohonanSkimGredKUJ");
            try
            {
                var data = await _butiranpermohonanskimgredkujext.TambahButiranPermohonanSkimGredKUJ(request);
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
                    _logger.LogError(ex, "Error in TambahButiranPermohonanSkimGredKUJ");
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
        /// Purpose: Retrieves a list of Butiran Permohonan Skim Gred KUJ records.  
        ///          Executes business logic via IButiranPermohonanSkimGredKUJExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <returns>HTTP 200 with status and item list if successful; HTTP 500 with error details if failed</returns>
        [HttpGet("senarai")]
        public async Task<ActionResult<IEnumerable<ButiranPermohonanSkimGredKUJDto>>> SenaraiButiranPermohonanSkimGredKUJ()
        {
            _logger.LogInformation("Calling SenaraiButiranPermohonanSkimGredKUJ");
            try
            {
                var data = await _butiranpermohonanskimgredkujext.SenaraiButiranPermohonanSkimGredKUJ();
                return Ok(new
                {
                    status = data.Count() > 0 ? "Berjaya" : "Gagal",
                    items = data
                });
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in SenaraiButiranPermohonanSkimGredKUJ");
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
        /// Purpose: Retrieves a specific Butiran Permohonan Skim Gred KUJ record by its unique identifier.  
        ///          Executes business logic via IButiranPermohonanSkimGredKUJExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="Id">Unique identifier for the Skim Gred KUJ record</param>
        /// <returns>HTTP 200 with record if found; HTTP 500 with error details if operation fails</returns>
        [HttpGet("baca/{id}")]
        public async Task<ActionResult<ButiranPermohonanSkimGredKUJDto>> BacaButiranPermohonanSkimGredKUJ(int Id)
        {
            _logger.LogInformation("Calling BacaButiranPermohonanSkimGredKUJ");
            try
            {
                var data = await _butiranpermohonanskimgredkujext.BacaButiranPermohonanSkimGredKUJ(Id);
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
                    _logger.LogError(ex, "Error in BacaButiranPermohonanSkimGredKUJ");
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
        /// Purpose: Permanently deletes a Butiran Permohonan Skim Gred KUJ record by its unique identifier.  
        ///          Executes deletion logic via IButiranPermohonanSkimGredKUJExt and returns a confirmation response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="Id">Unique identifier for the Skim Gred KUJ record to be deleted</param>
        /// <returns>HTTP 200 with success message if deleted; HTTP 500 with error details if operation fails</returns>
        [HttpDelete("hapus-terus/{id}")]
        public async Task<ActionResult> HapusTerusButiranPermohonanSkimGredKUJ([FromQuery] int Id)
        {
            _logger.LogInformation("Calling HapusTerusButiranPermohonanSkimGredKUJ");
            try
            {
                await _butiranpermohonanskimgredkujext.HapusTerusButiranPermohonanSkimGredKUJ(Id);
                return Ok(new { message = "Berjaya Hapus Butiran Permohonan Skim Gred KUJ" });
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in HapusTerusButiranPermohonanSkimGredKUJ");
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
        /// Purpose: Updates an existing Butiran Permohonan Skim Gred KUJ record using the provided DTO.  
        ///          Executes update logic via IButiranPermohonanSkimGredKUJExt and returns a confirmation response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="request">Payload containing updated details for the Skim Gred KUJ record</param>
        /// <returns>HTTP 200 with success message if updated; HTTP 500 with error details if operation fails</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> KemaskiniButiranPermohonanSkimGredKUJ([FromBody] ButiranPermohonanSkimGredKUJDto request)
        {
            _logger.LogInformation("Calling KemaskiniButiranPermohonanSkimGredKUJ");
            try
            {
                await _butiranpermohonanskimgredkujext.KemaskiniButiranPermohonanSkimGredKUJ(request);
                return Ok(new { message = "Berjaya Kemaskini Butiran Permohonan Skim Gred KUJ" });
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in KemaskiniButiranPermohonanSkimGredKUJ");
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
