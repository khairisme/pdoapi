using HR.Application.DTOs;
using HR.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [ApiController]
    [Route("api/pdo/[controller]")]
    public class MaklumatKlasifikasiPerkhidmatanController : ControllerBase
    {

        private readonly IMaklumatKlasifikasiPerkhidmatanService _maklumatKlasifikasiPerkhidmatanService;
        private readonly ILogger<MaklumatKlasifikasiPerkhidmatanController> _logger;

        public MaklumatKlasifikasiPerkhidmatanController(IMaklumatKlasifikasiPerkhidmatanService maklumatKlasifikasiPerkhidmatanService, ILogger<MaklumatKlasifikasiPerkhidmatanController> logger)
        {
            _maklumatKlasifikasiPerkhidmatanService = maklumatKlasifikasiPerkhidmatanService;
            _logger = logger;
        }

        /// <summary>
        ///Get Maklumat Klasifikasi Perkhidmatan
        /// </summary>
        /// <param name="MaklumatKlasifikasiPerkhidmatanDto"></param>
        /// <returns></returns>
        [HttpPost("getMaklumatKlasifikasiPerkhidmatan")]
        public async Task<IActionResult> getMaklumatKlasifikasiPerkhidmatan([FromBody] MaklumatKlasifikasiPerkhidmatanFilterDto filter)
        {
            _logger.LogInformation("Getting all MaklumatKlasifikasiPerkhidmatanDto");

            var result = await _maklumatKlasifikasiPerkhidmatanService.GetMaklumatKlasifikasiPerkhidmatan(filter);

            return Ok(new
            {
                status = result.Count() > 0 ? "Sucess" : "Failed",
                items = result

            });
        }

        /// <summary>
        ///Create MaklumatKlasifikasiPerkhidmatanCreateRequestDto
        /// </summary>
        /// <param name="MaklumatKlasifikasiPerkhidmatanCreateRequestDto"></param>
        /// <returns></returns>
        [HttpPost("createMaklumatKlasifikasiPerkhidmatan")]
        public async Task<IActionResult> Create([FromBody] MaklumatKlasifikasiPerkhidmatanCreateRequestDto maklumatKlasifikasiPerkhidmatanCreateRequestDto)
        {
            _logger.LogInformation("Creating a new maklumatKlasifikasiPerkhidmatan");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isSuccess = await _maklumatKlasifikasiPerkhidmatanService.CreateAsync(maklumatKlasifikasiPerkhidmatanCreateRequestDto);

            return Ok(new
            {
                status = isSuccess ? "Sucess" : "Failed",
                items = isSuccess

            });

        }
    }
}
