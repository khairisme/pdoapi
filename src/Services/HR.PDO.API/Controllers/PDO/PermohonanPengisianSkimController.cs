using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.Interfaces.PDO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HR.API.Controllers.PDO
{
    //[Authorize]
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
        /// This API may change as query is still not finalized.
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

        /// <summary>
        /// GetBilanganPengisianHadSiling
        /// </summary>
        /// <param name="IdPermohonanPengisian">IdPermohonanPengisian</param>
        /// <param name="IdPermohonanPengisianSkim">IdPermohonanPengisianSkim</param>
        /// <returns>Returns aggregated data showing total JumlahBilanganPengisian and HadSilingDitetapkan</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid parameters provided</response>
        /// <response code="500">Internal server error occurred while processing the request</response>
        /// <remarks>
        /// This API may change as query is still not finalized.
        /// 
        /// </remarks>
        [HttpGet("getBilanganPengisianHadSiling")]

        public async Task<IActionResult> GetBilanganPengisianHadSiling([FromQuery] int IdPermohonanPengisian, [FromQuery] int IdPermohonanPengisianSkim)
        {
            _logger.LogInformation("GetBilanganPengisianHadSiling: GetBilanganPengisianHadSiling method called from controller with IdPermohonanPengisian: {IdPermohonanPengisian}, IdPermohonanPengisianSkim: {IdPermohonanPengisianSkim}", IdPermohonanPengisian, IdPermohonanPengisianSkim);
            try
            {
                // Validate input parameters
                if (IdPermohonanPengisian <= 0 || IdPermohonanPengisianSkim <= 0)
                {
                    _logger.LogWarning("GetBilanganPengisianHadSiling: Invalid parameters - IdPermohonanPengisian: {IdPermohonanPengisian}, IdPermohonanPengisianSkim: {IdPermohonanPengisianSkim}", IdPermohonanPengisian, IdPermohonanPengisianSkim);
                    return BadRequest(new
                    {
                        status = "Error",
                        message = "Invalid parameters. Both IdPermohonanPengisian and IdPermohonanPengisianSkim must be greater than 0.",
                        data = new BilanganPengisianHadSilingResponseDto()
                    });
                }

                var data = await _permohonanPengisianSkimService.GetBilanganPengisianHadSiling(IdPermohonanPengisian, IdPermohonanPengisianSkim);

                _logger.LogInformation("GetBilanganPengisianHadSiling: Successfully retrieved BilanganPengisian summary");

                return Ok(new
                {
                    status = (data.JumlahBilanganPengisian > 0 || data.HadSilingDitetapkan > 0) ? "Success" : "Failed",
                    data = data
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBilanganPengisianHadSiling: Error occurred in controller while processing request with IdPermohonanPengisian: {IdPermohonanPengisian}, IdPermohonanPengisianSkim: {IdPermohonanPengisianSkim}", IdPermohonanPengisian, IdPermohonanPengisianSkim);

                return StatusCode(500, new
                {
                    status = "Error",
                    message = "An error occurred while retrieving data.",
                    data = new BilanganPengisianHadSilingResponseDto()
                });
            }
        }

        //Nitya Code Start
        /// <summary>
        /// GetBilanganPengisian
        /// </summary>
        /// <param name="IdPermohonanPengisian">IdPermohonanPengisian</param>

        /// <returns>Returns aggregated data showing total JumlahBilanganPengisian and HadSilingDitetapkan</returns>

        /// <remarks>
        /// 
        /// 
        /// </remarks>
        [HttpGet("getBilanganPengisian/{idPermohonanPengisian}")]
        public async Task<IActionResult> GetBilanganPengisian(int idPermohonanPengisian)
        {
            _logger.LogInformation("Fetching count for IdPermohonanPengisian: {id}", idPermohonanPengisian);

            try
            {
                int count = await _permohonanPengisianSkimService.GetBilanganPengisianByIdAsync(idPermohonanPengisian);

                return Ok(new
                {
                    status = "Success",
                    bilangan = count
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting bilangan pengisian for Id: {id}", idPermohonanPengisian);

                return StatusCode(500, new
                {
                    status = "Error",
                    bilangan = 0
                });
            }
        }
        /// <summary>
        /// updateUlasanAndHadSiling
        /// </summary>
        /// <param name="request">IdPermohonanPengisian</param>

        /// <returns>Returns aggregated data showing total JumlahBilanganPengisian and HadSilingDitetapkan</returns>

        /// <remarks>
        /// 
        /// 
        /// </remarks>
        [HttpPost("updateUlasanAndHadSiling")]
        public async Task<IActionResult> UpdateUlasanAndHadSiling([FromBody] CombinedUpdateRequestDto request)
        {
            try
            {
                var result = await _permohonanPengisianSkimService.UpdateUlasanAndHadSilingAsync(request);
                return Ok(new { status = result ? "Success" : "Failed" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating ulasan and grid data");
                return StatusCode(500, new { status = "Error" });
            }
        }
        //Nitya Code End


        /// <summary>
        /// GetJumlahDanSiling
        /// </summary>
        /// <param name="request">PaparPermohonanDanSilingRequestDto containing IdPermohonanPengisian and IdPermohonanPengisianSkim</param>
        /// <returns>Returns aggregated data showing total JumlahBilanganPengisian and HadSilingDitetapkan</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid parameters provided</response>
        /// <response code="500">Internal server error occurred while processing the request</response>
        /// <remarks>
        /// This API may change as query is still not finalized.
        /// 
        /// </remarks>
        [HttpPost("getJumlahDanSiling")]

        public async Task<IActionResult> GetJumlahDanSiling([FromBody] PaparPermohonanDanSilingRequestDto request)
        {
            _logger.LogInformation("GetJumlahDanSiling: GetJumlahDanSiling method called from controller with  : {@Request}", request );
            try
            {

                var data = await _permohonanPengisianSkimService.GetJumlahDanSilingAsync(request);

                _logger.LogInformation("GetBilanganPengisianHadSiling: Successfully retrieved BilanganPengisian summary");
                if (data == null)
                {
                    _logger.LogWarning("GetJumlahDanSiling: No data found for IdPermohonanPengisian: {IdPermohonanPengisian}, IdPermohonanPengisianSkim: {IdPermohonanPengisianSkim}", request.IdPermohonanPengisian, request.IdPermohonanPengisianSkim);
                    return NotFound(new
                    {
                        status = "Failed",
                       
                        data = new PaparPermohonanDanSilingResponseDto()
                    });
                }
                return Ok(new
                {
                    status = (data.JumlahBilanganPengisian > 0 || data.HadSilingDitetapkan > 0) ? "Success" : "Failed",
                    data = data
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetJumlahDanSiling: Error occurred in controller while processing ", request);

                return StatusCode(500, new
                {
                    status = "Error",
                    message = "An error occurred while retrieving data.",
                    data = new PaparPermohonanDanSilingResponseDto()
                });
            }
        }


    }
}
