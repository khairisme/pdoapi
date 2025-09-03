using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Services;
using HR.PDO.Application.Services.PDO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.PDO.API.Controllers.PDO
{
    //[Authorize]
    [ApiController]
    [Route("api/pdo/v1/[controller]")]
    public class PermohonanPeriwatanController : Controller
    {
        private readonly IPermohonanPeriwatanService _permohonanPeriwatan;
        private readonly ILogger<PermohonanPeriwatanController> _logger;

        public PermohonanPeriwatanController(IPermohonanPeriwatanService permohonanPeriwatan, ILogger<PermohonanPeriwatanController> logger)
        {
            _permohonanPeriwatan = permohonanPeriwatan;
            _logger = logger;
        }
        /// <summary>
        ///Create permohonanPeriwatan
        /// </summary>
        /// <param name="permohonanPeriwatanCreateRequestDto"></param>
        /// <returns></returns>
        [HttpPost("newpermohonanPeriwatan")]
        public async Task<IActionResult> Create([FromBody] PermohonanPeriwatanCreateRequestDto permohonanPeriwatanCreateRequestDto)
        {
            _logger.LogInformation("Creating a new permohonanPeriwatan");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isSuccess = await _permohonanPeriwatan.CreateAsync(permohonanPeriwatanCreateRequestDto);

            return Ok(new
            {
                status = isSuccess ? "Sucess" : "Failed",
                items = isSuccess

            });

        }
        /// <summary>
        ///Create AktivitiOrganisasi
        /// </summary>
        /// <param name="AktivitiOrganisasiCreateRequestDto"></param>
        /// <returns></returns>
        [HttpPost("newAktivitiOrganisasi")]
        public async Task<IActionResult> CreateAktivitiOrganisasi([FromBody] AktivitiOrganisasiCreateRequestDto dto)
        {
            _logger.LogInformation("Creating a new AktivitiOrganisasi");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isSuccess = await _permohonanPeriwatan.CreateAktivitiOrganisasiAsync(dto);

            return Ok(new
            {
                status = isSuccess ? "Sucess" : "Failed",
                items = isSuccess

            });

        }


        /// <summary>
        /// Get Ulasan Status Permohonan Jawatan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("getulasanstatus/{id}")]
        public async Task<IActionResult> GetUlasanStatus(int id)
        {
            var result = await _permohonanPeriwatan.GetUlasanStatusAsync(id);
            if (result == null)
                return NotFound("Ulasan not found.");
            return Ok(result);
        }
        /// <summary>
        /// set Status Permohonan
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("setStatusPermohonan")]
        public async Task<IActionResult> SimpanStatus([FromBody] SimpanStatusPermohonanDto dto)
        {
            try
            {
                var result = await _permohonanPeriwatan.SimpanStatusPermohonanAsync(dto);
                return result ? Ok("Status updated successfully.") : BadRequest("Failed to update status.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
