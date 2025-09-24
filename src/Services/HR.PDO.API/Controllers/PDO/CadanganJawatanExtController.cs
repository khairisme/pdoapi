using HR.PDO.Application.DTOs;
using HR.PDO.Application.Interfaces.PDO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using  Shared.Contracts.DTOs;
using Swashbuckle.AspNetCore.Annotations;
/// <summary>
/// Created By: Khairi Abu Bakar  
/// Date: 19/09/2025  
/// Purpose: API controller for managing Cadangan Jawatan operations under the PDO module.  
///          Handles routing, delegates business logic to ICadanganJawatanExt, and logs operational events.
/// </summary>
namespace HR.PDO.API.Controllers.PDO
{
    [ApiController]
    [Route("api/pdo/v1/cadangan-jawatan")]
    public class CadanganJawatanExtController : ControllerBase
    {
        private readonly ILogger<CadanganJawatanExtController> _logger;
        private readonly ICadanganJawatanExt _cadanganjawatanext;

        /// <summary>
        /// Constructor for CadanganJawatanExtController.  
        /// Injects service and logger dependencies for executing business logic and structured logging.
        /// </summary>
        /// <param name="cadanganjawatanext">Service layer for Cadangan Jawatan operations</param>
        /// <param name="logger">Logger instance for diagnostics and traceability</param>
        public CadanganJawatanExtController(
            ICadanganJawatanExt cadanganjawatanext,
            ILogger<CadanganJawatanExtController> logger)
        {
            _cadanganjawatanext = cadanganjawatanext;
            _logger = logger;
        }

        /// <summary>
        /// Created By: Khairi Abu Bakar  
        /// Date: 19/09/2025  
        /// Purpose: Retrieves a list of Cadangan Jawatan records associated with a specific Butiran Permohonan ID.  
        ///          Executes business logic via ICadanganJawatanExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="IdButiranPermohonan">Unique identifier for the Butiran Permohonan record</param>
        /// <returns>HTTP 200 with list of Cadangan Jawatan records; HTTP 500 with error details if operation fails</returns>
        [HttpPost("senarai")]
        public async Task<ActionResult<IEnumerable<CadanganJawatanDto>>> SenaraiButiranCadanganJawatan([FromBody] SenaraiCadanganJawatanRequestDto request)
        {
            _logger.LogInformation("Calling SenaraiCadanganJawatan");
            try
            {
                var data = await _cadanganjawatanext.SenaraiButiranCadanganJawatan(request);
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
                    _logger.LogError(ex, "Error in SenaraiCadanganJawatan");
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
        /// Purpose: Adds one or more Cadangan Jawatan records based on the provided request DTO.  
        ///          Executes business logic via ICadanganJawatanExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="request">Payload containing Cadangan Jawatan details to be added</param>
        /// <returns>HTTP 200 with list of added records; HTTP 500 with error details if operation fails</returns>
        [HttpPost("tambah")]
        public async Task<ActionResult> TambahCadanganJawatan([FromBody] SenaraiCadanganJawatanRequestDto request)
        {
            _logger.LogInformation("Calling TambahCadanganJawatan");
            try
            {
                var data = await _cadanganjawatanext.TambahCadanganJawatan(request);
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
                    _logger.LogError(ex, "Error in TambahCadanganJawatan");
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
        /// Purpose: Updates the Unit Organisasi field for one or more Cadangan Jawatan records.  
        ///          Executes business logic via ICadanganJawatanExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="request">Payload containing updated Unit Organisasi details for Cadangan Jawatan</param>
        /// <returns>HTTP 200 with updated records if successful; HTTP 500 with error details if operation fails</returns>
        [HttpPatch("unit-organisasi")]
        public async Task<ActionResult> KemaskiniUnitOrganisasiCadanganJawatan([FromBody] KemaskiniUnitOrganisasiCadanganJawatanRequestDto request)
        {
            _logger.LogInformation("Calling KemaskiniUnitOrganisasiCadanganJawatan");
            try
            {
                var data = await _cadanganjawatanext.KemaskiniUnitOrganisasiCadanganJawatan(request);
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
                    _logger.LogError(ex, "Error in KemaskiniUnitOrganisasiCadanganJawatan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, new
                {
                    status = "Gagal",
                    message = ex.Message + " - " + (ex.InnerException != null ? err : "")
                });
            }
        }
        /// <summary>
        /// Created By: Khairi Abu Bakar  
        /// Date: 19/09/2025  
        /// Purpose: Updates one or more existing Cadangan Jawatan records using the provided request DTO.  
        ///          Executes business logic via ICadanganJawatanExt and returns a confirmation response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="request">Payload containing updated Cadangan Jawatan details</param>
        /// <returns>HTTP 201 with reference to creation action; HTTP 500 with error details if operation fails</returns>
        [HttpPost("kemaskini")]
        public async Task<ActionResult> KemaskiniCadanganJawatan([FromBody] KemaskiniCadanganJawatanRequestDto request)
        {
            _logger.LogInformation("Calling KemaskiniCadanganJawatan");
            try
            {
                await _cadanganjawatanext.KemaskiniCadanganJawatan(request);
                return CreatedAtAction(nameof(TambahCadanganJawatan), new { request }, null);
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in KemaskiniCadanganJawatan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, new
                {
                    status = "Gagal",
                    message = ex.Message + " - " + (ex.InnerException != null ? err : "")
                });
            }
        }
    }
}
