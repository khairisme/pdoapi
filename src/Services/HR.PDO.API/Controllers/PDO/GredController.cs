using HR.PDO.Application.DTOs;
using HR.PDO.Application.DTOs.PDO;
using HR.PDO.Application.Interfaces.PDO;
using HR.PDO.Application.Services;
using HR.PDO.Core.Entities;
using HR.PDO.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shared.Contracts.DTOs;

/// <summary>
/// Controller for managing Gred-related operations within the PDO module.
/// Provides endpoints for retrieving, creating, and updating Gred data via IGredService.
/// </summary>
/// <remarks>
/// Author: Khairi Abu Bakar  
/// Date: 19 September 2025  
/// Purpose: Acts as the API entry point for Gred functionality in the HR.PDO system. 
/// Ensures structured routing, logging, and service delegation for Gred-related business logic.
/// </remarks>
[ApiController]
[Route("api/pdo/v1/[controller]")]
public class GredController : ControllerBase
{
    private readonly IGredService _gredService;
    private readonly ILogger<GredController> _logger;

    public GredController(IGredService gredService, ILogger<GredController> logger)
    {
        _gredService = gredService;
        _logger = logger;
    }





    /// <summary>
    /// Carian Gred
    /// </summary>

    /// <param name="filter"></param>
    /// <returns></returns>

    [HttpPost("getGredList")]
    public async Task<IActionResult> GetGredList([FromBody] GredFilterDto filter)
    {
        _logger.LogInformation("Getting Carian Gred List");
        

        var data = await _gredService.GetGredListAsync(filter);
        return Ok(new
        {
            status = data.Count() > 0 ? "Berjaya" : "Gagal",
            items = data

        });
    }
    /// <summary>
    /// Search Maklumat Gred
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("getMaklumatGred")]
    public async Task<IActionResult> GetMaklumatGred([FromBody] GredFilterDto filter)
    {
        var result = await _gredService.GetFilteredGredList(filter);
        return Ok(new
        {
            status = result.Count() > 0 ? "Berjaya" : "Gagal",
            items = result

        });
        
    }
    /// <summary>
    ///Create newGredJawatan
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>

    [HttpPost("newGredJawatan")]
    public async Task<IActionResult> Create([FromBody] CreateGredDto dto)
    {
        _logger.LogInformation("Creating a new GredJawatan");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var isSuccess = await _gredService.CreateAsync(dto);

            if (!isSuccess)
                return StatusCode(500, new {status="Gagal", message="Rekod gagal diwujudkan"});

            return Ok("Created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during creation : " + ex.InnerException.ToString());
            return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
        }
    }
    /// <summary>
    /// daftar Hantar GredJawatan 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("daftarhantar")]
    public async Task<IActionResult> DaftarGredJawatan([FromBody] CreateGredDto dto)
    {
        try
        {
            var result = await _gredService.DaftarGredJawatanAsync(dto);
            if (!result)
                return StatusCode(500, "The application has failed to sent.");

            return Ok("The application has been sent.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during DaftarGredJawatan");
            return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
        }
    }


    /// <summary>
    ///Update GredJawatan
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("setGredJawatan")]
    public async Task<IActionResult> Update([FromBody] CreateGredDto dto)
    {
        _logger.LogInformation("update GredJawatan");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var isSuccess = await _gredService.UpdateAsync(dto);

            if (!isSuccess)
                return StatusCode(500, new { status = "Gagal", message = "Gagal kemaskini rekod" });

            return Ok(new {status="Berjaya", message="Updated successfully"});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during updation");
            return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
        }
    }

    /// <summary>
    /// set Hantar Gred Jawatan
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("sethantar")]
    public async Task<IActionResult> setHantarGredJawatan([FromBody] CreateGredDto dto)
    {
        try
        {
            var isSuccess = await _gredService.UpdateHantarGredJawatanAsync(dto);

            if (!isSuccess)
                return StatusCode(500, "The application has failed to sent.");

            return Ok("The application has been sent.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during setHantarGredJawatan");
            return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
        }

    }
    /// <summary>
    /// Papar Maklumat Gred
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("getMaklumatGred/{id}")]
    public async Task<ActionResult<PaparMaklumatGredDto>> GetMaklumatGred(int id)
    {
        try
        {
            var result = await _gredService.GetMaklumatGred(id);

            if (result == null)
                return NotFound(new { message = "Information not found." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during GetMaklumatGred");
            return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
        }
    }

    /// <summary>
    ///validate duplicate Gred
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("valGredJawatan")]
    public async Task<IActionResult> ValGredJawatan([FromBody] CreateGredDto dto)
    {
        try
        {
            var isDuplicate = await _gredService.CheckDuplicateNamaAsync(dto);
            if (isDuplicate)
                return Conflict("Nama already exists for another record.");

            return Ok(isDuplicate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during creation");
            return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
        }
    }


    /// <summary>
    /// Maklumat Sedia Ada 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("getMaklumatGredSediaAda/{id}")]
    public async Task<ActionResult<KumpulanPerkhidmatanStatusDto>> GetMaklumatGredSediaAda(int id)
    {
        try
        {
            var result = await _gredService.GetMaklumatGredSediaAda(id);

            if (result == null)
                return NotFound(new { message = "Information not found." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during GetMaklumatGredSediaAda");
            return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
        }
    }
    /// <summary>
    /// Maklumat Baharu
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("getMaklumatGredBaharu/{id}")]
    public async Task<ActionResult<KumpulanPerkhidmatanRefStatusDto>> GetMaklumatGredBaharu(int id)
    {
        try
        {
            var result = await _gredService.GetMaklumatGredBaharuAsync(id);

            if (result == null)
                return NotFound(new { message = "Information not found." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during GetMaklumatGredBaharu");
            return StatusCode(500, new{status = "Gagal", message = ex.Message + " - " + ex.InnerException != null ? ex.InnerException.Message.ToString() : ""});
        }
    }


    /// <summary>
    /// Set Kemaskinistatus
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    [HttpPost("setKemaskinistatus")]
    public async  Task<IActionResult> SetKemaskinistatus([FromBody] CreateGredDto dto)
    {
        try
        {
            var result = await _gredService.KemaskiniStatusAsync(dto);

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
    /// Delete or Update Kumpulan Perkhidmatan
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrUpdate(int id)
    {
        try
        {
            var result = await _gredService.DeleteOrUpdateGredAsync(id);

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

}
