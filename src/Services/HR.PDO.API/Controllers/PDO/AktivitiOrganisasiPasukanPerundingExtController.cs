using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.Interfaces.PDO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using  Shared.Contracts.DTOs;
using Swashbuckle.AspNetCore.Annotations;
/// <summary>
/// Created By: Khairi Abu Bakar  
/// Date: 19/09/2025  
/// Purpose: API controller for Aktiviti Organisasi Pasukan Perunding under the PDO module.  
///          Handles routing and delegates business logic to IAktivitiOrganisasiPasukanPerundingExt service.  
///          Uses ILogger for structured logging of operations and error tracking.
/// </summary>
namespace HR.PDO.API.Controllers.PDO
{
    [ApiController]
    [Route("api/pdo/v1/aktiviti-organisasi-pasukan-perunding")]
    public class AktivitiOrganisasiParukanPerundingExtController : ControllerBase
    {
        private readonly ILogger<AktivitiOrganisasiParukanPerundingExtController> _logger;
        private readonly IAktivitiOrganisasiPasukanPerundingExt _aktivitiOrganisasiPasukanPerundingExt;

        /// <summary>
        /// Constructor for AktivitiOrganisasiParukanPerundingExtController.
        /// Injects service and logger dependencies for handling business logic and diagnostics.
        /// </summary>
        /// <param name="aktivitiOrganisasiPasukanPerundingExt">Service layer for Aktiviti Organisasi Pasukan Perunding operations</param>
        /// <param name="logger">Logger instance for structured logging</param>
        public AktivitiOrganisasiParukanPerundingExtController(
            IAktivitiOrganisasiPasukanPerundingExt aktivitiOrganisasiPasukanPerundingExt,
            ILogger<AktivitiOrganisasiParukanPerundingExtController> logger)
        {
            _aktivitiOrganisasiPasukanPerundingExt = aktivitiOrganisasiPasukanPerundingExt;
            _logger = logger;
        }

        /// <summary>
        /// Created By: Khairi Abu Bakar  
        /// Date: 19/09/2025  
        /// Purpose: Retrieves the full list of Aktiviti Organisasi Pasukan Perunding records.  
        ///          Logs the operation, handles exceptions, and returns a structured response with status and data.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> SenaraiAktivitiOrganisasiPasukanPerunding()
        {
            try
            {
                _logger.LogInformation("Getting all RujJenisSaraan");

                var result = await _aktivitiOrganisasiPasukanPerundingExt.SenaraiAktivitiOrganisasiPasukanPerunding();

                return Ok(new
                {
                    status = result.Count() > 0 ? "Berjaya" : "Gagal",
                    items = result
                });
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in RujukanJenisJawatan");
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
        /// Purpose: Adds a new Aktiviti Organisasi Pasukan Perunding record based on the provided request DTO.  
        ///          Logs the operation, handles exceptions, and returns a structured response indicating success or failure.
        /// </summary>
        /// <param name="request">Request payload containing Aktiviti Organisasi Pasukan Perunding details</param>
        /// <returns>HTTP 200 with result if successful; HTTP 500 with error details if failed</returns>
        [HttpPost]
        public async Task<IActionResult> TambahAktivitiOrganisasiPasukanPerunding([FromBody] TambahAktivitiOrganisasiPasukanPerundingRequestDto request)
        {
            try
            {
                _logger.LogInformation("Processing TambahAktivitiOrganisasiPasukanPerunding request");

                var result = await _aktivitiOrganisasiPasukanPerundingExt.TambahAktivitiOrganisasiPasukanPerunding(request);

                return Ok(new
                {
                    status = result != null ? "Berjaya" : "Gagal",
                    item = result
                });
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in TambahAktivitiOrganisasiPasukanPerunding");
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
        /// Purpose: Permanently deletes an Aktiviti Organisasi Pasukan Perunding record based on the provided request DTO.  
        ///          Logs the operation, handles exceptions, and returns a structured response indicating success or failure.
        /// </summary>
        /// <param name="request">Request payload containing the target Aktiviti Organisasi Pasukan Perunding details for deletion</param>
        /// <returns>HTTP 200 with deletion status; HTTP 500 with error details if operation fails</returns>
        [HttpDelete]
        public async Task<IActionResult> HapusTerusAktivitiOrganisasiPasukanPerunding([FromBody] TambahAktivitiOrganisasiPasukanPerundingRequestDto request)
        {
            try
            {
                _logger.LogInformation("Processing HapusTerusAktivitiOrganisasiPasukanPerunding request");

                var result = await _aktivitiOrganisasiPasukanPerundingExt.HapusTerusAktivitiOrganisasiPasukanPerunding(request);

                return Ok(new
                {
                    status = result == true ? "Berjaya" : "Gagal",
                    item = result
                });
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in HapusTerusAktivitiOrganisasiPasukanPerunding");
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
