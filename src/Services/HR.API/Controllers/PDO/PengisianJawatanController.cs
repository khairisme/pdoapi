using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

                return Ok(result);
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
                if (result == null)
                    return NotFound($"No Pengisian Jawatan Count found for idSkimPerkhidmatan {idSkimPerkhidmatan}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during GetPengisianJawatanAsync");
                return StatusCode(500, "Internal Server Error");
            }
        }
        /// <summary>
        ///Create pengisianJawatan
        /// </summary>
        /// <param name="pengisianJawatanDto"></param>
        /// <returns></returns>
        [HttpPost("newPengisianJawatan")]
        public async Task<IActionResult> Create([FromBody] PengisianJawatanDto pengisianJawatanDto)
        {
            _logger.LogInformation("Creating a new PengisianJawatan");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isSuccess = await _pengisianJawatanService.CreateAsync(pengisianJawatanDto);

            return Ok(new
            {
                status = isSuccess ? "Sucess" : "Failed",
                items = isSuccess

            });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting Pengisian Jawatan with ID: {Id} using Entity Framework Core", id);

            var result = await _pengisianJawatanService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        #endregion

    }
}