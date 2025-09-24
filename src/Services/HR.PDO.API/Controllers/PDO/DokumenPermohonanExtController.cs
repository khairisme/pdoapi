using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using HR.PDO.Application.Interfaces.PDO;using HR.PDO.Application.DTOs;
/// <summary>
/// Created By: Khairi Abu Bakar  
/// Date: 19/09/2025  
/// Purpose: API controller for managing Dokumen Permohonan operations under the PDO module.  
///          Handles routing, delegates business logic to IDokumenPermohonanExt, and logs operational events.  
///          Also provides access to hosting environment via IWebHostEnvironment for file-related operations.
/// </summary>
namespace HR.PDO.API.Controllers.PDO
{
    [ApiController]
    [Route("api/pdo/v1/dokumen-permohonan")]
    public class DokumenPermohonanExtController : ControllerBase
    {
        private readonly ILogger<DokumenPermohonanExtController> _logger;
        private readonly IDokumenPermohonanExt _dokumenpermohonanext;
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Constructor for DokumenPermohonanExtController.  
        /// Injects service layer, logger, and hosting environment for executing business logic and file operations.
        /// </summary>
        /// <param name="env">Web hosting environment for accessing file paths and configuration</param>
        /// <param name="dokumenpermohonanext">Service layer for Dokumen Permohonan operations</param>
        /// <param name="logger">Logger instance for diagnostics and traceability</param>
        public DokumenPermohonanExtController(
            IWebHostEnvironment env,
            IDokumenPermohonanExt dokumenpermohonanext,
            ILogger<DokumenPermohonanExtController> logger)
        {
            _env = env;
            _dokumenpermohonanext = dokumenpermohonanext;
            _logger = logger;
        }
        /// <summary>
        /// Created By: Khairi Abu Bakar  
        /// Date: 19/09/2025  
        /// Purpose: Handles file upload for Dokumen Permohonan.  
        ///          Saves the uploaded file to the server's "uploads" directory and returns a public URL for access.  
        ///          Validates input and ensures the target directory exists before writing.
        /// </summary>
        /// <param name="file">File uploaded via multipart/form-data</param>
        /// <returns>HTTP 200 with public URL if successful; HTTP 400 if no file provided</returns>
        [HttpPost("muat-naik")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var publicUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
            return Ok(new { url = publicUrl });
        }
        /// <summary>
        /// Created By: Khairi Abu Bakar  
        /// Date: 19/09/2025  
        /// Purpose: Creates a new Dokumen Permohonan record using the provided request DTO.  
        ///          Executes business logic via IDokumenPermohonanExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="request">Payload containing metadata and configuration for the new Dokumen Permohonan</param>
        /// <returns>HTTP 200 with created record if successful; HTTP 500 with error details if operation fails</returns>
        [HttpPost]
        public async Task<ActionResult> WujudDokumenPermohonanBaru([FromBody] WujudDokumenPermohonanRequestDto request)
        {
            _logger.LogInformation("Calling WujudDokumenPermohonanBaru");
            try
            {
                var data = await _dokumenpermohonanext.WujudDokumenPermohonanBaru(request);
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
                    _logger.LogError(ex, "Error in WujudDokumenPermohonanBaru");
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
        /// Purpose: Permanently deletes a Dokumen Permohonan record based on the provided request DTO.  
        ///          Executes deletion logic via IDokumenPermohonanExt and returns a confirmation response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="request">Payload containing identifiers and metadata for the document to be deleted</param>
        /// <returns>HTTP 200 if deletion is successful; HTTP 500 with error details if operation fails</returns>
        [HttpDelete]
        public async Task<ActionResult> HapusTerusDokumenPermohonan([FromBody] HapusTerusDokumenPermohonanRequest request)
        {
            _logger.LogInformation("Calling HapusTerusDokumenPermohonan");
            try
            {
                await _dokumenpermohonanext.HapusTerusDokumenPermohonan(request);
                return Ok(new { status = "Berjaya", message = "Dokumen berjaya dihapuskan." });
            }
            catch (Exception ex)
            {
                string err = "";
                if (ex != null)
                {
                    _logger.LogError(ex, "Error in HapusTerusDokumenPermohonan");
                    if (ex.InnerException != null)
                    {
                        err = ex.InnerException.Message.ToString();
                    }
                }

                return StatusCode(500, new
                {
                    status = "Gagal",
                    message = ex.Message + " - " + err
                });
            }
        }

        /// <summary>
        /// Created By: Khairi Abu Bakar  
        /// Date: 19/09/2025  
        /// Purpose: Retrieves a list of Dokumen Permohonan records linked to a specific Permohonan Jawatan ID.  
        ///          Executes business logic via IDokumenPermohonanExt and returns a structured response.  
        ///          Logs the operation and handles exceptions with detailed error reporting.
        /// </summary>
        /// <param name="IdPermohonanJawatan">Unique identifier for the Permohonan Jawatan record</param>
        /// <returns>HTTP 200 with list of document links; HTTP 500 with error details if operation fails</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RujJenisDokumenLinkDto>>> SenaraiDokumenPermohonan(int IdPermohonanJawatan)
        {
            _logger.LogInformation("Calling SenaraiDokumenPermohonan");
            try
            {
                var data = await _dokumenpermohonanext.SenaraiDokumenPermohonan(IdPermohonanJawatan);
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
                    _logger.LogError(ex, "Error in SenaraiDokumenPermohonan");
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
