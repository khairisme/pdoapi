using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Services.PDO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HR.PDO.API.Controllers.PDO
{
   // [Authorize]
    [ApiController]
    [Route("api/pdo/v1/[controller]")]
    public class RujStatusKekosonganJawatanController : ControllerBase
    {
        private readonly IRujStatusKekosonganJawatanService _rujStatusKekosonganJawatanService;
        private readonly ILogger<RujStatusKekosonganJawatanController> _logger;

        public RujStatusKekosonganJawatanController(IRujStatusKekosonganJawatanService rujStatusKekosonganJawatanService, ILogger<RujStatusKekosonganJawatanController> logger)
        {
            _rujStatusKekosonganJawatanService = rujStatusKekosonganJawatanService;
            _logger = logger;
        }
        /// <summary>
        /// GetStatusKekosonganJawatan
        /// </summary>
        /// <returns>Returns a list of data</returns>
        /// <response code="200">Success</response>
        /// <response code="500">Internal server error occurred while processing the request</response>
        /// <remarks>
        /// This API may change as query is still not finalized.
        /// 
        /// </remarks>
        [HttpGet("getStatusKekosonganJawatan")]
        public async Task<IActionResult> GetStatusKekosonganJawatan()
        {
            _logger.LogInformation("GetStatusKekosonganJawatan: GetStatusKekosonganJawatan method called from controller");
            try
            {
                var data = await _rujStatusKekosonganJawatanService.GetStatusKekosonganJawatan();

                _logger.LogInformation("GetStatusKekosonganJawatan: Successfully retrieved {Count} records", data.Count());

                return Ok(new
                {
                    status = data.Count() > 0 ? "Berjaya" : "Gagal",
                    items = data
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetStatusKekosonganJawatan: Error occurred in controller while processing request");

                return StatusCode(500, new
                {
                    status = "Error",
                    items = new List<RujStatusKekosonganJawatanResponseDto>()
                });
            }
        }
    }
}
