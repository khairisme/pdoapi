using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.API.Controllers.PDO
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PengisianJawatanController : ControllerBase
    {
        private readonly IPengisianJawatanService _pengisianJawatanService;
        private readonly ILogger<PengisianJawatanController> _logger;

        public PengisianJawatanController(IPengisianJawatanService pengisianJawatanService, ILogger<PengisianJawatanController> logger)
        {
            _pengisianJawatanService = pengisianJawatanService;
            _logger = logger;
        }

        #region Public API Methods
        /// <summary>
        /// GetPengisianJawatan
        /// </summary>
        /// <param name="idSkimPerkhidmatan"></param>
        /// <returns></returns>

        [HttpGet("GetPengisianJawatan/{idSkimPerkhidmatan}")]
        public async Task<IActionResult> GetPengisianJawatan(int idSkimPerkhidmatan)
        {
            try
            {
                var result = await _pengisianJawatanService.GetPengisianJawatanAsync(idSkimPerkhidmatan);
                if (result == null)
                    return NotFound($"No Pengisian Jawatan found for idSkimPerkhidmatan {idSkimPerkhidmatan}");

                return Ok(new
                {
                    status = result.Count > 0 ? "Success" : "Failed",
                    items = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during GetPengisianJawatanAsync");
                return StatusCode(500, "Internal Server Error");
            }
        }
        /// <summary>
        /// GetPengisianJawatanCount
        /// </summary>
        /// <param name="idSkimPerkhidmatan"></param>
        /// <returns></returns>

        [HttpGet("GetPengisianJawatanCount/{idSkimPerkhidmatan}")]
        public async Task<IActionResult> GetPengisianJawatanCount(int idSkimPerkhidmatan)
        {
            try
            {
                var result = await _pengisianJawatanService.GetPengisianJawatanCountAsync(idSkimPerkhidmatan);
                if (result == 0)
                    return NotFound($"No Pengisian Jawatan Count found for idSkimPerkhidmatan {idSkimPerkhidmatan}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during GetPengisianJawatanAsync");
                return StatusCode(500, "Internal Server Error");
            }
        }
        

       
        // Nitya Code Start
        /// <summary>
        /// Delete pengisian jawatan
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>ActionResult with success status</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting Pengisian Jawatan with ID: {Id} using Entity Framework Core", id);

            var result = await _pengisianJawatanService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        /// <summary>
        /// Create pengisian jawatan
        /// </summary>
        /// <param name="pengisianJawatanDtos">List of pengisian jawatan data transfer objects</param>
        /// <returns>ActionResult with success status</returns>

        [HttpPost("newPengisianJawatan")]
        public async Task<IActionResult> Create([FromBody] List<PengisianJawatanDto> pengisianJawatanDtos)
        {
            _logger.LogInformation("Creating new PengisianJawatan records");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isSuccess = await _pengisianJawatanService.CreateAsync(pengisianJawatanDtos);

            return Ok(new { status = isSuccess ? "Success" : "Failed", items = isSuccess });
        }

        /// <summary>
        /// Get AgensiWithSkim
        /// </summary>
        /// <param name="pengisianJawatanDtos">List of pengisian jawatan data transfer objects</param>
        /// <returns>ActionResult with success status</returns>

        [HttpGet("getAgensiWithSkim")]
        public async Task<IActionResult> GetAgensiWithSkim()
        {
            _logger.LogInformation("Fetching Agensi with Skim Pengisian");

            try
            {
                var result = await _pengisianJawatanService.GetAgensiWithSkimPengisianAsync();

                return Ok(new
                {
                    status = result.Any() ? "Success" : "No Data",
                    items = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Agensi with Skim Pengisian");

                return StatusCode(500, new
                {
                    status = "Error",
                    items = new List<AgensiWithSkimDto>()
                });
            }
        }

        /// <summary>
        /// Get getPermohonanDetailById
        /// </summary>
        /// <param name="idPermohonan">List of pengisian jawatan data transfer objects</param>
        /// <returns>ActionResult with success status</returns>
        [HttpGet("getPermohonanDetailById/{idPermohonan}")]
        public async Task<IActionResult> GetPermohonanDetailById(int idPermohonan)
        {
            _logger.LogInformation("Fetching permohonan detail for ID: {id}", idPermohonan);

            try
            {
                var result = await _pengisianJawatanService.GetPermohonanDetailByIdAsync(idPermohonan);

                return Ok(new
                {
                    status = result != null ? "Success" : "No Data",
                    item = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching permohonan detail for ID: {id}", idPermohonan);
                return StatusCode(500, new
                {
                    status = "Error",
                    item = new PermohonanDetailDto()
                });
            }
        }
        /// <summary>
        /// Get GetSenaraiJawatanUntukPengisian
        /// </summary>
        /// <param name="idSkimPerkhidmatan">List of pengisian jawatan data transfer objects</param>
        /// <returns>ActionResult with success status</returns>
        [HttpGet("GetSenaraiJawatanUntukPengisian")]
        public async Task<IActionResult> GetSenaraiJawatanUntukPengisian([FromQuery] int idSkimPerkhidmatan)
        {
            try
            {
                var result = await _pengisianJawatanService.GetSenaraiJawatanUntukPengisian(idSkimPerkhidmatan);
                return Ok(new
                {
                    status = result.Any() ? "Success" : "Not Found",
                    items = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching Senarai Jawatan Untuk Pengisian for SkimId: {idSkim}", idSkimPerkhidmatan);
                return StatusCode(500, new
                {
                    status = "Error",
                    items = new List<SenaraiJawatanPengisianDto>()
                });
            }
        }
        /// <summary>
        /// Get GetSenaraiJawatanSebenar
        /// </summary>

        /// <returns>ActionResult with success status</returns>
        [HttpGet("GetSenaraiJawatanSebenar")]
        public async Task<IActionResult> GetSenaraiJawatanSebenar()
        {
            try
            {
                var result = await _pengisianJawatanService.GetSenaraiJawatanSebenarAsync();
                return Ok(new
                {
                    status = result.Any() ? "Success" : "Not Found",
                    items = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSenaraiJawatanSebenar");
                return StatusCode(500, new
                {
                    status = "Error",
                    items = new List<UnitOrganisasiDataDto>()
                });
            }
        }
        // Nitya Code End
        #endregion

    }
}