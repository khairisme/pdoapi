using HR.Application.DTOs;
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
//[Authorize]
[ApiController]
[Route("api/pdo/[controller]")]
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
                return StatusCode(500, "Failed to create the record.");

            return Ok("Created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during creation : " + ex.InnerException.ToString());
            return StatusCode(500, ex.InnerException.Message.ToString());
        }
    }
    /// <summary>
    /// Tindakan
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("tindakan/{id}")]
    public async Task<IActionResult> SoftDeleteSkimKetua(int id)
    {
        var success = await _skimKetuaPerkhidmatanService.SoftDeleteSkimKetuaPerkhidmatanAsync(id);
        if (!success)
            return NotFound(new { message = "Record not found" });

        return Ok(new { message = "deleted successfully" });
    }

}
