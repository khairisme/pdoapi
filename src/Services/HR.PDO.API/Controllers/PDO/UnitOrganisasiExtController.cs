using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using  Shared.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
namespace HR.PDO.API.Controllers.PDO
{
    [ApiController]
    [Route("api/pdo/v1/unit-organisasi")]
    public class UnitOrganisasiExtController : ControllerBase
    {
        private readonly ILogger<UnitOrganisasiExtController> _logger;
        private readonly IUnitOrganisasiExt _unitorganisasiext;

        public UnitOrganisasiExtController(IUnitOrganisasiExt unitorganisasiext, ILogger<UnitOrganisasiExtController> logger)
        {
            _unitorganisasiext = unitorganisasiext;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the hierarchical structure of Unit Organisasi (Organizational Units)
        /// using filtering, pagination, and sorting options provided in the request payload.
        /// </summary>
        /// <param name="request">
        /// The request payload containing filtering, pagination, and sorting options.
        /// </param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing a collection of 
        /// <see cref="StrukturUnitOrganisasiDto"/> objects if successful, 
        /// or an error response if the operation fails.
        /// </returns>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Provides a clean endpoint for fetching Unit Organisasi hierarchy 
        ///               with support for paging, filtering, and sorting.
        /// </remarks>
        [HttpPost("struktur")]
        [ProducesResponseType(typeof(IEnumerable<StrukturUnitOrganisasiDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StrukturUnitOrganisasiDto>>> StrukturUnitOrganisasi(
            [FromBody] StrukturUnitOrganisasiRequestDto request)
        {
            _logger.LogInformation("Calling StrukturUnitOrganisasi with request: {@Request}", request);

            try
            {
                var data = await _unitorganisasiext.StrukturUnitOrganisasi(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var err = ex.InnerException?.Message ?? string.Empty;
                _logger.LogError(ex, "Error in StrukturUnitOrganisasi with request: {@Request}", request);
                return StatusCode(500, ex.Message + " - " + err);
            }
        }


        /// <summary>
        /// Updates an existing Unit Organisasi (Organizational Unit) with new details.
        /// </summary>
        /// <param name="request">
        /// The request payload containing:
        /// <list type="bullet">
        ///   <item><description><c>UserId</c>: The user performing the update.</description></item>
        ///   <item><description><c>Id</c>: The identifier of the Unit Organisasi to update.</description></item>
        ///   <item><description><c>DaftarDto</c>: The details of the Unit Organisasi to update.</description></item>
        /// </list>
        /// </param>
        /// <returns>
        /// Returns <c>201 Created</c> if successful, or <c>500 Internal Server Error</c> if an exception occurs.
        /// </returns>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Provides an endpoint for updating Unit Organisasi details with full payload validation.
        /// </remarks>
        [HttpPut("kemaskini")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> KemaskiniUnitOrganisasi(
            [FromBody] UnitOrganisasiDaftarDto request)
        {
            _logger.LogInformation("Calling KemaskiniUnitOrganisasi with request: {@Request}", request);

            try
            {
                await _unitorganisasiext.KemaskiniUnitOrganisasi(request);

                return CreatedAtAction(
                    nameof(KemaskiniUnitOrganisasi),
                    new { request },
                    null
                );
            }
            catch (Exception ex)
            {
                var err = ex.InnerException?.Message ?? string.Empty;
                _logger.LogError(ex, "Error in KemaskiniUnitOrganisasi with request: {@Request}", request);

                return StatusCode(500, ex.Message + " - " + err);
            }
        }

        /// <summary>
        /// Permanently deletes a Unit Organisasi entity.
        /// </summary>
        /// <param name="request">
        /// The request payload containing:
        /// <list type="bullet">
        ///   <item><description><c>UserId</c>: The user performing the delete.</description></item>
        ///   <item><description><c>Id</c>: The identifier of the Unit Organisasi to delete.</description></item>
        /// </list>
        /// </param>
        /// <returns>
        /// Returns <c>200 OK</c> if deletion is successful, otherwise an error status code.
        /// </returns>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Provides a safe endpoint to permanently remove a UnitOrganisasi entity with audit logging.
        /// </remarks>
        [HttpDelete("hapus-terus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> HapusTerusUnitOrganisasi(
            [FromBody] HapusTerusUnitOrganisasiRequestDto request)
        {
            _logger.LogInformation("Calling HapusTerusUnitOrganisasi with request: {@Request}", request);

            try
            {
                await _unitorganisasiext.HapusTerusUnitOrganisasi(request);
                return Ok(new { message = "UnitOrganisasi deleted successfully.", request.Id });
            }
            catch (Exception ex)
            {
                var err = ex.InnerException?.Message ?? string.Empty;
                _logger.LogError(ex, "Error in HapusTerusUnitOrganisasi with request: {@Request}", request);

                return StatusCode(500, ex.Message + (string.IsNullOrEmpty(err) ? "" : $" - {err}"));
            }
        }

        /// <summary>
        /// Searches Unit Organisasi entities based on the provided criteria.
        /// </summary>
        /// <param name="request">
        /// The request payload containing search parameters:
        /// <list type="bullet">
        ///   <item><description>Keyword, filters, or other criteria for searching Unit Organisasi.</description></item>
        /// </list>
        /// </param>
        /// <returns>
        /// Returns a list of <see cref="UnitOrganisasiDto"/> matching the search criteria.
        /// </returns>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Provides a search endpoint for UnitOrganisasi entities using a structured DTO payload.
        /// </remarks>
        [HttpPost("carian")]
        [ProducesResponseType(typeof(IEnumerable<UnitOrganisasiDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UnitOrganisasiDto>>> CarianUnitOrganisasi(
            [FromBody] UnitOrganisasiCarianDto request)
        {
            _logger.LogInformation("Calling CarianUnitOrganisasi with request: {@Request}", request);

            try
            {
                var data = await _unitorganisasiext.CarianUnitOrganisasi(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var err = ex.InnerException?.Message ?? string.Empty;
                _logger.LogError(ex, "Error in CarianUnitOrganisasi with request: {@Request}", request);
                return StatusCode(500, ex.Message + (string.IsNullOrEmpty(err) ? "" : $" - {err}"));
            }
        }

        /// <summary>
        /// Retrieves reference (lookup) data for Unit Organisasi, typically used in dropdowns or selection lists.
        /// </summary>
        /// <returns>
        /// A list of <see cref="DropDownDto"/> containing the reference information for Unit Organisasi.
        /// </returns>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar  
        /// Created On  : 2025-09-03  
        /// Purpose     : Provides a read-only endpoint for fetching UnitOrganisasi reference data for UI selection or other purposes.
        /// </remarks>
        [HttpGet("ruj")]
        [ProducesResponseType(typeof(IEnumerable<DropDownDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DropDownDto>>> RujukanUnitOrganisasi()
        {
            _logger.LogInformation("Calling RujukanUnitOrganisasi");

            try
            {
                var data = await _unitorganisasiext.RujukanUnitOrganisasi();
                return Ok(data);
            }
            catch (Exception ex)
            {
                var err = ex.InnerException?.Message ?? string.Empty;
                _logger.LogError(ex, "Error in RujukanUnitOrganisasi");
                return StatusCode(500, ex.Message + (string.IsNullOrEmpty(err) ? "" : $" - {err}"));
            }
        }

        /// <summary>
        /// Retrieves a list of UnitOrganisasi based on search/filter criteria.
        /// </summary>
        /// <param name="request">The search/filter criteria encapsulated in a DTO.</param>
        /// <returns>A list of UnitOrganisasi matching the search criteria.</returns>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar
        /// Created On  : 2025-09-03
        /// Purpose     : Provides a POST endpoint to fetch UnitOrganisasi list based on the given search criteria.
        ///               Using DTO allows future extension without changing method signature.
        /// </remarks>
        [HttpPost("senarai")]
        [ProducesResponseType(typeof(IEnumerable<UnitOrganisasiDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UnitOrganisasiDto>>> SenaraiUnitOrganisasi(
            [FromBody] UnitOrganisasiCarianDto request)
        {
            _logger.LogInformation("Calling SenaraiUnitOrganisasi with request: {@Request}", request);

            try
            {
                var data = await _unitorganisasiext.SenaraiUnitOrganisasi(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var err = ex.InnerException?.Message ?? string.Empty;
                _logger.LogError(ex, "Error in SenaraiUnitOrganisasi with request: {@Request}", request);
                return StatusCode(500, ex.Message + (string.IsNullOrEmpty(err) ? "" : $" - {err}"));
            }
        }

        /// <summary>
        /// Renames an existing UnitOrganisasi entity.
        /// </summary>
        /// <param name="request">
        /// The request payload containing:
        /// <list type="bullet">
        /// <item><description><c>UserId</c>: The user performing the rename.</description></item>
        /// <item><description><c>Id</c>: The identifier of the unit to rename.</description></item>
        /// <item><description><c>Nama</c>: The new name for the unit.</description></item>
        /// </list>
        /// </param>
        /// <returns>
        /// Returns <see cref="CreatedAtActionResult"/> if the rename was successful, otherwise an error status code.
        /// </returns>
        /// <remarks>
        /// Author      : Khairi bin Abu Bakar
        /// Created On  : 2025-09-03
        /// Purpose     : Provides a PATCH endpoint to rename a UnitOrganisasi using a DTO payload
        ///               for future extensibility and consistency across the API.
        /// </remarks>
        [HttpPatch("penjenamaan")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PenjenamaanSemulaUnitOrganisasi(
            [FromBody] PenjenamaanUnitOrganisasiDto request)
        {
            _logger.LogInformation("Calling PenjenamaanSemulaUnitOrganisasi with request: {@Request}", request);

            try
            {
                await _unitorganisasiext.PenjenamaanSemulaUnitOrganisasi(request);
                return CreatedAtAction(nameof(PenjenamaanSemulaUnitOrganisasi), new { request }, null);
            }
            catch (Exception ex)
            {
                var err = ex.InnerException?.Message ?? string.Empty;
                _logger.LogError(ex, "Error in PenjenamaanSemulaUnitOrganisasi with request: {@Request}", request);
                return StatusCode(500, ex.Message + (string.IsNullOrEmpty(err) ? "" : $" - {err}"));
            }
        }

        [HttpDelete("mansuh/{Id}")]
        public async Task<ActionResult> MansuhUnitOrganisasi([FromQuery] Guid UserId, int Id)
        {
            _logger.LogInformation("Calling MansuhAktivitiOrganisasi");
            try
            {
                await _unitorganisasiext.MansuhUnitOrganisasi(UserId, Id);
                return CreatedAtAction(nameof(MansuhUnitOrganisasi), new { UserId, Id }, null);
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in MansuhUnitOrganisasi");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }


        [HttpGet("baca/{Id}")]
        public async Task<ActionResult<object>> BacaUnitOrganisasi(int Id)
        {
            _logger.LogInformation("Calling BacaUnitOrganisasi");
            try
            {
                var data = await _unitorganisasiext.BacaUnitOrganisasi(Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in BacaUnitOrganisasi");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, ex.Message + "-" + err);
            }
        }

        [HttpPost("wujud")]
        public async Task<ActionResult> WujudUnitOrganisasiBaru([FromQuery] Guid UserId, UnitOrganisasiWujudDto request)
        {
            _logger.LogInformation("Calling WujudUnitOrganisasiBaru");
            try
            {
                await _unitorganisasiext.WujudUnitOrganisasiBaru(UserId, request);
                return CreatedAtAction(nameof(WujudUnitOrganisasiBaru), new { UserId, request }, null);
            }
            catch (Exception ex)
            {
                String err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in WujudUnitOrganisasiBaru");
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
