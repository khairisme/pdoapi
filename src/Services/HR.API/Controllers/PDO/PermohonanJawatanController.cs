using HR.Application.DTOs.PDO;
using HR.Application.Interfaces.PDO;
using HR.Application.Services;
using HR.Core.Entities;
using HR.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Contracts.DTOs;

namespace HR.API.Controllers.PDO;
//[Authorize]
[ApiController]
[Route("api/pdo/[controller]")]
public class PermohonanJawatanController : ControllerBase
{
    private readonly IPermohonanJawatanService _permohonanJawatanService;
    private readonly ILogger<PermohonanJawatanController> _logger;

    public PermohonanJawatanController(IPermohonanJawatanService permohonanJawatanService, ILogger<PermohonanJawatanController> logger)
    {
        _permohonanJawatanService = permohonanJawatanService;
        _logger = logger;
    }
    /// <summary>
    ///Get Carl PermohonanJawatan
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("search")]
    public async Task<ActionResult<IEnumerable<PermohonanJawatanSearchResponseDto>>> Search([FromBody] PermohonanJawatanFilterDto filter)
    {
        _logger.LogInformation("search PermohonanJawatan");
        var result = await _permohonanJawatanService.Search(filter).ToListAsync();
        return Ok(new
        {
            status = result.Count() > 0 ? "Sucess" : "Failed",
            items = result

        });
    }
    /// <summary>
    /// GetSenaraiPermohonanJawatan
    /// ----------------------------
    /// Fetches a list of permohonan jawatan (job applications) based on:
    /// - Nombor Rujukan
    /// - Tajuk Permohonan (partial match)
    /// - Kod Status Permohonan
    /// </summary>
    /// <param name="filter">Filtering criteria for permohonan jawatan</param>
    /// <returns>
    /// - 200 OK with list of matched permohonan
    /// - 404 if no record found
    /// - 500 if an error occurs
    /// </returns>
    [HttpPost("GetSenaraiPermohonanJawatan")]
    public async Task<IActionResult> GetSenaraiPermohonanJawatan([FromBody] PermohonanJawatanFilterDto2 filter)
    {
        try
        {
            var result = await _permohonanJawatanService.GetSenaraiPermohonanJawatanAsync(filter);

            return Ok(new
            {
                status = result.Any() ? "Success" : "Not Found",
                items = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetSenaraiPermohonanJawatan");
            return StatusCode(500, new
            {
                status = "Error",
                items = new List<PermohonanJawatanResponseDto>()
            });
        }
    }
    /// <summary>
    /// GetSenaraiPermohonanPindaan
    /// ----------------------------
    /// Fetches a list of permohonan pindaan pengisian jawatan based on optional filters:
    /// - Nombor Rujukan (contains)
    /// - Tajuk Permohonan (contains)
    /// - Kod Status Permohonan (exact)
    /// </summary>
    /// <param name="filter">Filter containing optional fields to search</param>
    /// <returns>
    /// - 200 OK: With filtered list of applications (Success or Not Found)
    /// - 500 Internal Server Error: If any unexpected issue occurs
    /// </returns>
    [HttpPost("GetSenaraiPermohonanPindaan")]
    public async Task<IActionResult> GetSenaraiPermohonanPindaan([FromBody] PermohonanPindaanFilterDto filter)
    {
        try
        {
            var result = await _permohonanJawatanService.GetSenaraiPermohonanPindaanAsync(filter);

            return Ok(new
            {
                status = result.Any() ? "Success" : "Not Found",
                items = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetSenaraiPermohonanPindaan");
            return StatusCode(500, new
            {
                status = "Error",
                items = new List<PermohonanPindaanResponseDto>()
            });
        }
    }
}
