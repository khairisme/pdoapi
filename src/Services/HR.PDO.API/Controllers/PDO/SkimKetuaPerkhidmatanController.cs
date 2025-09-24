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

namespace HR.PDO.API.Controllers.PDO;
//[Authorize]
[ApiController]
[Route("api/pdo/v1/[controller]")]
public class SkimKetuaPerkhidmatanController : ControllerBase
{
    private readonly ISkimKetuaPerkhidmatanService _skimKetuaPerkhidmatanService;
    private readonly ILogger<SkimKetuaPerkhidmatanController> _logger;

    public SkimKetuaPerkhidmatanController(ISkimKetuaPerkhidmatanService skimKetuaPerkhidmatanService, ILogger<SkimKetuaPerkhidmatanController> logger)
    {
        _skimKetuaPerkhidmatanService = skimKetuaPerkhidmatanService;
        _logger = logger;
    }






    /// <summary>
    ///Create newSkimKetuaPerkhidmatan
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>

    [HttpPost("newSkimKetuaPerkhidmatan")]
    public async Task<IActionResult> Create([FromBody] List<SkimKetuaPerkhidmatanRequestDto> dto)
    {
        _logger.LogInformation("Creating a new SkimKetuaPerkhidmatan");


        try
        {
            var isSuccess = await _skimKetuaPerkhidmatanService.CreateAsync(dto);

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
    /// Tindakan
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("tindakan")]
    public async Task<IActionResult> SoftDeleteSkimKetua([FromQuery] int IdSkim, [FromQuery] int IdJawatan)
    {
        var success = await _skimKetuaPerkhidmatanService.SoftDeleteSkimKetuaPerkhidmatanAsync(IdSkim, IdJawatan);
        if (!success)
            return NotFound(new { message = "Record not found" });

        return Ok(new { message = "deleted successfully" });
    }

}
