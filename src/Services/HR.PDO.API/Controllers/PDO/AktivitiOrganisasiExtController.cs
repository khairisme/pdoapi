using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO {
    [ApiController]
    [Route("api/pdo/v1/aktiviti-organisasi")]
    public class AktivitiOrganisasiExtController : ControllerBase
    {
        private readonly ILogger<AktivitiOrganisasiExtController> _logger;
        private readonly IAktivitiOrganisasiExt _aktivitiorganisasiext;

        /// <summary>
        /// Controller for managing Aktiviti Organisasi extended operations.
        /// </summary>
        /// <param name="aktivitiorganisasiext">
        /// The service contract for Aktiviti Organisasi operations.
        /// </param>
        /// <param name="logger">
        /// The logger instance for capturing diagnostic and error information.
        /// </param>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Exposes endpoints for AktivitiOrganisasiExt, using dependency injection
        ///               for service and logging.
        /// </remarks>
        public AktivitiOrganisasiExtController(
            IAktivitiOrganisasiExt aktivitiorganisasiext,
            ILogger<AktivitiOrganisasiExtController> logger)
        {
            _aktivitiorganisasiext = aktivitiorganisasiext;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the details of a specific AktivitiOrganisasi (Organizational Activity) by its unique ID.
        /// </summary>
        /// <param name="Id">
        /// The unique identifier of the AktivitiOrganisasi to retrieve.
        /// </param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing the <see cref="AktivitiOrganisasiDto"/> 
        /// if found, or an error response if the operation fails.
        /// </returns>
        [HttpGet("baca/{Id}")]
        [ProducesResponseType(typeof(AktivitiOrganisasiDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AktivitiOrganisasiDto>> BacaAktivitiOrganisasi(int Id)
        {
            _logger.LogInformation("Calling BacaAktivitiOrganisasi with Id: {Id}", Id);

            try
            {
                var data = await _aktivitiorganisasiext.BacaAktivitiOrganisasi(Id);

                if (data == null)
                {
                    _logger.LogWarning("AktivitiOrganisasi with Id {Id} not found", Id);
                    return NotFound($"AktivitiOrganisasi with Id {Id} not found.");
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                var err = ex.InnerException?.Message ?? string.Empty;
                _logger.LogError(ex, "Error in BacaAktivitiOrganisasi for Id {Id}", Id);

                return StatusCode(500, ex.Message + " - " + err);
            }
        }
        /// <summary>
        /// Creates a new Aktiviti Organisasi record.
        /// Best practice: use [FromBody] for POST because the payload is complex 
        /// and should be passed as JSON in the request body.
        /// </summary>
        [HttpPost("tambah")]
        public async Task<ActionResult> WujudAktivitiOrganisasiBaru(
            [FromBody] WujudAktivitiOrganisasiRequestDto request, // DTO bound from request body (JSON payload)
            CancellationToken ct = default) // Support cancellation tokens for long-running tasks
        {
            _logger.LogInformation("Calling WujudAktivitiOrganisasiBaru");

            try
            {
                // Call the service/extension layer to handle the actual business logic
                await _aktivitiorganisasiext.WujudAktivitiOrganisasiBaru(request);

                // Best practice: return 201 Created for POST requests
                // CreatedAtAction should point to a GET method that can retrieve the created resource.
                // If such a GET endpoint does not exist, consider returning Ok() or a custom response.
                return CreatedAtAction(nameof(WujudAktivitiOrganisasiBaru), new { request }, null);
            }
            catch (Exception ex)
            {
                string err = string.Empty;

                // Best practice: log full exception details
                _logger.LogError(ex, "Error in WujudAktivitiOrganisasiBaru");

                if (ex.InnerException != null)
                {
                    err = ex.InnerException.Message;
                }

                // Best practice: avoid leaking sensitive exception details directly to the client.
                // Instead, return a standard error response object (e.g., { status, message }) 
                // while logging the internal exception details for troubleshooting.
                return StatusCode(500, ex.Message + "-" + err);
            }
        }
        /// <summary>
        /// Rename an existing Aktiviti Organisasi (organizational activity).
        /// </summary>
        /// <param name="request">
        /// The request payload containing:
        /// <list type="bullet">
        /// <item><description><c>UserId</c>: The user performing the rename.</description></item>
        /// <item><description><c>Id</c>: The identifier of the activity to rename.</description></item>
        /// <item><description><c>Nama</c>: The new name for the activity.</description></item>
        /// </list>
        /// </param>
        /// <returns>
        /// <para><c>201 Created</c> if the activity was successfully renamed.</para>
        /// <para><c>500 Internal Server Error</c> if an exception occurs.</para>
        /// </returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     PUT /penjenamaan-semula/{Id}
        ///     {
        ///        "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///        "id": 123,
        ///        "nama": "Aktiviti Baru"
        ///     }
        /// 
        /// </remarks>
        [HttpPut("penjenamaan-semula/{Id}")]
        public async Task<ActionResult> PenjenamaanAktivitiOrganisasi([FromBody] PenjenamaanAktivitiOrganisasiRequestDto request)
        {
            _logger.LogInformation("Calling PenjenamaanAktivitiOrganisasi");
            try
            {
                await _aktivitiorganisasiext.PenjenamaanAktivitiOrganisasi(request);
                return CreatedAtAction(nameof(PenjenamaanAktivitiOrganisasi), new { request }, null);
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in PenjenamaanAktivitiOrganisasi");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

        /// <summary>
        /// Move an existing Aktiviti Organisasi (organizational activity) to a new parent node.
        /// </summary>
        /// <param name="request">
        /// The request payload containing:
        /// <list type="bullet">
        /// <item><description><c>UserId</c>: The user performing the move.</description></item>
        /// <item><description><c>Id</c>: The identifier of the activity being moved.</description></item>
        /// <item><description><c>NewParentId</c>: The identifier of the new parent node.</description></item>
        /// <item><description><c>OldParentId</c>: The identifier of the old parent node.</description></item>
        /// </list>
        /// </param>
        /// <returns>
        /// <para><c>201 Created</c> if the activity was successfully moved.</para>
        /// <para><c>500 Internal Server Error</c> if an exception occurs.</para>
        /// </returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     PUT /pindah/{Id}
        ///     {
        ///        "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///        "id": 123,
        ///        "newParentId": 45,
        ///        "oldParentId": 12
        ///     }
        /// 
        /// </remarks>
        [HttpPut("pindah/{Id}")]
        public async Task<ActionResult> PindahAktivitiOrganisasi([FromBody] PindahAktivitiOrganisasiRequestDto request)
        {
            _logger.LogInformation("Calling PindahAktivitiOrganisasi");
            try
            {
                await _aktivitiorganisasiext.PindahAktivitiOrganisasi(request);
                return CreatedAtAction(nameof(PindahAktivitiOrganisasi), new { request }, null);
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in PindahAktivitiOrganisasi");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

        /// <summary>
        /// Deactivates (soft deletes / retires) an Aktiviti Organisasi entity.
        /// </summary>
        /// <param name="request">The request payload containing UserId and entity Id.</param>
        /// <returns>
        /// Returns <see cref="CreatedAtActionResult"/> if successful, otherwise an error status code.  
        /// </returns>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Marks an AktivitiOrganisasi entity as inactive (soft delete) instead of 
        ///               permanently removing it. Provides safer handling for audit trail.  
        /// </remarks>
        [HttpDelete("mansuh/{Id}")]
        public async Task<ActionResult> MansuhAktivitiOrganisasi([FromBody] MansuhAktivitiOrganisasiRequestDto request)
        {
            _logger.LogInformation("Calling MansuhAktivitiOrganisasi");

            try
            {
                await _aktivitiorganisasiext.MansuhAktivitiOrganisasi(request);

                return CreatedAtAction(
                    nameof(MansuhAktivitiOrganisasi),
                    new { request },
                    null
                );
            }
            catch (Exception ex)
            {
                string err = ex.InnerException?.Message ?? "";
                _logger.LogError(ex, "Error in MansuhAktivitiOrganisasi");

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

        /// <summary>
        /// Permanently deletes an AktivitiOrganisasi entity.  
        /// </summary>
        /// <param name="request">The request payload containing UserId and Id of the entity to delete.</param>
        /// <returns>HTTP 200 if successful, or an error response.</returns>
        /// <remarks>
        /// Best practice:
        /// - Use DTO for consistent API schema.  
        /// - DELETE should normally take the Id from the route, but we allow a body here since we need UserId for auditing.  
        /// - Returns 200 OK instead of CreatedAtAction, because DELETE does not create resources.  
        /// </remarks>
        [HttpDelete("hapus-terus")]
        public async Task<ActionResult> HapusTerusAktivitiOrganisasi([FromBody] HapusTerusAktivitiOrganisasiRequestDto request)
        {
            _logger.LogInformation("Calling HapusTerusAktivitiOrganisasi");

            try
            {
                await _aktivitiorganisasiext.HapusTerusAktivitiOrganisasi(request);
                return Ok(new { message = "AktivitiOrganisasi deleted successfully.", request.Id });
            }
            catch (Exception ex)
            {
                var err = ex.InnerException?.Message ?? string.Empty;
                _logger.LogError(ex, "Error in HapusTerusAktivitiOrganisasi for UserId: {UserId}, Id: {Id}", request?.UserId, request?.Id);

                return StatusCode(500, ex.Message + (string.IsNullOrEmpty(err) ? "" : $" - {err}"));
            }
        }

        /// <summary>
        /// Retrieves the hierarchical structure of AktivitiOrganisasi based on optional filters.
        /// </summary>
        /// <param name="parentId">Optional parent node ID to filter the structure.</param>
        /// <param name="includeInactive">Whether to include inactive activities.</param>
        /// <returns>A structured list of AktivitiOrganisasi nodes.</returns>
        [HttpPost("struktur")]
        [ProducesResponseType(typeof(IEnumerable<StrukturAktivitiOrganisasiDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StrukturAktivitiOrganisasiDto>>> StrukturAktivitiOrganisasi(
            [FromBody] StrukturAktivitiOrganisasiRequestDto request)
        {
            _logger.LogInformation("Calling StrukturAktivitiOrganisasi with ParentId={ParentId}", request.parentId);

            try
            {

                var data = await _aktivitiorganisasiext.StrukturAktivitiOrganisasi(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var err = ex.InnerException?.Message ?? string.Empty;
                _logger.LogError(ex, "Error in StrukturAktivitiOrganisasi");

                return StatusCode(500, ex.Message + " - " + err);
            }
        }

    }
}
