using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.API.Controllers.PDO
{
    //[Authorize]
    [ApiController]
    [Route("api/pdo/[controller]")]
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
        
    }
}
