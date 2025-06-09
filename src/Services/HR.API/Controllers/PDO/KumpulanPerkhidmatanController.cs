using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Application.Services;
using HR.Core.Entities;
using HR.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shared.Contracts.DTOs;

namespace HR.API.Controllers.PDO;
[Authorize]
[ApiController]
[Route("api/pdo/[controller]")]
public class KumpulanPerkhidmatanController : ControllerBase
{
    private readonly IKumpulanPerkhidmatanService _kumpulanPerkhidmatan;
    private readonly ILogger<KumpulanPerkhidmatanController> _logger;

    public KumpulanPerkhidmatanController(IKumpulanPerkhidmatanService kumpulanPerkhidmatan, ILogger<KumpulanPerkhidmatanController> logger)
    {
        _kumpulanPerkhidmatan = kumpulanPerkhidmatan;
        _logger = logger;
    }





    /// <summary>
    ///Get all KumpulanPerkhidmatan
    /// </summary>
    /// <param name="KumpulanPerkhidmatanDto"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("getAll")]
    public async Task<IActionResult> getAll()
    {
        _logger.LogInformation("Getting all KumpulanPerkhidmatanDto");

        var result = await _kumpulanPerkhidmatan.GetAllAsync();

        return Ok(new
        {
            status = result.Count() > 0 ? "Sucess" : "Failed",
            items = result

        });
    }

    /// <summary>
    ///Get Carl MaklumatKumpulanPerkhidmatan
    /// </summary>
    /// <param name="KumpulanPerkhidmatanFilterDto"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("getPermohonanPerjawatan")]
    public async Task<IActionResult> getMaklumatKumpulanPerkhidmatan([FromBody] KumpulanPerkhidmatanFilterDto filter)
    {
        _logger.LogInformation("Getting Carl Maklumat Kumpulan Perkhidmatan");
        try
        {
            var result = await _kumpulanPerkhidmatan.GetKumpulanPerkhidmatanAsync(filter);

            return Ok(new
            {
                status = result.Count() > 0 ? "Sucess" : "Failed",
                items = result

            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during carl Maklumat Kumpulan Perkhidmatan" + ex.Message.ToString());
            return StatusCode(500,  ex.InnerException.Message.ToString());
        }
    }
    /// <summary>
    ///Create KumpulanPerkhidmatan
    /// </summary>
    /// <param name="KumpulanPerkhidmatanFilterDto"></param>
    /// <returns></returns>
    
    [HttpPost("newPermohonanPerjawatan")]
    public async Task<IActionResult> Create([FromBody] KumpulanPerkhidmatanDto kumpulanPerkhidmatanDto)
    {
        _logger.LogInformation("Creating a new KumpulanPerkhidmatan");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var isSuccess = await _kumpulanPerkhidmatan.CreateAsync(kumpulanPerkhidmatanDto);

            if (!isSuccess)
                return StatusCode(500, "Failed to create the record.");

            return Ok("Created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during creation : "+ ex.InnerException.ToString());
            return StatusCode(500, ex.InnerException.Message.ToString());
        }
    }
    /// <summary>
    ///get KumpulanPerkhidmatan
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("getKumpulanPerkhidmatan/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _kumpulanPerkhidmatan.GetKumpulanPerkhidmatanByIdAsync(id);
            if (result == null)
                return NotFound($"No KumpulanPerkhidmatan found for ID {id}");

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during getKumpulanPerkhidmatan");
            return StatusCode(500, ex.InnerException.Message.ToString());
        }
    }

