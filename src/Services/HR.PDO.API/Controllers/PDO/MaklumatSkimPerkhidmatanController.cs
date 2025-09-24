using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.PDO.API.Controllers.PDO
{
    //[Authorize]
    [ApiController]
    [Route("api/pdo/v1/[controller]")]
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
                    status = result.Count() > 0 ? "Berjaya" : "Gagal",
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

            int? id = await _maklumatSkimPerkhidmatan.CreateAsync(maklumatSkimPerkhidmatanCreateRequestDto);

            return Ok(new
            {
                status = id>0 ? "Berjaya" : "Gagal",
                id = id

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
        ///get Skim Perkhidmatan By Id
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
                    return NotFound($"No Senarai Skim Perkhidmatan found for Id {id}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during getSenaraiSkimPerkhidmatan");
                return StatusCode(500, "Internal Server Error");
            }
        }


        /// <summary>
        ///get Skim Perkhidmatan By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getSenaraiSkimPerkhidmatanOld/{id}")]
        public async Task<IActionResult> GetSenaraiSkimPerkhidmatanOld(int id)
        {
            try
            {
                var result = await _maklumatSkimPerkhidmatan.GetSenaraiSkimPerkhidmatanByIdOldAsync(id);
                if (result == null)
                    return NotFound($"No Senarai Skim Perkhidmatan found for Id {id}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during getSenaraiSkimPerkhidmatanOld");
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
            _logger.LogInformation("update MaklumatSkimPerkhidmatan");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var isSuccess = await _maklumatSkimPerkhidmatan.UpdateAsync(dto);

                if (!isSuccess)
                    return StatusCode(500, new { status = "Gagal", message = "Gagal kemaskini rekod" });

                return Ok(new {status="Berjaya", message="Updated successfully"});
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
                status = result.Count() > 0 ? "Berjaya" : "Gagal",
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
                    status = result.Count() > 0 ? "Berjaya" : "Gagal",
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
                status = result.Count() > 0 ? "Berjaya" : "Gagal",
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
                    return StatusCode(500, new { status = "Gagal", message = "Gagal kemaskini rekod" });

                return Ok(new {status="Berjaya", message="Updated successfully"});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during SetKemaskinistatus");
                return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
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
                return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
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
                return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
            }
        }
        /// <summary>
        ///get Skim Perkhidmatan By Kod
        /// </summary>
        /// <param name="kod"></param>
        /// <returns></returns>
        [HttpGet("getSenaraiSkimPerkhidmatanByKod/{kod}")]
        public async Task<IActionResult> GetByKod(string kod)
        {
            try
            {
                var result = await _maklumatSkimPerkhidmatan.GetSenaraiSkimPerkhidmatanByKodAsync(kod);
                if (result == null)
                    return NotFound($"No Senarai Skim Perkhidmatan found for Kod {kod}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during getSenaraiSkimPerkhidmatanByKod");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpGet("getKetuaPerkhidmatanById/{id}")]
        public async Task<IActionResult> GetKetuaPerkhidmatanById(int id)
        {
            var data = await _maklumatSkimPerkhidmatan.GetSkimPerkhidmatanByIdAsync(id);
            return Ok(new
            {
                status = data.Count() > 0 ? "Berjaya" : "Gagal",
                items = data

            });
        }
    }
}

