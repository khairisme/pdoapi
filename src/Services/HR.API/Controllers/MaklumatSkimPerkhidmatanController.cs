using HR.Application.DTOs;
using HR.Application.Interfaces;
using HR.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [ApiController]
    [Route("api/pdo/[controller]")]
    public class MaklumatSkimPerkhidmatanController : Controller
    {
        private readonly IMaklumatSkimPerkhidmatanService _maklumatSkimPerkhidmatan;
        private readonly ILogger<MaklumatSkimPerkhidmatanController> _logger;

        public MaklumatSkimPerkhidmatanController(IMaklumatSkimPerkhidmatanService maklumatSkimPerkhidmatan, ILogger<MaklumatSkimPerkhidmatanController> logger)
        {
            _maklumatSkimPerkhidmatan = maklumatSkimPerkhidmatan;
            _logger = logger;
        }
        /// <summary>
        ///Get GetSenaraiSkimPerkhidmatan
        /// </summary>
        /// <param name="MaklumatSkimPerkhidmatanFilterDto"></param>
        /// <returns></returns>
        [HttpPost("getSenaraiSkimPerkhidmatan")]
        public async Task<IActionResult> GetSenaraiSkimPerkhidmatan([FromBody] MaklumatSkimPerkhidmatanFilterDto filter)
        {
            _logger.LogInformation("Getting Carl Maklumat Kumpulan Perkhidmatan");
            try
            {
                var result = await _maklumatSkimPerkhidmatan.GetSenaraiSkimPerkhidmatan(filter);

                return Ok(new
                {
                    status = result.Count() > 0 ? "Sucess" : "Failed",
                    items = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during carl Maklumat Kumpulan Perkhidmatan");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        ///Create MaklumatKlasifikasiPerkhidmatanCreateRequestDto
        /// </summary>
        /// <param name="MaklumatKlasifikasiPerkhidmatanCreateRequestDto"></param>
        /// <returns></returns>
        [HttpPost("newSenaraiSkimPerkhidmatan")]
        public async Task<IActionResult> Create([FromBody] MaklumatSkimPerkhidmatanCreateRequestDto maklumatSkimPerkhidmatanCreateRequestDto)
        {
            _logger.LogInformation("Creating a new MaklumatSkimPerkhidmatan");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isSuccess = await _maklumatSkimPerkhidmatan.CreateAsync(maklumatSkimPerkhidmatanCreateRequestDto);

            return Ok(new
            {
                status = isSuccess ? "Sucess" : "Failed",
                items = isSuccess

            });

        }
    }
}