    /// <summary>
    ///validate duplicate KumpulanPerkhidmatan
    /// </summary>
    /// <param name="KumpulanPerkhidmatanDto"></param>
    /// <returns></returns>
    [HttpPost("valKumpulanPerkhidmata")]
    public async Task<IActionResult> ValKumpulanPerkhidmata([FromBody] KumpulanPerkhidmatanDto kumpulanPerkhidmatanDto)
    {
        try
        {
            var isDuplicate = await _kumpulanPerkhidmatan.CheckDuplicateKodNamaAsync(kumpulanPerkhidmatanDto);
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
    ///Update KumpulanPerkhidmatan
    /// </summary>
    /// <param name="KumpulanPerkhidmatanFilterDto"></param>
    /// <returns></returns>
    [HttpPost("setPermohonanPerjawatan")]
    public async Task<IActionResult> Update([FromBody] KumpulanPerkhidmatanDto kumpulanPerkhidmatanDto)
    {
        _logger.LogInformation("update KumpulanPerkhidmatan");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var isSuccess = await _kumpulanPerkhidmatan.UpdateAsync(kumpulanPerkhidmatanDto);

            if (!isSuccess)
                return StatusCode(500, "Failed to update the record.");

            return Ok("Updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during updation");
            return StatusCode(500, ex.InnerException.Message.ToString());
        }
    }


    /// <summary>
    ///Get Carl MaklumatKumpulanPerkhidmatan
    /// </summary>
    /// <param name="KumpulanPerkhidmatanFilterDto"></param>
    /// <returns></returns>
    [HttpPost("getStatusKumpulanPerkhidmatan")]
    public async Task<IActionResult> getStatusKumpulanPerkhidmatan([FromBody] KumpulanPerkhidmatanFilterDto filter)
    {
        _logger.LogInformation("Getting Carl getStatusKumpulanPerkhidmatan");
        try
        {
            var result = await _kumpulanPerkhidmatan.GetStatusKumpulanPerkhidmatan(filter);

            return Ok(new
            {
                status = result.Count() > 0 ? "Sucess" : "Failed",
                items = result

            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during carl getStatusKumpulanPerkhidmatan");
            return StatusCode(500, ex.InnerException.Message.ToString());
        }
    }
    /// <summary>
    /// Maklumat Sedia Ada 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("getMaklumatSediaAda/{id}")]
    public async Task<ActionResult<KumpulanPerkhidmatanStatusDto>> GetMaklumatSediaAda(int id)
    {
        try
        {
            var result = await _kumpulanPerkhidmatan.GetMaklumatSediaAda(id);

            if (result == null)
                return NotFound(new { message = "Information not found." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during GetMaklumatSediaAda");
            return StatusCode(500, ex.InnerException.Message.ToString());
        }
    }
    /// <summary>
    /// Maklumat Baharu
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("getMaklumatBaharu/{id}")]
    public async Task<ActionResult<KumpulanPerkhidmatanButiranDto>> GetMaklumatBaharu(int id)
    {
        try
        {
            var result = await _kumpulanPerkhidmatan.GetMaklumatBaharuAsync(id);

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
    /// Set Kemaskinistatus
    /// </summary>
    /// <param name="perkhidmatanDto"></param>
    /// <returns></returns>
    [HttpPost("setKemaskinistatus")]
    public async Task<ActionResult<KumpulanPerkhidmatanButiranDto>> SetKemaskinistatus([FromBody]  KumpulanPerkhidmatanRefStatusDto perkhidmatanDto)
    {
        try
        {
            var result = await _kumpulanPerkhidmatan.KemaskiniStatusAsync(perkhidmatanDto);

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
    /// daftar Hantar KumpulanPermohonan 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("daftarhantar")]
    public async Task<IActionResult> DaftarHantarKumpulanPermohonan([FromBody] KumpulanPerkhidmatanDto dto)
    {
        try
        {
            var result = await _kumpulanPerkhidmatan.DaftarHantarKumpulanPermohonanAsync(dto);
            if (!result)
                return StatusCode(500, "The application has failed to sent.");

            return Ok("The application has been sent.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during DaftarHantarKumpulanPermohonan");
            return StatusCode(500, ex.InnerException.Message.ToString());
        }
    }

    /// <summary>
    /// set Hantar kumpulan Permohonan Permohonan
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("sethantar")]
    public async Task<IActionResult> setHantarKumpulanPermohonan([FromBody] KumpulanPerkhidmatanDto dto)
    {
        try
        {
            var isSuccess = await _kumpulanPerkhidmatan.UpdateHantarKumpulanPermohonanAsync(dto);

            if (!isSuccess)
                return StatusCode(500, "The application has failed to sent.");

            return Ok("The application has been sent.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during setHantarKumpulanPermohonan");
            return StatusCode(500, ex.InnerException.Message.ToString());
        }

    }
}
