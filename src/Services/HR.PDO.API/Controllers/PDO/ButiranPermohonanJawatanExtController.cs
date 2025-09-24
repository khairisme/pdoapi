using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
/// <summary>
/// Created By: Khairi Abu Bakar  
/// Date: 19/09/2025  
/// Purpose: API controller for handling Butiran Permohonan Jawatan operations under the PDO module.  
///          Manages routing, delegates business logic to IButiranPermohonanJawatanExt, and logs operational events.
/// </summary>
namespace HR.PDO.API.Controllers.PDO
{
    [ApiController]
    [Route("api/pdo/v1/butiran-permohonan-jawatan")]
    public class ButiranPermohonanJawatanExtController : ControllerBase
    {
        private readonly ILogger<ButiranPermohonanJawatanExtController> _logger;
        private readonly IButiranPermohonanJawatanExt _butiranpermohonanjawatanext;

        /// <summary>
        /// Constructor for ButiranPermohonanJawatanExtController.  
        /// Injects service and logger dependencies for executing business logic and structured logging.
        /// </summary>
        /// <param name="butiranpermohonanjawatanext">Service layer for Butiran Permohonan Jawatan operations</param>
        /// <param name="logger">Logger instance for diagnostics and traceability</param>
        public ButiranPermohonanJawatanExtController(
            IButiranPermohonanJawatanExt butiranpermohonanjawatanext,
            ILogger<ButiranPermohonanJawatanExtController> logger)
        {
            _butiranpermohonanjawatanext = butiranpermohonanjawatanext;
            _logger = logger;
        }

        /// <summary>
        /// Created By: Khairi Abu Bakar  
        /// Date: 19/09/2025  
        /// Purpose: Adds a new Butiran Permohonan Jawatan record based on the provided request DTO.  
        ///          Executes business logic via IButiranPermohonanJawatanExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="request">Payload containing details of the Butiran Permohonan Jawatan to be added</param>
        /// <returns>HTTP 200 with status and result if successful; HTTP 500 with error details if failed</returns>
        [HttpPost("tambah")]
        public async Task<ActionResult> TambahButiranPermohonanJawatan(TambahButiranPermohonanJawatanDto request)
        {
            _logger.LogInformation("Calling TambahButiranPermohonanJawatan");
            try
            {
                var data = await _butiranpermohonanjawatanext.TambahButiranPermohonanJawatan(request);
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
                    _logger.LogError(ex, "Error in TambahButiranPermohonanJawatan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }
        // <summary>
        /// Created By: Khairi Abu Bakar  
        /// Date: 19/09/2025  
        /// Purpose: Retrieves a list of Butiran Permohonan Jawatan records.  
        ///          Executes business logic via IButiranPermohonanJawatanExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <returns>HTTP 200 with status and item list if successful; HTTP 500 with error details if failed</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ButiranPermohonanJawatanDto>>> SenaraiButiranPermohonanJawatan()
        {
            _logger.LogInformation("Calling SenaraiButiranPermohonanJawatan");
            try
            {
                var data = await _butiranpermohonanjawatanext.SenaraiButiranPermohonanJawatan();
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
                    _logger.LogError(ex, "Error in SenaraiButiranPermohonanJawatan");
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
        /// Purpose: Retrieves a specific Butiran Permohonan Jawatan record based on the provided request DTO.  
        ///          Executes business logic via IButiranPermohonanJawatanExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="request">Payload containing identifiers to locate the Butiran Permohonan Jawatan record</param>
        /// <returns>HTTP 200 with record if found; HTTP 404 if no record exists; HTTP 500 with error details if failed</returns>
        [HttpPost("baca")]
        public async Task<ActionResult<ButiranPermohonanJawatanDto>> BacaButiranPermohonanJawatan(AddButiranPermohonanJawatanRequestDto request)
        {
            _logger.LogInformation("Calling BacaButiranPermohonanJawatan");
            try
            {
                var data = await _butiranpermohonanjawatanext.BacaButiranPermohonanJawatan(request);
                if (data == null)
                {
                    return NotFound(new { message = "Tiada rekod" });
                }

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
                    _logger.LogError(ex, "Error in BacaButiranPermohonanJawatan");
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
        /// Purpose: Permanently deletes a Butiran Permohonan Jawatan record based on the provided request DTO.  
        ///          Executes deletion logic via IButiranPermohonanJawatanExt and returns a confirmation response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="request">Payload containing identifiers for the Butiran Permohonan Jawatan to be deleted</param>
        /// <returns>HTTP 200 with success message if deleted; HTTP 500 with error details if operation fails</returns>
        [HttpDelete("hapus-terus/{id}")]
        public async Task<ActionResult> HapusTerusButiranPermohonanJawatan([FromQuery] AddButiranPermohonanJawatanRequestDto request)
        {
            _logger.LogInformation("Calling HapusTerusButiranPermohonanJawatan");
            try
            {
                await _butiranpermohonanjawatanext.HapusTerusPermohonanJawatan(request);
                return Ok(new { message = "Berjaya Hapus Butiran Permohonan" });
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in HapusTerusPermohonanJawatan");
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
