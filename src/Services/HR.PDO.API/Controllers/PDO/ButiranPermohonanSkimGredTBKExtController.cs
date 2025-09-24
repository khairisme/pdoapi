using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
/// <summary>
/// Created By: Khairi Abu Bakar  
/// Date: 19/09/2025  
/// Purpose: API controller for managing Butiran Permohonan Skim Gred TBK operations under the PDO module.  
///          Handles routing, delegates business logic to IButiranPermohonanSkimGredTBKExt, and logs operational events.
/// </summary>
namespace HR.PDO.API.Controllers.PDO
{
    [ApiController]
    [Route("api/pdo/v1/butiran-permohonan-skim-gred-tbk")]
    public class ButiranPermohonanSkimGredTBKExtController : ControllerBase
    {
        private readonly ILogger<ButiranPermohonanSkimGredTBKExtController> _logger;
        private readonly IButiranPermohonanSkimGredTBKExt _butiranpermohonanskimgredtbkext;

        /// <summary>
        /// Constructor for ButiranPermohonanSkimGredTBKExtController.  
        /// Injects service and logger dependencies for executing business logic and structured logging.
        /// </summary>
        /// <param name="butiranpermohonanskimgredtbkext">Service layer for Skim Gred TBK operations</param>
        /// <param name="logger">Logger instance for diagnostics and traceability</param>
        public ButiranPermohonanSkimGredTBKExtController(
            IButiranPermohonanSkimGredTBKExt butiranpermohonanskimgredtbkext,
            ILogger<ButiranPermohonanSkimGredTBKExtController> logger)
        {
            _butiranpermohonanskimgredtbkext = butiranpermohonanskimgredtbkext;
            _logger = logger;
        }

        /// <summary>
        /// Created By: Khairi Abu Bakar  
        /// Date: 19/09/2025  
        /// Purpose: Adds a new Butiran Permohonan Skim Gred TBK record using the provided request DTO.  
        ///          Executes business logic via IButiranPermohonanSkimGredTBKExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="request">Payload containing details of the Skim Gred TBK application to be added</param>
        /// <returns>HTTP 200 with status and result if successful; HTTP 500 with error details if failed</returns>
        [HttpPost("tambah")]
        public async Task<ActionResult> TambahButiranPermohonanSkimGredTBK([FromBody] TambahButiranPermohonanSkimGredTBKDto request)
        {
            _logger.LogInformation("Calling TambahButiranPermohonanSkimGredTBK");
            try
            {
                var data = await _butiranpermohonanskimgredtbkext.TambahButiranPermohonanSkimGredTBK(request);
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
                    _logger.LogError(ex, "Error in TambahButiranPermohonanSkimGredTBK");
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
        /// Purpose: Retrieves a list of Butiran Permohonan Skim Gred TBK records.  
        ///          Executes business logic via IButiranPermohonanSkimGredTBKExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <returns>HTTP 200 with status and item list if successful; HTTP 500 with error details if failed</returns>
        [HttpGet("senarai")]
        public async Task<ActionResult<IEnumerable<ButiranPermohonanSkimGredTBKDto>>> SenaraiButiranPermohonanSkimGredTBK()
        {
            _logger.LogInformation("Calling SenaraiButiranPermohonanSkimGredTBK");
            try
            {
                var data = await _butiranpermohonanskimgredtbkext.SenaraiButiranPermohonanSkimGredTBK();
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
                    _logger.LogError(ex, "Error in SenaraiButiranPermohonanSkimGredTBK");
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
        /// Purpose: Retrieves a specific Butiran Permohonan Skim Gred TBK record by its unique identifier.  
        ///          Executes business logic via IButiranPermohonanSkimGredTBKExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="Id">Unique identifier for the Skim Gred TBK record</param>
        /// <returns>HTTP 200 with record if found; HTTP 500 with error details if operation fails</returns>
        [HttpGet("baca/{id}")]
        public async Task<ActionResult<ButiranPermohonanSkimGredTBKDto>> BacaButiranPermohonanSkimGredTBK(int Id)
        {
            _logger.LogInformation("Calling BacaButiranPermohonanSkimGredTBK");
            try
            {
                var data = await _butiranpermohonanskimgredtbkext.BacaButiranPermohonanSkimGredTBK(Id);
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
                    _logger.LogError(ex, "Error in BacaButiranPermohonanSkimGredTBK");
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
        /// Purpose: Permanently deletes a Butiran Permohonan Skim Gred TBK record by its unique identifier.  
        ///          Executes deletion logic via IButiranPermohonanSkimGredTBKExt and returns a confirmation response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="Id">Unique identifier for the Skim Gred TBK record to be deleted</param>
        /// <returns>HTTP 200 with success message if deleted; HTTP 500 with error details if operation fails</returns>
        [HttpDelete("hapus-terus/{id}")]
        public async Task<ActionResult> HapusTerusButiranPermohonanSkimGredTBK([FromQuery] int Id)
        {
            _logger.LogInformation("Calling HapusTerusButiranPermohonanSkimGredTBK");
            try
            {
                await _butiranpermohonanskimgredtbkext.HapusTerusButiranPermohonanSkimGredTBK(Id);
                return Ok(new { message = "Berjaya Hapus Butiran Permohonan Skim Gred TBK" });
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in HapusTerusButiranPermohonanSkimGredTBK");
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
        /// Purpose: Updates an existing Butiran Permohonan Skim Gred TBK record using the provided DTO.  
        ///          Executes update logic via IButiranPermohonanSkimGredTBKExt and returns a confirmation response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="request">Payload containing updated details for the Skim Gred TBK record</param>
        /// <returns>HTTP 200 with success message if updated; HTTP 500 with error details if operation fails</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> KemaskiniButiranPermohonanSkimGredTBK([FromBody] ButiranPermohonanSkimGredTBKDto request)
        {
            _logger.LogInformation("Calling KemaskiniButiranPermohonanSkimGredTBK");
            try
            {
                await _butiranpermohonanskimgredtbkext.KemaskiniButiranPermohonanSkimGredTBK(request);
                return Ok(new { message = "Berjaya Kemaskini Butiran Permohonan Skim Gred TBK" });
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in KemaskiniButiranPermohonanSkimGredTBK");
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
