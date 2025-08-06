using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers.PDO
{
    //[Authorize]
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
        ///dapatkanMaklumatKlasifikasiPerkhidmatan
        /// </summary>
        /// <param name="PenapisMaklumatKlasifikasiPerkhidmatan"></param>
        /// <returns></returns>
        [HttpPost("getSearchMaklumatKlasifikasiPerkhidmatan")]
        public async Task<IActionResult> GetSearchMaklumatKlasifikasiPerkhidmatan([FromBody] PenapisMaklumatKlasifikasiPerkhidmatanDto filter)
        {
            _logger.LogInformation("Getting all PenapisMaklumatKlasifikasiPerkhidmatanDto");

            var result = await _maklumatKlasifikasiPerkhidmatanService.GetSearchMaklumatKlasifikasiPerkhidmatan(filter);

            return Ok(new
            {
                status = result.Count() > 0 ? "Sucess" : "Failed",
                items = result

            });
        }

        /// <summary>
        ///Create ciptaPerkhidmatanKlasifikasi
        /// </summary>
        /// <param name="MaklumatKlasifikasiPerkhidmatanCreateRequestDto"></param>
        /// <returns></returns>
        [HttpPost("newMaklumatKlasifikasiPerkhidmatan")]
        public async Task<IActionResult> Create([FromBody] MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto maklumatKlasifikasiPerkhidmatanCreateRequestDto)
        {
            _logger.LogInformation("Creating a new maklumatKlasifikasiPerkhidmatan");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isSuccess = await _maklumatKlasifikasiPerkhidmatanService.NewAsync(maklumatKlasifikasiPerkhidmatanCreateRequestDto);

            return Ok(new
            {
                status = isSuccess ? "Sucess" : "Failed",
                items = isSuccess

            });

        }
        /// <summary>
        ///validate duplicate MaklumatKlasifikasiPerkhidmatan
        /// </summary>
        /// <param name="maklumatKlasifikasiPerkhidmatanCreateRequestDto"></param>
        /// <returns></returns>
        [HttpPost("valMaklumatKlasifikasiPerkhidmatan")]
        public async Task<IActionResult> ValKumpulanPerkhidmata([FromBody] MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto maklumatKlasifikasiPerkhidmatanCreateRequestDto)
        {
            try
            {
                var isDuplicate = await _maklumatKlasifikasiPerkhidmatanService.CheckDuplicateKodNamaAsync(maklumatKlasifikasiPerkhidmatanCreateRequestDto);
                if (isDuplicate)
                    return Conflict("Kod or Nama already exists for another record.");

                return Ok(isDuplicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during creation");
                return StatusCode(500, ex.InnerException.Message.ToString());
            }
        }

        /// <summary>
        ///get MaklumatKlasifikasiPerkhidmatan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getMaklumatKlasifikasiPerkhidmatan/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _maklumatKlasifikasiPerkhidmatanService.GetMaklumatKlasifikasiPerkhidmatan(id);
            if (result == null)
                return NotFound($"No MaklumatKlasifikasiPerkhidmatan found for ID {id}");

            return Ok(result);

        }

        /// <summary>
        ///get MaklumatKlasifikasiPerkhidmatan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getMaklumatBaharu/{id}")]
        public async Task<IActionResult> GetNewInforationById(int id)
        {
            var result = await _maklumatKlasifikasiPerkhidmatanService.GetMaklumatKlasifikasiPerkhidmatan(id);
            if (result == null)
                return NotFound($"No MaklumatKlasifikasiPerkhidmatan found for ID {id}");

            return Ok(result);

        }

        [HttpGet("getMaklumatSediaAda/{id}")]
        public async Task<IActionResult> getMaklumatSediaAda(int id)
        {
            var result = await _maklumatKlasifikasiPerkhidmatanService.GetMaklumatKlasifikasiPerkhidmatanOld(id);
            if (result == null)
                return NotFound($"No MaklumatKlasifikasiPerkhidmatan found for ID {id}");

            return Ok(result);

        }


        /// <summary>
        ///get MaklumatKlasifikasiPerkhidmatan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getMaklumatBaharuView/{id}")]
        public async Task<IActionResult> GetMaklumatKlasifikasiPerkhidmatan(int id)
        {
            var result = await _maklumatKlasifikasiPerkhidmatanService.GetMaklumatKlasifikasiPerkhidmatanView(id);
            if (result == null)
                return NotFound($"No MaklumatKlasifikasiPerkhidmatan found for ID {id}");

            return Ok(result);

        }

        /// <summary>
        ///Update MaklumatKlasifikasiPerkhidmatan
        /// </summary>
        /// <param name="MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto"></param>
        /// <returns></returns>
        [HttpPost("setMaklumatKlasifikasiPerkhidmatan")]
        public async Task<IActionResult> Update([FromBody] MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto updateDto)
        {
            _logger.LogInformation("update MaklumatKlasifikasiPerkhidmatan");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var isSuccess = await _maklumatKlasifikasiPerkhidmatanService.SetAsync(updateDto);

            return Ok(new
            {
                status = isSuccess ? "Sucess" : "Failed",
                items = isSuccess
            });

        }

        /// <summary>
        ///dapatkanSenaraiPengesahanPerkhidmatanKlasifikasi
        /// </summary>
        /// <param name="PenapisPerkhidmatanKlasifikasiDto"></param>
        /// <returns></returns>
        [HttpPost("getSenaraiPengesahanPerkhidmatanKlasifikasi")]
        public async Task<IActionResult> getSenaraiPengesahanPerkhidmatanKlasifikasi([FromBody] PenapisPerkhidmatanKlasifikasiDto filter)
        {
            _logger.LogInformation("Getting all PenapisPerkhidmatanKlasifikasiDto");

            var result = await _maklumatKlasifikasiPerkhidmatanService.GetSenaraiPengesahanPerkhidmatanKlasifikasi(filter);

            return Ok(new
            {
                status = result.Count() > 0 ? "Sucess" : "Failed",
                items = result

            });
        }

        /// <summary>
        ///Update MaklumatKlasifikasiPerkhidmatan
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        [HttpPost("setStatusPengesahanMaklumatKlasifikasiPerkhidmatan")]
        public async Task<IActionResult> SetStatusPengesahanMaklumatKlasifikasiPerkhidmatan([FromBody] MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto updateDto)
        {
            _logger.LogInformation("update MaklumatKlasifikasiPerkhidmatan");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var isSuccess = await _maklumatKlasifikasiPerkhidmatanService.KemaskiniStatusAsync(updateDto);

            return Ok(new
            {
                status = isSuccess ? "Sucess" : "Failed",
                items = isSuccess
            });

        }
        /// <summary>
        ///Get all MaklumatKlasifikasiPerkhidmatanDto
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("getAll")]
        public async Task<IActionResult> getAll()
        {
            _logger.LogInformation("Getting all MaklumatKlasifikasiPerkhidmatanDto");

            var result = await _maklumatKlasifikasiPerkhidmatanService.GetAllAsync();

            return Ok(new
            {
                status = result.Count() > 0 ? "Sucess" : "Failed",
                items = result

            });
        }
        /// <summary>
        /// daftar Hantar MaklumatKlasifikasi Perkhidmatan 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("daftarhantar")]
        public async Task<IActionResult> DaftarHantarMaklumatKlasifikasiPerkhidmatan([FromBody] MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto dto)
        {
            var result = await _maklumatKlasifikasiPerkhidmatanService.DaftarHanatarMaklumatKlasifikasiPerkhidmatanAsync(dto);
            if (!result)
                return StatusCode(500, "The application has failed to sent.");

            return Ok("The application has been sent.");
        }
        /// <summary>
        /// set Hantar MaklumatKlasifikasi Perkhidmatan 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("sethantar")]
        public async Task<IActionResult> SetHantarMaklumatKlasifikasiPerkhidmatan([FromBody] MaklumatKlasifikasiPerkhidmatanCreateUpdateRequestDto dto)
        {
            var result = await _maklumatKlasifikasiPerkhidmatanService.SetHanatarMaklumatKlasifikasiPerkhidmatanAsync(dto);
            if (!result)
                return StatusCode(500, "The application has failed to sent.");

            return Ok("The application has been sent.");
        }


        /// <summary>
        /// Delete or Update Kumpulan Perkhidmatan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrUpdate(int id)
        {
            try
            {
                var result = await _maklumatKlasifikasiPerkhidmatanService.DeleteOrUpdateKlasifikasiPerkhidmatanAsync(id);

                if (!result)
                    return NotFound(new { message = "Record not found." });

                return Ok(new { message = result == true ? "Deleted success" : "Deleted failed" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during DeleteOrUpdate");
                return StatusCode(500, ex.InnerException.Message.ToString());
            }
        }
    }
}
