using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers.PDO
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
        /// <summary>
        ///validate duplicate KumpulanPerkhidmatan
        /// </summary>
        /// <param name="KumpulanPerkhidmatanDto"></param>
        /// <returns></returns>
        [HttpPost("ValSkimPerkhidmatan")]
        public async Task<IActionResult> ValSkimPerkhidmatan([FromBody] MaklumatSkimPerkhidmatanCreateRequestDto dto)
        {
            try
            {
                var isDuplicate = await _maklumatSkimPerkhidmatan.CheckDuplicateKodNamaAsync(dto);
                if (isDuplicate)
                    return Conflict("Nama already exists for another record.");

                return Ok(isDuplicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during creation");
                return StatusCode(500, "Internal Server Error");
            }
        }
        /// <summary>
        ///get KumpulanPerkhidmatan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getSenaraiSkimPerkhidmatan/{kod}")]
        public async Task<IActionResult> GetById(string kod)
        {
            try
            {
                var result = await _maklumatSkimPerkhidmatan.GetSenaraiSkimPerkhidmatanByIdAsync(kod);
                if (result == null)
                    return NotFound($"No Senarai Skim Perkhidmatan found for Kod {kod}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during getKumpulanPerkhidmatan");
                return StatusCode(500, "Internal Server Error");
            }
        }
        /// <summary>
        ///Update MaklumatKlasifikasiPerkhidmatan
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("setMaklumatKlasifikasiPerkhidmatan")]
        public async Task<IActionResult> Update([FromBody] MaklumatSkimPerkhidmatanCreateRequestDto dto)
        {
            _logger.LogInformation("update MaklumatKlasifikasiPerkhidmatan");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var isSuccess = await _maklumatSkimPerkhidmatan.UpdateAsync(dto);

                if (!isSuccess)
                    return StatusCode(500, "Failed to update the record.");

                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during updation");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

