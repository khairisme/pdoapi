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
[Authorize]
[ApiController]
[Route("api/pdo/[controller]")]
public class JawatanController : ControllerBase
{
    private readonly IJawatanService _jawatanService;
    private readonly ILogger<JawatanController> _logger;

    public JawatanController(IJawatanService jawatanService, ILogger<JawatanController> logger)
    {
        _jawatanService = jawatanService;
        _logger = logger;
    }

    /// <summary>
    /// Get all Jawatan with Agensi
    /// </summary>
    /// <param name="namaJwtn"></param>
    /// <param name="kodCartaOrganisasi"></param>
    /// <returns></returns>

    [HttpGet("searchJawatan")]
    public async Task<IActionResult> SearchJawatan([FromQuery] string namaJwtn, [FromQuery] string kodCartaOrganisasi)
    {
        var data = await _jawatanService.GetJawatanWithAgensiAsync(namaJwtn ?? "", kodCartaOrganisasi ?? "");
        return Ok(new
        {
            status = data.Count() > 0 ? "Sucess" : "Failed",
            items = data

        });
    }
    [HttpPost("getCarianJawatan")]
    public async Task<IActionResult> GetCarianJawatan([FromBody] CarianJawatanFilterDto filter)
    {
        _logger.LogInformation("Getting Carian Jawatan List");

        var data = await _jawatanService.GetCarianJawatanAsync(filter);
        return Ok(new
        {
            status = data.Count() > 0 ? "Sucess" : "Failed",
            items = data

        });
    }
    /// <summary>
    /// GetCarianJawatan
    /// </summary>
    /// <param name="filter">Filter criteria</param>
    /// <returns>Returns a list of data matching the filter criteria</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal server error occurred while processing the request</response>
    /// <remarks>
    /// This API may change as query is still not finalized.
    /// 
    /// All filter parameters are optional - if not provided, they will be ignored in the search.
    /// 
    /// </remarks>
    [HttpPost("getCarianJawatanSebenar")]
    public async Task<IActionResult> GetCarianJawatanSebenar([FromBody] CarianJawatanSebenarFilterDto filter)
    {
        _logger.LogInformation("GetCarianJawatanSebenar: GetCarianJawatanSebenar method called from controller with filter: {@Filter}", filter);
        try
        {
            var data = await _jawatanService.GetCarianJawatanSebenar(filter);

            _logger.LogInformation("GetCarianJawatanSebenar: Successfully retrieved {Count} records", data.Count);

            return Ok(new
            {
                status = data.Count > 0 ? "Success" : "Failed",
                items = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetCarianJawatanSebenar: Error occurred in controller while processing request with filter: {@Filter}", filter);

            return StatusCode(500, new
            {
                status = "Error",
                items = new List<CarianJawatanSebenarResponseDto>()
            });
        }
    }


}
