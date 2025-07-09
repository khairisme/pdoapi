using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HR.API.Controllers.PDO
{
    [Authorize]
    [ApiController]
    [Route("api/pdo/[controller]")]
    public class PermohonanPengisianSkimController : ControllerBase
    {
        private readonly IPermohonanPengisianSkimService _permohonanPengisianSkimService;
        private readonly ILogger<PermohonanPengisianSkimController> _logger;

        public PermohonanPengisianSkimController(IPermohonanPengisianSkimService permohonanPengisianSkimService, ILogger<PermohonanPengisianSkimController> logger)
        {
            _permohonanPengisianSkimService = permohonanPengisianSkimService;
            _logger = logger;
        }
        /// <summary>
        /// GetPegawaiTeknologiMaklumat
        /// </summary>
        /// <param name="IdSkimPerkhidmatan">IdSkimPerkhidmatan</param>
        /// <param name="IdPermohonanPengisianSkim">IdPermohonanPengisianSkim</param>
        /// <returns>Returns a list of  data matching the criteria</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid parameters provided</response>
        /// <response code="500">Internal server error occurred while processing the request</response>
        /// <remarks>
        /// 
        /// Both parameters are required for the search.
        /// 
        /// </remarks>
        [HttpGet("getPegawaiTeknologiMaklumat")]
        public async Task<IActionResult> GetPegawaiTeknologiMaklumat([FromQuery] int IdSkimPerkhidmatan, [FromQuery] int IdPermohonanPengisianSkim)
        {
            _logger.LogInformation("GetPegawaiTeknologiMaklumat: GetPegawaiTeknologiMaklumat method called from controller with IdSkimPerkhidmatan: {IdSkimPerkhidmatan}, IdPermohonanPengisian: {IdPermohonanPengisian}", IdSkimPerkhidmatan, IdPermohonanPengisianSkim);
            try
            {
                // Validate input parameters
                if (IdSkimPerkhidmatan <= 0 || IdPermohonanPengisianSkim <= 0)
                {
                    _logger.LogWarning("GetPegawaiTeknologiMaklumat: Invalid parameters - IdSkimPerkhidmatan: {IdSkimPerkhidmatan}, IdPermohonanPengisian: {IdPermohonanPengisian}", IdSkimPerkhidmatan, IdPermohonanPengisianSkim);
                    return BadRequest(new
                    {
                        status = "Error",
                        message = "Invalid parameters. Both IdSkimPerkhidmatan and IdPermohonanPengisian must be greater than 0.",
                        items = new List<PegawaiTeknologiMaklumatResponseDto>()
                    });
                }

                var data = await _permohonanPengisianSkimService.GetPegawaiTeknologiMaklumat(IdSkimPerkhidmatan, IdPermohonanPengisianSkim);

                _logger.LogInformation("GetPegawaiTeknologiMaklumat: Successfully retrieved {Count} records", data.Count);

                return Ok(new
                {
                    status = data.Count > 0 ? "Success" : "Failed",
                    items = data
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetPegawaiTeknologiMaklumat: Error occurred in controller while processing request with IdSkimPerkhidmatan: {IdSkimPerkhidmatan}, IdPermohonanPengisian: {IdPermohonanPengisian}", IdSkimPerkhidmatan, IdPermohonanPengisianSkim);

                return StatusCode(500, new
                {
                    status = "Error",
                    message = "An error occurred while retrieving data.",
                    items = new List<PegawaiTeknologiMaklumatResponseDto>()
                });
            }
        }
    }
}
