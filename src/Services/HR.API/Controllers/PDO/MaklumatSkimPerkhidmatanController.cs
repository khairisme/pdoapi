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
        ///Create MaklumatSkimPerkhidmatanCreateRequestDto
        /// </summary>
        /// <param name="MaklumatSkimPerkhidmatanCreateRequestDto"></param>
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
        ///validate duplicate ValSkimPerkhidmatan
        /// </summary>
        /// <param name="MaklumatSkimPerkhidmatanCreateRequestDto"></param>
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
        [HttpGet("getSenaraiSkimPerkhidmatan/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _maklumatSkimPerkhidmatan.GetSenaraiSkimPerkhidmatanByIdAsync(id);
                if (result == null)
                    return NotFound($"No Senarai Skim Perkhidmatan found for Kod {id}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during getKumpulanPerkhidmatan");
                return StatusCode(500, "Internal Server Error");
            }
        }
        /// <summary>
        ///Update MaklumatSkimPerkhidmatan
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("setMaklumatSkimPerkhidmatan")]
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
        /// <summary>
        /// GetStatusSkimPerkhidmatan
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("getStatusSkimPerkhidmatan")]
        public async Task<IActionResult> GetStatusSkimPerkhidmatan([FromBody] SkimPerkhidmatanFilterDto filter)
        {
            var result = await _maklumatSkimPerkhidmatan.GetActiveSkimPerkhidmatan(filter);
            return Ok(result);
        }
        /// <summary>
        /// daftar Hantar Skim Perkhidmatan 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("daftarhantar")]
        public async Task<IActionResult> DaftarHantarSkimPerkhidmatan([FromBody] MaklumatSkimPerkhidmatanCreateRequestDto dto)
        {
            var result = await _maklumatSkimPerkhidmatan.DaftarHantarSkimPerkhidmatanAsync(dto);
            if (!result)
                return StatusCode(500, "The application has failed to sent.");

            return Ok("The application has been sent.");
        }

        /// <summary>
        /// set Hantar Skim Perkhidmatan
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("sethantar")]
        public async Task<IActionResult> setHantarSkimPerkhidmatan([FromBody] MaklumatSkimPerkhidmatanCreateRequestDto dto)
        {

            var isSuccess = await _maklumatSkimPerkhidmatan.UpdateHantarSkimPerkhidmatanAsync(dto);

            if (!isSuccess)
                return StatusCode(500, "The application has failed to sent.");

            return Ok("The application has been sent.");

        }
        /// <summary>
        /// GetSkimWithJawatan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("getSkimWithJawatan/{id}")]
        public async Task<IActionResult> GetSkimWithJawatan(int id)
        {
            var result = await _maklumatSkimPerkhidmatan.GetSkimWithJawatanAsync(id);
            return Ok(new
            {
                status = result.Count() > 0 ? "Sucess" : "Failed",
                items = result

            });
        }

        /// <summary>
        ///Get GetCarianSkimPerkhidmatan
        /// </summary>
        /// <param name="CarianSkimPerkhidmatanFilterDto"></param>
        /// <returns></returns>
        [HttpPost("getCarianSkimPerkhidmatan")]
        public async Task<IActionResult> GetCarianSkimPerkhidmatan([FromBody] CarianSkimPerkhidmatanFilterDto filter)
        {
            _logger.LogInformation("Getting Carian Skim Perkhidmatan Data");
            try
            {
                var result = await _maklumatSkimPerkhidmatan.GetCarianSkimPerkhidmatan(filter);

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
        ///Get all SkimPerkhidmatan
        /// </summary>
        /// <param name="RujStatusPermohonanDto"></param>
        /// <returns></returns>
        [HttpGet("getAll")]
        public async Task<IActionResult> getAll()
        {
            _logger.LogInformation("Getting all SkimPerkhidmatan");

            var result = await _maklumatSkimPerkhidmatan.GetAllAsync();

            return Ok(new
            {
                status = result.Count() > 0 ? "Sucess" : "Failed",
                items = result

            });
        }
        /// <summary>
        /// Set Kemaskinistatus
        /// </summary>
        /// <param name="perkhidmatanDto"></param>
        /// <returns></returns>
        [HttpPost("setKemaskinistatus")]
        public async Task<ActionResult<SkimPerkhidmatanButiranDto>> SetKemaskinistatus([FromBody] SkimPerkhidmatanRefStatusDto perkhidmatanDto)
        {
            try
            {
                var result = await _maklumatSkimPerkhidmatan.KemaskiniStatusAsync(perkhidmatanDto);

                if (!result)
                    return StatusCode(500, "Failed to update the record.");

                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during SetKemaskinistatus");
                return StatusCode(500, ex.InnerException.Message.ToString());
            }
        }
        /// <summary>
        /// Maklumat Baharu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getMaklumatBaharu/{id}")]
        public async Task<ActionResult<SkimPerkhidmatanRefStatusDto>> GetMaklumatBaharu(int id)
        {
            try
            {
                var result = await _maklumatSkimPerkhidmatan.GetMaklumatBaharuAsync(id);

                if (result == null)
                    return NotFound(new { message = "Information not found." });

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during GetMaklumatBaharu");
                return StatusCode(500, ex.InnerException.Message.ToString());
            }
        }
        /// <summary>
        /// Delete or Update Skim Perkhidmatan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrUpdate(int id)
        {
            try
            {
                var result = await _maklumatSkimPerkhidmatan.DeleteOrUpdateSkimPerkhidmatanAsync(id);

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

