using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;

/// <summary>
/// Extended controller for managing implications of job application requests within the PDO module.
/// Provides endpoints for retrieving and processing data related to service impact, position classification, and organizational context.
/// </summary>
/// <remarks>
/// Author: Khairi Abu Bakar  
/// Date: 19 September 2025  
/// Purpose: Serves as the external-facing API layer for Implikasi Permohonan Jawatan operations, 
/// enabling structured access to business logic via IImplikasiPermohonanJawatanExt. 
/// Supports logging, traceability, and modular service delegation.
/// </remarks>
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/implikasi-permohonan-jawatan")]
    public class ImplikasiPermohonanJawatanExtController : ControllerBase
    {
        private readonly ILogger<ImplikasiPermohonanJawatanExtController> _logger;
        private readonly IImplikasiPermohonanJawatanExt _implikasipermohonanjawatanext;

        public ImplikasiPermohonanJawatanExtController(IImplikasiPermohonanJawatanExt implikasipermohonanjawatanext, ILogger<ImplikasiPermohonanJawatanExtController> logger)
        {
            _implikasipermohonanjawatanext = implikasipermohonanjawatanext;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new record for job application implication (Implikasi Permohonan Jawatan).
        /// </summary>
        /// <param name="request">The DTO containing implication details to be added.</param>
        /// <returns>
        /// HTTP 200 with status and result if successful;  
        /// HTTP 500 with error details if an exception occurs.
        /// </returns>
        /// <remarks>
        /// Author: Khairi Abu Bakar  
        /// Date: 19 September 2025  
        /// Purpose: Provides a POST endpoint to persist implication data related to job applications, 
        /// enabling structured logging and consistent error reporting for external consumers.
        /// </remarks>
        [HttpPost("tambah")]
        public async Task<ActionResult> TambahImplikasiPermohonanJawatan([FromBody] TambahImplikadiPermohonanJawatanRequestDto request)
        {
            _logger.LogInformation("Calling TambahImplikasiPermohonanJawatan");

            try
            {
                var data = await _implikasipermohonanjawatanext.TambahImplikasiPermohonanJawatan(request);

                return Ok(new
                {
                    status = data != null ? "Berjaya" : "Gagal",
                    items = data
                });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? string.Empty;
                _logger.LogError(ex, "Error in TambahImplikasiPermohonanJawatan");

                return StatusCode(500, new
                {
                    status = "Gagal",
                    message = $"{ex.Message} - {innerMessage}"
                });
            }
        }

        /// <summary>
        /// Retrieves a list of implication records (Implikasi Permohonan Jawatan) that are marked as either "mansuh" or "wujud" for a given job application.
        /// </summary>
        /// <param name="IdPermohonanJawatan">The ID of the job application to filter implication records.</param>
        /// <returns>
        /// HTTP 200 with status and filtered implication records;  
        /// HTTP 500 with error details if an exception occurs.
        /// </returns>
        /// <remarks>
        /// Author: Khairi Abu Bakar  
        /// Date: 19 September 2025  
        /// Purpose: Provides a GET endpoint to retrieve implication records tied to a specific job application, 
        /// focusing on status flags such as "mansuh" or "wujud". Enables structured logging and consistent error reporting.
        /// </remarks>
        [HttpGet("senarai-mansuh-wujud")]
        public async Task<ActionResult> SenaraiMansuhWujudImplikasiPermohonanJawatan([FromQuery] int IdPermohonanJawatan)
        {
            _logger.LogInformation("Calling SenaraiMansuhWujudImplikasiPermohonanJawatan");

            try
            {
                var data = await _implikasipermohonanjawatanext.SenaraiMansuhWujudImplikasiPermohonanJawatan(IdPermohonanJawatan);

                return Ok(new
                {
                    status = data != null ? "Berjaya" : "Gagal",
                    items = data
                });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? string.Empty;
                _logger.LogError(ex, "Error in SenaraiMansuhWujudImplikasiPermohonanJawatan");

                return StatusCode(500, new
                {
                    status = "Gagal",
                    message = $"{ex.Message} - {innerMessage}"
                });
            }
        }

    }
}
